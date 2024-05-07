using System;

namespace Game.Infrastructure.Disposer
{
    public interface IDisposer
    {
        public void Add<TContext>(IDisposable disposable);
        public void Dispose<TContext>();
    }
}