using LogicAPI.Server.Components;

namespace MicroGates.Server {
	
    public class Xor : LogicComponent {
        protected override void DoLogicUpdate() {
			bool b_xor = false;
			int pin_count = base.Inputs.Count;
			
			//xor
			for(int i = 0; i < pin_count; i++) {
				if( base.Inputs[i].On ) {
					if(b_xor) {
						b_xor = false; break;
					} else {
						b_xor = true;
					}
				}
			}
			
            base.Outputs[0].On = b_xor;
        }
    }
}