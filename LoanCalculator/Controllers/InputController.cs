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
                        FirstName = loanCalculator.FirstName,
                        LastName = loanCalculator.LastName,
                        LoanAmount = loanCalculator.LoanAmount,
                        NumYears = loanCalculator.NumYears,
                    });
            }
            return View();
        }

        public ActionResult Summary(string FirstName,string LastName , double LoanAmount, int NumYears)
        {
            LoanCalculatorModel loanCalculator = new LoanCalculatorModel
            {
                FirstName = FirstName,
                LastName = LastName,
                LoanAmount = LoanAmount,
                NumYears = NumYears,
            };

            return View(loanCalculator);
        }
        [HttpPost]
        public ActionResult Summary(LoanCalculatorModel loanCalculator)
        {
            

            return RedirectToAction("Approved",
                new
                {
                    FirstName = loanCalculator.FirstName,
                    LastName = loanCalculator.LastName,
                }
                );
        }

        public ActionResult Approved(string FirstName, string LastName)
        {
            LoanCalculatorModel loanCalculator = new LoanCalculatorModel
            {
                FirstName = FirstName,
                LastName = LastName,
               
            };

            return View(loanCalculator);
        }
    }
}