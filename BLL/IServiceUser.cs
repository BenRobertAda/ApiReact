using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IServiceUser
    {
        bool Add(User user);
        bool Update(User user);
        bool Delete(int id);
        List<User> FindById();
        User Find(int id);
        User LogIn(string login, string password);
    }
}
