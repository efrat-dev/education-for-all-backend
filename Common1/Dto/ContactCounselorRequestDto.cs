namespace Common.Dto
{
    public class ContactCounselorRequestDto : ContactRequestDto
    {
        public string CounselorName { get; set; } = string.Empty;
        public string CounselorEmail { get; set; } = string.Empty;
    }
}
