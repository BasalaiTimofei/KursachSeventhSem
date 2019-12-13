using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Backend.Models.View;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService
    {
        private readonly ApplicationContext _applicationContext;

        public UserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<string[]> Registration(RegistrationUserViewModel model, string role)
        {
            if (_applicationContext.Users.Any(w => w.UserName == model.UserName))
            {
                throw new Exception();
            }
            if (_applicationContext.Users.Any(w => w.Email == model.Email))
            {
                throw new Exception();
            }
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,

                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,

                State = model.State,
                City = model.City,
                Address = model.Address,
                HouseNumber = model.HouseNumber,

                DateTimeCreate = DateTime.Now,
                Role = await _applicationContext.Roles.FirstOrDefaultAsync(w => w.Name == role)
            };
            await _applicationContext.Users.AddAsync(user);
            await _applicationContext.SaveChangesAsync();

            var basket = new Basket
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id
            };
            await _applicationContext.Baskets.AddAsync(basket);
            await _applicationContext.SaveChangesAsync();

            return new[] {user.Id, role};
        }

        public async Task<string[]> Login(LoginUserViewModel model)
        {
            if (_applicationContext.Users.Any(w => w.UserName == model.UserName && w.Password == model.Password))
            {
                throw new Exception();
            }

            var user = await _applicationContext.Users.FirstOrDefaultAsync(w =>
                w.UserName == model.UserName && w.Password == model.Password);

            return new[] {user.Id, user.Role.Name};
        }

        public async Task Delete(DeleteUserViewModel model)
        {
            if (_applicationContext.Users.Any(w => w.UserName == model.UserName && w.Password == model.Password))
            {
                throw new Exception();
            }

            var user = await _applicationContext.Users.FirstOrDefaultAsync(w =>
                w.UserName == model.UserName && w.Password == model.Password);

            _applicationContext.BasketProducts.RemoveRange(
                _applicationContext.BasketProducts.Where(w => w.Basket.UserId == user.Id));
            _applicationContext.Baskets.Remove(user.Basket);
            _applicationContext.Assessments.RemoveRange(user.Assessments);
            _applicationContext.OrderProducts.RemoveRange(
                _applicationContext.OrderProducts.Where(w => w.Order.UserId == user.Id));
            _applicationContext.Orders.RemoveRange(user.Orders);
            _applicationContext.Comments.RemoveRange(user.Comments);

            await _applicationContext.SaveChangesAsync();
        }
    }
}