using System.Collections.Generic;

namespace Game.Core.Deform.Deformation.Damager
{
    internal interface IChanger : IInitializable<IEnumerable<IDeformable>>
    {
        public void Change();
    }
}