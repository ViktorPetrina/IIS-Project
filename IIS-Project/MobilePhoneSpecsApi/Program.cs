
using Microsoft.EntityFrameworkCore;
using MobilePhoneSpecsApi.AutoMapper;
using MobilePhoneSpecsApi.Models;
using MobilePhoneSpecsApi.Repository;
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

            builder.Services.AddDbContext<SpecificationsDbContext>();

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
            app.MapControllers();

            app.Run();
        }
    }
}
