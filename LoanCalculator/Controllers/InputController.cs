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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoanCalculatorModel loanCalculator)
        {
            TryUpdateModel(loanCalculator);
            if (ModelState.IsValid)
            {
                return RedirectToAction(
                    "Summary",
                    new
                    {
                        LoanAmount = loanCalculator.LoanAmount,
                        NumYears = loanCalculator.NumYears,
                    });
            }
            return View();
        }

        public ActionResult Summary(double LoanAmount, int NumYears)
        {
            LoanCalculatorModel loanCalculator = new LoanCalculatorModel
            {
                LoanAmount = LoanAmount,
                NumYears = NumYears,
            };

            return View(loanCalculator);
        }
    }
}