using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Park
{
    public class SynchronizationContextRemove : INotifyCompletion
    {
        public bool IsCompleted
        {
            get
            {

                return SynchronizationContext.Current == null;
            }
        }

        public void OnCompleted(Action continuation)
        {
            var prevContext = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(null);
                continuation();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(prevContext);
            }
        }

        public SynchronizationContextRemove GetAwaiter()
        {
            return this;
        }

        public void GetResult()
        {

        }
    }
}
