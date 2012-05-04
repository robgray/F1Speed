using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using F1Speed.Service.Repositories.Mappings;
using NHibernate.Tool.hbm2ddl;

namespace F1Speed.Service.Tests
{
    public abstract class InMemoryDatabase : IDisposable
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;

        protected ISession Session { get; set; }

        protected InMemoryDatabase()
        {
            _sessionFactory = CreateSessionFactory();
            Session = _sessionFactory.OpenSession();
            BuildSchema(Session);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                return Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LapMap>())
                    .ExposeConfiguration(cfg => _configuration = cfg)
                    .BuildSessionFactory();

            } catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return null;
        }

        private static void BuildSchema(ISession session)
        {
            var export = new SchemaExport(_configuration);
            export.Execute(true, true, false, session.Connection, null);
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
