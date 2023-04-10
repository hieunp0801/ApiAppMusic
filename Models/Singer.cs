using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAppMusic.Models
{
    public class Singer
    {
        [Key]
        public int Id {get;set;}

        [Required]
        [MaxLength(255)]
        [Column(TypeName="nvarchar")]
        public string  NameSinger {get;set;}
        
        [Required]
        [MaxLength(255)]
        [Column(TypeName="nvarchar")]
        public string ImageSinger {get;set;}
        
        [Required]
        [MaxLength(255)]
        [Column(TypeName="nvarchar")]
        public string DateofBirth {get;set;}
    }
}