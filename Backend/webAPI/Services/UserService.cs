using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs;
using webAPI.DTOs.Request;
using webAPI.Interfaces.User;

namespace webAPI.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public UserResponse Update(int id, UserRequest updatedModel)
    {
        var data = this._mapper.Map<UserModel>(updatedModel);
        var result = this._userRepository.Update(id, data);
        return this._mapper.Map<UserResponse>(result);
    }

    public void Delete(int id)
    {
        this._userRepository.Delete(id);
    }

    public List<UserResponse> GetAll()
    {
        return this._mapper.Map<List<UserResponse>>(this._userRepository.GetAllUsers());
    }

    public UserResponse GetById(int id)
    {
        return this._mapper.Map<UserResponse>(this._userRepository.GetUserById(id));
    }
}