namespace Common.Dto
{
    public class DeleteTokenDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
