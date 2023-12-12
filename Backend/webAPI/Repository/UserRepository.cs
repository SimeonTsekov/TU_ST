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

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            existingUser.Age = updatedUser.Age;
            existingUser.Height = updatedUser.Height;

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
    }
}
