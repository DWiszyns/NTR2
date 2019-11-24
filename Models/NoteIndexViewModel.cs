using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTR2.Models;

namespace NTR2.Models
{
    public class NoteIndexViewModel
    {
        public string Category { get; set; }
        [Display(Name="Date To")]
        public DateTime DateTo { get; set; }
        [Display(Name="Date From")]
        public DateTime DateFrom { get; set; }
        public PaginatedList<Note> Notes { get; set; }
        public List<SelectListItem> NoteCategories { get; }
        public NoteIndexViewModel()
        {
            List <Note> emptyNoteList = new List<Note> {};
            Notes = new PaginatedList<Note>(emptyNoteList , 0, 10);
            NoteCategories = new List<SelectListItem>
            {
                new SelectListItem { Value = "sport", Text = "sport" },
                new SelectListItem { Value = "notsport", Text = "notsport" },
            };
        }
        public NoteIndexViewModel(PaginatedList<Note> notes, string[] categories)
        {
            Notes=notes;
            NoteCategories = new List<SelectListItem> {};
            foreach(var c in categories)
            {
                NoteCategories.Add(new SelectListItem { Value = c, Text = c });
            }
        }
        public NoteIndexViewModel(PaginatedList<Note> notes, string[] categories, string category, DateTime dateFrom,DateTime dateTo )
        {
            Notes=notes;
            NoteCategories = new List<SelectListItem> {};
            foreach(var c in categories)
            {
                NoteCategories.Add(new SelectListItem { Value = c, Text = c });
            }
            Category=category;
            DateTo=dateTo;
            DateFrom=dateFrom;
        }
    }
}