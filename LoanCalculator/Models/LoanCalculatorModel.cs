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
            decimal amountInterestRate = db.AmountInterestRates.Single(
                air => LoanAmount > decimal.ToDouble(air.MinAmount) && LoanAmount <= decimal.ToDouble(air.MaxAmount)
            ).InterestRate;

            decimal yearInterestRate = db.YearInterestRates.Single(
                yir => NumYears >yir.MinYears && NumYears <= yir.MaxYears
                ).InterestRate;

            return amountInterestRate + yearInterestRate;
        }
    }
}