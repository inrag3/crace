using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.VehicleSystem.Gearboxes
{
    public class Gearbox : IGearbox, IReadOnlyGearbox
    {
        private readonly WaitForSeconds _waiter;
        private readonly ICoroutinePerformer _performer;

        private readonly IReadOnlyList<float> _ratio;

        private Coroutine _coroutine;

        public Gearbox(GearboxConfig config, ICoroutinePerformer performer)
        {
            _performer = performer;
            Config = config;
            _waiter = new WaitForSeconds(Config.ChangingGearDelay);
            _ratio = Config.Ratio;
        }
        public event Action<int> GearChanged;

        public int CurrentGear { get; private set; } = 0;

        public bool IsChanging { get; private set; }
        public int Count => Config.Ratio.Count;
        public int Index { get; }
        public GearboxConfig Config { get; }

        public void Next()
        {
            _performer.StartPerform(ChangeGear(() => CurrentGear = Clamp(++CurrentGear)));
        }

        public void Neutral()
        {
            _performer.StartPerform(ChangeGear(() => CurrentGear = 0));
        }

        public void Previous()
        {
            _performer.StartPerform(ChangeGear(() => CurrentGear = Clamp(--CurrentGear)));
        }
        
        public float GetRatio(int index) => _ratio[index + 1] * Config.MainRatio;

        private IEnumerator ChangeGear(Action action)
        {
            IsChanging = true;
            yield return _waiter;
            IsChanging = false;
            action?.Invoke();
            GearChanged?.Invoke(CurrentGear);
        }

        private int Clamp(int gear) => Mathf.Clamp(gear, -1, Config.Ratio.Count - 2);
    }

    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator coroutine);

        public void StopPerform(Coroutine coroutine);
    }

    public interface IGearbox
    {
        public int CurrentGear { get; }
        public bool IsChanging { get; }
        public int Count { get; }
        public void Neutral();
        public float GetRatio(int index);
        public event Action<int> GearChanged;
        public void Next();
        public void Previous();
    }

    public interface IReadOnlyGearbox
    {
        public event Action<int> GearChanged;
        public int CurrentGear { get; }
        public bool IsChanging { get; }
    }
}
