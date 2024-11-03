using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_Site.Utilities;
using Test_Site_1.Application.Services.Carts;
using Test_Site_1.Application.Services.Finances.Command.AddReuestPay;
using Test_Site_1.Application.Services.Finances.Queries.IGetRequestPayService;
using Test_Site_1.Application.Services.Orders.Command;
using ZarinPal.Class;

namespace Test_Site.Controllers
{

    [Authorize]
    public class PayController : Controller
    {

        



        private readonly IAddRequestpayService _addRequestpayService;
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger; 
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly IAddNewOrderService _addNewOrderService;
       


        public PayController(IAddRequestpayService addRequestpayService ,
                             ICartService cartService,CookiesManeger cookiesManeger,
                             IGetRequestPayService getRequestPayService,
                             IAddNewOrderService addNewOrderService 
                            )
        {

            _addRequestpayService = addRequestpayService;
            _cartService = cartService;
            _cookiesManeger = cookiesManeger;
            _getRequestPayService = getRequestPayService;
            _addNewOrderService = addNewOrderService;

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();


        }



      
        public async Task<IActionResult> Index()
        {
            long? userId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId);
            if (cart.Data.SumAmount > 0)
            {
                var requestPay = _addRequestpayService.Execute(cart.Data.SumAmount, userId.Value);
                //ارسال به درگاخ پرداخت

                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"https://localhost:44340/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

            }
            else 
            {
                return RedirectToAction("Index","Cart");
            }
            



        }


        public async  Task<IActionResult> Verify(Guid guid,string authority,string status) 
        {
            var requestPay = _getRequestPayService.Execute(guid);
            var verification = await _payment.Verification(new DtoVerification

            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);


            //var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{10000}\"}}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //VerificationPayResultDto verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content); 

            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);

            if (verification.Status == 100)
            {
                _addNewOrderService.Execute(new RequestAddNewOrderSericeDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    RequestPayId = requestPay.Data.Id
                });

                return RedirectToAction("Index", "Orders");


            }
            else 
            {
                return View();
            }
        }

        public class VerificationPayResultDto 
        {
            public int status {  get; set; }
            public long RefId {  get; set; }
        }
    }
}
