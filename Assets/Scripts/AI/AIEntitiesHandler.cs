using System.Collections.Generic;
using UnityEngine;

namespace Mechadroids {
    // Keeps the state for all AI activity
    public class AIEntitiesHandler {
        private readonly AISettings aiSettings;
        private readonly Transform parentHolder;
        private Transform playerTransform;
        private int dictIndex = 0;

        private Dictionary<int, EnemyEntityHandler> EnemyEntityHandlers { get; } = new();

        public AIEntitiesHandler(AISettings aiSettings, Transform parentHolder) {
            this.aiSettings = aiSettings;
            this.parentHolder = parentHolder;

        }

        public void Initialize() {

            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Object.FindObjectsOfType<EnemySpawner>();
            foreach(EnemySpawner spawner in Object.FindObjectsOfType<EnemySpawner>()) {
                spawner.aiEntitiesHandler = this;
            }
            /*int globalindex = 0;
            foreach(EnemyGroup enemy in aiSettings.enemiesToSpawn) {
                for(int i = 0; i < enemy.enemyCount; i++) {
                    EnemyEntityHandler enemyEntityHandler = new(enemy.enemySettings, parentHolder, playerTransform);
                    enemyEntityHandler.Initialize();
                    EnemyEntityHandlers.TryAdd(dictIndex, enemyEntityHandler);
                    dictIndex++;
                }
            }*/

        }

        public void SpawnEnemyGroup(EnemyGroup group) {
            for(int i = 0; i < group.enemyCount; i++) {
                EnemyEntityHandler enemyEntityHandler = new(group.enemySettings, parentHolder, playerTransform);
                enemyEntityHandler.Initialize();
                EnemyEntityHandlers.TryAdd(dictIndex, enemyEntityHandler);
                dictIndex++;
            }
        }

        public void Tick() {
            // tick all the enemies
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Tick();
            }
        }

        public void PhysicsTick() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.PhysicsTick();
            }
        }

        public void Dispose() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Dispose();
            }
            EnemyEntityHandlers.Clear();
        }
    }
}
