namespace Common.Dto
{
    public class LoginRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; //The hashed password
    }
}
