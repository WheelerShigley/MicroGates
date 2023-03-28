using LogicAPI.Client;
using LogicLog;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LogicWorld;
using LogicWorld.UI;
using LICC;
using System;
using System.Collections.Generic;
using LogicAPI.Data;
using LogicAPI.Data.BuildingRequests;
using LogicWorld.Building.Overhaul;
using LogicWorld.Interfaces;
using JimmysUnityUtilities;
using LogicLocalization;
using System.Reflection;

//from CheeseMod
namespace MicroGates.Client {
	public class Loader : ClientMod {
		public static ILogicLogger logger;
		
		protected override void Initialize() {
			logger = Logger;
			SingletonGateMenu.initialize();
		}
	}
}