using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class AddressViewModel
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string AlternateEmail { get; set; }

    }
    public class PermanentAddressViewmodel
    {
        public string Address { get; set; }
        public string Upazilla { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
    }

    public class AddAddressRequest
    {
        public string BelongsTo { get; set; }
        public string Type { get; set; }
        public string Upazilla { get; set; }
        public string District { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string AlternateEmail { get; set; }

    }
}
