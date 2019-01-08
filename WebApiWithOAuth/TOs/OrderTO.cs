using System;
namespace WebApiWithOAuth.TOs
{
    public class OrderTO
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public UserTO User { get; set; }
    }
}
