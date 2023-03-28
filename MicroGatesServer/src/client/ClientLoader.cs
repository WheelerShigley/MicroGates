using LogicAPI.Client;
using LogicLog;

namespace MicroGates.Resizable {
    public class MicroGateServerClient : ClientMod {
		public static ILogicLogger logger;
		protected override void Initialize() {
			logger = Logger;
        }
	}
}
