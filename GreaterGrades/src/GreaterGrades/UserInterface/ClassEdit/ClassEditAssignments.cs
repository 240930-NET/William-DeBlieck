using System;
using GreaterGrades.Models;
using GreaterGrades.Repositories;
using System.Linq;

namespace GreaterGrades.UserInterface.ClassEdit
{
    public class ClassEditAssignmentsMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;

        public ClassEditAssignmentsMenu(IClassRepository classRepository, IAssignmentRepository assignmentRepository, IGradeRepository gradeRepository, IStudentRepository studentRepository)
        {
            _classRepository = classRepository;
            _assignmentRepository = assignmentRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        public void DisplayAssignmentsMenu(Guid classId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Edit Assignments for Class ID: {classId} ===");
                Console.WriteLine("1. Add Assignment to Class");
                Console.WriteLine("2. Remove Assignment from Class");
                Console.WriteLine("3. Edit Assignment");
                Console.WriteLine("4. Grade Assignment");
                Console.WriteLine("5. View All Assignments in Class");
                Console.WriteLine("6. View Grade Distribution for Assignment");
                Console.WriteLine("7. Back to Edit Class Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddAssignmentToClass(classId);
                        break;
                    case "2":
                        RemoveAssignmentFromClass(classId);
                        break;
                    case "3":
                        EditAssignment(classId);
                        break;
                    case "4":
                        GradeAssignment(classId);
                        break;                    
                    case "5":
                        ViewAllAssignmentsInClass(classId);
                        break;
                    case "6":
                        ShowGradingDistribution(classId);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddAssignmentToClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Add Assignment to Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter the name of the new assignment: ");
            var assignmentName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(assignmentName))
            {
                Console.WriteLine("Assignment name cannot be empty. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter the maximum score for the assignment: ");
            var maxScoreInput = Console.ReadLine();

            if (!int.TryParse(maxScoreInput, out int maxScore) || maxScore <= 0)
            {
                Console.WriteLine("Invalid maximum score. It must be a positive integer. Press any key to return.");
                Console.ReadKey();
                return;
            }


            classToUpdate.AddAssignment(assignmentName, int.Parse(maxScoreInput), _assignmentRepository, _studentRepository, _classRepository, _gradeRepository);
            _classRepository.Update(classToUpdate);

            Console.WriteLine($"Assignment '{assignmentName}' with a maximum score of {maxScore} added to the class successfully! Press any key to return.");
            Console.ReadKey();
        }

        private void RemoveAssignmentFromClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Remove Assignment from Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToUpdate.GetAssignments(_assignmentRepository).ToList();
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments found in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Assignments in Class:");
            for (int i = 0; i < assignments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {assignments[i].Name}");
            }

            Console.Write("Enter the number of the assignment to remove: ");
            var assignmentNumberInput = Console.ReadLine();

            if (!int.TryParse(assignmentNumberInput, out int assignmentNumber) || assignmentNumber < 1 || assignmentNumber > assignments.Count)
            {
                Console.WriteLine("Invalid number. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignmentToRemove = assignments[assignmentNumber - 1];

            classToUpdate.RemoveAssignment(assignmentToRemove.Id, _assignmentRepository, _studentRepository, _classRepository, _gradeRepository);

            Console.WriteLine("Assignment removed from class successfully! Press any key to return.");
            Console.ReadKey();
        }



        private void ViewAllAssignmentsInClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Assignments in Class ===");

            var classToView = _classRepository.GetById(classId);
            if (classToView == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToView.GetAssignments(_assignmentRepository);
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments in this class.");
            }
            else
            {
                Console.WriteLine("Assignments in Class:");
                foreach (var assignment in assignments)
                {
                    Console.WriteLine($"ID: {assignment.Id} | Name: {assignment.Name}");
                }
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
        public void EditAssignment(Guid classId) {
            Console.Clear();
            Console.WriteLine("=== Edit Assignment from Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToUpdate.GetAssignments(_assignmentRepository);
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments found in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Assignments in Class:");
            for (int i = 0; i < assignments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {assignments[i].Name}");
            }

            Console.Write("Enter the number of the assignment to remove: ");
            var assignmentNumberInput = Console.ReadLine();

            if (!int.TryParse(assignmentNumberInput, out int assignmentNumber) || assignmentNumber < 1 || assignmentNumber > assignments.Count)
            {
                Console.WriteLine("Invalid number. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignmentToEdit = assignments[assignmentNumber - 1];

            Console.WriteLine($"Name {assignmentToEdit.Name}:");
            var newName = Console.ReadLine();

            if (newName == null) {
                Console.WriteLine("Invalid name!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Max Score {assignmentToEdit.MaxScore}:");
            var newMaxScore = Console.ReadLine();

            if (!int.TryParse(newMaxScore, out int newMaxScoreInt) || newMaxScoreInt < 1) {
                Console.WriteLine("Invalid Score!");
                Console.ReadKey();
                return;
            }

            assignmentToEdit.MaxScore = newMaxScoreInt;
            assignmentToEdit.Name = newName;
            _assignmentRepository.Update(assignmentToEdit);
        }
        public void GradeAssignment(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Grade Assignment for Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToUpdate.GetAssignments(_assignmentRepository).ToList();
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments found in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Assignments in Class:");
            for (int i = 0; i < assignments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {assignments[i].Name}");
            }

            Console.Write("Enter the number of the assignment to grade: ");
            var assignmentNumberInput = Console.ReadLine();

            if (!int.TryParse(assignmentNumberInput, out int assignmentNumber) || assignmentNumber < 1 || assignmentNumber > assignments.Count)
            {
                Console.WriteLine("Invalid number. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignmentToGrade = assignments[assignmentNumber - 1];
            var grades = assignmentToGrade.GetGrades(_gradeRepository);
            if (!grades.Any())
            {
                Console.WriteLine("No grades found for this assignment. Press any key to return.");
                Console.ReadKey();
                return;
            }

            foreach (var grade in grades)
            {
                Console.Clear();
                Console.WriteLine($"Grading Assignment: {assignmentToGrade.Name}");
                Console.WriteLine($"Max Score: {assignmentToGrade.MaxScore}\n");

                var student = _studentRepository.GetById(grade.StudentId);
                if (student != null)
                {
                    Console.WriteLine($"Student: {student.FirstName} {student.LastName} (ID: {student.Id})");
                    Console.WriteLine($"Current Grade: {(grade.Score.HasValue ? grade.Score.ToString() : "_")}/{assignmentToGrade.MaxScore}");
                    Console.Write("Enter new score (or type 'skip' to skip): ");
                    var newScoreInput = Console.ReadLine();

                    if (string.Equals(newScoreInput, "skip", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Grade unchanged.\n");
                    }
                    else if (int.TryParse(newScoreInput, out int newScore) && newScore >= 0 && newScore <= assignmentToGrade.MaxScore)
                    {
                        grade.Score = newScore;
                        _gradeRepository.Update(grade); // Update the grade in the repository
                        Console.WriteLine("Grade updated.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input. Grade remains {grade.Score}/{assignmentToGrade.MaxScore}.\n");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"Student not found for grade ID {grade.StudentId}. Skipping...\n");
                    Console.ReadKey();
                }
            }
        }
        public void ShowGradingDistribution(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Show Grading Distribution ===");

            var classToView = _classRepository.GetById(classId);
            if (classToView == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToView.GetAssignments(_assignmentRepository).ToList();
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments found in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select an assignment to view its grading distribution:");
            for (int i = 0; i < assignments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {assignments[i].Name}");
            }

            Console.Write("Enter the number of the assignment: ");
            var assignmentNumberInput = Console.ReadLine();

            if (!int.TryParse(assignmentNumberInput, out int assignmentNumber) || assignmentNumber < 1 || assignmentNumber > assignments.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var selectedAssignment = assignments[assignmentNumber - 1];
            var grades = selectedAssignment.GetGrades(_gradeRepository)
                                        .Where(g => g.Score.HasValue)
                                        .Select(g => g.Score.Value)
                                        .ToList();

            if (!grades.Any())
            {
                Console.WriteLine("No grades available for this assignment. Press any key to return.");
                Console.ReadKey();
                return;
            }

            int binCount;
            while (true)
            {
                Console.Write("Enter the number of bins for the distribution: ");
                var binInput = Console.ReadLine();

                if (int.TryParse(binInput, out binCount) && binCount > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid number of bins. Please enter a positive integer.");
                }
            }

            // Calculate bin ranges
            int maxScore = selectedAssignment.MaxScore;
            double binSize = (double)maxScore / binCount;
            int[] bins = new int[binCount];

            foreach (var score in grades)
            {
                int binIndex = (int)(score / binSize);
                if (binIndex >= binCount)
                {
                    binIndex = binCount - 1;
                }
                bins[binIndex]++;
            }

            
            int maxBin = bins.Max();
            int scale = maxBin > 50 ? 50 : maxBin;

            Console.WriteLine($"\nGrading Distribution for '{selectedAssignment.Name}':");
            for (int i = 0; i < binCount; i++)
            {
                double lowerBound = binSize * i;
                double upperBound = binSize * (i + 1);
                if (i == binCount - 1)
                {
                    upperBound = maxScore; // Ensure the last bin includes the max score
                }

                // Calculate the number of asterisks proportional to the bin count
                int asterisks = (int)((double)bins[i] / scale * 50);
                string bar = new string('*', asterisks);

                Console.WriteLine($"{lowerBound,5:0}-{upperBound,5:0} | {bar} ({bins[i]})");
            }

            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
        }

    }
}
