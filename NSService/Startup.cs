using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using NSService.Entities;
using NSService.Services;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace NSService
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        private NLog.Logger _logger;
        HL7CommunicationService HL7CommunicationService;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            var mllpHostname = Startup.Configuration["mllpClientHost:mllpHostname"];
            int mllpPortNumber = Convert.ToInt32(Startup.Configuration["mllpClientPort:mllpPortNumber"]);

            HL7CommunicationService = new HL7CommunicationService(mllpHostname, mllpPortNumber);

            StartHL7Listener();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.AddTransient<LocalService>();
            var connectionString = Startup.Configuration["connectionString:NsToolConnectionString"];
            services.AddDbContext<PatientInfoContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IPatientInfoRepository, PatientInfoRepository>();

            // services.AddScoped<HL7CommunicationService, HL7CommunicationService>();
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            // Loging Configuration
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget() { FileName = "log.txt", Name = "logfile" };
            config.LoggingRules.Add(new NLog.Config.LoggingRule("*", NLog.LogLevel.Info, logfile));
            NLog.LogManager.Configuration = config;
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _logger.Log(NLog.LogLevel.Info, "Nlog Logger created.");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.Patient, Models.PatientWithoutExaminationDTO>();
                cfg.CreateMap<Entities.Patient, Models.PatientDTO>();
                cfg.CreateMap<Entities.Patient, Models.PatientCreationDTO>();
                cfg.CreateMap<Models.PatientCreationDTO, Entities.Patient>();

                cfg.CreateMap<Entities.Examination, Models.ExaminationsDTO>();
                cfg.CreateMap<Models.ExaminationCreationDTO, Entities.Examination>();
                cfg.CreateMap<Models.ExamiantionUpdateDTO,Entities.Examination>();
                cfg.CreateMap< Entities.Examination, Models.ExamiantionUpdateDTO>();

            });

            _logger.Log(NLog.LogLevel.Info, "Mappers created.");
            app.UseMvc();
            app.UseStatusCodePages();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("");
            });
        }

        public void StartHL7Listener()
        {
            
            //IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
            //while (!bootstrap.Initialize())
            //{
            //    Thread.Sleep(1000);
            //}

            //if (!bootstrap.Initialize())
            //{
            //    // To Raise Error.
            //}
            //else
            //{
            //    StartResult startResult = bootstrap.Start();
            //    foreach (IWorkItem workItem in bootstrap.AppServers)
            //    {
            //        // To Log out these items.
            //        if (workItem.State == ServerState.Running)
            //        {
            //        }
            //        else
            //        {
            //        }
            //    }
            //}

        }
    }
}
