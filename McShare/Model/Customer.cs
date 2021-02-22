using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace McShare.Model
{
    public class Customer
    {
        [Column("Customer ID")]
        [Key]
        public string CustomerID { get; set; }

        [Column("Customer Name")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Column("Date of Birth")]
        public string DOB { get; set; }

        [Column("Incorp Date")]
        public string IncorpDate { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Contact Detail")]
        public double ContactDetail { get; set; }

        [Column("Registration Number")]
        public string RegistrationNum { get; set; }

        [Column("Customer Type")]
        [StringLength(50)]
        public string CustomerType { get; set; }

        [Column("Number of Shares")]
        public int NumShares { get; set; }

        [Column("Shares per unit price")]
        public double SharesPrice { get; set; }

        [Column("Balance")]
        public double Balance { get; set; }

    }
}
