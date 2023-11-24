using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface IUserRepository
    {
        UserModel Create(UserModel newUser);
        UserModel Update(int userId, UserModel updatedUser);
        void Delete(int userId);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        UserModel FindUserByEmail(string email);
    }
}
