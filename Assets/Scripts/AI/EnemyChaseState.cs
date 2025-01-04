using UnityEngine;

namespace Mechadroids {
    public class EnemyChaseState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private Transform playerTransform;

        public EnemyChaseState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform playerTransform) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerTransform = playerTransform;
        }

        public void Enter() {
            // dafjiaefj
        }

        public void LogicUpdate() {
            if(IsPlayerInDetectionRange()) {
                MoveTowardsPlayer();
            } else {
                TransitionToIdleState();
            }
        }

        public void PhysicsUpdate() {
            // No physics updates needed in idle state
        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToIdleState() {
            Exit();
            entityHandler.EntityState = new EnemyIdleState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }

        private void MoveTowardsPlayer() {
            Vector3 targetPoint = playerTransform.position;
            targetPoint.y = enemyReference.transform.position.y;
            Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;
            // Move towards the target point
            enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;
            // Rotate towards the target point
            RotateTowards(direction);
        }
        private void RotateTowards(Vector3 direction) {
            if(direction.magnitude == 0) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyReference.transform.rotation = Quaternion.Slerp(
                enemyReference.transform.rotation,
                targetRotation,
                enemyReference.enemySettings.enemy.patrolRotationSpeed * Time.deltaTime
            );
        }
        private bool IsPlayerInDetectionRange() {
            if(playerTransform == null) return false;
            float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
            return distance <= enemyReference.enemySettings.enemy.detectionRadius;
        }
    }

}
