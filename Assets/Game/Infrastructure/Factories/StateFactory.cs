using Game.Infrastructure.States;
using Zenject;

namespace Game.Infrastructure.Factories
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }
        public T Create<T>() where T : IState
        {
            return _container.Resolve<T>();
        }
    }
}