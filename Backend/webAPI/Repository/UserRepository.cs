using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces.User;

namespace webAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly webAPIDbContext _dbContext;

        public UserRepository(webAPIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserModel Create(UserModel newUser)
        {
            this._dbContext.UserModels.Add(newUser);
            this._dbContext.SaveChanges();
            return newUser;
        }

        public UserModel Update(int userId, UserModel updatedUser)
        {
            var existingUser = this.GetUserById(userId);

            if(updatedUser.Username != null && !updatedUser.Username.Equals(""))
            {
                existingUser.Username = updatedUser.Username;
            }

            if (updatedUser.Email != null && !updatedUser.Email.Equals(""))
            {
                existingUser.Email = updatedUser.Email;
            }

            if (updatedUser.Password != null && !updatedUser.Password.Equals(""))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            }

            if (updatedUser.Age != -1)
            {
                existingUser.Age = updatedUser.Age;
            }

            if (updatedUser.Height != -1)
            {
                existingUser.Height = updatedUser.Height;
            }

            if (updatedUser.Sex != Sex.None)
            {
                existingUser.Sex = updatedUser.Sex;
            }

            this._dbContext.SaveChanges();

            return existingUser;
        }

        public void Delete(int userId)
        {
            var userToRemove = this.GetUserById(userId);

            this._dbContext.UserModels.Remove(userToRemove);
            this._dbContext.SaveChanges();
        }

        public List<UserModel> Get(string order, int count)
        {
            var query = this._dbContext.UserModels.AsQueryable();

            query = order.ToLower() switch
            {
                "asc" => query.OrderBy(a => a.CreatedDate),
                "desc" => query.OrderByDescending(a => a.CreatedDate),
                _ => throw new ArgumentException("Invalid order parameter. Accepted values are 'asc' or 'desc'.")
            };

            if (count > 0)
            {
                query = query.Take(count);
            }

            return query.ToList();
        }

        public UserModel GetUserById(int userId)
        {
            return this._dbContext.UserModels
                .FirstOrDefault(u => u.Id == userId) ?? throw new NullReferenceException("The user with id '" + userId + "' was not found.");
        }

        public UserModel FindUserByEmail(string email)
        {
            return this._dbContext.UserModels
                .FirstOrDefault(u => u.Email == email) ?? throw new NullReferenceException("The user with email '" + email + "' was not found.");
        }

        public List<Role> GetRolesForUser(int userId)
        {
            var roles = this._dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToList();

            return roles.Count == 0 ? new List<Role>() : roles!;
        }
    }
}
