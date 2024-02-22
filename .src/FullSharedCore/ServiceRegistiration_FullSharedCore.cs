using FullSharedCore.Aspects.Caching;
using FullSharedCore.Aspects.Caching.Factories;
using FullSharedCore.Aspects.Secured.Jwt;
using FullSharedCore.Aspects.Secured.Jwt.Models;
using FullSharedCore.DataAccess.Abstract;
using FullSharedCore.DataAccess.Concrete;
using FullSharedCore.Helpers.LoadAssemblyies;
using FullSharedCore.Utilities.EmailSender.Abstract;
using FullSharedCore.Utilities.EmailSender.Concrete;
using FullSharedCore.Utilities.EmailSender.Models;
using FullSharedCore.Utilities.HttpClientOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServiceStack.Redis;
using Shared.Aspects.Secured.Jwt;
using System.Reflection;

namespace FullSharedCore
{
    public static class ServiceRegistiration_FullSharedCore
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static void Register_Core(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<HttpClient>();
            services.AddScoped<IHttpClientOperation,HttpClientOperation>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(GetMySolutionMyDlls.List));
           
            #region TokenOptionsModel  List<MailSenderBaseModel>  Config
            TokenOptionsModel tokenOptionsModel = new TokenOptionsModel();
            tokenOptionsModel.Audience = _configuration["TokenOptionsModel:Audience"];
            tokenOptionsModel.Issuer = _configuration["TokenOptionsModel:Issuer"];
            tokenOptionsModel.AccessTokenExpiration = Convert.ToInt32(_configuration["TokenOptionsModel:AccessTokenExpiration"]);
            tokenOptionsModel.SecurityKey = _configuration["TokenOptionsModel:SecurityKey"];


            List<MailSenderBaseModel> mailSenderBaseModels = new List<MailSenderBaseModel>();
            Type modelType = typeof(MailSenderBaseModel);
            var MailSenderBaseModelKeys = _configuration.GetSection("MailSenderBaseModel");
            var MailSebnderBaseModelList = _configuration.GetSection("MailSenderBaseModel").GetChildren();
            foreach (var item in MailSebnderBaseModelList)
            {
                var Model = item.GetChildren();
                if (Model is null) continue;

                var obje = Activator.CreateInstance(modelType);
                foreach (PropertyInfo prop in modelType.GetProperties())
                {
                    var propValue = Model.FirstOrDefault(x => x.Key.Equals(prop.Name))?.Value;
                    if (propValue is null)
                    {
                        if (prop.PropertyType.Equals(typeof(List<string>)))
                        {
                            List<string> list = new List<string>();
                            foreach (var itemSub in Model.FirstOrDefault(x => x.Key.Equals(prop.Name)).GetChildren())
                                list.Add(itemSub.Value);
                            prop.SetValue(obje, list, null);
                        }
                        else if (prop.PropertyType.Equals(typeof(List<int>)))
                        {
                            List<int> list = new List<int>();
                            foreach (var itemSub in Model.FirstOrDefault(x => x.Key.Equals(prop.Name)).GetChildren())
                            {
                                int _value = 0;
                                int.TryParse(itemSub.Value, out _value);
                                list.Add(_value);
                            }
                            prop.SetValue(obje, list, null);
                        }
                        else if (prop.PropertyType.Equals(typeof(bool)))
                            prop.SetValue(obje, false, null);
                        else
                        {
                            prop.SetValue(obje, Convert.ChangeType(null, prop.PropertyType), null);
                        }

                    }
                    else
                    {

                        if (prop.PropertyType.Equals(typeof(List<string>)))
                        {
                            List<string> list = new List<string>();
                            foreach (var itemSub in Model.FirstOrDefault(x => x.Key.Equals(prop.Name)).GetChildren())
                                list.Add(itemSub.Value);
                            prop.SetValue(obje, list, null);
                        }
                        else if (prop.PropertyType.Equals(typeof(List<int>)))
                        {
                            List<int> list = new List<int>();
                            foreach (var itemSub in Model.FirstOrDefault(x => x.Key.Equals(prop.Name)).GetChildren())
                            {
                                int _value = 0;
                                int.TryParse(itemSub.Value, out _value);
                                list.Add(_value);
                            }
                            prop.SetValue(obje, list, null);
                        }
                        else if (prop.PropertyType.Equals(typeof(bool)))
                        {
                            if (propValue.ToLower().Equals("true"))
                                prop.SetValue(obje, true, null);
                            else
                                prop.SetValue(obje, false, null);
                        }
                        else
                        {
                            prop.SetValue(obje, Convert.ChangeType(propValue, prop.PropertyType), null);
                        }

                    }
                }
                mailSenderBaseModels.Add((MailSenderBaseModel)obje);
            }


            services.AddScoped<ITokenHelper>(sp => new JwtHelper(tokenOptionsModel));
            services.AddScoped<IMailSender>(sp => new MailSender(mailSenderBaseModels));
            #endregion

             

            #region Cache Config
            services.AddMemoryCache();
            services.AddScoped<ICacheManager, Cache_MM>();


            #region Distributed Cache Config
            string Distributed_Redis_HostIp = _configuration["Distributed.Redis:HostIp"];
            int Distributed_Redis_HostPort = Convert.ToInt32(_configuration["Distributed.Redis:HostPort"]);
            string Distributed_Redis_Password = _configuration["Distributed.Redis:Password"];
            int Distributed_Redis_Db = Convert.ToInt32(_configuration["Distributed.Redis:Db"]);
            services.AddScoped<IRedisClient>(sp => new RedisClient(Distributed_Redis_HostIp, Distributed_Redis_HostPort, Distributed_Redis_Password, Distributed_Redis_Db));
            #endregion
            #endregion

           
        }



























    }
}
