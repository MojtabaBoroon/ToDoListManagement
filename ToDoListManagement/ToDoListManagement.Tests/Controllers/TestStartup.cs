using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoListManagement.Persistence;
using ToDoListManagement.Persistence.Abstractions;
using ToDoListManagement.Persistence.DataAccessContext;

namespace ToDoListManagement.Tests.Controllers
{
    public class TestStartup<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseInMemoryDatabase("ToDoDB");
            });
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}