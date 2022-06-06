using Autofac;
using Autofac.Extras.DynamicProxy;
using EShop.Services.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace EShop.Services.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogInterceptor>().AsSelf();

            builder.RegisterAssemblyTypes(typeof(ServicesModule).Assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogInterceptor))
                ;
        }
    }
}
