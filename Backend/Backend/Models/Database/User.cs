using System;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class User
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
        public Role Role { get; set; }

        public Basket Basket { get; set; }

        public List<Assessment> Assessment { get; set; }
        public List<Order> Orders { get; set; }
        public List<Comment> Comments { get; set; }

        public User()
        {
            Assessment = new List<Assessment>();
            Orders = new List<Order>();
            Comments = new List<Comment>();
        }
    }
}