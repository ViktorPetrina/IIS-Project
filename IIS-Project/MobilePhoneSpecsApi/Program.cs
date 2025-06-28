using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MobilePhoneSpecsApi.AutoMapper;
using MobilePhoneSpecsApi.Models;
using MobilePhoneSpecsApi.Repository;
using MobilePhoneSpecsApi.SOAP;
using SoapCore;
using System;
using System.Reflection;
using System.Text;

namespace MobilePhoneSpecsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

            builder.Services.AddControllers().AddXmlSerializerFormatters();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSoapCore();
            builder.Services.AddDbContext<SpecificationsDbContext>();
            builder.Services.AddSingleton<ISearchService, SpecificationSearchService>();
            builder.Services.AddScoped<IRepository<Specification>, SpecificationsRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSoapEndpoint<ISearchService>("/SpecificationSearchService.asmx", new SoapEncoderOptions());
            app.MapControllers();

            app.Run();
        }
    }
}
