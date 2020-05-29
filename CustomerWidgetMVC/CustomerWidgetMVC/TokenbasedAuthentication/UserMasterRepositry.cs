using CustomerWidgetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWidgetMVC.TokenbasedAuthentication
{
    public class UserMasterRepositry : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        CustomerWidgetEntities context = new CustomerWidgetEntities();

        //This method is used to check and validate the user credentials
        public Customer ValidateUser(string username, string password)
        {
            return context.Customers.FirstOrDefault(user =>
            user.Email.Equals(username)
            && user.Password == password);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}