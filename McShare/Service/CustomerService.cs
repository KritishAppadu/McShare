using McShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace McShare.Service
{
    public class CustomerService:CustomerInterface
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext _customerContext)
        {
            _context = _customerContext;
        }

        public bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }

        public List<Customer> GetCustomer()
        {
            return _context.Customer.ToList();
        }

        public List<Customer> GetCustomerByName(string name)
        {
            name = name.ToLower();

            var result = GetCustomer();

            var customer = (from order in result
                                 where order.CustomerName.ToLower().Contains(name)
                                 select order).ToList();

            //var customer = result.Where(item => name.ToLower().Any(stringToCheck =>item.CustomerName.ToLower().Contains(stringToCheck))).ToList();

            return customer;
        }

        public string UpdateCustomerName(string id, string name)
        {
            var existingCustomer = _context.Customer.Where(x => x.CustomerID == id).FirstOrDefault<Customer>();

            if (existingCustomer != null)

            {
                existingCustomer.CustomerName = name;
                _context.SaveChanges();
                return existingCustomer.CustomerID;
            }
            else
                return "0";
        }

        public string UpdateCustomerShares(string id, int NumShares)
        {
            var existingCustomer = _context.Customer.Where(x => x.CustomerID == id).FirstOrDefault<Customer>();

            if (existingCustomer != null)

            {
                existingCustomer.NumShares = NumShares;
                existingCustomer.Balance = NumShares * existingCustomer.SharesPrice;
                _context.SaveChanges();
                return existingCustomer.CustomerID;
            }
            else
                return "0";
        }

        public void RetrieveData(string filepath) //Retrieve data from xml file and insert in DB
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);

            Customer customer = new Customer();

            XmlNodeList detailNodes = xml.SelectNodes("//DataItem_Customer");
            foreach (XmlNode detail in detailNodes)
            {
                string customer_id = detail.SelectSingleNode("customer_id").InnerText;
                string Customer_Type = detail.SelectSingleNode("Customer_Type").InnerText;
                string Date_Of_Birth = detail.SelectSingleNode("Date_Of_Birth").InnerText;
                string Date_Incorp = detail.SelectSingleNode("Date_Incorp").InnerText;
                string Registration_No = detail.SelectSingleNode("Registration_No").InnerText;
                string Mailing_Address = detail.SelectSingleNode("Mailing_Address/Address_Line1").InnerText + " " + detail.SelectSingleNode("Mailing_Address/Address_Line2").InnerText + " " + detail.SelectSingleNode("Mailing_Address/Town_City").InnerText + " " + detail.SelectSingleNode("Mailing_Address/Country").InnerText;
                string Name = detail.SelectSingleNode("Contact_Details/Contact_Name").InnerText;
                string Contact_DetailsString = detail.SelectSingleNode("Contact_Details/Contact_Number").InnerText;
                string Shares_DetailsNumString = detail.SelectSingleNode("Shares_Details/Num_Shares").InnerText;
                string Shares_DetailsPriceString = detail.SelectSingleNode("Shares_Details/Share_Price").InnerText;

                double Contact_Details;

                if (Contact_DetailsString == "")
                    Contact_Details = 0;
                else
                    Contact_Details = Math.Round(double.Parse(Contact_DetailsString, System.Globalization.CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero);

                int Shares_DetailsNum = Int32.Parse(Shares_DetailsNumString);
                double Shares_DetailsPrice = Math.Round(float.Parse(Shares_DetailsPriceString, System.Globalization.CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero);
                
                customer.CustomerID = customer_id;
                customer.CustomerName = Name;
                customer.CustomerType = Customer_Type;
                customer.DOB = Date_Of_Birth;
                customer.IncorpDate = Date_Incorp;
                customer.RegistrationNum = Registration_No;
                customer.Address = Mailing_Address;
                customer.ContactDetail = Contact_Details;
                customer.SharesPrice = Shares_DetailsPrice;
                customer.NumShares = Shares_DetailsNum;
                customer.Balance = Shares_DetailsNum * Shares_DetailsPrice;

                _context.Customer.Add(customer);
                _context.SaveChanges();
            }
        }

        public void StoreError(string errorMessage)
        {
            ErrorLog errorLog = new ErrorLog();

            errorLog.ErrorDesc = errorMessage;

            _context.ErrorLog.Add(errorLog);
            _context.SaveChanges();
        }
    }
}
