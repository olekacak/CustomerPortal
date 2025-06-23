using BlazorCRM.Data;
using CustomerPortal.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Register MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// untuk library synfucion]
SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8V1NNaF1cWWhPYVF1WmFZfVtgcV9CZVZVQWYuP1ZhSXxWdkNiX39bdHFRQ2ZUVkF9XUs=");
builder.Services.AddSyncfusionBlazor();

// sebab blazor nak absolute URL (like "https://localhost:7026/api/customer/login"),
builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7026/");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers(); // For API

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers(); // For API
app.UseDeveloperExceptionPage();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
