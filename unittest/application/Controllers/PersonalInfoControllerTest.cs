using application.Controllers;
using application.Interface.PersonalInfo;
using application.Model.Dto.PersonalInfo;
using common.library.SeedWork.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using System.Data.Entity.Core.Objects;
using Xunit;

namespace unittest.application.Controllers
{
    public class PersonalInfoControllerTest
    {
        private PersonalInfoController _personalInfoController;
        private Mock<IPersonalInfoService> _personalInfoService;

        private void Init()
        {
            _personalInfoService = new Mock<IPersonalInfoService>();

            _personalInfoController = new PersonalInfoController(_personalInfoService.Object);
        }


        [Fact]
        public async Task SubmitPersonalInfoAsync_Positive()
        {
            //Arrange
            Init();

            AddPersonalInfoRequestDto requestDto = new AddPersonalInfoRequestDto()
            {
                FirstName = "Test",
                MiddleName = "Test",
                LastName = "Test",
                Address = "Test",
                Age = 26,
                Status = "Single"
            };

            var response = new AddPersonalInfoResponseDto
            {
                Status = "Successful",
                CreatedOn = DateTimeOffset.UtcNow.ToString(),
                TransactionNumber = "12345"
            };

            List<ExceptionsDto> exceptions = new List<ExceptionsDto>();

            _personalInfoService.Setup(x => x.SubmitPersonalInfoAsync(It.IsAny<AddPersonalInfoRequestDto>())).ReturnsAsync((response, exceptions));

            //Act
            var result = await _personalInfoController.SubmitPersonalInfoAsync(requestDto);
            OkObjectResult okObjectResult = (OkObjectResult)result;

            //Assert
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task SubmitPersonalInfoAsync_Exception()
        {
            //Arrange
            Init();

            AddPersonalInfoRequestDto requestDto = new AddPersonalInfoRequestDto()
            {
                FirstName = "Test",
                MiddleName = "Test",
                LastName = "Test",
                Address = "Test",
                Age = 26,
                Status = "Single"
            };

            _personalInfoService.Setup(x => x.SubmitPersonalInfoAsync(It.IsAny<AddPersonalInfoRequestDto>())).ThrowsAsync(new Exception());

            //Act
            var result = await _personalInfoController.SubmitPersonalInfoAsync(requestDto);
            OkObjectResult okObjectResult = (OkObjectResult)result;

            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, okObjectResult.StatusCode);   
        }

        [Fact]
        public async Task RetrievePersonalInfoAsync_Positive()
        {
            //Arrange
            Init();

            var requestDto = new RetrievePersonalInfoRequestDto()
            {
                 TransactionNumberRequestID = "6d31d9ec-1982-4c59-a8cd-d6391bf6b0c0"
            };

            var response = new RetrievePersonalInfoResponseDto()
            {
                 FirstName = "Test",
                 MiddleName= "Test",
                 LastName = "Test", 
                 Age= 26,
                 Status= "Single"
            };
            List<ExceptionsDto> exceptions = new List<ExceptionsDto>();

            _personalInfoService.Setup(x => x.RetrievePersonalInfoASync(It.IsAny<RetrievePersonalInfoRequestDto>())).ReturnsAsync((response, exceptions));

            //Act
            var result = await _personalInfoController.RetrievePersonalInfoAsync(requestDto);
            OkObjectResult okObjectResult = (OkObjectResult)result;

            //Assert
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task RetrievePersonalInfoAsync_Negative()
        {
            //Arrange
            Init();

            var requestDto = new RetrievePersonalInfoRequestDto()
            {
                TransactionNumberRequestID = "12345"
            };

            var response = new RetrievePersonalInfoResponseDto()
            {
                FirstName = "Test",
                MiddleName = "Test",
                LastName = "Test",
                Age = 26,
                Status = "Single"
            };
            List<ExceptionsDto> exceptions = new List<ExceptionsDto>();
            exceptions.Add(new ExceptionsDto("02", "_tes", "test"));

            _personalInfoService.Setup(x => x.RetrievePersonalInfoASync(It.IsAny<RetrievePersonalInfoRequestDto>())).ReturnsAsync((response, exceptions));

            //Act
            var result = await _personalInfoController.RetrievePersonalInfoAsync(requestDto);
            OkObjectResult okObjectResult = (OkObjectResult)result;

            //Assert
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task RetrievePersonalInfoAsync_Exception()
        {
            // Arrange
            Init();

            var requestDto = new RetrievePersonalInfoRequestDto()
            {
                TransactionNumberRequestID = "6d31d9ec-1982-4c59-a8cd-d6391bf6b0c0"
            };

            _personalInfoService.Setup(x => x.RetrievePersonalInfoASync(It.IsAny<RetrievePersonalInfoRequestDto>())).ThrowsAsync(new Exception());

            // Act
            var result = await _personalInfoController.RetrievePersonalInfoAsync(requestDto);

            // Assert
            var objectResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ObjectResult>(result); // Check if result is of type ObjectResult
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

    }
}

