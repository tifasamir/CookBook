using CodeBook.db;
using CodeBook.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBook.services
{
    public class LessonService
    {
        CodeBookContext _ctx;
        IoService ioService = new IoService();
        public LessonService(CodeBookContext ctx)
        {
            this._ctx = ctx;
        }
        public List<Lesson> findAll() {
            return _ctx.Lesson.Include(b => b.Views)
                  .ToList();
        }
        public Lesson find(int id) {
            Lesson i = _ctx.Lesson.Include(b => b.Views)
                               .FirstOrDefault(e => e.LessonId == id);
            return i;
                
        }

        public void save(Lesson l) {
             _ctx.Lesson.Add(l);
             _ctx.SaveChanges();
        }
        public void update(Lesson l) {     
            _ctx.Entry(l).State = EntityState.Modified;
            _ctx.Lesson.Update(l);
     
            _ctx.SaveChanges();
        }

        public List<Lesson> findLessByChId(int id)
        {
            return _ctx.Lesson.Where(x => x.ChapterId == id).ToList();
        }

        public void delete(Lesson l)
        {
            _ctx.Lesson.Remove(l);
            _ctx.SaveChanges();
        }

        public int getMax()
        {
         Lesson l =   _ctx.Lesson.OrderByDescending(u => u.LessonId).FirstOrDefault();
            if (l != null)
            {
                return l.LessonId;
            }
            else
            {
                return 1;
            }
        }

        public void deleteCascade(int lessonId)
        {
            Lesson l = _ctx.Lesson.Include(e => e.Views)//.ThenInclude(c => c.Lessons)
                               .Where(e => e.LessonId == lessonId)
                               .First();
            ioService.removeRelatedFiles(l);
            _ctx.Lesson.Remove(l);
            _ctx.SaveChanges();
        }

       
    }
}
