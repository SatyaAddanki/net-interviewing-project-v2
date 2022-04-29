using System.Collections.Generic;
using Application;
using Domain.V1;
using Infrastructure;
using WebApi.Controllers;
using Xunit;

namespace WebAPI.Test
{
    public class InsuranceTests : IClassFixture<TestStartup>
    {
       private readonly IBusinessRule _businessRule;
      
        public InsuranceTests()
        {            
            _businessRule = new BusinessRules();
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500EurosForLaptop_ShouldAdd500EurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 2,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500Euros_ShouldAddZeroEurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 0;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 5,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500EurosForCamera_ShouldAdd500EurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 6,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 1,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPrice500_ShouldAddThousandEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 7,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPrice2000_ShouldAddTwoThousandEurosToInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 2000;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 8,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceMoreThan500EurosForLaptop_ShouldAddExtra500EurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500+1000;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 4,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceMoreThan500EurosForCamera_ShouldAddExtra500EurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000;
            var insuranceDto = new InsuranceDto
            {
                ProductId = 3,
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(insuranceDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrderWithProducts_ShouldProvideTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 0 + 1000;     
            var orderDto = new OrderDto
            {
                ProductIds = new List<int>() { 1, 5 }
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(orderDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrderWithOneCamera_ShouldAdd500ToTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000;
            var orderDto = new OrderDto
            {
                ProductIds = new List<int>() { 3 }
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(orderDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrderWithTwoCamera_ShouldAdd500ToTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000 + 0;
            var orderDto = new OrderDto
            {
                ProductIds = new List<int>() { 3, 6 }
            };
            var insuranceController = new InsuranceController(_businessRule);

            //Act
            var result = insuranceController.CalculateInsurance(orderDto);

            //Assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
    }



}