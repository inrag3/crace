using System;
using System.Collections.Generic;

namespace Game.Infrastructure.Disposer
{
    public class Disposer : IDisposer, IDisposable
    {
        private readonly IDictionary<Type, IList<IDisposable>> _disposables =
            new Dictionary<Type, IList<IDisposable>>();
        
        public void Add<T>(IDisposable disposable)
        {
            if (!_disposables.TryGetValue(typeof(T), out var disposables))
            {
                disposables = new List<IDisposable>();
                _disposables.Add(typeof(T), disposables);
            }
            disposables.Add(disposable);
        }

        public void Dispose<T>()
        {
            if (!_disposables.TryGetValue(typeof(T), out var disposables)) 
                return;

            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }

            disposables.Clear();
            _disposables.Remove(typeof(T));
        }

        public void Dispose()
        {
            foreach (var list in _disposables.Values)
            {
                foreach (var disposable in list)
                {
                    disposable.Dispose();
                }
            }
            _disposables.Clear();
        }
    }
}

