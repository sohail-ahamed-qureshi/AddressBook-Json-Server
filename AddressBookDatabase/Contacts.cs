using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDatabase
{
    public class Contacts
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
