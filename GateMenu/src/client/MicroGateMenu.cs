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
using EccsWindowHelper.Client;
using EccsWindowHelper.Client.Prefabs;
using JimmysUnityUtilities;
using LogicLocalization;
using LogicUI.HoverTags;
using LogicUI.MenuParts.Toggles;
using LogicUI.MenuTypes;
using LogicUI.MenuTypes.ConfigurableMenus;
using HarmonyLib;
using System.Reflection;

namespace MicroGates.Shared {
    public interface IGateMenu {
        GameObject gameObject { get; }
    }

    public class MicroGateMenu : ClientMod {
		public static ILogicLogger logger;
        public static List<IGateMenu> allmenus = new List<IGateMenu>();
        IGateMenu InputSlider;
        
        public static void RegisterMenu(IGateMenu menu) { allmenus.Add(menu); }

		protected override void Initialize() {
			logger = Logger;
            var type = typeof(ChairMenu).Assembly.GetType("LogicWorld.UI.ComponentMenusManager");
            var method = type.GetMethod("Reset", BindingFlags.Static | BindingFlags.NonPublic);
            var postfix = new HarmonyMethod(typeof(MicroGateMenu).GetMethod("ReinitializeMenus", BindingFlags.Static | BindingFlags.Public));
            new Harmony("GateMenuPatcher").Patch(method, null, postfix);
        }

        public static void ReinitializeMenus() {
            var type = typeof(IGateMenu);
            allmenus[0].gameObject.GetComponent<EditComponentMenu>().Initialize();
        }
	}
}
