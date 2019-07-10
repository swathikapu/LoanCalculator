using System;
using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.common
{
    public class IsValid : ValidationAttribute
    {
        public IsValid() : base( "Please enter string")
        {

        }
    }
}