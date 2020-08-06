using System;

namespace MiddlewareHandsOn.Api.Models
{
    public class User
    {
        public User(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public string Name { get; }
        public string Country { get; }

    }
}
