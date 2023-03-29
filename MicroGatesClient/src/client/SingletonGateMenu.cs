using LogicAPI.Client;
using LogicLog;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LogicWorld;
using LogicWorld.UI;
using LICC;
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
using LogicWorld.GameStates;
using TMPro;
using LogicUI.MenuParts;
using System.IO;
using LogicWorld.BuildingManagement;

using System.Collections.Generic;
using MicroGates.Client;
using MicroGates.Shared;

namespace MicroGates.Client {
    //Based off of: https://github.com/Ecconia/Ecconia-LogicWorld-Mods
    public class SingletonGateMenu : ToggleableSingletonMenu<SingletonGateMenu>, IGateMenu {
        public static GameObject contentPlane;
        public static GameObject widthPegSliderTransform;
        public static InputSlider widthPegSlider;

        public static void initialize() {
            contentPlane = constructContent();
            WindowBuilder wb = new WindowBuilder {
                x = 0, y = 0, w = 0, h = 0,
                rootName = "GateMenu",
                titleKey = "GateMenu.EditGateMenu",
                contentPlane = contentPlane,
                singletonClass = typeof(SingletonGateMenu),
            };
            var controller = wb.build();
            WindowBuilder.updateContentPlane(contentPlane);
            Instance.gameObject.AddComponent<GateMenu>().SetupListener();
            MicroGates.Shared.MicroGateMenu.RegisterMenu(Instance);
            OnMenuHidden += GameStateManager.TransitionBackToBuildingState;
        }

        private static GameObject constructContent() {
            GameObject gameObject = WindowHelper.makeGameObject("GateMenu: ContentPlane");
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            {
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.sizeDelta = new Vector2(10, 10);
                rectTransform.anchoredPosition = new Vector2(0, 0);
            }

            ContentSizeFitter fitter = gameObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            VerticalLayoutGroup verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childForceExpandHeight = false;
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childForceExpandWidth = false;
            verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.spacing = 20;
            //Now here is where we add the prefab children
            //Fixed height toggle thing
            GameObject heightThingy = WindowHelper.makeGameObject("GateMenu: Toggle Height");
            RectTransform rectTransform2 = heightThingy.AddComponent<RectTransform>();
            {
                rectTransform2.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform2.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform2.pivot = new Vector2(0.5f, 0.5f);
                rectTransform2.sizeDelta = new Vector2(12.5f, 12.5f); //define size here
                rectTransform2.anchoredPosition = new Vector2(0, 0);
            }
            heightThingy.SetActive(true);
            gameObject.addChild(heightThingy);
            //Add slider: generateNamedSlider("CRM.BitWidth", fontSize?, TitleOffset, SliderLength, 1, out widthPegSlider);
            widthPegSliderTransform = Prefabs.NamedSliderPrefab.generateNamedSlider("GateMenu.Size", 360, 360 + 20*("Input Count".Length), 360, 50, out widthPegSlider);
            widthPegSlider.SliderInterval = 1.0f;
            widthPegSlider.Min = 2.0f;
            widthPegSlider.Max = 64.0f;
            gameObject.addChild(widthPegSliderTransform);

            gameObject.SetActive(true);
            return gameObject;
        }
    }

    public class GateMenu : EditComponentMenu {
        bool is_resizable = false;
        protected override void OnStartEditing() {
            if (FirstComponentBeingEdited.ClientCode is MicroGates.Resizable.ResizableVoidClient) {
                SingletonGateMenu.widthPegSlider.SetValueWithoutNotify((float)FirstComponentBeingEdited.Component.Data.OutputCount);
                is_resizable = true;
            } else {
                SingletonGateMenu.widthPegSlider.SetValueWithoutNotify((float)FirstComponentBeingEdited.Component.Data.OutputCount);
                is_resizable = false;
            }
            base.OnStartEditing();
        }
        public void SetupListener() {
            SingletonGateMenu.widthPegSlider.OnValueChangedInt += WidthPegSlider_OnValueChangedInt;
        }

        private void WidthPegSlider_OnValueChangedInt(int inputCount) {
            if (!is_resizable) return;
            BuildRequestManager.SendBuildRequest(new BuildRequest_ChangeDynamicComponentPegCounts(FirstComponentBeingEdited.Address, inputCount, 1), null);
        }

        protected override IEnumerable<string> GetTextIDsOfComponentTypesThatCanBeEdited() {
            return new string[] { "MicroGates.ResizableOrGate", "MicroGates.ResizableAndGate", "MicroGates.ResizableXorGate" };
        }
    }
}
