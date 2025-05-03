namespace Common.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsReported { get; set; }
        public bool IsDeleted { get; set; }

        // IDs of related entities
        public int TopicId { get; set; }

        // Only one of the following should be populated to indicate who created the post
        public int? UserId { get; set; }
        public int? CounselorId { get; set; }

    }
}
