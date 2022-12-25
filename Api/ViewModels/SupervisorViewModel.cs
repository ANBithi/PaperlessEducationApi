using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class SupervisorViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ReportsTo { get; set; }
    }
}
