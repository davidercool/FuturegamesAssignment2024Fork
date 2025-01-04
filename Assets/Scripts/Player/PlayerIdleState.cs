using UnityEngine;

namespace Mechadroids {
    public class PlayerIdleState : IEntityState {
        private readonly InputHandler inputHandler;
        private readonly PlayerReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;
        private readonly IEntityHandler entityHandler;

        private float currentSpeed;
        private float turretAngle = 0f;
        private float barrelAngle = 0f;

        public PlayerIdleState(
            IEntityHandler entityHandler,
            InputHandler inputHandler,
            PlayerReference playerReference,
            HitIndicator hitIndicatorInstance) {
            this.inputHandler = inputHandler;
            this.playerReference = playerReference;
            this.hitIndicatorInstance = hitIndicatorInstance;
            this.entityHandler = entityHandler;
        }

        public void Enter() {
            Debug.Log("Entering PlayerIdleState");
        }

        public void LogicUpdate() {
            if(inputHandler.MovementInput.magnitude > 0 || inputHandler.MouseDelta.magnitude > 0) {
                TransitionToActiveState();
            }
        }

        public void PhysicsUpdate() {
            // Implement physics update if needed
        }

        public void Exit() {
            // Clean up when exiting the state
        }

        private void TransitionToActiveState() {
            Exit();
            entityHandler.EntityState = new PlayerActiveState(entityHandler, inputHandler, playerReference, hitIndicatorInstance);
            entityHandler.EntityState.Enter();
        }
    }
}
