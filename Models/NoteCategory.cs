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
        [Key]
        [ForeignKey("NoteID")]
        public int NoteID {get;set;}
        [Key]
        [ForeignKey("CategoryID")]
        public int CategoryID {get;set;}
    }
}