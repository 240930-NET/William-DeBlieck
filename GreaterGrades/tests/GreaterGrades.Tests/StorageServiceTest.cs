using System.Collections.Generic;
using GreaterGrades.Services;
using Moq;
using Xunit;

namespace GreaterGrades.Tests
{
    public class StorageServiceTests
    {
        private readonly Mock<IStorageService> _storageServiceMock;

        public StorageServiceTests()
        {
            _storageServiceMock = new Mock<IStorageService>();
        }

        [Fact]
        public void LoadData_ShouldReturnListOfDeserializedObjects()
        {
            // Arrange
            var mockData = new List<string> { "Student1", "Student2" };
            _storageServiceMock.Setup(s => s.LoadData<string>()).Returns(mockData);

            // Act
            var result = _storageServiceMock.Object.LoadData<string>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains("Student1", result);
            Assert.Contains("Student2", result);
        }

        [Fact]
        public void SaveData_ShouldSerializeAndSaveObjects()
        {
            // Arrange
            var dataToSave = new List<int> { 1, 2, 3 };
            _storageServiceMock.Setup(s => s.SaveData(dataToSave));

            // Act
            _storageServiceMock.Object.SaveData(dataToSave);

            // Assert
            _storageServiceMock.Verify(s => s.SaveData(It.IsAny<List<int>>()), Times.Once);
        }

        [Fact]
        public void LoadData_ShouldReturnEmptyList_WhenNoDataExists()
        {
            // Arrange
            _storageServiceMock.Setup(s => s.LoadData<string>()).Returns(new List<string>());

            // Act
            var result = _storageServiceMock.Object.LoadData<string>();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SaveData_ShouldBeCalledOnce()
        {
            // Arrange
            var dataToSave = new List<string> { "Test1", "Test2" };

            // Act
            _storageServiceMock.Object.SaveData(dataToSave);

            // Assert
            _storageServiceMock.Verify(s => s.SaveData(dataToSave), Times.Once);
        }
    }
}
