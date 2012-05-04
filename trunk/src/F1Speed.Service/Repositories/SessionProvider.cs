using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Service.Repositories.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace F1Speed.Service.Repositories
{
    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _config;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                       .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromAppSetting("database")))
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LapMap>().Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                       .ExposeConfiguration(cfg => _config = cfg)
                       .BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static Configuration Config
        {
            get
            {
                if (_config == null || _sessionFactory == null)
                {
                    var sf = SessionFactory;
                }
                return _config;
            }
        }
    }
}
