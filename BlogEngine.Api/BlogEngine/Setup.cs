using BlogEngine.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace BlogEngine
{
    public static class Setup
    {
        public static void AddCustomAuthentication(this IServiceCollection services, ApiSettings apiSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = apiSettings.Values.Issuer,
                        ValidAudience = apiSettings.Values.Audience,
                        IssuerSigningKey = Create(apiSettings.Values.SecurityKey)
                    };
                    options.IncludeErrorDetails = true;
                });
        }

        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
            {
                config.AddPolicy("Public",
                    policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.Role, "Public"); });

                config.AddPolicy("Writer",
                    policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.Role, "Writer", "Public"); });

                config.AddPolicy("Editor",
                    policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.Role, "Editor", "Public"); });
            });
        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowPolicy", policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BlogEngine",
                    Description = "BlogEngine API",
                    Contact = new OpenApiContact { Name = "BlogEngine" }
                });
                option.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        BearerFormat = "apiKey",
                        Name = "Authorization",
                        In = ParameterLocation.Header
                    });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
