using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class DonationController : Controller
    {
        // GET: Donation
        public ActionResult Index()
        {
            if(Session["PersonKey"]==null)
            {

                //Message m = new Message("You must be logged in to add a donation");
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonKey, DonationDate, DonationAmount, DoantionConfirmationCode")]Donation d)
        {
            d.DonationConfirmationCode = Guid.NewGuid();
            d.DonationDate = DateTime.Now;
            d.PersonKey = (int)Session["PersonKey"];
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            db.Donations.Add(d);
            db.SaveChanges();
            Message M = new Message("Donation has been entered");
            return View("Result", M);
        }

        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}