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
            return RedirectToAction("Summary", new { LoanAmount = LoanAmount, NumYears = NumYears });
        }

        public ActionResult Summary(double LoanAmount, int NumYears)
        {
            ViewBag.LoanAmount = LoanAmount;
            ViewBag.NumYears = NumYears;
            return View(ViewBag);
        }
    }
}