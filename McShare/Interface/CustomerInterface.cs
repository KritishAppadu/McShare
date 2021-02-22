using McShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McShare
{
    public interface CustomerInterface
    {
        public List<Customer> GetCustomer(); //display list of all customers details in DB
        public List<Customer> GetCustomerByName(string name); //Lists customers based on name
        public string UpdateCustomerName(string id, string name); //changes customer name
        public string UpdateCustomerShares(string id, int NumShares); 
        public void RetrieveData(string filepath); //retrieve data from xml and store in DB
        public void StoreError(string errorMessage); // store xml against xsd error in database table
        public bool CustomerExists(string id);
    }
}
