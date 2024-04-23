

using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        
        private IDictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            
        }
        
        public void Initialize()
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
            _currentState?.Enter();
        }


        public void Exit<T>() where T : IState
        {
        }
    }
}