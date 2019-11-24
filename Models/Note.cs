using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NTR2.Models
{
    public class Note
    {
        public int NoteID {get;set;}
        [Required(ErrorMessage = "Title is required")]
        [Remote("doesFileNameExist", "Note", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [MaxLength(64)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime NoteDate { get; set; }
        public ICollection<Category> NoteCategories { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose extension")]
        public string Extension{get;set;}
        public Note()
        {
            Title="write some title";
            NoteCategories=new string []{};
            NoteDate=DateTime.Now;
            Description="Write some text";
        }
        public Note(string title, string[] categories, DateTime date, string text, string extension)
        {
            Title=title;
            NoteCategories=categories;
            NoteDate=date;
            Description=text;
            Extension=extension;
        }
         public Note(string title, string[] categories, DateTime date)
        {
            Title=title;
            NoteCategories=categories;
            NoteDate=date;
        }
    }
}