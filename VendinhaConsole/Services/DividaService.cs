using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendinhaConsole.Services
{
    public class DividaService
    {
        private readonly ISessionFactory session;

        public DividaService(ISessionFactory session)
        {
            this.session = session;
        }
    }
}
