using MudBlazor.Services;
using WebUI.Site._Data;

var builder = WebApplication.CreateBuilder(args);
 



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped(typeof(TestServisi));
builder.Services.AddHttpClient<TestServisi>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7133/");
});


var app = builder.Build();
 





if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
     app.UseHsts();
}






app.UseHttpsRedirection(); 
app.UseStaticFiles(); 
app.UseRouting(); 
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); 
app.Run();
