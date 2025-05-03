using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Repositories.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Likes { get; set; }
        public string Content { get; set; } = string.Empty;

        [DefaultValue(false)]
        public bool IsReported { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        // related entities

        #region belongs to topic

        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; } = null!;

        #endregion

        #region belongs to user/counselor

        //Every post can be made by a user or a counselor
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Counselor")]
        public int? CounselorId { get; set; }
        public virtual Counselor? Counselor { get; set; }

        #endregion
    }
}
