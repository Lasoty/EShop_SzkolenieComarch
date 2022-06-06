using Autofac;
using EShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.IoC
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>)).AsSelf().As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(KeyRepository<,>)).AsSelf().As(typeof(IKeyRepository<,>));

            builder.RegisterAssemblyTypes(typeof(DataModule).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
