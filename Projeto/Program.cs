using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine($"String de conexão: {connectionString}");
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<AuthService>();

Console.WriteLine("Serviços adicionados ao contêiner.");

var app = builder.Build();

Console.WriteLine("Aplicativo construído.");

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

Console.WriteLine("Pipeline de requisições HTTP configurado.");

app.Run();

Console.WriteLine("Aplicativo iniciado.");
