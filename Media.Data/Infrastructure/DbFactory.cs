using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        MediaContext dbContext;
        public MediaContext Init()
        {
            return dbContext ?? (dbContext = new MediaContext());
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
