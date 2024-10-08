﻿using application.Common;
using application.Interface.PersonalInfo;
using application.Model.Dto.PersonalInfo;
using application.Validators.PersonalInfo;
using common.library.BaseClass;
using common.library.SeedWork.Response;
using domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonalInfoController : AbstactController
    {
        private readonly IPersonalInfoService _personalInfoService;

        private readonly string controllerName = nameof(PersonalInfoController);
        private string actionMethodName = string.Empty;

        public PersonalInfoController(
            IPersonalInfoService personalInfoService)
        {
            _personalInfoService = personalInfoService;
        }

        [HttpPost("SubmitPersonalInfo")]
        [ProducesResponseType(typeof(AddPersonalInfoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SubmitPersonalInfoAsync(AddPersonalInfoRequestDto requestDto)
        {
            actionMethodName = nameof(SubmitPersonalInfoAsync);
            var fullActionMethodName = SharedHelper.GetFullActionMethodName(controllerName, actionMethodName);

            try
            {
                ValidatorExtension.Validate<AddPersonalInfoRequestDto, PersonalInfoDataValidator>(requestDto);

                var responseDto = await _personalInfoService.SubmitPersonalInfoAsync(requestDto);

                ResponseDto response = PrepareResponse(true,
                fullActionMethodName,
                ReturnCode.Success,
                responseDto.Item2!,
                responseDto.Item1);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return PopulateException(ex, fullActionMethodName);
            }

        }

        [HttpPost("RetrievePersonalInfo")]
        [ProducesResponseType(typeof(RetrievePersonalInfoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrievePersonalInfoAsync(RetrievePersonalInfoRequestDto requestDto)
        {
            actionMethodName = nameof(RetrievePersonalInfoAsync);
            var fullActionMethodName = SharedHelper.GetFullActionMethodName(controllerName, actionMethodName);

            try
            {
                ValidatorExtension.Validate<RetrievePersonalInfoRequestDto, RetrievePersonalInfoValidator>(requestDto);

                var responseDto = await _personalInfoService.RetrievePersonalInfoASync(requestDto);

                ResponseDto response = null!;

                if (responseDto.Item2.Any())
                {
                    response = PrepareResponse(false,
                        fullActionMethodName,
                        ReturnCode.BusinessError,
                        responseDto.Item2!,
                        new List<RetrievePersonalInfoResponseDto>());
                }
                else
                {
                    response = PrepareResponse(true,
                        fullActionMethodName,
                        ReturnCode.Success,
                        new List<ExceptionsDto>(),
                        responseDto.Item1!);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                return PopulateException(ex, fullActionMethodName);
            }
        }


    }
}
