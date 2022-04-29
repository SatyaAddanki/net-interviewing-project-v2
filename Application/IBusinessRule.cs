using Domain;
using System.Collections.Generic;

namespace Application
{
    /// <summary>
    /// Interface that provide insurance details
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// provide product type for the given product
        /// </summary>
        /// <param name="productID"></param>
        Insurance GetProductType(int productID);

        /// <summary>
        /// provide sales price for the given product
        /// </summary>
        /// <param name="productID"></param>
        Insurance GetSalesPrice(int productID);

        /// <summary>
        /// Provide insurance for the product
        /// </summary>
        /// <param name="insurance"></param>
        /// <param name="firstCamera"></param>
        Insurance GetInsurance(Insurance insurance, bool firstCamera=true);

        /// <summary>
        /// Provide total insurance for products in the given Order
        /// </summary>
        /// <param name="products"></param>  
        Insurance GetInsurance(List<int> products);
    }
}
