using Application;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Infrastructure
{
    /// <summary>
    /// This class implements the IBusinessRule.
    /// It contains methods to get the insurance details.
    /// </summary>
    public class BusinessRule : IBusinessRule
    {
        private const string baseAddress = "http://localhost:5002";
        private const string laptop = "Laptops";
        private const string smartPhone = "Smartphones";
        private const string digitalCamera = "Digital cameras";
        private Insurance _insurance;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRule"/> class.
        /// </summary>
        public BusinessRule()
        {
            _insurance = new Insurance();
        }

        ///<inheritdoc/> 
        public Insurance GetProductType(int productID)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            string json = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
            var collection = JsonConvert.DeserializeObject<dynamic>(json);

            json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Id == product.ProductTypeId && collection[i].CanBeInsured == true)
                {
                    _insurance.ProductTypeName = collection[i].Name;
                    _insurance.ProductTypeHasInsurance = true;
                }
            }
            return this._insurance;
        }

        ///<inheritdoc/> 
        public Insurance GetSalesPrice(int productID)
        {

            HttpClient client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            string json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            _insurance.SalesPrice = product.SalesPrice;
            return this._insurance;
        }

        ///<inheritdoc/> 
        public Insurance GetInsurance(Insurance insurance, bool firstCamera = true)
        {
            if (insurance.ProductTypeName == null && insurance.SalesPrice == 0)
            {
                _insurance = GetProductType(insurance.ProductId);
                _insurance = GetSalesPrice(insurance.ProductId);
            }

            if (_insurance.SalesPrice < 500)
            {
                if (_insurance.ProductTypeName == laptop || _insurance.ProductTypeName == smartPhone && _insurance.ProductTypeHasInsurance)
                {
                    _insurance.InsuranceValue += 500;
                }
                else if (_insurance.ProductTypeName == digitalCamera && _insurance.ProductTypeHasInsurance && firstCamera)
                {
                    _insurance.InsuranceValue += 500;
                }
                else
                {
                    _insurance.InsuranceValue += 0;
                }
            }
            else
            {
                if (_insurance.SalesPrice >= 500 && _insurance.SalesPrice < 2000 && _insurance.ProductTypeHasInsurance)
                {
                    _insurance.InsuranceValue += 1000;
                }
                else if (_insurance.SalesPrice >= 2000 && _insurance.ProductTypeHasInsurance)
                {
                    _insurance.InsuranceValue += 2000;
                }

                if (_insurance.ProductTypeName == laptop || _insurance.ProductTypeName == smartPhone && _insurance.ProductTypeHasInsurance)
                {
                    _insurance.InsuranceValue += 500;
                }
                else if (_insurance.ProductTypeName == digitalCamera && _insurance.ProductTypeHasInsurance && firstCamera)
                {
                    _insurance.InsuranceValue += 500;
                }
            }
            return this._insurance;
        }

        ///<inheritdoc/>      
        public Insurance GetInsurance(List<int> products)
        {
            bool firstCamera = true;
            foreach (var product in products)
            {
                _insurance = GetProductType(product);
                _insurance = GetSalesPrice(product);
                if (_insurance.ProductTypeName == digitalCamera && firstCamera)
                {
                    _insurance = GetInsurance(_insurance, true);
                    firstCamera = false;
                }
                else
                {
                    _insurance = GetInsurance(_insurance, firstCamera);
                }
            }
            return this._insurance;
        }
    }
}