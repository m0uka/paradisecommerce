using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ParadiseCommerce.Services.Billing.Consumers;
using ParadiseCommerce.Services.Billing.Repositories;
using ParadiseCommerce.Services.Billing.Services;
using ParadiseCommerce.Services.Contracts;
using Stripe;

namespace ParadiseCommerce.Services.Billing
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
            services.AddControllers();
            
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
                new MongoClient(Configuration.GetConnectionString("MongoDb")));
            
            // Payment gateways
            services.AddTransient<IStripePaymentService, StripePaymentService>();
            
            // Services
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<BillConsumer>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("billing-service", e =>
                    {
                        e.Consumer<BillConsumer>(context);
                    });
                });
            });

            
            
            services.AddMassTransitHostedService();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ParadiseCommerce.Services.Billing", Version = "v1"});
            });

            StripeConfiguration.ApiKey = Configuration["PaymentGateways:Stripe:ApiKey"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParadiseCommerce.Services.Billing v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}