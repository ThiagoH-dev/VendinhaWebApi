using VendinhaConsole.Entidades;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System.Data.Common;

namespace VendinhaConsole
{
    [TestFixture]
    public class GenerateSchema_Fixture
    {
        [Test]
        public static void Can_generate_schema()
        {
            var cfg = new Configuration();
            cfg.Configure();

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}
