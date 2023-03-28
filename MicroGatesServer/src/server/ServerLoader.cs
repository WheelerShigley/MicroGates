using LogicAPI.Server;
using LogicLog;

namespace MicroGates.Server {
    public class ServerLoader : ServerMod {
        protected override void Initialize() {
            Logger.Info("mod initialized");
        }
    }
}