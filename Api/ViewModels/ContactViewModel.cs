using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }


    public class AddContactRequest
    {
        public string BelongsTo { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

    }
    public class GetContactRequest
    {
        public string BelongsTo { get; set; }
        public string Type { get; set; }
    }
    public class GetAddressRequest
    {
        public string BelongsTo { get; set; }
        public string Type { get; set; }
    }
}
