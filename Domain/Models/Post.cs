using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max Length is 100"), MinLength(2, ErrorMessage = "Min Length is 2")]
        public string Title { get; set; }
        [ValidateNever]
        public string Image { get; set; }
        [MinLength(2, ErrorMessage = "Min Length is 2")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Category")]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public ICollection<Comment> Comments { get; set; }
    }
}
