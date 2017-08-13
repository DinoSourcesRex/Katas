using System;
using System.Diagnostics;

namespace CustomerManagement.Tests.Acceptance.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CustomerRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PreviouslyOrdered { get; set; }
        public bool WebCustomer { get; set; }
        public DateTime LastActive { get; set; }
        public string FavouriteColours { get; set; }

        private string DebuggerDisplay => $"{Name} : {LastActive} : {FavouriteColours}";
    }
}