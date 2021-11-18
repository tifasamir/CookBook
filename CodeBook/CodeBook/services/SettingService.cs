using CodeBook.db;
using CodeBook.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBook.services
{
    public class SettingService
    {
        CodeBookContext _ctx;
        public SettingService(CodeBookContext ctx)
        {
            this._ctx = ctx;
        }
        public List<Setting> findAll()
        {
            return _ctx.Setting.ToList();
        }
        public Setting find(int id)
        {
            return _ctx.Setting.Find(id);
        }

        public void save(Setting l)
        {


            _ctx.Setting.Add(l);
            _ctx.SaveChanges();


        }
        public void update(Setting l)
        {
            _ctx.Setting.Update(l);
            _ctx.SaveChanges();
        }

        //public List<Setting> findChByLangId(int id)
        //{
        //    return _ctx.Setting.Where(x => x.S == id).ToList();
        //}

        public void delete(Setting l)
        {
            _ctx.Setting.Remove(l);
            _ctx.SaveChanges();
        }

        public void update(string key, int value)
        {
          Setting ss=   _ctx.Setting.Where(e => e.key == key).FirstOrDefault(); ;
            if (ss != null)
            {
            ss.value = value.ToString();
            _ctx.Setting.Update(ss);
            _ctx.SaveChanges();
            }
           
        }
        //public void deleteCascade(int id)
        //{
        //    try
        //    {
        //        Setting l = _ctx.Chapter.Include(e => e.Lessons)//.ThenInclude(c => c.Lessons)
        //                     .Where(e => e.ChapterId == id)
        //                    .FirstOrDefault();
        //        _ctx.Chapter.Remove(l);
        //        _ctx.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}
