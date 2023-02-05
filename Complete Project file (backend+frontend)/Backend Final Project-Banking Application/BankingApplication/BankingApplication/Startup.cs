using BankingApplication.AccountInterest;
using BankingApplication.DAL;
using BankingApplication.IdProvider;
using BankingApplication.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmallOfficeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BankDB")));
            services.AddControllers().AddNewtonsoftJson(JsonOptions=> 
            {
                JsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddTransient<IAccountRepo, AccountRepo>();
            services.AddTransient<ITransactionRepo, TransactionRepo>();

            services.AddTransient<IGenerateID, GenerateID>();

            services.AddTransient<CurrentAccount>();
            services.AddTransient<SavingAccount>();

            services.AddTransient<Func<string, IAccount>>(
                ServiceProvider => accountType =>
                {
                    switch (accountType)
                    {
                        case "Saving":
                            return ServiceProvider.GetService<SavingAccount>();
                        case "Current":
                            return ServiceProvider.GetService<CurrentAccount>();
                        default:
                            throw new KeyNotFoundException();
                    }
                });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option =>
            option.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
