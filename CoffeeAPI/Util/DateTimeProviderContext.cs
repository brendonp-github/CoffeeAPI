using System.Collections;

namespace CoffeeAPI.Util
{
    public class DateTimeProviderContext : IDisposable //based off https://dvoituron.com/2020/01/22/UnitTest-DateTime/
    {
        internal DateTime ContextDateTimeNow;
        private static ThreadLocal<Stack> ThreadScopeStack = new ThreadLocal<Stack>(() => new Stack());
        private Stack _contextStack = new Stack();

        public DateTimeProviderContext(DateTime contextDateTimeNow)
        {
            ContextDateTimeNow = contextDateTimeNow;
            ThreadScopeStack.Value!.Push(this);
        }

        public static DateTimeProviderContext Current
        {
            get
            {
                if (ThreadScopeStack.Value!.Count == 0)
                    return null!;
                else
                    return ThreadScopeStack.Value.Peek() as DateTimeProviderContext;
            }
        }

        public void Dispose()
        {
            if (ThreadScopeStack.Value != null && ThreadScopeStack.Value.Count > 0)
            {
                ThreadScopeStack.Value.Pop();
            }
        }
    }
}
