using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Carts;
using Test_Site_1.Domain.Entities.Products;


namespace Test_Site_1.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto AddToCart(long ProductId, Guid BrowserId);
        ResultDto RemoveFromCart(long ProductId, Guid BrowserId);
        ResultDto<CartDto> GetMyCart(Guid BrowserId , long? UserId);


        ResultDto Add(long CartItemId);
        ResultDto LowOff(long CartItemId);
    }


    public class CartService : ICartService
    {

        private readonly IDataBaseContext _context;


        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Add(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };
        }
        public ResultDto LowOff(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            if (cartItem.Count <= 1)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }

            cartItem.Count--;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };

        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId && p.Finished == false).FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,

                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }
            var product = _context.Products.Find(ProductId);
            var cartItem = _context.CartItems.Where(p => p.ProductId == ProductId && p.CartId == p.Id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    Cart = cart,
                    CartId = 1,
                    Price = product.Price,
                    Product = product
                };
                _context.CartItems.Add(newCartItem);
                _context.SaveChanges();
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول{product.Name} با موفقیت ثبت شد "
            };



        }

        public ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId)
        {
            var cart = _context.Carts.
                Include(x => x.cartItems)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.ProductImages)             
                .Where(x => x.BrowserId == x.BrowserId && x.Finished == false)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();


            if (UserId != null )
            {
                var user = _context.Users.Find(UserId);
                cart.User = user;
                _context.SaveChanges();
            }
            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    ProductCount = cart.cartItems.Count(),
                    SumAmount = cart.cartItems.Sum(p=>p.Price *p.Count),
                    CartItems = cart.cartItems.Select(x => new CartItemDto
                    {
                       
                        Count = x.Count,
                        Price = x.Price,
                        Product = x.Product.Name , 
                        Id = x.Id,
                        Image = x.Product.ProductImages.FirstOrDefault().Src?? "",
                        
                       
                    }).ToList(),
                },
                IsSuccess = true
                
            };
               
        }

       

        public ResultDto RemoveFromCart(long ProductId, Guid BrowserId)
        {
            var cartitem = _context.CartItems.Where(p => p.Cart.BrowserId == BrowserId).FirstOrDefault();
            if (cartitem != null)
            {
                cartitem.IsRemoved = true;
                cartitem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت حذف شد "
                };

            }
            else 
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد "
                };
            }
        }

       
    }
    public class CartDto
    {

        public long CartId { get; set; }
        public int ProductCount { get; set; }
        public int SumAmount {  get; set; }

        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public long Id { get; set; }    
        public string Product { get; set; }
        public string Image { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }

    }
}
