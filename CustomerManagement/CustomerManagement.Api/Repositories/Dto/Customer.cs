using System;
using System.Diagnostics;

namespace CustomerManagement.Api.Repositories.Dto
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PreviouslyOrdered { get; set; }
        public bool WebCustomer { get; set; }
        public DateTime LastActive { get; set; }
        public string FavouriteColours { get; set; }

        private string DebuggerDisplay => $"{Name} : {LastActive} : {string.Join(", ", FavouriteColours)}";
    }
}