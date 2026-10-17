// Modern .NET, Chapter 4, Section 4.1
using System;
using System.Collections.Generic;

namespace LegacyShop.Models
{
    public class Customer
    {
        private string _name;
        private string _email;
        private List<Order> _orders;

        public Customer() { }

        public Customer(string name, string email)
        {
            _name = name;
            _email = email;
            _orders = new List<Order>();
        }

        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public List<Order> Orders { get { return _orders; } set { _orders = value; } }

        public override bool Equals(object obj)
        {
            var other = obj as Customer;
            if (other == null) return false;
            return _email == other._email && _name == other._name;
        }

        public override int GetHashCode()
        {
            return (_name + _email).GetHashCode();
        }

        public string GetDisplayName()
        {
            if (_name != null && _name.Length > 0)
                return _name;
            else if (_email != null)
                return _email;
            else
                return "Unknown";
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
