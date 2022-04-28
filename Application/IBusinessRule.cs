using Domain;
using Domain.V1;

namespace Application
{
    public interface IBusinessRule
    {
        Insurance GetProductType(int productID);

        Insurance GetSalesPrice(int productID);

        Insurance GetInsurance(Insurance insurance, bool secondCamera=false);
    }
}
