﻿using RazorClassLibrary.DTOs;

namespace RazorClassLibrary.Requests;

public class AddBundleRequest
{
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<VoucherDTO>? Vouchers { get; set; }
}
