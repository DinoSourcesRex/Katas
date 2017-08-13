using System;
using System.Collections.Generic;

namespace CustomerManagement.Tests.Acceptance.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public bool PreviouslyOrdered { get; set; }
        public bool WebCustomer { get; set; }
        public DateTime LastActive { get; set; }
        public List<string> FavouriteColours { get; set; }
    }
}