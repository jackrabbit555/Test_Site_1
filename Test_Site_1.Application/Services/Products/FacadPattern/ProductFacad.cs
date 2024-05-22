using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Products.Commands.AddNewCategory;
using Test_Site_1.Application.Services.Products.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Services.Products.Commands.RemoveCategory;
using Test_Site_1.Application.Services.Products.Commands.EditCategory;
using Test_Site_1.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Hosting;

using Test_Site_1.Application.Services.Products.Queries.GetAllCategories;
using Microsoft.Extensions.Hosting;
using Test_Site_1.Application.Services.Products.Queries.GetProductForAdmin;
using Test_Site_1.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Test_Site_1.Application.Services.Products.Commands.RemoveProduct;

namespace Test_Site_1.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductFacad(IDataBaseContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IAddNewCategoryService _addNewCategory;
        public IAddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategoryService(_context);
            }
        }    
        
        
        private IGetCategoriesService  _getCategoriesService;
        public IGetCategoriesService  GetCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);
            }
        }



       private IRemoveCategoryService _removeCategory;
        public IRemoveCategoryService RemoveCategoryService 
        {
            get
            {
                return _removeCategory = _removeCategory ?? new RemoveCategoryService(_context);
            } 
        }


        private IEditCategoryService _editCategory;
        public IEditCategoryService EditCategoryService 
        {
            get 
            {
                return _editCategory = _editCategory ?? new EditCategoryService(_context);
            }
        }


        private IAddNewProductService _addNewProduct;
        public IAddNewProductService AddNewProductService 
        {
            get 
            { 
                return _addNewProduct = _addNewProduct?? new AddNewProductService(_context,_environment);
            }
        }

        private IGetAllCategoriesService _getAllCategories;
        public IGetAllCategoriesService GetAllCategoriesService 
        {
            get 
            { 
                return _getAllCategories = _getAllCategories ?? new GetAllCategoriesService(_context);
            }
        }

        private IGetProductForAdminService _getProductForAdminService;

        public IGetProductForAdminService GetProductForAdminService 
        {
            get 
            { 
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }
        private IGetProductDetailForAdmin _getProductDetailForAdmin;
        public IGetProductDetailForAdmin GetProductDetailForAdmin 
        { 
            get 
            {
                return _getProductDetailForAdmin = _getProductDetailForAdmin ?? new GetProductDetailForAdmin(_context);
            } 
        }


        private IRemoveProductService _removeProductService;
        public IRemoveProductService RemoveProductService 
        {
            get 
            { 
                return _removeProductService  = _removeProductService ?? new RemoveProductService(_context);
            }
        }

    }
}
