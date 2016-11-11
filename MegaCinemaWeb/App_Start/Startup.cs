using Microsoft.Owin;
using System;
using Owin;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using MegaCinemaData.Infrastructures;
using MegaCinemaData;
using MegaCinemaData.Repositories;
using MegaCinemaService;
using System.Web.Mvc;
using System.Diagnostics;
using Microsoft.Owin.Security.DataProtection;
using System.Web;
using MegaCinemaModel.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(MegaCinemaWeb.App_Start.Startup))]

namespace MegaCinemaWeb.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);
            ConfigureAuth(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //register for web controller and web api
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<MegaCinemaDBContext>().AsSelf().InstancePerRequest();

            //repository
            builder.RegisterAssemblyTypes(typeof(StatusRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            //service 
            builder.RegisterAssemblyTypes(typeof(StatusService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //set web api and controller

            //asp identity 
            //builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();
        }
    }
}
