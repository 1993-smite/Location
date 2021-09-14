using System;
using System.Collections.Generic;
using System.Text;

namespace DadataLocation.Models
{
    public class Phone
    {
        protected string _number;

        public string GetNumber => _number;

        public Phone(string number)
        {
            _number = number;
        }
    }

    public class PhoneLocation: Phone
    {
        public Location Location { get; private set; }

        public PhoneLocation(string number, Location location):base(number)
        {
            Location = location;
        }
    }
}
