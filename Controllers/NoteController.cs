using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTR2.Models;
using NTR2.Repositories;
using NTR2;
using NTR2.Data;

namespace NTR2.Controllers
{
    public class NoteController : Controller
    {
        private List<Note> Notes;
        private ApplicationDbContext _context;
        public NoteController(ApplicationDbContext context)
        {
            _context=context;
            var notes = new List<Note>();
            this.Notes=_context.Notes.ToList();
        }
        public IActionResult Index(DateTime dateFrom, DateTime dateTo, string category="",int pageNumber=1)
        {
            if(dateFrom==DateTime.MinValue) dateFrom = DateTime.Today.AddYears(-1);
            if(dateTo==DateTime.MinValue) dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            if(category==null) category="";
            string[] possibleCategories= new string[]{};
            this.Notes=_context.Notes.ToList();
            foreach(var n in Notes)
            {
                if(n.NoteCategories!=null && n.NoteCategories.Count!=0)foreach(var c in n.NoteCategories)
                {
                    if(!possibleCategories.Contains(c.Note.Title)) possibleCategories=possibleCategories.Append(c.Note.Title).ToArray();

                }
            }
            var notes = new List<Note>();
            foreach(var n in Notes)
            {
                 if(n.NoteDate>=dateFrom && n.NoteDate<=dateTo && (category==""||n.NoteCategories.Where(m=>m.Category.Title==category).Any()))
                {
                    notes.Add(n);
                }
            }
            PaginatedList<Note> list = new PaginatedList<Note>(notes,pageNumber,10);
            return View("Index",new NoteIndexViewModel(list,possibleCategories,category,dateFrom,dateTo));
        }
        public IActionResult Clear()
        {
            return Index(DateTime.MinValue,DateTime.MinValue);
        }
        [HttpPost]
        public IActionResult AddCategory(NoteEditViewModel model,List<NoteCategory> noteCategories)
        {
            if(ModelState.IsValid)
            {
                if(String.IsNullOrEmpty(model.NewCategory))
                {
                    ModelState.AddModelError("Category error","Empty category");
                    if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                        return View("Edit",model);
                    else return View("Add",model);
                }
                else if(noteCategories.Where(m=>m.Category.Title==model.NewCategory).Any())
                {
                    ModelState.AddModelError("Category error","Category already exists");
                    if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                        return View("Edit",model);
                    else return View("Add",model);
                }
                model.Note.NoteCategories=noteCategories.Append(new NoteCategory
                    {Note=model.Note,Category=new Category(model.NewCategory)}).ToArray();
                model.NewCategory="";
            }
            
            if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                return View("Edit",model);
            else return View("Add",model);
        }
        [HttpPost]
        public IActionResult RemoveCategories(NoteEditViewModel model)
        {
            foreach(var c in model.CategoriesToRemove ?? new string[] { })
            {
                model.Note.NoteCategories=model.Note.NoteCategories.Where(v=>v.Category.Title!=c).ToArray();
            }
            model.CategoriesToRemove=new string[]{};
            if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                return View("Edit",new NoteEditViewModel(model.Note));
            else return View("Add",new NoteEditViewModel(model.Note));
        }
        public IActionResult Add()
        {
            Note n = new Note();
            n.Title="write some title";
            n.NoteDate=DateTime.Now;
            n.Description="Write some text";
            n.NoteCategories = new List<NoteCategory>();
            return View(new NoteEditViewModel(n));
        }
        [HttpPost]
        public IActionResult Add(NoteEditViewModel model, List<NoteCategory> noteCategories)
        {
            if(ModelState.IsValid)
            {
                if(model.Note.Title=="write some title"||model.Note.Title=="")
                {
                    ModelState.AddModelError("Title error","Wrong title");
                    return View(model);
                }
                else if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                {
                    ModelState.AddModelError("Title error","Title already taken");
                    return View(model);
                }
                using(var transaction = _context.Database.BeginTransaction())
                {
                    _context.Notes.Add(model.Note);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                foreach(var n in noteCategories)
                {
                    Category newCategory;
                    if(!_context.Categories.Where(m=>m.Title==n.Category.Title).Any())
                    {
                        using(var transaction = _context.Database.BeginTransaction())
                        {
                            newCategory=new Category{Title=n.Category.Title};
                            _context.Categories.Add(newCategory);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        newCategory=_context.Categories.Where(m=>m.Title==n.Category.Title).FirstOrDefault();
                    }
                    using(var transaction = _context.Database.BeginTransaction())
                    {
                        bool noteExists = _context.Notes.Where(m=>m.NoteID==model.Note.NoteID).Any();
                        bool categoryExists = _context.Categories.Where(m=>m.CategoryID==newCategory.CategoryID).Any();
                        NoteCategory c = new NoteCategory{NoteID=model.Note.NoteID,CategoryID=newCategory.CategoryID};
                        _context.NoteCategories.Add(c);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();
            return Index(DateTime.MinValue,DateTime.MinValue,"");
        }
        public IActionResult Edit(string title)
        {
            Note n = Notes.Where(m => m.Title == title).FirstOrDefault();
            return View(new NoteEditViewModel(n));
        }
        [HttpPost]
        public IActionResult Edit(NoteEditViewModel model)
        {            
            Note oldNote = Notes.Where(m=>m.Title==model.OldTitle).FirstOrDefault();
            Note newNote = model.Note;
            if(ModelState.IsValid)
            {
                if(model.Note.Title!=model.OldTitle&&(Notes.Where(m=>m.Title==model.Note.Title).Any()||model.Note.Title==""))
                {
                    ModelState.AddModelError("Title error","Title already taken");
                    return View(model);
                }
                _context.Notes.Update(newNote);
                _context.SaveChanges();
            }
            return Index(DateTime.MinValue,DateTime.MinValue,"");
        }
        public IActionResult Delete(string title)
        {
            //Note note=Notes.Where(m=>m.Title==title).FirstOrDefault();
            //Notes.Remove(note);
            Note note=_context.Notes.Find(title);
            _context.Notes.Remove(note);
            _context.SaveChanges();
            return Index(DateTime.MinValue,DateTime.MinValue);
        }
        [HttpPost]
        public JsonResult doesFileNameExist(string title)
        {
        var file = _context.Notes.Find(title);
        return Json(file == null);
        }
    }
}
