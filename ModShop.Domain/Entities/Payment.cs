using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Domain.Entities
{
    public class Payment : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public double Amount { get; set; }

        public string RRN { get; set; }
        public string TraceNumber { get; set; }

        public string Token { get; set; }

        public DateTime? PayTime { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentGatway Gatway { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public override bool Search(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public enum PaymentStatus
    {
        [Display(Name = "انجام نشده")]
        NotHappend = 0,
        [Display(Name = "موفق")]
        Success = 1,
        [Display(Name = "ناموفق")]
        Failed = 2
    }

    public enum PaymentGatway
    {
        [Display(Name = "زرین پال")]
        ZarrinPal = 0,
        [Display(Name = "آسان پرداخت")]
        AsanPardakht = 1,
        [Display(Name = "پاسارگاد")]
        Pasargad = 2,
        [Display(Name = "ملت")]
        Mellat = 3
    }

}
