using CodeBook.db;
using CodeBook.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBook.services
{
    public class LangService
    {
        CodeBookContext _ctx;
        public LangService(CodeBookContext ctx)
        {
            this._ctx = ctx;
        }
        public List<Lang> findAll()
        {
            return _ctx.Lang.ToList();
        }
        public Lang find(int id)
        {
            return _ctx.Lang.Find(id);
        }

        public void save(Lang l)
        {
            _ctx.Lang.Add(l);
            _ctx.SaveChanges();
        }
        public void update(Lang l)
        {
            _ctx.Lang.Update(l);
            _ctx.SaveChanges();
        }

        public void deleteById(int id)
        {
            Lang l = _ctx.Lang.Find(id);
            _ctx.Lang.Remove(l);
            _ctx.SaveChanges();
        }

        public void delete(Lang l)
        {
            
            _ctx.Lang.Remove(l);
            _ctx.SaveChanges();
        }

        public void deleteCascade(int id)
        {
            try
            {
                Lang l = _ctx.Lang.Where(e => e.LangId == id).Include(e => e.Chapters).ThenInclude(c => c.Lessons).FirstOrDefault();


                _ctx.Lang.Remove(l);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {

            }
         

           
        }
    }
}
