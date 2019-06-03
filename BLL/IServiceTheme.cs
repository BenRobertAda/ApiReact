using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IServiceTheme
    {
        bool Add(Theme theme);
        bool Update(Theme theme);
        bool Delete(int id);
        List<Theme> FindById();
        Theme Find(int id);
    }
}
