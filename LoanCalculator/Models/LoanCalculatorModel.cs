using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class LoanCalculatorModel
    {
        [Required]
        public Nullable<double> LoanAmount { get; set; } 
        [Required]
        public Nullable<int> NumYears { get; set; }
        LoanInterestDBEntities db = new LoanInterestDBEntities();

        public decimal calculateInterest()
        {
            // Converting AmountInterestRates table to AsEnumerable as 
            // Linq is throwing an exception when converting decimal to double.
            decimal amountInterestRate = db.AmountInterestRates.AsEnumerable().Single(
                air => LoanAmount > Convert.ToDouble(air.MinAmount) && LoanAmount <= Convert.ToDouble(air.MaxAmount)
            ).InterestRate;

            decimal yearInterestRate = db.YearInterestRates.Single(
                yir => NumYears >yir.MinYears && NumYears <= yir.MaxYears
            ).InterestRate;

            return amountInterestRate + yearInterestRate;
        }
    }
}