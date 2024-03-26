using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;

namespace MySanpeteWeb;

public partial class MySanpeteDbContext : DbContext
{
    public MySanpeteDbContext()
    {
    }

    public MySanpeteDbContext(DbContextOptions<MySanpeteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    public virtual DbSet<BlogReaction> BlogReactions { get; set; }

    public virtual DbSet<Bundle> Bundles { get; set; }

    public virtual DbSet<BundleVoucher> BundleVouchers { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<EndUser> EndUsers { get; set; }

    public virtual DbSet<Occasion> Occasions { get; set; }

    public virtual DbSet<Podcast> Podcasts { get; set; }

    public virtual DbSet<PodcastComment> PodcastComments { get; set; }

    public virtual DbSet<PodcastReaction> PodcastReactions { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<UserComment> UserComments { get; set; }

    public virtual DbSet<UserOccasion> UserOccasions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=MySanpeteDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blog_pkey");

            entity.ToTable("blog", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BlogContent).HasColumnName("blog_content");
            entity.Property(e => e.Commentable)
                .HasDefaultValue(true)
                .HasColumnName("commentable");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PublishDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("publish_date");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_author_id_fkey");
        });

        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blog_comment_pkey");

            entity.ToTable("blog_comment", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_comment_blog_id_fkey");

            entity.HasOne(d => d.Comment).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_comment_comment_id_fkey");
        });

        modelBuilder.Entity<BlogReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blog_reaction_pkey");

            entity.ToTable("blog_reaction", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.ReactionId).HasColumnName("reaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogReactions)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_reaction_blog_id_fkey");

            entity.HasOne(d => d.Reaction).WithMany(p => p.BlogReactions)
                .HasForeignKey(d => d.ReactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_reaction_reaction_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.BlogReactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Bundle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bundle_pkey");

            entity.ToTable("bundle", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BundleName).HasColumnName("bundle_name");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<BundleVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bundle_voucher_pkey");

            entity.ToTable("bundle_voucher", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BundleId).HasColumnName("bundle_id");
            entity.Property(e => e.DiscountPrice).HasColumnName("discount_price");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

            entity.HasOne(d => d.Bundle).WithMany(p => p.BundleVouchers)
                .HasForeignKey(d => d.BundleId)
                .HasConstraintName("bundle_voucher_bundle_id_fkey");

            entity.HasOne(d => d.Voucher).WithMany(p => p.BundleVouchers)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("bundle_voucher_voucher_id_fkey");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("business_pkey");

            entity.ToTable("business", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.BusinessName).HasColumnName("business_name");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.Website).HasColumnName("website");
        });

        modelBuilder.Entity<EndUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("end_user_pkey");

            entity.ToTable("end_user", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(80)
                .HasColumnName("user_name");
            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

            entity.HasOne(d => d.UserRole).WithMany(p => p.EndUsers)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("end_user_user_role_id_fkey");
        });

        modelBuilder.Entity<Occasion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("occasion_pkey");

            entity.ToTable("occasion", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.XCoordinate).HasColumnName("x_coordinate");
            entity.Property(e => e.YCoordinate).HasColumnName("y_coordinate");

            entity.HasOne(d => d.Business).WithMany(p => p.Occasions)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("occasion_business_id_fkey");
        });

        modelBuilder.Entity<Podcast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("podcast_pkey");

            entity.ToTable("podcast", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentable)
                .HasDefaultValue(true)
                .HasColumnName("commentable");
            entity.Property(e => e.PodcastName).HasColumnName("podcast_name");
            entity.Property(e => e.PodcastUrl).HasColumnName("podcast_url");
        });

        modelBuilder.Entity<PodcastComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("podcast_comment_pkey");

            entity.ToTable("podcast_comment", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.PodcastId).HasColumnName("podcast_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.PodcastComments)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("podcast_comment_comment_id_fkey");

            entity.HasOne(d => d.Podcast).WithMany(p => p.PodcastComments)
                .HasForeignKey(d => d.PodcastId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("podcast_comment_podcast_id_fkey");
        });

        modelBuilder.Entity<PodcastReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("podcast_reaction_pkey");

            entity.ToTable("podcast_reaction", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PodcastId).HasColumnName("podcast_id");
            entity.Property(e => e.ReactionId).HasColumnName("reaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Podcast).WithMany(p => p.PodcastReactions)
                .HasForeignKey(d => d.PodcastId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("podcast_reaction_podcast_id_fkey");

            entity.HasOne(d => d.Reaction).WithMany(p => p.PodcastReactions)
                .HasForeignKey(d => d.ReactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("podcast_reaction_reaction_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PodcastReactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reaction_pkey");

            entity.ToTable("reaction", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Unicode).HasColumnName("unicode");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_pkey");

            entity.ToTable("review", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.ReviewText).HasColumnName("review_text");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Business).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_business_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_user_id_fkey");
        });

        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_comment_pkey");

            entity.ToTable("user_comment", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentText).HasColumnName("comment_text");
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("post_date");
            entity.Property(e => e.ReplyId).HasColumnName("reply_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Reply).WithMany(p => p.InverseReply)
                .HasForeignKey(d => d.ReplyId)
                .HasConstraintName("user_comment_reply_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_comment_user_id_fkey");
        });

        modelBuilder.Entity<UserOccasion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_occasion_pkey");

            entity.ToTable("user_occasion", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OccasionId).HasColumnName("occasion_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Occasion).WithMany(p => p.UserOccasions)
                .HasForeignKey(d => d.OccasionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_occasion_occasion_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserOccasions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_occasion_user_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pkey");

            entity.ToTable("user_role", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
        });

        modelBuilder.Entity<UserVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_vourcher_pkey");

            entity.ToTable("user_voucher", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChargeId).HasColumnName("charge_id");
            entity.Property(e => e.FinalPrice)
                .HasColumnType("money")
                .HasColumnName("final_price");
            entity.Property(e => e.Isused)
                .HasDefaultValue(false)
                .HasColumnName("isused");
            entity.Property(e => e.PromoCode).HasColumnName("promo_code");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.TimesClaimd).HasColumnName("times_claimd");
            entity.Property(e => e.TotalReclaimable).HasColumnName("total_reclaimable");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_vourcher_user_id_fkey");

            entity.HasOne(d => d.Voucher).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_vourcher_voucher_id_fkey");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("voucher_pkey");

            entity.ToTable("voucher", "mysanpete");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PromoCode).HasColumnName("promo_code");
            entity.Property(e => e.PromoDescription).HasColumnName("promo_description");
            entity.Property(e => e.PromoName).HasColumnName("promo_name");
            entity.Property(e => e.PromoStock).HasColumnName("promo_stock");
            entity.Property(e => e.RetailPrice)
                .HasColumnType("money")
                .HasColumnName("retail_price");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StripeId).HasColumnName("stripe_id");
            entity.Property(e => e.TotalReclaimable).HasColumnName("total_reclaimable");

            entity.HasOne(d => d.Business).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_business_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
