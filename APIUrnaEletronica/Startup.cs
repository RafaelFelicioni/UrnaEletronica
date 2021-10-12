using APIUrnaEletronica.Data;
using APIUrnaEletronica.Repository;
using APIUrnaEletronica.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APIUrnaEletronica
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder => {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddDbContext<APIUrnaContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DataBaseUrna")));
            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<IVotoRepository, VotoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("ApiCorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
