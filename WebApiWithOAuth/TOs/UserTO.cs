using System;
using System.Collections.Generic;

namespace WebApiWithOAuth.TOs
{
    public class UserTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<OrderTO> Orders { get; set; } = new List<OrderTO>();
    }
}
