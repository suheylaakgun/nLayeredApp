using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.ProductName = createProductRequest.ProductName;
            product.UnitPrice = createProductRequest.UnitPrice;
            product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
            product.UnitsInStock = createProductRequest.UnitsInStock;

            Product createdProduct = await _productDal.AddAsync(product);

            CreatedProductResponse createdProductResponse = new CreatedProductResponse();
            createdProductResponse.Id = createdProduct.Id;
            createdProductResponse.ProductName = createdProduct.ProductName;
            createdProductResponse.UnitPrice = createdProduct.UnitPrice;
            createdProductResponse.QuantityPerUnit = createdProduct.QuantityPerUnit;
            createdProductResponse.UnitsInStock = createdProduct.UnitsInStock;

            return createdProductResponse;
        }

        #region Commented Poor Code (async Task<IPaginate<GetListProductResponse>>)
        //public async Task<IPaginate<GetListProductResponse>> GetListAsync()
        //{
        //    var productList = await _productDal.GetListAsync();
        //    List<GetListProductResponse> getListProductResponse = new List<GetListProductResponse>();

        //    foreach (var product in productList.Items)
        //    {
        //        getListProductResponse.Add(new GetListProductResponse
        //        {
        //            Id = product.Id,
        //            ProductName = product.ProductName,
        //            QuantityPerUnit = product.QuantityPerUnit,
        //            UnitPrice = product.UnitPrice,
        //            UnitsInStock = product.UnitsInStock
        //        });
        //    }

        //    Paginate<GetListProductResponse> listResponse = new Paginate<GetListProductResponse>();
        //    listResponse.Items = getListProductResponse;
        //    listResponse.Count = productList.Count;
        //    listResponse.Index = productList.Index;
        //    listResponse.Size = productList.Size;
        //    listResponse.From = productList.From;
        //    listResponse.Pages = productList.Pages;
        //    return listResponse;
        //} 
        #endregion

        public async Task<IPaginate<GetListProductResponse>> GetListAsync()
        {
            var productList = await _productDal.GetListAsync();
            var mappedList = _mapper.Map<Paginate<GetListProductResponse>>(productList);
            return mappedList;
        }
    }
}
