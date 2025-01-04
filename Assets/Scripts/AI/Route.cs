using UnityEngine;

namespace Mechadroids {
    // Route points for the AI
    [CreateAssetMenu(menuName = "Mechadroids/RouteSettings", fileName = "Route", order = 0)]
    public class Route : ScriptableObject {
        public int routeId;
        public Vector3 [] routePoints;
        public bool showGizmo = true;

        public void DrawGizmo(Transform parentTransform) {
            if(!showGizmo)
                return;

            Gizmos.color = Color.yellow;

            for(int i = 0; i < routePoints.Length; i++) {
                Vector3 worldPosition = parentTransform.position + routePoints[i];
                Gizmos.DrawSphere(worldPosition, 1f);

                if(i > 0) {
                    Vector3 previousWorldPosition = parentTransform.position + routePoints[i - 1];
                    Gizmos.DrawLine(previousWorldPosition, worldPosition);
                }
            }
        }
    }




}
