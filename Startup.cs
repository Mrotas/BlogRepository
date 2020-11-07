using BlogRepository.DataAccess.Dao;
using BlogRepository.DataAccess.Dao.Interfaces;
using BlogRepository.Domain;
using BlogRepository.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace BlogRepository
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                string uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
                return new MongoClient(uri);
            });

            //Daos
            services.AddTransient(typeof(IBlogDao), typeof(BlogDao));
            services.AddTransient(typeof(ICommentDao), typeof(CommentDao));
            services.AddTransient(typeof(ICommentLikeDao), typeof(CommentLikeDao));
            services.AddTransient(typeof(IPostDao), typeof(PostDao));
            services.AddTransient(typeof(IPostLikeDao), typeof(PostLikeDao));
            services.AddTransient(typeof(IUserDao), typeof(UserDao));

            //Services
            services.AddTransient(typeof(IBlogService), typeof(BlogService));
            services.AddTransient(typeof(ICommentService), typeof(CommentService));
            services.AddTransient(typeof(IPostService), typeof(PostService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
