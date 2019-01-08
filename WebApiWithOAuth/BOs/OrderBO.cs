using System;
using System.Collections.Generic;
using System.Linq;
using WebApiWithOAuth.Context;
using WebApiWithOAuth.TOs;

namespace WebApiWithOAuth.BOs
{
    public class OrderBO
    {
        private readonly MyDbContext _context = new MyDbContext();

        public IEnumerable<OrderTO> GetAll()
        {
            return _context.DbSetOrder;
        }

        public OrderTO Get(int id)
        {
            return _context.DbSetOrder.FirstOrDefault(i => i.ID == id);
        }
    }
}
