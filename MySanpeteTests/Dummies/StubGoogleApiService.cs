﻿using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.Dummies;

internal class StubGoogleApiService : IGoogleApiService
{
    public Task<string?> GetAddressOfLatLon(decimal? lat, decimal? lon)
    {
        throw new NotImplementedException();
    }

    public async Task<Coords?> GetCoordsOfAddress(string address)
    {
        await Task.CompletedTask;
        return new Coords() { X = 0, Y = 0 };
    }
}
