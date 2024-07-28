using Blogging.Application.Interfaces;
using Blogging.Domain.Entities;
using Blogging.Infrastructure.Authentication;
using Blogging.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blogging.Domain.Repositories;
using Blogging.Infrastructure.Repositories;
using Blogging.Infrastructure.Seeders;


namespace Blogging.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrasturcture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BloggingDbContext>(options => 
                                                 options.UseSqlServer(configuration.GetConnectionString("BloggingDb")));

        services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<BloggingDbContext>();


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenKey = configuration["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(tokenKey)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<IUserFollowsRepository, UserFollowsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IBlogSeeder, BlogSeeder>();



    }
}
