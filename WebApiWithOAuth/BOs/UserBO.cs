using System;
using System.Collections.Generic;
using WebApiWithOAuth.Context;
using WebApiWithOAuth.TOs;
using System.Linq;

namespace WebApiWithOAuth.BOs
{
    public class UserBO
    {
        private readonly MyDbContext _context = new MyDbContext();

        public IEnumerable<UserTO> GetAll()
        {
            return _context.DbSetUser;
        }

        public UserTO Get(int id)
        {
            return _context.DbSetUser.FirstOrDefault(i => i.ID == id);
        }
    }
}
