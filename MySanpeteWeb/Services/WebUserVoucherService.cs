using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using RazorClassLibrary.DTOs;
using System.Diagnostics;
using Stripe;

namespace MySanpeteWeb.Services
{
    public class WebUserVoucherService : IUserVoucherService
    {
        private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
        private IStripeService stripeService;
        private IVoucherService voucherService;

        public WebUserVoucherService(IDbContextFactory<MySanpeteDbContext> dbContextFactory, IStripeService stripeService, IVoucherService voucherService)
        {
            this.dbContextFactory = dbContextFactory;
            this.stripeService = stripeService;
            this.voucherService = voucherService;

        }

        public async Task<UserVoucherDTO> AddUserVoucher(AddUserVoucherRequest request)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var voucher = await voucherService.GetVoucher(request.voucherId);

            UserVoucher userVoucher = new UserVoucher()
            {
                PurchaseDate = DateTime.Now.ToUniversalTime(),
                Isused = false,
                VoucherId = request.voucherId,
                ChargeId = request.chargeId,
                FinalPrice = request.finalPrice,
                LastUpdated = DateTime.Now.ToUniversalTime(),
                TimesClaimed = 0,
                PromoCode = voucher.PromoCode,
                UserId = request.userId,
                TotalReclaimable = voucher.AmmountReclaimable
            };

            context.Add(userVoucher);
            await context.SaveChangesAsync();

            var result = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == userVoucher.Id);

            if (result is null)
            {
                throw new Exception("User Voucher was not found");
            }

            return result.ToDto();
        }

        public async Task<bool> DeleteUserVoucher(int id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var userVoucher = await context.UserVouchers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userVoucher is null)
            {
                return false;
            }

            context.UserVouchers.Remove(userVoucher);
            return true;
        }

        public async Task<List<UserVoucherDTO>> GetAllByBusiness(string businessEmail)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var allUserVouchers = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Where(x => x.Voucher.Business.Email == businessEmail)
                .Select(x => x.ToDto())
                .ToListAsync();

            return allUserVouchers;
        }

        public async Task<List<UserVoucherDTO>> GetAllByUser(int userId)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var allUserVouchers = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .Select(x => x.ToDto())
                .ToListAsync();

            return allUserVouchers;
        }

        public async Task<List<UserVoucherDTO>> GetAllByVoucher(int voucherId)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var allUserVouchers = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Where(x => x.VoucherId == voucherId)
                .Select(x => x.ToDto())
                .ToListAsync();

            return allUserVouchers;
        }

        public async Task<List<UserVoucherDTO>> GetAllUserVouchers()
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var allUserVouchers = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Select(x => x.ToDto())
                .ToListAsync();

            return allUserVouchers;
        }

        public async Task<UserVoucherDTO> GetById(int id)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var voucher = await context.UserVouchers
                .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .Select(x => x.ToDto())
                .FirstOrDefaultAsync();

            if (voucher is null)
            {
                throw new Exception("User Voucher not found");
            }

            return voucher;
        }

        public async Task<UserVoucherDTO> UpdateUserVoucher(UserVoucherDTO userVoucher)
        {
            var context = await dbContextFactory.CreateDbContextAsync();

            var vuc = await context.UserVouchers
                 .Include(x => x.Voucher)
                    .ThenInclude(x => x.Business)
                .Include(x => x.User)
                .Where(x => x.Id == userVoucher.Id)
                .FirstOrDefaultAsync();

            if (vuc is null)
            {
                throw new Exception("User Voucher not found");
            }

            vuc.Isused = userVoucher.Is_Used;
            vuc.LastUpdated = DateTime.Now.ToUniversalTime();
            vuc.TimesClaimed = userVoucher.Times_Claimed;

            context.UserVouchers.Update(vuc);
            await context.SaveChangesAsync();

            return vuc.ToDto();
        }
    }
}
