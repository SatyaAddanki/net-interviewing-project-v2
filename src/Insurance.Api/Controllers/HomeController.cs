using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {

            
            int productId = toInsure.ProductId;

            BusinessRules.GetProductType(ProductApi, productId, ref toInsure);
            BusinessRules.GetSalesPrice(ProductApi, productId, ref toInsure);

            float insurance = 0f;

            if (toInsure.SalesPrice < 500)
            {
                if (toInsure.ProductTypeName == "Laptops" || toInsure.ProductTypeName == "Smartphones" && toInsure.ProductTypeHasInsurance)
                {
                    toInsure.InsuranceValue = 500;
                }
                else
                {
                    toInsure.InsuranceValue = 0;
                }
            }
            else
            {
                if (toInsure.SalesPrice > 500 && toInsure.SalesPrice < 2000)
                    if (toInsure.ProductTypeHasInsurance)
                        toInsure.InsuranceValue += 1000;
                if (toInsure.SalesPrice >= 2000)
                    if (toInsure.ProductTypeHasInsurance)
                        toInsure.InsuranceValue += 2000;
                if (toInsure.ProductTypeName == "Laptops" || toInsure.ProductTypeName == "Smartphones" && toInsure.ProductTypeHasInsurance)
                    toInsure.InsuranceValue += 500;
            }

            return toInsure;
        }

        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] OrderDto order)
        {
            InsuranceDto insuranceDto = new InsuranceDto();
            foreach (var products in order.ProductId)
            {
                int productId = products;

                BusinessRules.GetProductType(ProductApi, productId, ref insuranceDto);
                BusinessRules.GetSalesPrice(ProductApi, productId, ref insuranceDto);

                float insurance = 0f;
                bool SecondCamera=false;

                if (insuranceDto.SalesPrice < 500)
                {
                    if (insuranceDto.ProductTypeName == "Laptops" || insuranceDto.ProductTypeName == "Smartphones" && insuranceDto.ProductTypeHasInsurance)
                    {
                        insuranceDto.InsuranceValue += 500;
                    }
                    if (insuranceDto.ProductTypeName == "Digital cameras" && !SecondCamera)
                    {
                        insuranceDto.InsuranceValue += 500;
                        SecondCamera = true;
                    }
                    else
                    {
                        insuranceDto.InsuranceValue += 0;
                    }
                }
                else
                {
                    if (insuranceDto.SalesPrice > 500 && insuranceDto.SalesPrice < 2000)
                        if (insuranceDto.ProductTypeHasInsurance)
                            insuranceDto.InsuranceValue += 1000;
                    if (insuranceDto.SalesPrice >= 2000)
                        if (insuranceDto.ProductTypeHasInsurance)
                            insuranceDto.InsuranceValue += 2000;
                    if (insuranceDto.ProductTypeName == "Laptops" || insuranceDto.ProductTypeName == "Smartphones" && insuranceDto.ProductTypeHasInsurance)
                        insuranceDto.InsuranceValue += 500;
                    if(insuranceDto.ProductTypeName == "Digital cameras" && !SecondCamera)
                    {
                        insuranceDto.InsuranceValue += 500;
                    }
                }
            }
            return insuranceDto;

        }

        public class InsuranceDto
        {
            public int ProductId { get; set; }
            public float InsuranceValue { get; set; }
            [JsonIgnore]
            public string ProductTypeName { get; set; }
            [JsonIgnore]
            public bool ProductTypeHasInsurance { get; set; }
            [JsonIgnore]
            public float SalesPrice { get; set; }
        }

        public class OrderDto
        {
            public List<int> ProductId { get; set; }

        }

        private const string ProductApi = "http://localhost:5002";
    }
}