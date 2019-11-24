using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NTR2.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID {get;set;}
        [Required(ErrorMessage = "Title is required")]
        [StringLength(64)]
        public string Title { get; set; }
        public ICollection<NoteCategory> NoteCategories { get; set; }
        public Category()
        {
            Title="write some title";
        }
        public Category(string category)
        {
            Title=category;
        }
    }
}