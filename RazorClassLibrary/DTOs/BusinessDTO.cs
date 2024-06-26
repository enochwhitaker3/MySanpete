﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.DTOs;

public class BusinessDTO
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    public string? LogoURL { get; set; }
    public string? PhoneNumber { get; set; }
    public string? WebsiteURL { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public double? XCoordinate { get; set; }
    public double? YCoordinate { get; set; }

    public IEnumerable<VoucherDTO>? Vouchers { get; set; }
}
