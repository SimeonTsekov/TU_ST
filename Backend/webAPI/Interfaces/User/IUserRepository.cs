using webApi.Data.Models;

namespace webAPI.Interfaces.User
{
    public interface IUserRepository
    {
        UserModel Create(UserModel newUser);
        UserModel? Update(int userId, UserModel updatedUser);
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        UserModel FindUserByEmail(string email);
        void Delete(int userId);
    }
}
