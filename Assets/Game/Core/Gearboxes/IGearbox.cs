using System;
using Zenject;

namespace Game.Core.Gearboxes
{
    public interface IGearbox
    {
        public int CurrentGear { get; }
        public bool IsChanging { get; }
        public event Action<int> GearChanged;
        public void Next();
        public void Previous();

        public float GetRatio();
    }

    public interface IReadOnlyGearbox
    {
        public event Action<int> GearChanged;
        public int CurrentGear { get; }
    }
}