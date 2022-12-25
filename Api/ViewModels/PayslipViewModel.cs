using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class PayslipViewModel
    {
        public int Pin { get; set; }
        public int CardNo { get; set; }
        public Int64 Tin { get; set; }
        public string SalaryIssueDate { get; set; }
        public double BasicSalary { get; set; }
        public double TotalWorkDays { get; set; }
        public double HousingSalary { get; set; }
        public double MedicalAllowance { get; set; }
        public double TaxDeduction { get; set; }
        public double Total { get; set; }
    }
}
