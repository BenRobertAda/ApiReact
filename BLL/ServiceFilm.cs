using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ServiceFilm : IServiceFilm
    {
        private readonly ApiReactContext _manager;
        public ServiceFilm(ApiReactContext manager)
        {
            _manager = manager;
        }
        public bool Add(Film film)
        {
            try
            {
                _manager.Films.Add(film);
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
                Film film = _manager.Films.Find(id);
                _manager.Films.Remove(film);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Film> FindById()
        {
            try
            {
                return _manager.Films.ToList();
            }
            catch (Exception e)
            {

                return null;

            }
        }

        public Film Find(int id)
        {
            try
            {

                return _manager.Films.Include(x=>x.Theme).FirstOrDefault(x=>x.Id == id);

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public bool Update(Film film)
        {
            try
            {
                Film filmtoupdate = _manager.Films.Find(film.Id);
                _manager.Entry(filmtoupdate).CurrentValues.SetValues(film);
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
