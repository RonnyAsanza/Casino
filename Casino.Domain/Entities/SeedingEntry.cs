using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Casino.Domain.Entities
{
    [Table("__SeedingHistory")]
    public class SeedingEntry
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
