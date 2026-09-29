using cn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace cn.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(contactmd cm)
        {
            DataSet ds = cm.GetData();

            ViewBag.ContactData = ds.Tables[0];

            return View();
        }

        //[HttpPost]
        //public IActionResult InsertData(contactmd cm)
        //{
        //    cm.InsertData(
        //        cm.name,
        //        cm.email,
        //        cm.contactnumber,
        //        cm.address);

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public IActionResult Delete(int id, int userid)
        {
            contactmd cm = new contactmd();

            cm.Delete(id);

            return RedirectToAction("ViewContact", new { id = userid });
        }
        [HttpGet]
        public IActionResult Update(int id, int userid)
        {
            usermodel um = new usermodel();
            ViewBag.UserData = um.GetUserById(userid).Tables[0];
            ViewBag.UserId = userid;

            contactmd cm = new contactmd();

            DataSet ds = cm.GetSingleData(id);

            DataRow dr = ds.Tables[0].Rows[0];

            contactmd c = new contactmd();

            c.id = Convert.ToInt32(dr["id"]);
            c.userid = userid;
            c.name = dr["name"].ToString();
            c.email = dr["email"].ToString();
            c.contactnumber = dr["contactnumber"].ToString();
            c.address = dr["address"].ToString();

            return View(c);
        }

        [HttpPost]
        public IActionResult Update(contactmd cm)
        {
            cm.Update(
                cm.name,
                cm.email,
                cm.contactnumber,
                cm.address,
                cm.id);

            return RedirectToAction("ViewContact", new { id = cm.userid });
        }
        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(usermodel um)
        {
            um.InsertData(
                um.name,
                um.email,
                um.password,
                um.contactnumber,
                um.gender,
                um.city,
                um.address);

            return RedirectToAction("Login");
        }

        // LOGIN PAGE

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(usermodel um)
        {
            DataSet ds = um.Login(um.email, um.password);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);

                return RedirectToAction("Dashboard",
                    new { id = id });
            }

            ViewBag.msg = "Invalid Email Or Password";

            return View();
        }
        //dashboard
        [HttpGet]
        public IActionResult Dashboard(int id)
        {
            usermodel um = new usermodel();

            contactmd cm = new contactmd();

            DataSet ds = um.GetUserById(id);

            ViewBag.UserData = ds.Tables[0];

            ViewBag.UserId = id;

            ViewBag.TotalContact = cm.TotalContact(id);

            return View();
        }
        //manage account
        [HttpGet]
        public IActionResult ManageAccount(int id)
        {
            usermodel um = new usermodel();

            DataSet ds = um.GetUserById(id);
            ViewBag.UserData = ds.Tables[0];
            ViewBag.UserId = id;

            if (ds.Tables[0].Rows.Count == 0)
            {
                return RedirectToAction("Login");
            }

            DataRow dr = ds.Tables[0].Rows[0];

            usermodel user = new usermodel();

            user.id = Convert.ToInt32(dr["id"]);
            user.name = dr["name"].ToString();
            user.email = dr["email"].ToString();
            user.password = dr["password"].ToString();
            user.contactnumber = dr["contactnumber"].ToString();
            user.gender = dr["gender"].ToString();
            user.city = dr["city"].ToString();
            user.address = dr["address"].ToString();

            return View(user);
        }

        
        [HttpPost]
        public IActionResult UpdateAccount(usermodel um)
        {
            int result = um.UpdateData(
                um.name,
                um.email,
                um.password,
                um.contactnumber,
                um.gender,
                um.city,
                um.address,
                um.id);

            return RedirectToAction("Dashboard", new { id = um.id });
        }
        // add contact
        [HttpGet]
        public IActionResult AddContact(int id)
        {
            usermodel um = new usermodel();
            ViewBag.UserData = um.GetUserById(id).Tables[0];
            ViewBag.UserId = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddContact(contactmd cm, int userid)
        {
            cm.InsertData(
                userid,
                cm.name,
                cm.email,
                cm.contactnumber,
                cm.address);

            return RedirectToAction("ViewContact", new { id = userid });
        }

        // view contact
        [HttpGet]
        public IActionResult ViewContact(int id)
        {
            usermodel um = new usermodel();
            ViewBag.UserData = um.GetUserById(id).Tables[0];
            ViewBag.UserId = id;

            contactmd cm = new contactmd();

            DataSet ds = cm.GetContactByUserId(id);

            ViewBag.ContactData = ds.Tables[0];

            ViewBag.UserId = id;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
