﻿using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace MySanpeteWeb.Services;

public class WebUserService : IUserService
{
    private readonly string authstring;
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebUserService(IDbContextFactory<MySanpeteDbContext> dbContextFactory, IConfiguration config)
    {
        this.dbContextFactory = dbContextFactory;
        authstring = config["AuthString"] ?? "DefaultAuthString";
    }
    public async Task<UserDTO?> AddUser(string email)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        //Check if user doesn't exist
        var isExists = await context.EndUsers.AnyAsync(u => u.UserEmail == email);
        if (isExists)
        {
            return null;
        }

        //Make sure the email is valid
        if (!IsValid(email))
        {
            return null;
        }

        Guid guid = Guid.NewGuid();

        var newUser = new EndUser()
        {
            UserEmail = email,
            Guid = guid
        };

        await context.EndUsers.AddAsync(newUser);
        await context.SaveChangesAsync();
        return newUser.ToDto();

    }

    private static bool IsValid(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    public async Task<bool> DeleteUser(Guid guid)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        //Make sure user exists
        var uud = (await context.EndUsers.Where(u => u.Guid == guid).ToListAsync()).FirstOrDefault();

        if (uud is null)
        {
            return false;
        }

        context.EndUsers.Remove(uud);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var userList = await context.EndUsers.Select(u => u.ToDto()).ToListAsync();

        return userList;
    }

    public async Task<UserDTO?> GetUser(string email)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.EndUsers.Where(u => u.UserEmail == email).FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }

        return user.ToDto();
    }

    public async Task<UserDTO?> GetUser(Guid guid)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.EndUsers.Where(u => u.Guid == guid).FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }

        return user.ToDto();
    }

    public async Task<UserDTO?> PatchUser(UpdateUserRequest userRequest)
    {
        if (userRequest.Username == null)
        {
            throw new Exception("Username cannot be null");
        }

        if (!IsValid(userRequest.UserEmail!))
        {
            throw new Exception("Email was invalid");
        }

        var context = await dbContextFactory.CreateDbContextAsync();

        var databaseUser = await context.EndUsers.Where(u => u.Guid == userRequest.Guid).FirstOrDefaultAsync();

        if (databaseUser is null)
        {
            return null;
        }

        databaseUser.UserName = userRequest.Username;
        databaseUser.UserEmail = userRequest.UserEmail!;
        if (userRequest.Photo is not null)
        {
            databaseUser.Photo = userRequest.Photo;
        }

        context.Update(databaseUser);
        await context.SaveChangesAsync();

        return databaseUser.ToDto();
    }

    public async Task<bool> SetRole(SetRoleRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        if (request.AuthString != authstring)
        {
            throw new Exception("You're not allowed here");
        }


        var user = await context.EndUsers.Where(u => u.Guid == request.UserId).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new Exception("No user found with given GUID");
        }

        context.Update(user);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<UserDTO?> GetAuthUser(string authId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.EndUsers.Where(u => u.Authid == authId).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new Exception($"{authId} is not a user");
        }

        return user.ToDto();
    }

}
