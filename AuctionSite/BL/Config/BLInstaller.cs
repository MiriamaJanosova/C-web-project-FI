using System;
using AutoMapper;
using BL.Facades.Base;
using BL.Identity;
using BL.QueryObjects.Common;
using BL.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Config;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BL.Config
{
    public class BLInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new EFInstaller().Install(container, store);

            

            container.Register(

                Component.For<IUserStore<User, int>>().ImplementedBy<IdentityUserStore>().LifestyleTransient(),
                Component.For<IRoleStore<Role, int>>().ImplementedBy<IdentityRoleStore>().LifestyleTransient(),

                //Component.For<Func<OffersPortalSignInManager>>().Instance(container.Resolve<OffersPortalSignInManager>).LifestyleTransient(),
                Component.For<Func<IdentityUserManager>>().Instance(container.Resolve<IdentityUserManager>)
                    .LifestyleTransient(),
                Component.For<Func<IdentityRoleManager>>().Instance(container.Resolve<IdentityRoleManager>)
                    .LifestyleTransient(),


                Component.For<IdentityUserManager>().LifestyleTransient(),
                Component.For<IdentityRoleManager>().LifestyleTransient(),

                Classes.FromThisAssembly()
                        .BasedOn(typeof(QueryObjectBase<,,,>))
                        .WithServiceBase()
                        .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<ServiceBase>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn(typeof(FacadeBase))
                    .LifestyleTransient(),

                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                    .LifestyleSingleton(),
                Component.For<IAuthenticationManager>().LifestyleTransient()
            );
            
        }
    }
}