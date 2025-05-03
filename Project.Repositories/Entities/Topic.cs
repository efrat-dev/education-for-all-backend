using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Repositories.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [DefaultValue(false)]
        public bool WasAnswered { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [NotMapped]
        public DateTime DateLastActive
        {
            get
            {
                return Posts.Any() ? Posts.Max(p => p.Date) : DateCreated;
            }
        }

        // related entities
        // Only users with a regular role are permitted to open a topic; counselors are not authorized
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Counselor>? Counselors { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
