using application.Interface.MyActivities;
using application.Model.Dto.MyActivities;
using application.Validators;
using domain.Validators;
using Microsoft.AspNetCore.Mvc;
using common.library.SeedWork.Response;
using common.library.Constant;

namespace application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyActivitiesController : ControllerBase
    {
        private readonly IMyActivitiesService _myActivitiesService;
        private readonly string controllerName = nameof(MyActivitiesController);
        private string actionMethodName = string.Empty;

        public MyActivitiesController(
             IMyActivitiesService myActivitiesService)
        {
            _myActivitiesService = myActivitiesService;
        }

        [HttpPost("RetrieveMyActivitiesDetails")]
        [ProducesResponseType(typeof(MyActivitiesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveMyActivitiesDetailsAsync(MyActivitiesRequestDto requestDto)
        {
            actionMethodName = nameof(RetrieveMyActivitiesDetailsAsync);
            string applicationName = "RetrieveMyActivitiesDetails";
            List<ExceptionsDto> errorList = new List<ExceptionsDto>();

            try
            {
                ValidatorExtension.Validate<MyActivitiesRequestDto, MyActivitiesValidator>(requestDto);

                var result = await _myActivitiesService.RetrieveMyActivitiesDetailAsync(requestDto);

                ResponseDto response = PrepareResponse(true,
                    applicationName,
                    "00",
                    errorList,
                    result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return PopulateException(ex, applicationName);
            }
        }

        protected ResponseDto PrepareResponse(bool success, string serviceName, string returnCode, List<ExceptionsDto> errors, object payload)
        {
            ResultDto result = new ResultDto(serviceName, returnCode, errors, payload);
            ResponseDto response = new ResponseDto(success, new List<ResultDto>() { result });
            return response;
        }
        protected ObjectResult PopulateException(Exception ex, string actionMethodName)
        {
            if (ex is ArgumentException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.INPUT_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.INPUT_ERROR, ex.Message, actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            else
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.TECHNICAL_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.TECHNICAL_ERROR, ex.Message, actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
