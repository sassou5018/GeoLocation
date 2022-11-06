using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLocation.Models
{
    public class GeoLoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Latitude { get; set; } 
        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Longitude { get; set; }
    }
}
