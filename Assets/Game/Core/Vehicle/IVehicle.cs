using System;

namespace Game.Core.Vehicle
{
    public interface IVehicle
    {
        public void Brake(float value);
        public void StartEngine();
        public void StopEngine();
        public void Accelerate(float value);

        public void Steering(float value);        
        public void NextGear();
        public void PreviousGear();
    }
}

public interface IVehicle
{
    public void Brake(float value);
    public void StartEngine();
    public void StopEngine();
    public void Accelerate(float value);

    public void Steering(float value);        
    public void NextGear();
    public void PreviousGear();
}