using Domain.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace Insurance.Api.Test
{
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
}
