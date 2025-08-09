using System;

namespace WorldOfSkyfire
{
    public sealed class DisposableAction(Action dispose) : IDisposable
    {
        private Action _dispose = dispose;

        public void Dispose()
        {
            _dispose?.Invoke(); _dispose = null;
        }
        public static readonly IDisposable Empty = new DisposableAction(() => { });
    }
}
