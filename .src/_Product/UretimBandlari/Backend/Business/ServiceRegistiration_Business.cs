using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract.ForOperational;
using Business.Abstract.ForUser;
using Business.Concrete.ForOperational;
using Business.Concrete.ForUser;
using Business.Mappers;
using Castle.DynamicProxy;
using Dal.Abstract.Contexts;
using Dal.Concrete.Contexts;
using FullSharedCore.Aspects.Base;
using FullSharedCore.DataAccess.Abstract;
using FullSharedCore.Helpers.LoadAssemblyies;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business
{

    public class ServiceRegistiration_Business_Autofact : Module
    {
        IConfiguration configuration;
        public ServiceRegistiration_Business_Autofact(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<PDbContextCommand>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestSqlServerCommand"));
                return new PDbContextCommand(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<PDbContextQuery>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestSqlServerQuery"));
                return new PDbContextQuery(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

 




            //Db Operation 
            builder.RegisterType<UnitOfWorkCommand>().As<IUnitOfWorkCommand>().As<IBaseUnitOfWorkCommand>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWorkQuery>().As<IUnitOfWorkQuery>().As<IBaseUnitOfWorkQuery>().InstancePerLifetimeScope();





            //AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            { 
                cfg.AddProfile<ForUserMapping>();
            }
           )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
          .As<IMapper>()
          .InstancePerLifetimeScope();

          


            // Services 
            builder.RegisterType<KullaniciManagement>().As<IKullaniciManagement>().InstancePerLifetimeScope();
            builder.RegisterType<OperationManagement>().As<IOperationManagement>().InstancePerLifetimeScope();
            builder.RegisterType<TestManagement>().As<ITestManagement>().InstancePerLifetimeScope();

       



            var proxy = new ProxyGenerationOptions()
            {
                Selector = new InterceptorCollection(), BaseTypeForInterfaceProxy = typeof(InterceptionBaseAttiribute),
                  
            };

             
            builder.RegisterAssemblyTypes(GetMySolutionMyDlls.List)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(proxy)
                .SingleInstance();



        }





    }
}
