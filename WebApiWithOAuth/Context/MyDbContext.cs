using System;
using System.Collections.Generic;
using WebApiWithOAuth.TOs;

namespace WebApiWithOAuth.Context
{
    public class MyDbContext
    {
        private static bool Initialize = true;

        private static readonly IList<UserTO> _users = new List<UserTO>
        {
            new UserTO { ID = 1, Name = "User 001", Email = "user.001@mail.com" },
            new UserTO { ID = 2, Name = "User 002", Email = "user.002@mail.com" },
            new UserTO { ID = 3, Name = "User 003", Email = "user.003@mail.com" }
        };

        private static readonly IList<OrderTO> _orders = new List<OrderTO>
        {
            new OrderTO { ID = 1, Type = "type 001", Value = "100" },
            new OrderTO { ID = 2, Type = "type 002", Value = "200" },
            new OrderTO { ID = 3, Type = "type 003", Value = "300" },
            new OrderTO { ID = 4, Type = "type 004", Value = "400" },
            new OrderTO { ID = 5, Type = "type 005", Value = "500" },
            new OrderTO { ID = 6, Type = "type 006", Value = "600" },
        };

        public IEnumerable<UserTO> DbSetUser => _users;

        public IEnumerable<OrderTO> DbSetOrder => _orders;

        public MyDbContext()
        {
            if (Initialize)
            {
                SetupUsersRelationship();
                //SetupOrdersRelationship();
                Initialize = false;
            }
        }

        private void SetupUsersRelationship()
        {
            _users[0].Orders.Add(_orders[0]);

            _users[1].Orders.Add(_orders[1]);
            _users[1].Orders.Add(_orders[2]);

            _users[2].Orders.Add(_orders[3]);
            _users[2].Orders.Add(_orders[4]);
            _users[2].Orders.Add(_orders[5]);
        }

        private void SetupOrdersRelationship()
        {
            _orders[0].User = _users[0];

            _orders[1].User = _users[1];
            _orders[2].User = _users[1];

            _orders[3].User = _users[2];
            _orders[4].User = _users[2];
            _orders[5].User = _users[2];
        }
    }
}
