using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebControlVacunas_CodigoX.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ControlVacunasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ControlVacunasContext") ?? throw new InvalidOperationException("Base de datos no encontrada.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var inicio = app.Services.CreateScope())
{
    var servicio = inicio.ServiceProvider;
    var contexto = servicio.GetRequiredService<ControlVacunasContext>();
    contexto.Database.EnsureCreated();
    BDInicializa.Inicializar(contexto);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
