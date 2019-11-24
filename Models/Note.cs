using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NTR2.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID {get;set;}
        [Required(ErrorMessage = "Title is required")]
        [Remote("doesFileNameExist", "Note", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [MaxLength(64)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime NoteDate { get; set; }
        public ICollection<NoteCategory> NoteCategories { get; set; }
        public string Description { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
        public Note()
        {
            Title="write some title";
            NoteCategories=new NoteCategory []{};
            NoteDate=DateTime.Now;
            Description="Write some text";
        }
        public Note(string title, ICollection <NoteCategory> categories, DateTime date, string text)
        {
            Title=title;
            NoteCategories=categories;
            NoteDate=date;
            Description=text;
        }
         public Note(string title, ICollection <NoteCategory>categories, DateTime date)
        {
            Title=title;
            NoteCategories=categories;
            NoteDate=date;
        }
    }
}