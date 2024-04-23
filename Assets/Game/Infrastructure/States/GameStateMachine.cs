

using System;
using System.Collections.Generic;
using Game.Infrastructure.Services.Logger;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameStateMachine : IStateMachine, IInitializable
    {
        private IDictionary<Type, IState> _states;
        private IState _currentState;
        private readonly ILoggerService _logger;

        public GameStateMachine(ILoggerService logger)
        {
            _logger = logger;
        }
        
        public void Initialize() //Zenject analog of Monobehavior:Start
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
            };
            Enter<BootstrapState>();
        }

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            
            _logger.Log($"{_currentState.GetType().Name} entered", this);
            
            _currentState.Enter();
        }
    }
}