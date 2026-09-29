using Microsoft.AspNetCore.Mvc;
using cn.Models;
using System.Data;

namespace cn.Controllers
{
    public class ContactController : Controller
    {
        // Model object
        contactmd cm = new contactmd();

        // =========================
        // CONTACT LIST
        // =========================
        public IActionResult Index()
        {
            DataSet ds = cm.GetData();

            return View(ds);
        }

        // =========================
        // ADD CONTACT - GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // ADD CONTACT - POST
        // =========================
        [HttpPost]
        public IActionResult Create(
            string name,
            string email,
            string contactnumber,
            string address)
        {
            // Logged-in user ID
            int? userid = HttpContext.Session.GetInt32("userId");

            // If session is not available
            if (userid == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Insert contact
            cm.InsertData(
                userid.Value,
                name,
                email,
                contactnumber,
                address
            );

            return RedirectToAction("Index");
        }

        // =========================
        // MY CONTACTS
        // =========================
        public IActionResult MyContacts()
        {
            int? userid = HttpContext.Session.GetInt32("userId");

            if (userid == null)
            {
                return RedirectToAction("Login", "User");
            }

            DataSet ds = cm.GetContactByUserId(userid.Value);

            return View(ds);
        }

        // =========================
        // EDIT - GET
        // =========================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            DataSet ds = cm.GetSingleData(id);

            return View(ds);
        }

        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        public IActionResult Edit(
            int id,
            string name,
            string email,
            string contactnumber,
            string address)
        {
            cm.Update(
                name,
                email,
                contactnumber,
                address,
                id
            );

            return RedirectToAction("Index");
        }

        // =========================
        // DELETE
        // =========================
        [HttpGet]
        public IActionResult Delete(int id)
        {
            cm.Delete(id);

            return RedirectToAction("Index");
        }

        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(int id)
        {
            DataSet ds = cm.GetSingleData(id);

            return View(ds);
        }
    }
}