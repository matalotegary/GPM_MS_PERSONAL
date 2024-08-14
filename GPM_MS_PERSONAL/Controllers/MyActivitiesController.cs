using application.Model.Dto.MyActivities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using domain.Validators;
using application.Validators;
using application.Interface.MyActivities;

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
            List<Exception> errorList = new List<Exception>();

            try
            {
                ValidatorExtension.Validate<MyActivitiesRequestDto, MyActivitiesValidator>(requestDto);

                var result = await _myActivitiesService.RetrieveMyActivitiesDetailAsync(requestDto);


            }
            catch (Exception ex)
            {

            }
        }
    }
}
