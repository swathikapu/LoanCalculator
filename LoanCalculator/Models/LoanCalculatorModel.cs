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
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public double LoanAmount { get; set; } 
        [Required]
        public int NumYears { get; set; }
        private double? _InterestRate = null;
        private double? _MonthlyPayment = null;
        LoanInterestDBEntities db = new LoanInterestDBEntities();

        public double calculateInterest()
        {
            if (_InterestRate != null)
                return (double) _InterestRate;
            // Converting AmountInterestRates table to AsEnumerable as 
            // Linq is throwing an exception when converting decimal to double.
            decimal amountInterestRate = db.AmountInterestRates.AsEnumerable().Single(
                air => LoanAmount > Convert.ToDouble(air.MinAmount) && LoanAmount <= Convert.ToDouble(air.MaxAmount)
            ).InterestRate;

            decimal yearInterestRate = db.YearInterestRates.Single(
                yir => NumYears >yir.MinYears && NumYears <= yir.MaxYears
            ).InterestRate;
            _InterestRate = Convert.ToDouble((decimal)(amountInterestRate + yearInterestRate));
            return (double) _InterestRate;
        }

        public double getMonthlyPayment()
        {
            if (_MonthlyPayment != null)
                return (double)_MonthlyPayment;
            double numOfMonths = Convert.ToDouble(NumYears * 12);
            double monthlyInterestRate = calculateInterest() / 1200;
            double power = Math.Pow(1 + monthlyInterestRate, numOfMonths);
            _MonthlyPayment = Math.Round(LoanAmount * monthlyInterestRate/ (1 - 1/ power), 2);
            return (double) _MonthlyPayment;
        }

        public double getTotalPayment()
        {
            double totalPayment = Math.Round(getMonthlyPayment() * NumYears * 12, 2);
            return totalPayment;
        }
    }
}