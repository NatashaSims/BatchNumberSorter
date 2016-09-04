using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public MediaContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = _dbFactory.Init());
            }
        }

        public void Commit()
        {
            DbContext.Commit();
        }

        private readonly IDbFactory _dbFactory;
        private MediaContext _dbContext;
    }
}
