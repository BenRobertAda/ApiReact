using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ServiceUser : IServiceUser
    {
        private readonly ApiReactContext _manager;
        public ServiceUser(ApiReactContext manager)
        {
            _manager = manager;
        }

        public bool Add(User user)
        {
            try
            {
                _manager.Users.Add(user);
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
                User user = _manager.Users.Find(id);
                _manager.Users.Remove(user);
                _manager.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<User> FindById()
        {
            try
            {
                return _manager.Users.ToList();
            }
            catch (Exception e)
            {

                return null;

            }
        }

        public User Find(int id)
        {
            try
            {

                return _manager.Users.Find(id);

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public bool Update(User user)
        {
            try
            {
                User usertoupdate = _manager.Users.Find(user.Id);
                _manager.Entry(usertoupdate).CurrentValues.SetValues(user);
                _manager.SaveChanges();
                return true;
            }

            catch (Exception e)
            {

                return false;
            }
        }

        public User LogIn(string login, string password)
        {
            try
            {

                return _manager.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
