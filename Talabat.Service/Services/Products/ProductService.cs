using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications;

namespace Talabat.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort,int?brandId,int? typeId,int? pageSize, int? pageIndex)
        {
            var spec = new ProductSpecifications(sort,brandId, typeId, pageSize, pageIndex);
            return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllAsyncSpec(spec));
        }
        public async Task<IEnumerable<CategoryBrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryBrandDto>>(brands);
        }

        public async Task<IEnumerable<CategoryBrandDto>> GetAllCategoriesAsync()
        {
            var category = await _unitOfWork.Repository<ProductCatgory, int>().GetAllAsync();
            var MappedCategory = _mapper.Map<IEnumerable<CategoryBrandDto>>(category);
            return MappedCategory;
        }


        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications( id);

            var product = await _unitOfWork.Repository<Product, int>().GetAsyncSpec(spec);
            var MappedProduct = _mapper.Map<ProductDto>(product);
            return MappedProduct;
        }
    }
}
