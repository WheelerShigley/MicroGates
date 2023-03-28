using LogicWorld.Interfaces;
using LogicWorld.Rendering.Dynamics;
using LogicWorld.SharedCode.Components;
using System.Collections.Generic;
using UnityEngine;
using JimmysUnityUtilities;
using System;

namespace MicroGates.Resizable {
    public class ResizableXorGate : ResizableGatePrefab {
        public override Color24 color_t => new Color24(29, 47, 142);
        public override string ComponentTextID => "MicroGates.ResizableXorGate";
    }
}
