using Domain.V1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace WebApi.Test
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
                                        Id = 1,
                                        Name = "SomeProduct",
                                        ProductTypeId = 1,
                                        SalesPrice = 750
                                    };
                                    break;
                                case 2:
                                    product = new ProductDto
                                    {
                                        Id = 2,
                                        Name = "SomeLaptop",
                                        ProductTypeId = 21,
                                        SalesPrice = 450
                                    };
                                    break;
                                case 3:
                                    product = new ProductDto
                                    {
                                        Id = 3,
                                        Name = "SomeCamera",
                                        ProductTypeId = 33,
                                        SalesPrice = 600
                                    };
                                    break;
                                case 4:
                                    product = new ProductDto
                                    {
                                        Id = 4,
                                        Name = "SomeLaptop",
                                        ProductTypeId = 21,
                                        SalesPrice = 600
                                    };
                                    break;
                                case 5:
                                    product = new ProductDto
                                    {
                                        Id = 5,
                                        Name = "SomeProduct",
                                        ProductTypeId = 1,
                                        SalesPrice = 450
                                    };
                                    break;
                                case 6:
                                    product = new ProductDto
                                    {
                                        Id = 6,
                                        Name = "SomeCamera",
                                        ProductTypeId = 33,
                                        SalesPrice = 300
                                    };
                                    break;
                                case 7:
                                    product = new ProductDto
                                    {
                                        Id = 7,
                                        Name = "SomeProduct",
                                        ProductTypeId = 1,
                                        SalesPrice = 500
                                    };
                                    break;
                                case 8:
                                    product = new ProductDto
                                    {
                                        Id = 8,
                                        Name = "SomeProduct",
                                        ProductTypeId = 1,
                                        SalesPrice = 2000
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
                                                       Id = 1,
                                                       Name = "Test type",
                                                       CanBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       Id = 21,
                                                       Name = "Laptops",
                                                       CanBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       Id = 33,
                                                       Name = "Digital cameras",
                                                       CanBeInsured = true
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
