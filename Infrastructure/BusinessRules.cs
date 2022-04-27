using Application;
using Domain.V1;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Infrastructure
{
    public class BusinessRules : IBusinessRule
    {
        private const string baseAddress = "http://localhost:5002";
        InsuranceDto insurance = new InsuranceDto();
        public InsuranceDto GetProductType(int productID)
        {
            HttpClient client = new HttpClient{ BaseAddress = new Uri(baseAddress) };
            string json = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
            var collection = JsonConvert.DeserializeObject<dynamic>(json);

            json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

           
                 
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].id == product.productTypeId && collection[i].canBeInsured == true)
                {
                    insurance.ProductTypeName = collection[i].name;
                    insurance.ProductTypeHasInsurance = true;
                }
            }
            return insurance;
        }

        public InsuranceDto GetSalesPrice(int productID)
        {

            HttpClient client = new HttpClient{ BaseAddress = new Uri(baseAddress)};
            string json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);          

            insurance.SalesPrice = product.salesPrice;
            return insurance;
        }

        public InsuranceDto GetInsurance(InsuranceDto insuranceDto)
        {
            this.insurance = GetProductType(insuranceDto.ProductId);
            this.insurance = GetSalesPrice(insuranceDto.ProductId);
            if (insurance.SalesPrice < 500)
            {
                if (insurance.ProductTypeName == "Laptops" || insurance.ProductTypeName == "Smartphones" && insurance.ProductTypeHasInsurance)
                {
                    insurance.InsuranceValue = 500;
                }
                else
                {
                    insurance.InsuranceValue = 0;
                }
            }
            else
            {
                if (insurance.SalesPrice > 500 && insurance.SalesPrice < 2000 && insurance.ProductTypeHasInsurance)
                    insurance.InsuranceValue += 1000;
                if (insurance.SalesPrice >= 2000 && insurance.ProductTypeHasInsurance)
                    insurance.InsuranceValue += 2000;
                if (insurance.ProductTypeName == "Laptops" || insurance.ProductTypeName == "Smartphones" && insurance.ProductTypeHasInsurance)
                    insurance.InsuranceValue += 500;
            }

            return insurance;
        }




    }
}