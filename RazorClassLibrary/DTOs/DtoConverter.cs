﻿using RazorClassLibrary.Data;
using RazorClassLibrary.Pages;
using System.Reflection.Metadata;

namespace RazorClassLibrary.DTOs;

public static class DtoConverter
{
    public static string websiteUrl { get; set; } = "https://mysanpete.azurewebsites.net";
    public static UserDTO ToDto(this EndUser user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Guid = user.Guid,
            Username = user.UserName,
            UserEmail = user.UserEmail,
            PhotoURL = $"{websiteUrl}/api/image/user/{user.Id}", // $"https://localhost:7059/api/image/user/{user.Id}",
        };
    }

    public static VoucherDTO ToDto(this Voucher voucher)
    {
        int? stock = 0;
        if (voucher.UserVouchers is not null && voucher.PromoStock is not null)
        {
            stock = voucher.PromoStock - voucher.UserVouchers.Count();
        }
        return new VoucherDTO()
        {
            Id = voucher.Id,
            BusinessName = voucher.Business.BusinessName,
            BusinessLogoURL = $"{websiteUrl}/api/image/business/{voucher.Business.Id}",
            StartDate = voucher.StartDate,
            EndDate = voucher.EndDate,
            PromoCode = voucher.PromoCode,
            PromoName = voucher.PromoName,
            PromoDescription = voucher.PromoDescription,
            LeftInStock = stock,
            RetailPrice = voucher.RetailPrice,
            AmmountReclaimable = voucher.TotalReclaimable,
            Stock = voucher.PromoStock,
            StripeId = voucher.StripeId,
            PriceId = voucher.PriceId,
            IsActive = voucher.IsActive,
            IsBundle = voucher.IsBundle,
        };
    }

    public static BlogDTO ToDto(this Blog blog)
    {
        return new BlogDTO()
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.BlogContent,
            AuthorName = blog.Author.UserName,
            PublishDate = blog.PublishDate,
            Commentable = blog.Commentable,
            PhotoURL = $"{websiteUrl}/api/image/blogs/{blog.Id}",
            Comments = blog.BlogComments.Select(x => x.Comment.ToDto()).ToList(),
            Reactions = blog.BlogReactions.Select(x => x.Reaction).ToList()
        };
    }

    public static CommentDTO ToDto(this UserComment comment)
    {
        return new CommentDTO()
        {
            Id = comment.Id,
            ReplyId = comment.ReplyId,
            Content = comment.CommentText,
            Replies = comment.InverseReply.Where(x => x.ReplyId == comment.Id).Select(x => x.ToDto()).ToList(),
            UserName = comment.User.UserName,
            UserPhotoURL = $"{websiteUrl}/api/image/user/{comment.User.Id}",
            PostedDate = comment.PostDate,
        };
    }

    public static PodcastDTO ToDto(this Podcast podcast)
    {
        return new PodcastDTO()
        {
            Id = podcast.Id,
            Name = podcast.PodcastName,
            URL = podcast.PodcastUrl,
            Comments = podcast.PodcastComments.Select(x => x.Comment.ToDto()).ToList(),
            Reactions = podcast.PodcastReactions.Select(x => x.Reaction).ToList(),
            PublishDate = podcast.PublishDate,
        };
    }

    public static BundleDTO ToDto(this Bundle bundle)
    {
        return new BundleDTO()
        {
            Name = bundle.BundleName,
            Id = bundle.Id,
            EndDate = bundle.EndDate,
            StartDate = bundle.StartDate,
            FinalPrice = bundle.BundleVouchers.Sum(x => x.DiscountPrice),
            Vouchers = bundle.BundleVouchers.Select(x => x.Voucher!.ToDto()).ToList(),
            StripeId = bundle.StripeId,
            PriceId = bundle.PriceId
        };
    }

    public static UserVoucherDTO ToDto(this UserVoucher userVoucher)
    {
        return new UserVoucherDTO()
        {
            Final_Price = userVoucher.FinalPrice,
            Purchase_Date = userVoucher.PurchaseDate,
            Charge_Id = userVoucher.ChargeId,
            Is_Used = userVoucher.Isused,
            Last_Updated = userVoucher.LastUpdated,
            Promo_Code = userVoucher.PromoCode,
            Times_Claimed = userVoucher.TimesClaimed,
            Total_Reclaimable = userVoucher.TotalReclaimable,
            Voucher = userVoucher.Voucher.ToDto(),
            User = userVoucher.User.ToDto(),
            Id = userVoucher.Id
        };
    }

    public static CommentDTO ToDto(this BlogComment comment)
    {
        return new CommentDTO()
        {
            Id = comment.Id,
            Content = comment.Comment.CommentText
        };
    }

    public static CommentDTO ToDto(this PodcastComment comment)
    {
        return new CommentDTO()
        {
            Id = comment.Id,
            Content = comment.Comment.CommentText
        };
    }

    public static BusinessDTO ToDto(this Business business)
    {
        var vouchers = business.Vouchers.Select(x => x.ToDto());

        return new BusinessDTO()
        {
            Id = business.Id,
            Address = business.Address,
            BusinessName = business.BusinessName,
            Email = business.Email,
            LogoURL = $"{websiteUrl}/api/image/business/{business.Id}",
            PhoneNumber = business.PhoneNumber,
            WebsiteURL = business.Website,
            Vouchers = vouchers,
            Description = business.Description,
            XCoordinate = business.XCoordinate,
            YCoordinate = business.YCoordinate,
        };
    }

    public static OccasionDTO ToDto(this Occasion occasion)
    {
        return new OccasionDTO()
        {
            Id = occasion.Id,
            Title = occasion.Title,
            XCoordinate = occasion.XCoordinate,
            YCoordinate = occasion.YCoordinate,
            StartDate = occasion.StartDate,
            EndDate = occasion.EndDate,
            BusinessId = occasion.BusinessId,
            Description = occasion.Description,
            Business = occasion.Business.ToDto(),
            PhotoURL = $"{websiteUrl}/api/image/occasion/{occasion.Id}"
        };

    }

    public static UserOccasionDTO ToDto(this UserOccasion userOccasion)
    {
        return new UserOccasionDTO()
        {
            Id = userOccasion.Id,
            Occasion = userOccasion.Occasion.ToDto(),
            User = userOccasion.User.ToDto()
        };
    }
}
