using application.Model.Dto.MyActivities;

namespace application.Interface.MyActivities
{
    public interface IMyActivitiesService
    {
       Task<MyActivitiesResponseDto> RetrieveMyActivitiesDetailAsync(MyActivitiesRequestDto requestDto);
    }
}
