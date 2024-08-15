using application.Interface.MyActivities;
using application.Model.Dto.MyActivities;
using application.Validators.MyActivities;
using common.library.BaseClass;
using common.library.SeedWork.Response;
using domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyActivitiesController : AbstactController
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
                    ReturnCode.Success,
                    errorList,
                    result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return PopulateException(ex, applicationName);
            }
        }
    }
}
