using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAppMusic.Models
{
    public class Music
    {
        [Key]
        public int Id {get;set;}
        [StringLength(255)]
        [Required]
        [Column(TypeName= "nvarchar")]
        public string NameMusic {get;set;}
        [StringLength(255)]
        [Required]
        [Column(TypeName= "nvarchar")]
        public string FileMusic {get;set;}
        [Required]

        public Singer Singer {get;set;}
    }
}