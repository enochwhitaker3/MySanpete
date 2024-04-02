using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services
{
    public interface IUserVoucherService
    {
        public Task<List<UserVoucherDTO>> GetAllUserVouchers();
        public Task<List<UserVoucherDTO>> GetAllByUser(int userId);
        public Task<List<UserVoucherDTO>> GetAllByVoucher(int voucherId);
        public Task<List<UserVoucherDTO>> GetAllByBusiness(string businessEmail);
        public Task<UserVoucherDTO> GetById(int id);
        public Task<UserVoucherDTO> AddUserVoucher(AddUserVoucherRequest request);
        public Task<UserVoucherDTO> UpdateUserVoucher(UserVoucherDTO userVoucher);
        public Task<bool> DeleteUserVoucher(int id);
    }
}
