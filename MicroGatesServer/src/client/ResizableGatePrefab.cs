using LogicWorld.Interfaces;
using LogicWorld.Rendering.Dynamics;
using LogicWorld.SharedCode.Components;
using System.Collections.Generic;
using UnityEngine;
using JimmysUnityUtilities;
using System;

namespace MicroGates.Resizable {
    public abstract class ResizableGatePrefab : PrefabVariantInfo {
        public override string ComponentTextID => "";
        
        public abstract Color24 color_t{ get; }

        //default data values
        public override PrefabVariantIdentifier GetDefaultComponentVariant() {
            //public PrefabVariantIdentifier(int inputCount, int outputCount)
            return new PrefabVariantIdentifier(2, 1);
        }

        public override ComponentVariant GenerateVariant(PrefabVariantIdentifier identifier) {
            Block prefabBlock = new Block { RawColor = color_t };

            int inputCount = identifier.InputCount;
            float size = (float)((inputCount-1)/2)+1.0f;

            PlacingRules placingRules = new PlacingRules();
            placingRules.AllowFineRotation = false;

            List<ComponentOutput> outputs = new List<ComponentOutput>();
            //Generate all the outputs
            outputs.Add(new ComponentOutput {
                    Position = new Vector3((float)(size)/-2.0f+0.5f, 0.3f, 0.2499f),
                    Rotation = new Vector3(0.0f, 0.0f, 0.0f),
            });

            //Generate the chip select and write lines
            List<ComponentInput> inputs = new List<ComponentInput>();
            //Create data-input pins
            {
                if(inputCount % 2 == 0) {
                    for (int i = 0; i < inputCount; i++) {
                        inputs.Add(new ComponentInput {
                            Position = new Vector3( (float)(inputCount)/-2.0f + 0.5f*(float)(i) + 0.75f, 0.5f, -0.333f),
                            Rotation = new Vector3(0.0f, 0.0f, 0.0f),
                            Length = 0.4f,
                        });
                    }
                }
                if(inputCount % 2 == 1 && inputCount != 1) {
                    for (int i = 0; i < inputCount; i++) {
                        inputs.Add(new ComponentInput {
                            Position = new Vector3( (float)(inputCount)/-2.0f + 0.5f*(float)(i) + 0.5f*(float)(i)/(float)(inputCount-1) + 0.25f, 0.5f, -0.333f),
                            Rotation = new Vector3(0.0f, 0.0f, 0.0f),
                            Length = 0.4f,
                        });
                    }
                }
                if(inputCount == 1) {
                    inputs.Add(new ComponentInput {
                            Position = new Vector3(0.0f, 0.5f, -0.333f),
                            Rotation = new Vector3(0.0f, 0.0f, 0.0f),
                            Length = 0.4f,
                        });
                }
            }

            prefabBlock.Scale       = new Vector3(size, 0.5f, 1.0f);
            prefabBlock.Position    = new Vector3(-0.5f*(float)((int)(size+1.0f))+1.0f, 0.0f, 0.0f);
            prefabBlock.Rotation    = new Vector3(0.0f, 180.0f, 0.0f);

            placingRules.GridPlacingDimensions = new Vector2Int(1, 1);
            return new ComponentVariant {
                VariantPlacingRules = placingRules,
                VariantPrefab = new Prefab {
                    Blocks = new Block[] { prefabBlock },
                    Outputs = outputs.ToArray(),
                    Inputs = inputs.ToArray()
                }
            };
        }
    }
}
