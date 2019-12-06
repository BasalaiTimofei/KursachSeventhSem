using System;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class UserDatabaseModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Название улици
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime DateTimeCreate { get; set; }

        public string RoleId { get; set; }
        public RoleDatabaseModel Role { get; set; }

        public BasketDatabaseModel Basket { get; set; }

        public List<AssessmentDatabaseModel> Assessment { get; set; }
        public List<OrderDatabaseModel> Orders { get; set; }
        public List<CommentDatabaseModel> Comments { get; set; }

        public UserDatabaseModel()
        {
            Assessment = new List<AssessmentDatabaseModel>();
            Orders = new List<OrderDatabaseModel>();
            Comments = new List<CommentDatabaseModel>();
        }
    }
}