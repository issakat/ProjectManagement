using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProjectManagement.Models;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContextApplication>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

            services.AddTransient<IEmailSender>(provider =>
            {
                var options = new SendGridOptions
                {
                    ApiKey = Configuration["SendGrid:ApiKey"],
                    SenderEmail = Configuration["SendGrid:SenderEmail"]
                };
                return new SendGridEmailSender(Configuration, Options.Create(options));
            });

            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));

            services.AddSingleton<IEmailSender, SendGridEmailSender>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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

    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly string _senderEmail;

        public SendGridEmailSender(IConfiguration configuration, IOptions<SendGridOptions> optionsAccessor)
        {
            Configuration = configuration;  

            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            var options = optionsAccessor.Value;

            if (string.IsNullOrEmpty(options.ApiKey))
            {
                throw new ArgumentException("SendGrid ApiKey must be provided", nameof(optionsAccessor));
            }

            if (string.IsNullOrEmpty(options.SenderEmail))
            {
                throw new ArgumentException("SenderEmail must be provided", nameof(optionsAccessor));
            }

            _client = new SendGridClient(options.ApiKey);
            _senderEmail = options.SenderEmail;
        }

        public IConfiguration Configuration { get; }  

        public async System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_senderEmail, "Your Sender Name"),
                Subject = subject,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            var response = await _client.SendEmailAsync(msg);
        }
    }
}
