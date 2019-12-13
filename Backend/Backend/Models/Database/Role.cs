using System.Collections.Generic;
using Backend.Interfaces;

namespace Backend.Models.Database
{
    public sealed class Role : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}