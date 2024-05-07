using Game.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Meta
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _button;
    
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        private void Awake()
        {
            _button.onClick.AddListener(Load);
        }

        private void Load()
        {
            _stateMachine.Enter<LoadLevelState>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Load);
        }
    }
}