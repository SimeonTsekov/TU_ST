using webAPI.DTOs;
using webAPI.DTOs.Request;

namespace webAPI.Interfaces.User;

public interface IUserService
{
    UserResponse Update(int id, UserRequest updatedModel);
    UserResponse GetById(int id);
    List<UserResponse> Get(string order, int count);
    void Delete(int id);
}