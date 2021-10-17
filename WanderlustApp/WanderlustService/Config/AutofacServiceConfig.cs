using Autofac;
using AutoMapper;
using System.Reflection;
using WanderlustInfrastructure.Query;
using WanderlustPersistence;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.Query.QueryObject;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Config
{
    /// <summary>
    /// Configuration class for inserting 
    /// </summary>
    public class AutofacServiceConfig : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacPersistenceConfig());

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(a => a.Namespace.Contains("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(a => a.Namespace.Contains("Facade"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<UserQueryObject>().As<QueryObjectBase<User, UserFilterDto, IQuery<User>>>().InstancePerDependency();
            builder.RegisterType<CountryQueryObject>().As<QueryObjectBase<Country, CountryFilterDto, IQuery<Country>>>().InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();
        }
    }
}
