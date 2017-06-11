﻿using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete.EntityFramework;
using StoreApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace StoreApp.Service.Controllers
{
    public class ProductsController : ApiController
    {
        IProductRepository _productRepository;

        public ProductsController()
        {
            //dependecy injection
            _productRepository = new EfProductRepository();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.Products;
        }

        public IEnumerable<Product> GetApprovedProducts()
        {
            return _productRepository.Products.Where(i => i.isApproved == true);
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _productRepository.Products.Where(i => i.Id == id).FirstOrDefault();

            if (product == null)
            {
                return (IHttpActionResult)BadRequest("Ürün bulunamadı.");
            }
            else
            {
                return Ok(product);
            }
        }

        //public Product GetProduct(int id)
        //{
        //    var product = _productRepository.Products.Where(i => i.Id == id).FirstOrDefault();

        //    if (product == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    else
        //    {
        //        return product;
        //    }
        //}

        public async Task PostProduct(Product entity)
        {
            await _productRepository.SaveProductAsync(entity);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
