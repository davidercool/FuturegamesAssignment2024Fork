using UnityEngine;

namespace Mechadroids {
    public class EnemyIdleState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private Transform playerTransform;
        private float idleDuration = 2f;
        private float idleTimer;

        public EnemyIdleState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform playerTransform) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerTransform = playerTransform;
        }

        public void Enter() {
            idleTimer = 0f;
            // Optionally set idle animation
        }

        public void LogicUpdate() {
            idleTimer += Time.deltaTime;
            if(idleTimer >= idleDuration) {
                TransitionToPatrolState();
            }
            if(IsPlayerInDetectionRange()) {
                TransitionToChaseState();
            };
        }

        public void PhysicsUpdate() {
            // No physics updates needed in idle state
        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }

        private bool IsPlayerInDetectionRange() {
            if(playerTransform == null) return false;
            float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
            return distance <= enemyReference.enemySettings.enemy.detectionRadius;
        }
        private void TransitionToChaseState() {
            Exit();
            entityHandler.EntityState = new EnemyChaseState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }
    }

}
