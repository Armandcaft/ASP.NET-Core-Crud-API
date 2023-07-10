using CrudAPI.Models;

namespace CrudAPI.Repositories
{
    public interface IMyUserRepository
    {
        MyUser GetUserById(int id);
        MyUser GetUserByEmail(string email);
        void AddUser(MyUser user);
        void UpdateUser(MyUser user);
        void DeleteUser(int id);
        object GetAllUsers();
        bool AnyUsers();

    }
}