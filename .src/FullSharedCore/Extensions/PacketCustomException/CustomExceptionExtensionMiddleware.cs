using FluentValidation;
using FluentValidation.Results;
using FullSharedCore.Extensions.PacketCustomException.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Authentication;

namespace FullSharedCore.Extensions.PacketCustomException
{
    public class CustomExceptionExtensionMiddleware
    {
        private RequestDelegate _next;

        public CustomExceptionExtensionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
                // Gerekli Dönüş Tipleri Belirlenecek.
                // await HandleExceptionAsync(httpContext, ex);
            }

        }
        // IActionTransaction _helper;
        private   Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
           // if (!e.Message.StartsWith("Şuan Başka bir İşlem Gerçekleşiyor"))
          //  {
           //     _helper = ServiceRegistiration_Shared.ServiceProvider.GetService<IActionTransaction>();
          //       _helper.SaveChanges(e);
          //  }
          


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());

            }

            if (e.GetType() == typeof(OperationCanceledException))
            {
                message ="Token Sonlandı. Ek bilgi: "+ e.Message; 
                httpContext.Response.StatusCode = 400; 
            }


            if (e.GetType().Equals(typeof(AuthenticationException)))
            {
                message = "Önce Giriş Yapıp Token Almalısınız. " + e.Message;
                httpContext.Response.StatusCode = 401;
            }
            if (e.GetType().Equals(typeof(BadImageFormatException)))
            {
                // Yazılımsal Hatayı Kritik uyarısı ile Geliştiriciye mail at.  Kullanıcıya standart mesaj dön.
                message = "Bu işlemi Şuan Gerçekleştiremiyoruz, Kısa bir süre sonra bu hizmet kullanıma açılacaktır.";
                httpContext.Response.StatusCode = 500;

                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = message
                }.ToString());
            }

            
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message+" Ek Bilgi : "+ e.Message
            }.ToString());
        }














    }
}
