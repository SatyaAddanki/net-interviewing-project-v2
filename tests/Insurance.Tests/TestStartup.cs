using Domain.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace WebAPI.Test
{
    public class TestStartup : IDisposable
    {
        private readonly IHost _host;
        public TestStartup()
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
                            switch (productId)
                            {
                                case 1:
                                    product = new ProductDto
                                    {
                                        id = 1,
                                        name = "SomeProduct",
                                        productTypeId = 1,
                                        salesPrice = 750
                                    };
                                    break;
                                case 2:
                                    product = new ProductDto
                                    {
                                        id = 2,
                                        name = "SomeLaptop",
                                        productTypeId = 21,
                                        salesPrice = 450
                                    };
                                    break;
                                case 3:
                                    product = new ProductDto
                                    {
                                        id = 3,
                                        name = "SomeCamera",
                                        productTypeId = 33,
                                        salesPrice = 600
                                    };
                                    break;
                                case 4:
                                    product = new ProductDto
                                    {
                                        id = 4,
                                        name = "SomeLaptop",
                                        productTypeId = 21,
                                        salesPrice = 600
                                    };
                                    break;
                                case 5:
                                    product = new ProductDto
                                    {
                                        id = 5,
                                        name = "SomeProduct",
                                        productTypeId = 1,
                                        salesPrice = 450
                                    };
                                    break;
                                case 6:
                                    product = new ProductDto
                                    {
                                        id = 6,
                                        name = "SomeCamera",
                                        productTypeId = 33,
                                        salesPrice = 300
                                    };
                                    break;
                                case 7:
                                    product = new ProductDto
                                    {
                                        id = 7,
                                        name = "SomeProduct",
                                        productTypeId = 1,
                                        salesPrice = 500
                                    };
                                    break;
                                case 8:
                                    product = new ProductDto
                                    {
                                        id = 8,
                                        name = "SomeProduct",
                                        productTypeId = 1,
                                        salesPrice = 2000
                                    };
                                    break;
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
}
