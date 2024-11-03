using System.ComponentModel.DataAnnotations;

namespace Test_Site_1.Domain.Entities.Orders
{
    public enum OrderState
    {
        [Display(Name = "در حال پردازش ")]
        Processing = 0,
        [Display(Name = "پردازش لغو شده")]
        Canceled = 1,
        [Display(Name ="تحویل شد")]
        Delivered = 2
            
    }
}
