using LogicAPI.Server.Components;

namespace MicroGates.Server {
	
    public class And : LogicComponent {
		protected override void DoLogicUpdate() {
			bool b_and = true;
			int pin_count = base.Inputs.Count;
			
			//or
			for(int i = 0; i < pin_count; i++) {
				if( !base.Inputs[i].On ) { b_and = false; break; }
			}
			
            base.Outputs[0].On = b_and;
        }
	}
}