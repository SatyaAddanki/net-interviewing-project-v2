using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.V1
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }

        public double salesPrice { get; set; }

        public int productTypeId { get; set; }

    }
}
