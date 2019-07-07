using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class LoanCalculatorModel
    {
        public double LoanAmount { get; set; }
        public int NumYears { get; set; }
        LoanInterestDBEntities db = new LoanInterestDBEntities();

        public decimal calculateInterest()
        {
            decimal amountInterestRate = db.AmountInterestRates.AsEnumerable().Single(
                air => LoanAmount > Convert.ToDouble(air.MinAmount) && LoanAmount <= Convert.ToDouble(air.MaxAmount)
            ).InterestRate;

            decimal yearInterestRate = db.YearInterestRates.AsEnumerable().Single(
                yir => NumYears >yir.MinYears && NumYears <= yir.MaxYears
            ).InterestRate;

            return amountInterestRate + yearInterestRate;
        }
    }
}