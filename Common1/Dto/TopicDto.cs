namespace Common.Dto
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool WasAnswered { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastActive { get; set; }

        // IDs of related entities
        public int UserId { get; set; }
        public List<int>? CounselorIds { get; set; }
        public List<int>? PostIds { get; set; }
    }
}
