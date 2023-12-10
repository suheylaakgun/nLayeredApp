using AutoMapper;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Paging;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AutoMapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Create Product
            CreateMap<CreateProductRequest, Product>().ReverseMap();
            CreateMap<Product, CreatedProductResponse>().ReverseMap();

            //GetList Product
            CreateMap<Paginate<Product>, Paginate<GetListProductResponse>>().ReverseMap();
        }
    }
}
