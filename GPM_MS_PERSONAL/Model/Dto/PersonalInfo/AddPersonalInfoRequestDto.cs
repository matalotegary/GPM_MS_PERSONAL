namespace application.Model.Dto.PersonalInfo
{
    public class AddPersonalInfoRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Age { get; set; } 
    }
}
