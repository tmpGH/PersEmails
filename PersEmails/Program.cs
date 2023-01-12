using PersEmails.Application;
using PersEmails.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            //app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Persons/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Persons}/{action=Index}/{id?}");

        app.Run();
    }
}