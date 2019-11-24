using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NTR2.Models;
using NTR2;

/* 
namespace NTR2.Repositories
{
    public class NoteRepository : IRepository<Note, string>
    {
        private const string directory = "./data";
        public Note FindById(string title)
        {
            Note note = null;
            string[] files = Directory.GetFiles("./data/");
            string extension="";
            if(files.Where(m=>m==title+".txt").Any()) extension="txt";
            else if(files.Where(m=>m==title+".md").Any()) extension="md";
            else return null;
            using (StreamReader file = new StreamReader("./data/"+title+"."+extension))
            {
                string line = file.ReadLine();
                HashSet<string> categories = getCategories(line);
                line = file.ReadLine();
                DateTime date = getDate(line);
                string content = "";
                while ((line = file.ReadLine()) != null)
                {
                    content += line;
                }

                note = new Note(title, categories.ToArray(), date, content);
            }

            return note;
        }

        public IEnumerable FindAll()
        {
            string[] files = Directory.GetFiles(directory);
            List<Note> notes = new List<Note>(); 
            foreach (string fileName in files) 
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    //reading line describing category
                    string line = file.ReadLine();
                    HashSet<string> categories = getCategories(line);
                    line = file.ReadLine();
                    DateTime date = getDate(line);
                    string text = file.ReadToEnd();
                    Note newNote = new Note(getNoteTitle(fileName), categories.ToArray(), date,text,getExtension(fileName));
                    notes.Add(newNote);
                }
            }

            return notes;
        }

        public void Update(Note oldNote, Note newNote)
        {
            //delete old 
            if(oldNote.Title!=newNote.Title) Delete(oldNote.Title);
            //save new
            Save(newNote);
        }

        public void Save(Note note)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("category: ");
            for (int i = 0; i < note.NoteCategories.Count(); ++i)
            {
                stringBuilder.Append(note.NoteCategories[i]);
                if (i < note.NoteCategories.Count() - 1)
                {
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Append("\ndate: ");
            note.NoteDate=DateTime.Today;
            stringBuilder.Append(note.NoteDate.ToString("yyyy/MM/dd") + "\n");
            stringBuilder.Append(note.Description);
            string path = directory +"/"+ note.Title +"."+ note.Extension;
            File.WriteAllText(path, stringBuilder.ToString());
        }

        public void Delete(string title)
        {
            string[] files = Directory.GetFiles(directory);
            string fileToDelete = files.Single(file => getNoteTitle(file).Equals(title));
            File.Delete(fileToDelete);
        }
        private HashSet<string> getCategories(string categoryString) 
        {
            return categoryString.Split(':')[1].Split(',').Select(item => item.Trim()).ToHashSet();
        }

        private DateTime getDate(string dateString) 
        {
            string date = dateString.Split(':')[1];
            date = date.Trim();
            
            return Convert.ToDateTime(date);
        }

        private string getNoteTitle(string fileName) 
        {
            return fileName.Split('/').Last().Split('.')[0];
        }

        private string getExtension(string fileName) 
        {
            return fileName.Split('/').Last().Split('.').Last();
        }

    }
}