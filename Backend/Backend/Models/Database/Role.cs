using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class RoleDatabaseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<UserDatabaseModel> Users { get; set; }

        public RoleDatabaseModel()
        {
            Users = new List<UserDatabaseModel>();
        }
    }
}