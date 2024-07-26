using Blogging.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Blogging.Infrastructure.Persistance;
internal class BloggingDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public BloggingDbContext(DbContextOptions<BloggingDbContext> options) : base(options)
    {

    }

    internal DbSet<Post> Posts { get; set; }
    internal DbSet<Comment> Comments { get; set; }

    internal DbSet<UserFollow> UserFollows { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /// Changing the default table names
        builder.Entity<User>(entity => entity.ToTable("Users"));
        builder.Entity<IdentityRole<int>>(entity => entity.ToTable("Roles"));
        builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims"));
        ///



        /////  [Post] 1 <HAS> * [Comment]
        //builder.Entity<Post>()
        //    .HasMany(p => p.Comments)
        //    .WithOne(c => c.Post)
        //    .HasForeignKey(c => c.PostId)
        //    .OnDelete(DeleteBehavior.Restrict);  

        //builder.Entity<Comment>()
        //    .HasOne(c => c.User)
        //    .WithMany(u => u.Comments) 
        //    .HasForeignKey(c => c.UserId)
        //    .OnDelete(DeleteBehavior.Restrict);
        /////

        ///  [User] 1 <HAS> * [Comment]
        builder.Entity<User>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        ///

        ///  [Post] 1 <HAS> * [Comment]
        builder.Entity<User>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        ///

        /// [User] * <FOLLOW> * [User]
        builder.Entity<UserFollow>()
               .HasKey(uf => new { uf.FollowerId, uf.FolloweeId });

        builder.Entity<UserFollow>()
               .HasOne(uf => uf.Follower)
               .WithMany(u => u.FollowedUsers)
               .HasForeignKey(uf => uf.FollowerId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<UserFollow>()
               .HasOne(uf => uf.Followee)
               .WithMany(u => u.Followers)
               .HasForeignKey(uf => uf.FolloweeId)
               .OnDelete(DeleteBehavior.NoAction);
        /// 
    }
}
