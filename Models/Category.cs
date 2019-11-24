using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NTR2.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CateogryID {get;set;}
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(64)]
        [StringLength(64)]
        [Index(IsUnique=true)]
        public string Title { get; set; }
        public Category()
        {
            Title="write some title";
        }
    }
}