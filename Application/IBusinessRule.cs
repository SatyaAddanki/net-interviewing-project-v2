using Domain;
using Domain.V1;
using System.Collections.Generic;

namespace Application
{
    public interface IBusinessRule
    {
        Insurance GetProductType(int productID);

        Insurance GetSalesPrice(int productID);

        Insurance GetInsurance(Insurance insurance, bool firstCamera=true);

        Insurance GetInsurance(List<int> products);
    }
}
