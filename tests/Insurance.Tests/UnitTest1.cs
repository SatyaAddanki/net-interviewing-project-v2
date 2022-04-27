using System;
using System.Collections.Generic;
using Insurance.Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 1,
            };
            var sut = new HomeController();

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

            var dto = new HomeController.InsuranceDto
            {
                ProductId = 2,
            };
            var sut = new HomeController();

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
            const int expectedInsuranceValue = 500+ 1000;
            List<int> ex = new List<int>() { 1, 2 };

            var dto = new HomeController.OrderDto
            {
                ProductId = new List<int>() { 1, 2 }
            };
            var sut = new HomeController();

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
           

            var dto = new HomeController.OrderDto
            {
                ProductId = new List<int>() { 3 }
            };
            var sut = new HomeController();

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
            const int expectedInsuranceValue = 500 + 1000;


            var dto = new HomeController.OrderDto
            {
                ProductId = new List<int>() { 3 }
            };
            var sut = new HomeController();

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
    }

    public class ControllerTestFixture : IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
                            ProductDto product = null;
                            if (productId == 2)
                            {
                                product = new ProductDto
                                {
                                    id = 2,
                                    name = "SomeLaptop",
                                    productTypeId = 21,
                                    salesPrice = 450
                                };
                            }
                            if (productId == 1)
                            {
                                product = new ProductDto
                                {
                                    id = 1,
                                    name = "SomeProduct",
                                    productTypeId = 1,
                                    salesPrice = 750
                                };
                            }
                            if (productId == 3)
                            {
                                product = new ProductDto
                                {
                                    id = 3,
                                    name = "SomeCamera",
                                    productTypeId = 33,
                                    salesPrice = 600
                                };
                            }

                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 21,
                                                       name = "Laptops",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 33,
                                                       name = "Digital cameras",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }

    }
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }

        public double salesPrice { get; set; }

        public int productTypeId { get; set; }

    }
}