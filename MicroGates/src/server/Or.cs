using LogicAPI.Server.Components;

namespace MicroGates.Server {
	
    public class Or : LogicComponent {
		protected override void DoLogicUpdate() {
			bool b_or = false;
			int pin_count = base.Inputs.Count;
			
			//or
			for(int i = 0; i < pin_count; i++) {
				if( base.Inputs[i].On ) { b_or = true; break; }
			}
			
            base.Outputs[0].On = b_or;
        }
	}
}