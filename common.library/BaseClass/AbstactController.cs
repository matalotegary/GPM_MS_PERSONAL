using common.library.Constant;
using common.library.SeedWork.Response;
using Microsoft.AspNetCore.Mvc;

namespace common.library.BaseClass
{
    public abstract class AbstactController : ControllerBase
    {
        protected ResponseDto PrepareResponse(bool success, string serviceName, string returnCode, List<ExceptionsDto> errors, object payload)
        {
            ResultDto result = new ResultDto(serviceName, returnCode, errors, payload);
            ResponseDto response = new ResponseDto(success, new List<ResultDto>() { result });
            return response;
        }

        protected ResponseDto PrepareResponse(bool success, List<ResultDto> results)
        {
            ResponseDto response = new ResponseDto(success, results);
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
