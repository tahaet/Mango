﻿namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {

        public int Id { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAMount { get; set; }
        public double MinAmount { get; set; }
    }
}
