using Autofac;
using E.Caching;
using E.Core.Repositories;
using E.Core.Services;
using E.Core.UnitOfWork;
using E.Repository;
using E.Repository.Repositories;
using E.Repository.UnitOfWork;
using E.Service.Mapping;
using E.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace EE.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly=Assembly.GetExecutingAssembly();//uzerinde calisilan assembly(api)
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));//repo katmaninda herhangi birini verebiliriz
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}
