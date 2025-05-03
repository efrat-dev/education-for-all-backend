using Project.Repositories.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Common.Dto
{
    public class RefreshTokenDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } 

        [Required]
        public int UserId { get; set; } 

        public DateTime ExpiryDate { get; set; } 

        public bool IsRevoked { get; set; } = false; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedAt { get; set; } 

        [ForeignKey("UserId")]
        public virtual User User { get; set; } 
    }
}
