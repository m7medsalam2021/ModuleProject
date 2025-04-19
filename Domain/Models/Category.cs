using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="The Name should be here.")]
        [MaxLength(200, ErrorMessage = "Max Length is 200"), MinLength(10, ErrorMessage = "Min Length is 10")]
        public string Name { get; set; }
        [MinLength(30, ErrorMessage = "Min Length is 30")]
        public string? Description { get; set; }
        [ValidateNever]
        public ICollection<Post> Posts { get; set; }
    }
}
