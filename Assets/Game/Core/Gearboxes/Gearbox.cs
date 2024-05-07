using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Configs;
using Game.Services.CoroutinePerformer;
using Game.Services.Logger;
using UnityEngine;

namespace Game.Core.Gearboxes
{
    public class Gearbox : IGearbox, IReadOnlyGearbox
    {
        private readonly GearboxConfig _config;
        private readonly WaitForSeconds _waiter;
        private readonly ICoroutinePerformer _performer;
        private readonly ILoggerService _logger;
        
        private readonly IReadOnlyList<float> _ratio;
        
        private Coroutine _coroutine;
        
        public event Action<int> GearChanged;
        public int CurrentGear { get; private set; }
        public bool IsChanging { get; private set; }
        
        public Gearbox(GearboxConfig config, ICoroutinePerformer performer, ILoggerService logger)
        {
            _logger = logger;
            _performer = performer;
            _config = config;
            _waiter = new WaitForSeconds(_config.ChangingGearDelay);
            _ratio = _config.Ratio;
        }

        public void Next()
        {
            _performer.StartPerform(ChangeGear(() =>  CurrentGear = Clamp(++CurrentGear)));
        }
        
        public void Previous()
        {
            _performer.StartPerform(ChangeGear(() =>  CurrentGear = Clamp(--CurrentGear)));
        }

        public float GetRatio() => _ratio[CurrentGear + 1];
 
        private IEnumerator ChangeGear(Action action)
        {
            var gear = CurrentGear;
            CurrentGear = 0;
            _logger.Log("changed gear: " + CurrentGear);
            IsChanging = true;
            yield return _waiter;
            CurrentGear = gear;
            IsChanging = false;
            action?.Invoke();
            GearChanged?.Invoke(CurrentGear);
            _logger.Log("changed gear: " + CurrentGear);
        }
        private int Clamp(int gear) => Mathf.Clamp(gear, -1, _ratio.Count - 2);
    }
}