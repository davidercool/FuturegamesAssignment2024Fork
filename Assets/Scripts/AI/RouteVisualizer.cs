using UnityEngine;
using Mechadroids;
public class RouteVisualizer : MonoBehaviour
{
    public Route route;
    private void OnDrawGizmos() {
        if(route != null) {
            route.DrawGizmo(transform);
        }
    }

}
