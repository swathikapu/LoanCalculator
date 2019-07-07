using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanCalculator.Models;

namespace LoanCalculator.Controllers
{
    public class InputController : Controller
    {
        // GET: Input
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(double LoanAmount, int NumYears)
        {
            LoanCalculatorModel loanCalculator = new LoanCalculatorModel();
            loanCalculator.LoanAmount = LoanAmount;
            loanCalculator.NumYears = NumYears;

            decimal InterestRate = loanCalculator.calculateInterest();
            return RedirectToAction("Summary", new { LoanAmount = LoanAmount, NumYears = NumYears, InterestRate = InterestRate});
        }

        public ActionResult Summary(double LoanAmount, int NumYears, decimal InterestRate )
        {
            ViewBag.LoanAmount = LoanAmount;
            ViewBag.NumYears = NumYears;
            ViewBag.InterestRate = InterestRate;

            return View(ViewBag);
        }
    }
}