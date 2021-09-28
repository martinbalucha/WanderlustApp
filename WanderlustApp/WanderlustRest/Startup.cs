using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.Query;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustPersistence.Repository;
using WanderlustService.Config;
using WanderlustService.Service;
using WanderlustService.Service.PasswordManagement;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustRest
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
            services.AddDbContext<WanderlustContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Postgres"));
            });

            //services.AddTransient(typeof(IUnitOfWorkContext), typeof(EntityFrameworkUnitOfWorkContext));
            //services.AddTransient(typeof(IQuery<>), typeof(EntityQuery<>));
            //services.AddTransient(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));



            //services.AddTransient(typeof(IPasswordService), typeof(PBKDF2PasswordService));
            //services.AddTransient(typeof(ICountryService), typeof(CountryService));

            ////services.AddTransient(typeof(QueryObjectBase<,,>), );
            

            //services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)));

            services.AddControllers();
        }

        /// <summary>
        /// Configures IoC container
        /// </summary>
        /// <param name="builder">IoC container builder</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterModule(new AutofacPersistenceConfig());
            builder.RegisterModule(new AutofacServiceConfig());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
