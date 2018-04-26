using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class RegisterController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "LastName," + "FirstName," + "Email," + "Phone," + "PlainPassword," + "Apartment," + "Street," + "City," + "State," + "Zipcode")]NewPerson r)
        {
            Message m = new Message();
            int result = db.usp_Register(r.LastName, r.FirstName, r.Email, r.Phone, r.PlainPassword, r.Apartment, r.Street, r.City, r.State, r.Zipcode);
            if (result != -1)
            {
                m.MessageText = "Welcome, " + r.Email;
            }
            else
            {
                m.MessageText = "Something went horribly wrong";
            }

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View();
        }

    }
}