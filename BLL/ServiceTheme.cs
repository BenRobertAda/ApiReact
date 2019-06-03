using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ServiceTheme : IServiceTheme
    {
        private readonly ApiReactContext _manager;
        public ServiceTheme(ApiReactContext manager)
        {
            _manager = manager;
        }
        public bool Add(Theme theme)
        {
            try
            {
                _manager.Themes.Add(theme);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Theme theme = _manager.Themes.Find(id);
                _manager.Themes.Remove(theme);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Theme> FindById()
        {
            try
            {
                return _manager.Themes.ToList();
            }
            catch (Exception e)
            {

                return null;

            }
        }

        public Theme Find(int id)
        {
            try
            {

                return _manager.Themes.Include(x => x.Films).FirstOrDefault(x => x.Id == id);

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public bool Update(Theme theme)
        {
            try
            {
                Theme themetoupdate = _manager.Themes.Find(theme.Id);
                _manager.Entry(themetoupdate).CurrentValues.SetValues(theme);
                _manager.SaveChanges();
                return true;
            }

            catch (Exception e)
            {

                return false;
            }
        }
    }
}
