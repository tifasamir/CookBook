using CodeBook.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBook.services
{
   public  class ViewerService
    {
        CodeBookContext _ctx;
        public ViewerService(CodeBookContext ctx)
        {
            this._ctx = ctx;
        }
        public List<Viewer> findAll()
        {
            return _ctx.Viewer.ToList();
        }
        public Viewer find(int id)
        {
            return _ctx.Viewer.Find(id);
        }

        public void save(Viewer l)
        {
            _ctx.Viewer.Add(l);
            _ctx.SaveChanges();
        }
        public void update(Viewer l)
        {
            _ctx.Viewer.Update(l);
            _ctx.SaveChanges();
        }

        public List<Viewer> findViewerByLessId(int id)
        {
            return _ctx.Viewer.Where(x => x.LessonId == id).ToList();
        }

        public int getMax()
        {
            Viewer l = _ctx.Viewer.OrderByDescending(u => u.ViewerId).FirstOrDefault();
            if (l != null)
            {
                return l.ViewerId;
            }
            else
            {
                return 1;
            }
        }

        public Viewer findFile(string search)
        {
            return _ctx.Viewer.Where(x => x.fileurl == search|| x.snippeturl== search).FirstOrDefault();
        }
    }
}
