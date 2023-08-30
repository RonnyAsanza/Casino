using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Casino.Domain.Entities
{
    [Serializable]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserKey { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public decimal Money { get; set; }

        [Required]
        public bool Enabled { get; set; } = false;

        [Required]
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateModifiedUtc { get; set; } = DateTime.UtcNow;
    }
}
