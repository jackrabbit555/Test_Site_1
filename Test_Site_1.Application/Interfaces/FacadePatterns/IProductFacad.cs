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
using Test_Site_1.Application.Services.Products.Queries.GetAllCategories;
using Test_Site_1.Application.Services.Products.Queries.GetProductForAdmin;
using Test_Site_1.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Test_Site_1.Application.Services.Products.Commands.RemoveProduct;
using Test_Site_1.Application.Services.Products.Queries.GetProductForSite;
using Test_Site_1.Application.Services.Products.Commands.EditProduct;
using Test_Site_1.Application.Services.Products.Queries.GetProductDeatilForSite;

namespace Test_Site_1.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        IAddNewCategoryService AddNewCategoryService { get; }
        IRemoveCategoryService RemoveCategoryService { get; }
        IAddNewProductService AddNewProductService { get; } 
        IGetAllCategoriesService GetAllCategoriesService { get; }

        IEditCategoryService EditCategoryService { get; }

        IGetCategoriesService  GetCategoriesService { get; }
        
        //Reciving the Products List For Admin
        IGetProductForAdminService GetProductForAdminService { get; } 

        IGetProductDetailForAdmin GetProductDetailForAdmin { get; }

        // Removing the Product
        IRemoveProductService RemoveProductService { get; }


        // Editing the Product
        IEditProductService EditProductService { get; }


        //Reciving the Products List For Site 

        IGetProductForSiteService GetProductForSiteService { get; }

        //Reciving The Product Details
        IGetProductDeatilForSiteService GetProductDeatilForSiteService { get; }
    }
}
