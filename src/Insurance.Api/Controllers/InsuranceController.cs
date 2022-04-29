using Application;
using Domain;
using Domain.V1;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// This class implements the Insurance controller.
    /// It contains methods to provide the Insurance details.
    /// </summary>
    public class InsuranceController : Controller
    {

        private readonly IBusinessRule _businessRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceController"/> class.
        /// </summary>
        /// <param name="businessRule"></param>
        public InsuranceController(IBusinessRule businessRule)
        {
            _businessRule = businessRule;
        }

        /// <summary>
        /// Provide the Insurance details
        /// </summary>
        /// <param name="insuranceDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto insuranceDto)
        {
            var insuranceResponse = _businessRule.GetInsurance(new Insurance()
            {
                InsuranceValue = insuranceDto.InsuranceValue,
                ProductId = insuranceDto.ProductId,
                ProductTypeHasInsurance = insuranceDto.ProductTypeHasInsurance,
                ProductTypeName = insuranceDto.ProductTypeName,
                SalesPrice = insuranceDto.SalesPrice
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
        /// Provide the Insurance details
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/insurance/order")]
        public InsuranceDto CalculateInsurance([FromBody] OrderDto orderDto)
        {
            var insuranceResponse = _businessRule.GetInsurance(orderDto.ProductIds);
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