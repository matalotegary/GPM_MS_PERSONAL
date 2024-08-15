using application.Model.Dto.PersonalInfo;
using common.library.SeedWork.Response;

namespace application.Interface.PersonalInfo
{
    public interface IPersonalInfoService
    {
        Task<(AddPersonalInfoResponseDto, List<ExceptionsDto?>)> SubmitPersonalInfoAsync(AddPersonalInfoRequestDto requestDto);
    }
}
