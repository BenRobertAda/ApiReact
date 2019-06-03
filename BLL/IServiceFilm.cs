using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IServiceFilm
    {
        bool Add(Film film);
        bool Update(Film theme);
        bool Delete(int id);
        List<Film> FindById();
        Film Find(int id);
    }
}
