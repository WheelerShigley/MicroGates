using LogicWorld.Interfaces;
using LogicWorld.Rendering.Dynamics;
using LogicWorld.SharedCode.Components;
using System.Collections.Generic;
using UnityEngine;
using JimmysUnityUtilities;
using System;

namespace MicroGates.Resizable {
    public class ResizableAndGate : ResizableGatePrefab {
        public override Color24 color_t => new Color24(109,25,22);
        public override string ComponentTextID => "MicroGates.ResizableAndGate";

    }
}
