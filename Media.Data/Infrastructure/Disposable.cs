using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }
            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }

        private bool _isDisposed;
    }
}
