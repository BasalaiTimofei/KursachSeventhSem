using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;

namespace Backend.Services
{
    public class RoleService
    {
        private readonly ApplicationContext _applicationContext;

        public RoleService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(string roleName)
        {
            var role = new RoleDatabaseModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName
            };
            await _applicationContext.Roles.AddAsync(role);
            await _applicationContext.SaveChangesAsync();
        }
    }
}