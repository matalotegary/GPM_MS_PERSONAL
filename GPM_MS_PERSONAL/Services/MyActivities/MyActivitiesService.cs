using application.Interface.MyActivities;
using application.Model.Dto.MyActivities;

namespace application.Services.MyActivities
{
    public class MyActivitiesService : IMyActivitiesService
    {
        //private readonly

        public MyActivitiesService()
        {
        }

        public async Task<MyActivitiesResponseDto> RetrieveMyActivitiesDetailAsync(MyActivitiesRequestDto requestDto)
        {
            MyActivitiesResponseDto responseDto = new();

            responseDto.ActivityId = "ActivityId";
            responseDto.ActivityName = "ActivityName";
            responseDto.Id = GetIdHelper(requestDto.TransactionNumber);

            return responseDto;
        }

        private static string GetIdHelper (string transactionNumber)
        {
            if (transactionNumber == "1")
            {
                return "0001";
            }
            else
            {
                return transactionNumber;
            }
        }

    }
}
