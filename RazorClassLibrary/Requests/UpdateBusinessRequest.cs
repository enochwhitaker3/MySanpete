﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Requests;

public class UpdateBusinessRequest
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }   
    public string? Address { get; set; }    
    public byte[]? Logo { get; set; }
}