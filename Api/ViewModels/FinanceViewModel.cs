using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class FinanceViewModel
    {
        public string SalaryIssueDate { get; set; }

        public string Month { get; set; }

        public double Salary { get; set; }

        public FinanceAddEnum SalaryType { get; set; }

        public int Year { get; set; }
    }
}
