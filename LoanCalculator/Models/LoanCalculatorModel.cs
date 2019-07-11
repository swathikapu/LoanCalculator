using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LoanCalculator.common;

namespace LoanCalculator.Models
{
    public class LoanCalculatorModel
    {
        LoanInterestDBEntities db = new LoanInterestDBEntities();
        [Required(ErrorMessage="Please enter your first name")]
        [RegularExpression(@"^(([A-Za-z]+))$", ErrorMessage="Please enter alphabets only")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression(@"^(([A-Za-z]+))$", ErrorMessage="Please enter alphabets only")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter loan amount (£1-£100000 max)")]
        [Range(1, 100000, ErrorMessage = "Please enter loan amount (£1-£100000 max)")]
        public double LoanAmount { get; set; } 
        [Required(ErrorMessage = "Please enter number of years (1-20 years max)")]
        [Range(1, 20, ErrorMessage = "Please enter loan amount(£1 -£100000 max)")]
        public int NumYears { get; set; }
        private double? _InterestRateCache = null;
        private double? _MonthlyPaymentCache = null;
        public double InterestRate
        {
            get
            {
                // Make sure when the user enters values not in database
                // E.g: Negative values/ zero/ more than maximum values specified,
                // default LoanAmount and NumYears to 1.
                if (LoanAmount <= 0.0 || LoanAmount > 100000)
                    LoanAmount = 1.0;
                if (NumYears <= 0 || NumYears > 20)
                    NumYears = 1;
                if (_InterestRateCache != null)
                    return (double)_InterestRateCache;
                // Converting AmountInterestRates table to AsEnumerable as 
                // Linq is throwing an exception when converting decimal to double.
                decimal amountInterestRate = db.AmountInterestRates.AsEnumerable().Single(
                    air => LoanAmount > Convert.ToDouble(air.MinAmount) && LoanAmount <= Convert.ToDouble(air.MaxAmount)
                ).InterestRate;
                decimal yearInterestRate = db.YearInterestRates.Single(
                    yir => NumYears > yir.MinYears && NumYears <= yir.MaxYears
                ).InterestRate;
                _InterestRateCache = Convert.ToDouble((decimal)(amountInterestRate + yearInterestRate));
                return (double)_InterestRateCache;
            }
        }
        public double MonthlyPayment
        {
            get
            {
                if (_MonthlyPaymentCache != null)
                    return (double)_MonthlyPaymentCache;
                double numOfMonths = Convert.ToDouble(NumYears * 12);
                double monthlyInterestRate = InterestRate / 1200;
                double power = Math.Pow(1 + monthlyInterestRate, numOfMonths);
                _MonthlyPaymentCache = Math.Round(LoanAmount * monthlyInterestRate / (1 - 1 / power), 2);
                return (double)_MonthlyPaymentCache;
            }
        }
        public double TotalPayment
        {
            get
            {
                return Math.Round(MonthlyPayment * NumYears * 12, 2);
            }
        }
        public double Principal
        {
            get
            {
                return LoanAmount * 100 / TotalPayment;
            }
        }
        public double Interest
        {
            get
            {
                return 100 - Principal;
            }
        }
    }
}