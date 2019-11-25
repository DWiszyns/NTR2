using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NTR2.Models
{
    public class NoteCategory
    {
        public int NoteID {get;set;}
        public Note Note { get; set; }
        public int CategoryID {get;set;}
        public Category Category { get; set; }
    }
}