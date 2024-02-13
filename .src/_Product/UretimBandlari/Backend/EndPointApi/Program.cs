using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business;
using FullSharedCore;
using FullSharedCore.Aspects.Secured.Jwt.Models;
using FullSharedCore.Extensions.PacketCustomException;
using FullSharedCore.Utilities.EmailSender.Models;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting.Internal;



var builder = WebApplication.CreateBuilder(args);

//var hostBuilder = new HostBuilder()
//    .UseContentRoot(Directory.GetCurrentDirectory())
//    .ConfigureHostConfiguration(configurationBuilder =>
//    {
//        configurationBuilder.AddCommandLine(args);
//    });
//IHostEnvironment host = new HostingEnvironment();
builder.Configuration.AddJsonFile($"appsettings.MailConfig.json", optional: true, reloadOnChange: true);




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);









builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builderx =>
{
    builderx.RegisterModule(new ServiceRegistiration_Business_Autofact(builder.Configuration));
});

builder.Services.Register_Core(builder.Configuration);

var app = builder.Build();



ServiceRegistiration_FullSharedCore.ServiceProvider = app.Services;

app.ConfigureCustomExceptionMiddleware();




























if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
