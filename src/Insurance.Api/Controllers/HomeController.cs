using Application;
using Domain.V1;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBusinessRule _businessRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessRule"></param>
        public HomeController(IBusinessRule businessRule)
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
            if (toInsure == null)
            {
                //TODO:Exception
            }
            return _businessRule.GetInsurance(toInsure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("api/insurance/order")]
        //public InsuranceDto CalculateInsurance([FromBody] OrderDto order)
        //{
        //    InsuranceDto insuranceDto = new InsuranceDto();
        //    foreach (var products in order.ProductId)
        //    {
        //        int productId = products;

        //        _businessRule.GetProductType(productId, ref insuranceDto);
        //        _businessRule.GetSalesPrice(productId, ref insuranceDto);

        //        bool SecondCamera = false;

        //        if (insuranceDto.SalesPrice < 500)
        //        {
        //            if (insuranceDto.ProductTypeName == "Laptops" || insuranceDto.ProductTypeName == "Smartphones" && insuranceDto.ProductTypeHasInsurance)
        //            {
        //                insuranceDto.InsuranceValue += 500;
        //            }
        //            if (insuranceDto.ProductTypeName == "Digital cameras" && !SecondCamera)
        //            {
        //                insuranceDto.InsuranceValue += 500;
        //                SecondCamera = true;
        //            }
        //            else
        //            {
        //                insuranceDto.InsuranceValue += 0;
        //            }
        //        }
        //        else
        //        {
        //            if (insuranceDto.SalesPrice > 500 && insuranceDto.SalesPrice < 2000)
        //                if (insuranceDto.ProductTypeHasInsurance)
        //                    insuranceDto.InsuranceValue += 1000;
        //            if (insuranceDto.SalesPrice >= 2000)
        //                if (insuranceDto.ProductTypeHasInsurance)
        //                    insuranceDto.InsuranceValue += 2000;
        //            if (insuranceDto.ProductTypeName == "Laptops" || insuranceDto.ProductTypeName == "Smartphones" && insuranceDto.ProductTypeHasInsurance)
        //                insuranceDto.InsuranceValue += 500;
        //            if (insuranceDto.ProductTypeName == "Digital cameras" && !SecondCamera)
        //            {
        //                insuranceDto.InsuranceValue += 500;
        //            }
        //        }
        //    }
        //    return insuranceDto;

        //}



    }
}