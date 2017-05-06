﻿using UnityEngine;

namespace Fudo {
    public class PlayerManager : Singleton<PlayerManager> {
        protected PlayerManager() { }

        ComponentManager componentManager;
        EntityManager entityManager;
        public GameObject playerPrefab;

        Transform root;

        public override void Init() {
            root = GameObject.Find("Root").transform;
        }

        public override void ReferenceManager() {
            componentManager = ComponentManager.Instance;
            entityManager = EntityManager.Instance;
        }

        public void CreatePlayer() {
            //GameObject go = new GameObject();
            //Entity entity = go.AddComponent(typeof(Entity)) as Entity;
            GameObject go = Instantiate(playerPrefab);
            Entity entity = go.GetComponent<Entity>();
            go.name = "Player";
            go.transform.parent = root;

            entity.id = entityManager.GenerateEntityID();
            entityManager.entities.Add(entity.id, entity);

            componentManager.entityGameObjects.Add(entity.id, go);
            componentManager.entityTransforms.Add(entity.id, go.transform);
            componentManager.rigidbodies.Add(entity.id, go.GetComponent<Rigidbody>());
            componentManager.positions.Add(entity.id, Vector3.zero);
            entity.components.Add(Enums.ComponentType.Position);
            componentManager.movementComponents.Add(entity.id, new Components.Movement());
            entity.components.Add(Enums.ComponentType.Movement);
            componentManager.rotations.Add(entity.id, new Quaternion());
            entity.components.Add(Enums.ComponentType.Rotation);
            componentManager.directions.Add(entity.id, new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)));
            entity.components.Add(Enums.ComponentType.Direction);
            componentManager.maxSpeeds.Add(entity.id, Random.Range(1, 3));
            entity.components.Add(Enums.ComponentType.MaxSpeed);

            int random = Random.Range(0, 4);
            if(random > 1) {
                componentManager.controllables.Add(entity.id, new Components.Controllable());
                entity.components.Add(Enums.ComponentType.Rotation);
            }

            //move to acceleration logic
            componentManager.movementComponents[entity.id].velocity = componentManager.rotations[entity.id] *
                componentManager.directions[entity.id].normalized * componentManager.maxSpeeds[entity.id];
        }
    }
}