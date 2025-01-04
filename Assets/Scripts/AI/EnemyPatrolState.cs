using UnityEngine;

namespace Mechadroids {
    public class EnemyPatrolState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private int currentPatrolIndex = 0;
        private Transform playerTransform;

        public EnemyPatrolState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform playerTransform) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerTransform = playerTransform;
        }

        public void Enter() {
        }

        public void LogicUpdate() {
            if(IsPlayerInDetectionRange()) {
                TransitionToChaseState();
            } else {
                MoveTowardsPatrolPoint();
            }
        }

        public void PhysicsUpdate() {

        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToChaseState() {
            Exit();
            entityHandler.EntityState = new EnemyChaseState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }

        private void SetNextPatrolDestination() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
            currentPatrolIndex %= enemyReference.enemySettings.routeSettings.routePoints.Length;
        }
        private void MoveTowardsPatrolPoint() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
            Vector3 targetPoint = enemyReference.enemySettings.routeSettings.routePoints[currentPatrolIndex];
            targetPoint.y = enemyReference.transform.position.y;
            Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;
            // Move towards the target point
            enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;
            // Rotate towards the target point
            RotateTowards(direction);
            // Check if the enemy has reached the patrol point
            if(Vector3.Distance(enemyReference.transform.position, targetPoint) <= 0.1f) {
                currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.enemySettings.routeSettings.routePoints.Length;
                SetNextPatrolDestination();
            }
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
