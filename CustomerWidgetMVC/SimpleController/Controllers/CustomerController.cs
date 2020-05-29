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
using SimpleController.Filter;
using System.Net.Mail;
using System.Net;
using System.Net.Http.Formatting;
using CustomerWidgetMVC.TokenbasedAuthentication;
using System.Net.Http.Headers;
using System.Security.Claims;

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

        ///public ActionResult Register(FormCollection c, Customer customer)
            public ActionResult Register(FormCollection c, Customer customer)
        {
            customer.Password = c["passval"];
            customer.Email = c["Email"];
            
            if (ModelState.IsValid)
            {

                if (customer.Password != null && customer.Email != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:59699/api/LoginApi/PostCustomer/");
                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Customer>("Customer", customer);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var re = result.Content.ReadAsAsync<Customer>().Result;

                           
                            //encrypt
                            var plainTextBytes = Encoding.UTF8.GetBytes(customer.Email);
                            string encodedText = Convert.ToBase64String(plainTextBytes);
                            //add to cookie
                            HttpCookie cookie = new HttpCookie("UserInfo", encodedText);
                            cookie.Expires.AddHours(0.5);
                            Response.Cookies.Add(cookie);

                            //Session
                            Session["Email"] = customer.Email;
                            Session["CustomerId"] = re.CustomerId;
                            Session["FirstName"] = re.FirstName;

                            var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", customer.Email},
                   {"password",  customer.Password},
               };
                            var tokenResponse = client.PostAsync("http://localhost:59699/oauth/token", new FormUrlEncodedContent(form)).Result;
                            //var token = tokenResponse.Content.ReadAsStringAsync().Result;  
                            var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                            if (string.IsNullOrEmpty(token.Error))
                            {
                                Session["Token"] = token.AccessToken;
                              
                            }
                            else
                            {
                                Console.WriteLine("Error : {0}", token.Error);
                            }
                            // return true;
                            return RedirectToAction("CarList");
                        }
                        
                        else
                        {
                            ModelState.AddModelError("", "Please enter correct Email Id or Password");
                             return View("Register");
                           // return false;
                        }
                    }
                }
                else
                {
                   
                    return RedirectToAction("Register", "Customer");
                }
            }
           return View("Register");
        }

        [customeFilter]
        public ActionResult CarList(Customer customer)
        {
      return View();
        }
        [customeFilter]
        public ActionResult Package(ServicePackage service)
        {
            return View();
        }
        [customeFilter]
        public ActionResult Dealer()
        {
            return View();
        }
        [customeFilter]
        public ActionResult Calendar()
        {
            return View();
        }
        [customeFilter]
        public ActionResult Logout()
        {

            Session.Remove("Email");
            Session.Remove("Password");

            if (Request.Cookies["UserInfo"] != null)
            {
                var c = new HttpCookie("UserInfo");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }c
            // var a = Session["Email"];
            return RedirectToAction("Register");
        }
        [customeFilter]
        public ActionResult Summary()
        {
            return View();
        }
        [customeFilter]
        public ActionResult Confirm()
        {
            var time = Session["slotTime"].ToString();
            ViewBag.time = time.Substring(0,5);
            return View();
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection fc)
        {
             
            string message = "";
            
            string EmailID = fc["Email"];
            using (CustomerWidgetEntities dc = new CustomerWidgetEntities())
            {
                var account = dc.Customers.Where(a => a.Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    Session["resetcode"] = resetCode;
                    Session["v_email"] = account.Email;
                   
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor)
        {
            var verifyUrl = "/Customer/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("customerwidget@gmail.com", "Reset Password");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "customerwidget@123"; 

            string subject = "";
            string body = "";
            if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><a href=" + link + ">Reset Password link</a><br/><br/>Regards,<br/>Customer Widget";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


        //ResetPassword
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            var code = Session["resetcode"];
            string rcode = code.ToString();
            if (rcode == id)
            {
                ViewBag.Code = rcode;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
            
        }


        [HttpPost]
        public ActionResult ResetPassword(FormCollection fc)
        {
            var message = "";
           

            string code = fc["code"];
            string password = fc["password"];
            using (CustomerWidgetEntities dc = new CustomerWidgetEntities())
            {
                string email = Session["v_email"].ToString();
                string scode = Session["resetcode"].ToString();
                var user = dc.Customers.Where(a => a.Email == email).FirstOrDefault();
                if (user != null)
                {
                    
                    if (code == scode)
                    {
                        user.Password = password;
                        dc.SaveChanges();
                        Session.Remove("v_email");
                        Session.Remove("resetcode");
                        //message = "New password updated successfully";
                        TempData["msg"] = "Now, You can login with new password";
                        return RedirectToAction("Register");
                    }
                    else
                    {
                        message = "Something invalid";
                    }
                }
                else
                {
                    message = "Something invalid";
                }
               
                ViewBag.Message = message;
                return View();
            }
        }




        }
    }
