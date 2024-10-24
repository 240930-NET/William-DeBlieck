using AutoMapper;
using GreaterGradesBackend.Api.Models;
using GreaterGradesBackend.Domain.Entities;
using GreaterGradesBackend.Domain.Interfaces;
using GreaterGradesBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreaterGradesBackend.Domain.Enums;
using GreaterGradesBackend.Api.Controllers;


namespace GreaterGradesBackend.Services.Implementations
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssignmentDto>> GetAllAssignmentsAsync()
        {
            var assignments = await _unitOfWork.Assignments.GetAllAsync();
            return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
        }

        public async Task<AssignmentDto> GetAssignmentByIdAsync(int assignmentId)
        {
            var assignment = await _unitOfWork.Assignments.GetAssignmentWithDetailsAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            return _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<AssignmentDto> CreateAssignmentAsync(CreateAssignmentDto createAssignmentDto)
        {
            var assignment = _mapper.Map<Assignment>(createAssignmentDto);
            
            await _unitOfWork.Assignments.AddAsync(assignment);

            var classEntity = await _unitOfWork.Classes.GetClassWithDetailsAsync(assignment.ClassId);

            foreach (User student in classEntity.Students)
            {
                var grade = new Grade
                {
                    User = student,
                    AssignmentId = assignment.AssignmentId,
                    Score = 0,
                    GradingStatus = GradeStatus.NotGraded
                };

                await _unitOfWork.Grades.AddAsync(grade);
            }

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AssignmentDto>(assignment);
        }


        public async Task<bool> UpdateAssignmentAsync(int assignmentId, UpdateAssignmentDto updateAssignmentDto)
        {
            var assignment = await _unitOfWork.Assignments.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return false;
            }

            _mapper.Map(updateAssignmentDto, assignment);
            _unitOfWork.Assignments.Update(assignment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAssignmentAsync(int assignmentId)
        {
            var assignment = await _unitOfWork.Assignments.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return false;
            }

            var classEntity = await _unitOfWork.Classes.GetClassWithDetailsAsync(assignment.ClassId);
            foreach (User student in classEntity.Students)
            {
                var grade = await _unitOfWork.Grades.GetGradeByUserAndAssignmentAsync(student.UserId, assignmentId);
                student.Grades.Remove(grade);
                _unitOfWork.Grades.Remove(grade);
            }

            _unitOfWork.Assignments.Remove(assignment);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
