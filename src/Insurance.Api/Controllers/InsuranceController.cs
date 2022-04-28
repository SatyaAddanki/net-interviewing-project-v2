using Application;
using Domain;
using Domain.V1;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class InsuranceController : Controller
    {

        private readonly IBusinessRule _businessRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRule"></param>
        public InsuranceController(IBusinessRule businessRule)
        {
            _businessRule = businessRule;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toInsure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {           
                var insuranceResponse = _businessRule.GetInsurance(new Insurance()
                {
                    InsuranceValue = toInsure.InsuranceValue,
                    ProductId = toInsure.ProductId,
                    ProductTypeHasInsurance = toInsure.ProductTypeHasInsurance,
                    ProductTypeName = toInsure.ProductTypeName,
                    SalesPrice = toInsure.SalesPrice
                });

                return new InsuranceDto()
                {
                    InsuranceValue = insuranceResponse.InsuranceValue,
                    ProductId = insuranceResponse.ProductId,
                    ProductTypeHasInsurance = insuranceResponse.ProductTypeHasInsurance,
                    ProductTypeName = insuranceResponse.ProductTypeName,
                    SalesPrice = insuranceResponse.SalesPrice
                };          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/insurance/order")]
        public InsuranceDto CalculateInsurance([FromBody] OrderDto order)
        {
            Insurance insuranceResponse = new Insurance();

            bool firstCamera = false;
            foreach (var product in order.ProductId)
            {              
                insuranceResponse = _businessRule.GetProductType(product);
                insuranceResponse = _businessRule.GetSalesPrice(product);
                if (insuranceResponse.ProductTypeName == "Digital cameras" && !firstCamera)
                {
                    insuranceResponse = _businessRule.GetInsurance(insuranceResponse, true);
                    firstCamera = true;
                }
                else
                {
                    insuranceResponse = _businessRule.GetInsurance(insuranceResponse);
                }
                             
            }
            return new InsuranceDto()
                {
                    InsuranceValue = insuranceResponse.InsuranceValue,
                    ProductId = insuranceResponse.ProductId,
                    ProductTypeHasInsurance = insuranceResponse.ProductTypeHasInsurance,
                    ProductTypeName = insuranceResponse.ProductTypeName,
                    SalesPrice = insuranceResponse.SalesPrice
                };                        
            }           

        }

}