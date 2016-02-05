using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SS.Web.Features.Class;
using StructureMap;

namespace SS.Web.Infrastructure.Mapping
{
    public class AutoMapperBootstrapper
    {
        private static bool _initialized;
        private static readonly object Lock = new object();

        public static void Initialize(IContainer container)
        {
            lock (Lock)
            {
                if (_initialized) return;
                InitializeInternal(container);
                _initialized = true;
            }
        }

        private static void InitializeInternal(IContainer container)
        {
            //var logger = Logger.Instance;

            //logger.Debug("Initializing Automapper");

            Mapper.Initialize(cfg =>
            {
                var profileNames = new List<string>();
                foreach (var profile in container.GetAllInstances<Profile>())
                {
                    profileNames.Add(profile.ProfileName);
                    cfg.AddProfile(profile);
                }

                //cfg.AddProfile<MappingProfile>();
                //logger.Verbose("Added profiles: {ProfileName}", profileNames);

                cfg.ConstructServicesUsing(container.GetInstance);
            });
        }
    }
}