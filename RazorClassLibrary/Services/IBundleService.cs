using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IBundleService
{
    public Task<List<BundleDTO>> GetAllBundles();
    public Task<BundleDTO> GetBundle(int bundleId);
    public Task<BundleDTO> AddNewBundle(AddBundleRequest request);
    public Task<BundleDTO> UpdateBundle(BundleDTO bundle);
    public Task<bool> DeleteBundle(int bundleId);
    public Task<bool> PurchaseBundle(PurchaseBundleRequest request);
}
