using System;
using System.Collections.Generic;
using Game.Infrastructure.Factories;
using Game.Services.Logger;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameStateMachine : IStateMachine, IInitializable
    {
        private IDictionary<Type, IState> _states;
        private IState _currentState;
        private readonly StateFactory _factory;

        public GameStateMachine(StateFactory factory)
        {
            _factory = factory;
        }
        
        public void Initialize()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = _factory.Create<BootstrapState>(),
                [typeof(MetaState)] = _factory.Create<MetaState>(),
                [typeof(LoadLevelState)] = _factory.Create<LoadLevelState>(),
                [typeof(GameloopState)]  = _factory.Create<GameloopState>(),
            };
            Enter<BootstrapState>();
        }

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];

            Debug.Log(_currentState);
            
            _currentState.Enter();
        }
    }
}