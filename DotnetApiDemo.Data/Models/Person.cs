using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetApiDemo.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }
    }
}
