using MVCNetCore_Service_Scope_Transient_Singleton.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IMyServiceSingleton, MyService>();
builder.Services.AddTransient<IMyServiceTransient, MyService>();
builder.Services.AddScoped<IMyServiceScoped, MyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Service}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
