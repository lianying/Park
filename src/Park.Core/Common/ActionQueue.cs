using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Common
{
    public class ActionQueue<T> : Queue<T>
    {
        public event EventHandler<ActionQueueArgs<T>> EnqueueHandler;

        public ActionQueue(int capacity):base(capacity) {
                
        }

        public ActionQueue() { }

        public ActionQueue(IEnumerable<T> enumerator) : base(enumerator) { }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            EnqueueHandler?.Invoke(this, new ActionQueueArgs<T>(item));
        }
    }

    public class ActionQueueArgs<T> : EventArgs
    {
        public T Arg { get; private set; }
        public ActionQueueArgs(T item)
        {
            Arg = item;
        }
    }
}
