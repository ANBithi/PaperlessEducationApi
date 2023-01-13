using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class StudenViewModel
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public ResultViewModel Result { get; set; }
    }
}
