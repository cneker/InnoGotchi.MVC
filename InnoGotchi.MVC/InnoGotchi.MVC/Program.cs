using InnoGotchi.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureHttpClient(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.ConfigureValidators();
builder.Services.CongifureMapping();
builder.Services.ConfigureCookieAuthentication();
builder.Services.AddAuthorization();
builder.Services.ConfigureServices();
builder.Services.ConfigureCookies();
builder.Services.ConfigureHelpers();
builder.Services.ConfigureClients();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
