using System.Collections.Generic;
using Server.Data.Entities;

namespace Server.Data.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(UserEntity entity);
        string? DeleteUser(string id);
        UserEntity? GetUserById(string id);
        List<UserEntity> ListUsers();
        void UpdateUser(UserEntity entity);
    }
}