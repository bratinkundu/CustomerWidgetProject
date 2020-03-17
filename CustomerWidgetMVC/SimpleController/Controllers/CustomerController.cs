using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SimpleController.Models;
using System.Web.Security;
using System.Text;
using Newtonsoft.Json;

namespace SimpleController.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection c, Customer customer)
        {
            customer.Password = c["passval"];
            customer.Email = c["Email"];
            //customer.CustomerId = int.Parse(c["CustomerId"]);
            //customer.FirstName = c["FirstName"];
            if (ModelState.IsValid)
            {

                if (customer.Password != null && customer.Email != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:59699/api/LoginAPI/PostCustomer");
                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Customer>("Customer", customer);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var re = result.Content.ReadAsAsync<Customer>().Result;

                            //var cookieText = Encoding.UTF8.GetBytes(customer.Email);
                            //var cookieEmail = Encoding.UTF8.GetBytes(customer.Email).ToString();
                            //var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText,cookieEmail, "ProtectCookie"));

                            //encrypt
                            var plainTextBytes = Encoding.UTF8.GetBytes(customer.Email);
                            string encodedText = Convert.ToBase64String(plainTextBytes);
                            //add to cookie
                            HttpCookie cookie = new HttpCookie("UserInfo", encodedText);
                            //cookie["Email"] = encodedText;
                            cookie.Expires.AddHours(0.5);
                            Response.Cookies.Add(cookie);

                            //Session
                            Session["Email"] = customer.Email;
                            Session["CustomerId"] = re.CustomerId;
                            Session["FirstName"] = re.FirstName;

                            return RedirectToAction("CarList");
                        }
                        //if email or password is incorrect
                        else
                        {
                            //TempData["Errormsg"] = "Please Enter Valid Username and Password";
                            ModelState.AddModelError("", "Please enter correct Email Id or Password");
                            return View("Register");
                        }
                    }
                }
                else
                {
                    //ModelState.AddModelError("", "Email Id or Password is invalid");
                    return RedirectToAction("Register", "Customer");
                }
            }
            return View("Register");
        }


        public ActionResult CarList(Customer customer)
        {
            return View();
        }
        public ActionResult Package(ServicePackage service)
        {
            return View();
        }
        public ActionResult Dealer()
        {
            return View();
        }
        public ActionResult Logout()
        {

            Session.Remove("Email");
            Session.Remove("Password");

            if (Request.Cookies["UserInfo"] != null)
            {
                var c = new HttpCookie("UserInfo");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            // var a = Session["Email"];
            return RedirectToAction("Register");
        }


    }
}