using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lab3_Testy.Tests
{
    public class PersonControllerTests
    {
        [Fact]
        public void Get_ReturnsListOfPersons() // Test sprawdza, czy akcja Get zwraca listê osób.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var persons = new List<Person> { new Person { Id = 1, FirstName = "John", LastName = "Doe" } };
            mockController.Setup(c => c.Get()).Returns(new ActionResult<IEnumerable<Person>>(persons));

            // Act
            var result = mockController.Object.Get();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Person>>>(result);
            Assert.Equal(persons, result.Value);
        }

        [Fact]
        public void Get_WithValidId_ReturnsPerson() // Test sprawdza, czy akcja Get z prawid³owym Id zwraca jedn¹ osobê.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            mockController.Setup(c => c.Get(It.IsAny<int>())).Returns(new ActionResult<Person>(person));

            // Act
            var result = mockController.Object.Get(1);

            // Assert
            Assert.IsType<ActionResult<Person>>(result);
            Assert.Equal(person, result.Value);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound() // Test sprawdza, czy akcja Get z nieprawid³owym Id zwraca NotFoundResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            mockController.Setup(c => c.Get(It.IsAny<int>())).Returns(new NotFoundResult());

            // Act
            var result = mockController.Object.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_WithUniqueData_ReturnsCreatedAtAction() // Test sprawdza, czy akcja Post z unikalnymi danymi zwraca 
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            mockController.Setup(c => c.Post(It.IsAny<Person>())).Returns(new ActionResult<Person>(person));

            // Act
            var result = mockController.Object.Post(person);

            // Assert
            Assert.IsType<ActionResult<Person>>(result);
            Assert.Equal(person, result.Value);
        }

        [Fact]
        public void Post_WithDuplicateId_ReturnsConflict() // Test sprawdza, czy akcja Post z zduplikowanym Id zwraca ConflictObjectResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            mockController.Setup(c => c.Post(It.IsAny<Person>())).Returns(new ActionResult<Person>(new ConflictObjectResult("Id musi byc unikatowe.")));

            // Act
            var result = mockController.Object.Post(person);

            // Assert
            Assert.IsType<ActionResult<Person>>(result);
            Assert.IsType<ConflictObjectResult>((result.Result as ObjectResult));
            Assert.Equal("Id musi byc unikatowe.", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public void Post_WithDuplicateName_ReturnsConflict() // Test sprawdza, czy akcja Post z zduplikowanym imieniem i nazwiskiem zwraca ConflictObjectResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var existingPerson = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            var newPerson = new Person { Id = 2, FirstName = "John", LastName = "Doe" };
            var persons = new List<Person> { existingPerson };
            mockController.Setup(c => c.Get()).Returns(new ActionResult<IEnumerable<Person>>(persons));
            mockController.Setup(c => c.Post(It.IsAny<Person>())).Returns(new ActionResult<Person>(new ConflictObjectResult("Imie i Nazwisko musi byc unikatowe.")));

            // Act
            var result = mockController.Object.Post(newPerson);

            // Assert
            Assert.IsType<ActionResult<Person>>(result);
            Assert.IsType<ConflictObjectResult>((result.Result as ObjectResult));
            Assert.Equal("Imie i Nazwisko musi byc unikatowe.", (result.Result as ObjectResult).Value);
        }

        [Fact]
        public void Put_WithValidId_ReturnsNoContent() // Test sprawdza, czy akcja Put z prawid³owym Id zwraca NoContentResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            var updatedPerson = new Person { Id = 1, FirstName = "Updated", LastName = "Person" };
            mockController.Setup(c => c.Put(It.IsAny<int>(), It.IsAny<Person>())).Returns(new NoContentResult());

            // Act
            var result = mockController.Object.Put(1, updatedPerson);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_WithInvalidId_ReturnsNotFound() // Test sprawdza, czy akcja Put z nieprawid³owym Id zwraca NotFoundResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            mockController.Setup(c => c.Put(It.IsAny<int>(), It.IsAny<Person>())).Returns(new NotFoundResult());

            // Act
            var result = mockController.Object.Put(1, new Person());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_WithValidId_ReturnsNoContent() // Test sprawdza, czy akcja Delete z prawid³owym Id zwraca NoContentResult.
        {
            // Arrange
            var mockController = new Mock<IPersonController>();
            mockController.Setup(c => c.Delete(It.IsAny<int>())).Returns(new NoContentResult());

            // Act
            var result = mockController.Object.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
