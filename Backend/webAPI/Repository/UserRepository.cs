﻿using webAPI.Data;
using webApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Interfaces;

namespace webAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly webAPIDbContext _dbContext;

        public UserRepository(webAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel Create(UserModel newUser)
        {
            _dbContext.UserModels.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;
        }

        public UserModel Update(int userId, UserModel updatedUser)
        {
            var existingUser = _dbContext.UserModels.Find(userId);
            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                existingUser.Passwords = updatedUser.Passwords;
                existingUser.Age = updatedUser.Age;
                existingUser.Height = updatedUser.Height;

                _dbContext.SaveChanges();
            }
            return existingUser;
        }

        public void Delete(int userId)
        {
            var userToRemove = _dbContext.UserModels.Find(userId);
            if (userToRemove != null)
            {
                _dbContext.UserModels.Remove(userToRemove);
                _dbContext.SaveChanges();
            }
        }

        public List<UserModel> GetAllUsers()
        {
            return _dbContext.UserModels.ToList();
        }

        public UserModel GetUserById(int userId)
        {
            return _dbContext.UserModels.Find(userId);
        }

        public UserModel FindUserByEmail(string email)
        {
            return _dbContext.UserModels.FirstOrDefault(u => u.Email == email);
        }
    }
}