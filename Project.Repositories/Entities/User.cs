using System.ComponentModel.DataAnnotations;

namespace Project.Repositories.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        //related entities
        public virtual ICollection<Topic>? Topics { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }

    }
}
