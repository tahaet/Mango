using Mango.Web.Models;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service.IService
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
           throw new NotImplementedException();
        }

        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }
    }
}
