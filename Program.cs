using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Entidades;
using DAL;
using BLL;
using Microsoft.EntityFrameworkCore;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazoredToast();

builder.Services.AddDbContext<Contexto>(options => 
        options.UseSqlite(builder.Configuration.GetConnectionString("ConStr"))
);

builder.Services.AddTransient<ProductosBLL>();
builder.Services.AddTransient<EmpacadosBLL>();

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
