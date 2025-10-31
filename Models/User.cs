using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWRestfulAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string Firstname { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Lastname { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Column("Acceslevel")]
        public int Accesslevel { get; set; }
    }
}