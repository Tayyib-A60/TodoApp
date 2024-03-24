using System.Collections.Generic;
using System.Linq;
using Server.Data.Entities;
using Server.Data.Interfaces;

namespace Server.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository()
        {
            _dataContext = new DataContext();
        }

        public void CreateUser(UserEntity entity)
        {
            _dataContext.Users.Add(entity);
            _dataContext.SaveChanges();
        }

        public void UpdateUser(UserEntity entity)
        {
            _dataContext.Users.Update(entity);
            _dataContext.SaveChanges();
        }

        public UserEntity? GetUserById(string id)
        {
            var User = _dataContext.Users.FirstOrDefault(User => User.ID.ToString() == id);
            return User;
        }

        public string? DeleteUser(string id)
        {
            var UserToDelete = _dataContext.Users.FirstOrDefault(User => User.ID.ToString() == id);

            if (UserToDelete is not null)
            {
                _dataContext.Users.Remove(UserToDelete);
                _dataContext.SaveChanges();
                return id;
            }

            return null;
        }

        public List<UserEntity> ListUsers()
        {
            return _dataContext.Users.ToList();
        }
    }
}