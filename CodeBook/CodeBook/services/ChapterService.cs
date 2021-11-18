using CodeBook.db;
using CodeBook.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBook.services
{
    public class ChapterService
    {
        CodeBookContext _ctx;
        public ChapterService(CodeBookContext ctx)
        {
            this._ctx = ctx;
        }
        public List<Chapter> findAll()
        {
            return _ctx.Chapter.ToList();
        }
        public Chapter find(int id)
        {
            return _ctx.Chapter.Find(id);
        }

        public void save(Chapter l)
        {
          
        
                _ctx.Chapter.Add(l);
                _ctx.SaveChanges();
           
          
        }
        public void update(Chapter l)
        {
            _ctx.Chapter.Update(l);
            _ctx.SaveChanges();
        }

        public List<Chapter> findChByLangId(int id)
        {
            return _ctx.Chapter.Where(x=> x.LangId ==id).ToList();
        }

        public void delete(Chapter l)
        {
            _ctx.Chapter.Remove(l);
            _ctx.SaveChanges();
        }
        public void deleteCascade(int id)
        {
            try
            {
                Chapter l = _ctx.Chapter.Include(e => e.Lessons)//.ThenInclude(c => c.Lessons)
                             .Where(e => e.ChapterId == id)
                            .FirstOrDefault();
                _ctx.Chapter.Remove(l);
                _ctx.SaveChanges();
            }catch(Exception ex)
            {

            }
          
        }
    }
}
