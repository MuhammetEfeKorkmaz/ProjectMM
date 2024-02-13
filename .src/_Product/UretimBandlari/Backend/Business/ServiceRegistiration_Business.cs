using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Concrete.ForUser;
using Business.Mappers;
using Castle.DynamicProxy;
using Dal.Abstract;
using Dal.Concrete;
using FullSharedCore.Aspects.Base;
using FullSharedCore.DataAccess.Abstract;
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
                var optionsBuilder = new DbContextOptionsBuilder<PDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TestSqlServer"));
                return new PDbContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();



            //Db Operation 
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().As<IBaseUnitOfWork>().InstancePerLifetimeScope();






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


            var proxy = new ProxyGenerationOptions()
            {
                Selector = new InterceptorCollection(), BaseTypeForInterfaceProxy = typeof(InterceptionBaseAttiribute),
                  
            };





            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(proxy)
                .SingleInstance();



        }





    }
}
