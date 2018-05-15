using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class GrantApplicationController : Controller
    {
        // GET: GrantApplication
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        public ActionResult Index()
        {
            if (Session["PersonKey"] == null)
            {

                Message m = new Message("You must be logged in to submit an Application");
                return RedirectToAction("Result", m);
            }
            ViewBag.Grants = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonKey, GrantApplicationDate, GrantApplicationStatusKey, GrantTypeKey, GrantApplicationReason, GrantApplicationRequestAmount")]GrantApplication d)
        {
            try
            {
                d.PersonKey = (int)Session["PersonKey"];
                d.GrantAppicationDate = DateTime.Now;
                d.GrantApplicationStatusKey = (int)1;

                db.GrantApplications.Add(d);
                db.SaveChanges();
                Message m = new Message();
                m.MessageText = "Thank you for your Application";
                return RedirectToAction("Result", m);
            }
            catch(Exception e)
            {
                Message m = new Message();
                m.MessageText = e.Message;
                return RedirectToAction("Result", m);
            }
           
        }

        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}