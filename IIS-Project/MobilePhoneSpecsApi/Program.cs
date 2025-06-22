using Microsoft.EntityFrameworkCore;
using MobilePhoneSpecsApi.AutoMapper;
using MobilePhoneSpecsApi.Models;
using MobilePhoneSpecsApi.Repository;
using MobilePhoneSpecsApi.SOAP;
using SoapCore;
using System.Reflection;

namespace MobilePhoneSpecsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddXmlSerializerFormatters();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSoapCore();
            builder.Services.AddDbContext<SpecificationsDbContext>();
            builder.Services.AddSingleton<ISearchService, SpecificationSearchService>();
            builder.Services.AddScoped<IRepository<Specification>, SpecificationsRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseSoapEndpoint<ISearchService>("/SpecificationSearchService.asmx", new SoapEncoderOptions());

            app.MapControllers();

            app.Run();
        }
    }
}
