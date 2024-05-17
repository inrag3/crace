using UnityEngine;

namespace Game.Core.Deform.Refresher
{
    [DisallowMultipleComponent]
    public class PerFrameRefresher : Refresher
    {
        protected override void LateUpdate() => 
            Refresh();
    }
}