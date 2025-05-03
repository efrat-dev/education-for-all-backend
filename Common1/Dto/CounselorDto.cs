namespace Common.Dto
{
    public class CounselorDto
    {
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string IdentityNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        #region personal details
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public string EducationalInstitutions { get; set; } = string.Empty;
        public string WorkHistory { get; set; } = string.Empty;
        public string AcademicDegrees { get; set; } = string.Empty;

        #endregion
    }
}
