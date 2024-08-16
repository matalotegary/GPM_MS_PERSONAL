using application.Common.Helpers;
using application.Constants;
using application.Interface.PersonalInfo;
using application.Model.Dto.PersonalInfo;
using common.library.Constant;
using common.library.SeedWork.Response;
using domain.DomainModels.PersonalInfo;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace application.Services.PersonalInfo
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;

        public PersonalInfoService(
            IPersonalInfoRepository personalInfoRepository)
        {
            _personalInfoRepository = personalInfoRepository;
        }

        public async Task<(AddPersonalInfoResponseDto, List<ExceptionsDto?>)> SubmitPersonalInfoAsync(AddPersonalInfoRequestDto requestDto)
        {
            var firstName = PersonalInfoHelper.CapitalizeEachWord(requestDto.FirstName);
            var middleName = PersonalInfoHelper.CapitalizeEachWord(requestDto.MiddleName);
            var lastName = PersonalInfoHelper.CapitalizeEachWord(requestDto.LastName);
            var address = PersonalInfoHelper.CapitalizeEachWord(requestDto.Address);
            var status = PersonalInfoHelper.CapitalizeEachWord(requestDto.Status);
            var age = requestDto.Age;

            AddPersonalInfoResponseDto responseDto = new();
            List<ExceptionsDto?> errorList = new();

            responseDto.Status = SharedConstants.ResponseStatuses.Failed;

            var executionStrategy = _personalInfoRepository.GetExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var personalInfoTable = new PersonalInfoEntity(
                    Guid.NewGuid(),
                    firstName,
                    middleName,
                    lastName,
                    address,
                    status,
                    age,
                    firstName,
                    UserTypeConstant.User);

                    await _personalInfoRepository.AddPersonalInfoAsync(personalInfoTable);
                    await _personalInfoRepository.SaveChangesAsync();

                    scope.Complete();
                    responseDto.Status = SharedConstants.ResponseStatuses.Success;
                    responseDto.CreatedOn = personalInfoTable.CreatedOn.ToString();
                    responseDto.TransactionNumber = personalInfoTable.TransactionNumberRequestID.ToString();
                }
            });

            return (responseDto, errorList);
        }

        public async Task<(RetrievePersonalInfoResponseDto?, List<ExceptionsDto?>)> RetrievePersonalInfoASync(RetrievePersonalInfoRequestDto requestDto)
        {

            RetrievePersonalInfoResponseDto? responseDto = new();
            List<ExceptionsDto?> errorList = new();

            var transNo = Guid.Parse(requestDto.TransactionNumberRequestID);

            var tableData = await _personalInfoRepository.FindByTransactionNumberRequestIDAsync(transNo);

            if (tableData! != null!)
            {
                responseDto.FirstName = tableData.FirstName!;
                responseDto.MiddleName = tableData.MiddleName!;
                responseDto.LastName = tableData.LastName!;
                responseDto.Age = (int)tableData.Age!;
                responseDto.Status = tableData.Status!;
            }
            else
            {
                errorList.Add(new ExceptionsDto("01", "No Record in Database", nameof(RetrievePersonalInfoASync)));

            }
            return (responseDto, errorList);
        }
    }
}
