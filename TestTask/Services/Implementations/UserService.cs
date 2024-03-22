using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Data;
using Microsoft.EntityFrameworkCore;
using TestTask.Enums;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> GetUser()
        {
            var user = await _applicationDbContext.Orders.GroupBy(order => order.UserId)
                                                         .Select(order => new { UserId = order.Key, OrderCount = order.Count() })
                                                         .OrderByDescending(order => order.OrderCount)
                                                         .FirstOrDefaultAsync();

            var userMax = await _applicationDbContext.Users.FirstOrDefaultAsync(users => users.Id == user.UserId);
            return userMax;
        }

        public async Task<List<User>> GetUsers()
        {
            var usersInactive = await _applicationDbContext.Users.Where(user => user.Status == UserStatus.Inactive)
                                                                 .ToListAsync<User>();
            return usersInactive;
        }
    }
}
