using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 ProductID { get; set; }
        public String Name { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }
        public Decimal Price { get; set; }
        public String Category { get; set; }
    }
}
