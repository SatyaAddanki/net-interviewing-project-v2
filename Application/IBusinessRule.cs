using Domain.V1;

namespace Application
{
    public interface IBusinessRule
    {
        InsuranceDto GetProductType(int productID);

        InsuranceDto GetSalesPrice(int productID);

        InsuranceDto GetInsurance(InsuranceDto insuranceDto);
    }
}
