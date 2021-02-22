using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace McShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerInterface _customerInterface;

        private static bool flag = false; // to check xml against xsd
        private static string errorMessage = ""; //error messages for failed validation


        public CustomerController(CustomerInterface customerInterface)
        {
            _customerInterface = customerInterface;

        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomer()
        {
            var cus = _customerInterface.GetCustomer();

            if (cus == null)
            {
                return NotFound("Customer does not exist!");
            }
            else
                return Ok(cus);
        }

        // GET: api/Customer/GetName
        [HttpGet("GetName")]
        public IActionResult GetCustomerByName(string name)
        {
            var cus = _customerInterface.GetCustomerByName(name);

            if (cus == null)
            {
                return NotFound("Customer not found");
            }
            else
            return Ok(cus);
        }

        //PUT :api/Customer/ChangeCustomerName
        [HttpPut("ChangeCustomerName")]
        public IActionResult UpdateCustomerName(string id, string name)
        {
            try
            {
                if (_customerInterface.CustomerExists(id))
                {
                    _customerInterface.UpdateCustomerName(id, name);
                    return Ok("Customer name updated successfully");
                }
                else
                    return NotFound("Customer does not exist!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }
        }

        //PUT :api/Customer/UpdateCustomerShares
        [HttpPut("UpdateCustomerShares")]
        public IActionResult UpdateCustomerShares(string id, int NumShares)
        {
            try
            {
                if (_customerInterface.CustomerExists(id))
                {
                    _customerInterface.UpdateCustomerShares(id, NumShares);
                    return Ok("Customer shares updated successfully");
                }
                else
                    return NotFound("Customer does not exist!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }

        }

       [HttpPost("UploadFiles")]
       public async Task<IActionResult> Post(List<IFormFile> files)
        {
            string message = "";

            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
    
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("", "xmlSchema.xsd");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(filePath, booksSettings);

            while (books.Read())
            {
                
            }

            books.Close();

            if(flag)
            {
                message = message + " failed validation";
                _customerInterface.StoreError(errorMessage);
                return Unauthorized(new { count = files.Count, size, filePath, message, errorMessage });
            }
            else
            {
                _customerInterface.RetrieveData(filePath);
                message = message + " good validation";
                return Ok(new { count = files.Count, size, filePath, message});
            }
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                flag = true;
                errorMessage = errorMessage + "WARNING: " + e.Message;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                flag = true;
                errorMessage = errorMessage + "ERROR: " + e.Message;
            }
        }
    }
}
