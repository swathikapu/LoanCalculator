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
        public double LoanAmount { get; set; } 
        [Required]
        public int NumYears { get; set; }
        LoanInterestDBEntities db = new LoanInterestDBEntities();

        public double calculateInterest()
        {
            // Converting AmountInterestRates table to AsEnumerable as 
            // Linq is throwing an exception when converting decimal to double.
            decimal amountInterestRate = db.AmountInterestRates.AsEnumerable().Single(
                air => LoanAmount > Convert.ToDouble(air.MinAmount) && LoanAmount <= Convert.ToDouble(air.MaxAmount)
            ).InterestRate;

            decimal yearInterestRate = db.YearInterestRates.Single(
                yir => NumYears >yir.MinYears && NumYears <= yir.MaxYears
            ).InterestRate;
            return Convert.ToDouble((decimal) (amountInterestRate + yearInterestRate));
        }


        public double getMonthlyPayment()
        {
            double numOfMonths = Convert.ToDouble(NumYears * 12);
            double monthlyInterestRate = calculateInterest() / 1200;
            double power = Math.Pow(1 + monthlyInterestRate, numOfMonths);
            double payment = Math.Round(LoanAmount * monthlyInterestRate/ (1 - 1/ power), 2);
            return payment;

        }

        public double getTotalPayment()
        {
            double totalPayment = Math.Round(getMonthlyPayment() * NumYears * 12, 2);
            return totalPayment;
        }
    }
}