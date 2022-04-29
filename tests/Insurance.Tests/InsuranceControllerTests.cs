using System.Collections.Generic;
using Application;
using Domain.V1;
using Infrastructure;
using WebApi.Controllers;
using WebAPI.Test;
using Xunit;

namespace WebAPI.Test
{
    public class InsuranceTests : IClassFixture<ControllerTestFixture>
    {

        private readonly IBusinessRule _businessRule;
        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
            _businessRule = new BusinessRules();
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;

            var dto = new InsuranceDto
            {
                ProductId = 1,
            };
            var sut = new InsuranceController(_businessRule);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500EurosForLaptop_ShouldAdd500EurosToInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500;

            var dto = new InsuranceDto
            {
                ProductId = 2,
            };
            var sut = new InsuranceController(_businessRule);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrder_ShouldProvideTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000;
            List<int> ex = new List<int>() { 1, 2 };

            var dto = new OrderDto
            {
                ProductIds = new List<int>() { 1, 2 }
            };
            var sut = new InsuranceController(_businessRule);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrderWithCamera_ShouldAdd500ToTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000;


            var dto = new OrderDto
            {
                ProductIds = new List<int>() { 3 }
            };
            var sut = new InsuranceController(_businessRule);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_GivenOrderWithTwoCamera_ShouldAdd500ToTotalInsuranceCost()
        {
            //Arrange
            const int expectedInsuranceValue = 500 + 1000 + 1000;


            var dto = new OrderDto
            {
                ProductIds = new List<int>() { 3, 3 }
            };
            var sut = new InsuranceController(_businessRule);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
    }



}