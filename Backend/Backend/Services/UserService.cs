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
        private readonly BasketService _basketService;

        public UserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _basketService = new BasketService(_applicationContext);
        }

        public async Task<string[]> Create(Registration model, string role)
        {
            var userDb = _applicationContext.Users.Where(w => w.UserName == model.UserName);
            if (_applicationContext.Users.Any(w => w.UserName == model.UserName))
            {
                //Вернуть ошибку о том что Уже есть Юзер с таким UserName
                throw new Exception();
            }
            if (_applicationContext.Users.Any(w => w.Email == model.Email))
            {
                //Вернуть ошибку о том что Уже есть Юзер с таким Email
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
                Role = await _applicationContext.Roles.FirstAsync(w => w.Name == role)
            };

            await _applicationContext.Users.AddAsync(user);
            await _applicationContext.SaveChangesAsync();

            await _basketService.Create(user.Id);

            return new[] {user.Id, role};
        }

        public string[] Login(Login model)
        {
            if (_applicationContext.Users.Any(w => w.UserName == model.UserName && w.Password == model.Password))
            {
                //Вернуть ошибку о том что данные неверны
                throw new Exception();
            }

            var user = _applicationContext.Users.FirstOrDefault(w =>
                w.UserName == model.UserName && w.Password == model.Password);

            if (user != null) return new[] {user.Id, user.Role.Name};
        }

        //public void Delete(DeleteUser model)
        //{

        //}
    }
}