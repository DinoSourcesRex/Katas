using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CustomerManagement.Client.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PreviouslyOrdered { get; set; }
        public bool WebCustomer { get; set; }
        public DateTime LastActive { get; set; }
        public List<string> FavouriteColours { get; set; }
        public bool Palindrome { get; set; }

        private string DebuggerDisplay => $"{Name} : {LastActive} : {string.Join(", ", FavouriteColours)}";
    }
}