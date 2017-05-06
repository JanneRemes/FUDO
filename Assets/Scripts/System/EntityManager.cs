﻿using UnityEngine;
using System.Collections.Generic;

namespace Fudo {
    public class EntityManager : Singleton<EntityManager> {
        protected EntityManager() { }

        ComponentManager componentManager;

        public Dictionary<int, Entity> entities;
        int nextEntityID = 1;

        public override void Init() {
            entities = new Dictionary<int, Entity>();
        }
        public override void ReferenceManager() {
            componentManager = ComponentManager.Instance;
        }

        public int GenerateEntityID() {
            do {
                if (!entities.ContainsKey(nextEntityID)) {
                    return nextEntityID;
                }
                nextEntityID++;
            } while (true);
        }

        public void DeleteEntity(int id) {
            Entity entity;
            if(entities.TryGetValue(id, out entity)) {
                entities.Remove(id);
                foreach (Enums.ComponentType componentType in entity.components) {
                    switch (componentType) {
                        default:
                            throw new System.Exception("Component deletion not implemented");
                        case Enums.ComponentType.Controllable:
                            componentManager.controllables.Remove(id);
                            break;
                        case Enums.ComponentType.Direction:
                            componentManager.directions.Remove(id);
                            break;
                        case Enums.ComponentType.IsVisible:
                            componentManager.isVisibles.Remove(id);
                            break;
                        case Enums.ComponentType.MaxSpeed:
                            componentManager.maxSpeeds.Remove(id);
                            break;
                        case Enums.ComponentType.Movement:
                            componentManager.movementComponents.Remove(id);
                            break;
                        case Enums.ComponentType.Position:
                            componentManager.positions.Remove(id);
                            break;
                        case Enums.ComponentType.PreviousFrameMovement:
                            componentManager.previousFrameMovementComponents.Remove(id);
                            break;
                        case Enums.ComponentType.Rotation:
                            componentManager.rotations.Remove(id);
                            break;
                        case Enums.ComponentType.Scale:
                            componentManager.scales.Remove(id);
                            break;
                    }
                }
                //Remove all references of Unity components and objects
                componentManager.rigidbodies.Remove(id);
                componentManager.entityTransforms.Remove(id);
                GameObject go;
                if (componentManager.entityGameObjects.TryGetValue(id, out go)) {
                    Destroy(go);
                    componentManager.entityGameObjects.Remove(id);
                    Debug.Log("Deleted gameObject succesfully");
                }
                Debug.Log("Deleted entity succesfully");
            } else {
                Debug.Log("Did not found entity with this id");
            }
        }
    }
}