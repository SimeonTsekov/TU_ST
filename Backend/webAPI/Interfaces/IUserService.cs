using webAPI.DTOs;
using webAPI.DTOs.Request;

namespace webAPI.Interfaces;

public interface IUserService
{
    UserResponse Update(int id, UserRequest updatedModel);
    void Delete(int id);
    List<UserResponse> GetAll();
    UserResponse GetById(int id);
}