using Coter1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Coter1.Controllers
{
    public class PremiumController : Controller
    {
        private ApplicationDbContext _context;

        public PremiumController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Premium
        [System.Web.Http.HttpPost]
        public ActionResult CalculatePremium([FromBody]DTOPremiumInputs inputData)
        {
            DTOPremiumOutput premium = new DTOPremiumOutput();
            int BasePremium = inputData.Revenue / Common.Constants.RevenueDivisor;

            var stateFactor = _context.StateFactors.SingleOrDefault(s => s.State == inputData.State.ToUpper().Replace("\"", ""));
            if (stateFactor == null)
            {
                return Json(new { error = "State Not Found" }, JsonRequestBehavior.AllowGet);
            }

            var businessFactor = _context.BusinessFactors.SingleOrDefault(b => b.Business == inputData.Business.ToLower());
            if (businessFactor == null)
            {
                return Json(new { error = "Business Not Found" }, JsonRequestBehavior.AllowGet);
            }

            decimal tempPremium = stateFactor.Factor * businessFactor.Factor * Common.Constants.hazardFactor * BasePremium;

            premium.premium = (int)tempPremium;

            return Json(premium, JsonRequestBehavior.AllowGet);
        }
    }
}