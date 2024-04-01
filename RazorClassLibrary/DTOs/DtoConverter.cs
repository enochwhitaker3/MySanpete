using RazorClassLibrary.Data;

namespace RazorClassLibrary.DTOs;

public static class DtoConverter
{
    public static UserDTO ToDto(this EndUser user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Guid = user.Guid,
            Username = user.UserName,
            UserEmail = user.UserEmail,
            Photo = user.Photo,
        };
    }

    public static VoucherDTO ToDto(this Voucher voucher)
    {
        return new VoucherDTO()
        {
            Id = voucher.Id,
            BusinessName = voucher.Business.BusinessName,
            BusinessLogo = voucher.Business.Logo,
            StartDate = voucher.StartDate,
            EndDate = voucher.EndDate,
            PromoCode = voucher.PromoCode,
            PromoName = voucher.PromoName,
            PromoDescription = voucher.PromoDescription,
            LeftInStock = voucher.PromoStock - voucher.UserVouchers.Count(),
            RetailPrice = voucher.RetailPrice,
            AmmountReclaimable = voucher.TotalReclaimable,
            Stock = voucher.PromoStock,
            PriceId = voucher.PriceId,
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
            Photo = blog.Photo,
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
            Content = comment.CommentText
        };
    }

    public static PodcastDTO ToDto(this Podcast podcast)
    {
        return new PodcastDTO()
        {
            Id = podcast.Id,
            URL = podcast.PodcastUrl,
            Comments = podcast.PodcastComments.Select(x => x.Comment.ToDto()).ToList(),
            Reactions = podcast.PodcastReactions.Select(x => x.Reaction).ToList(),
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
            User = userVoucher.User.ToDto()
        };
    }
}
