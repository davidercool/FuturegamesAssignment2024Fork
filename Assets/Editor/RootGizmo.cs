using UnityEngine;
using Mechadroids;
using UnityEditor;

[CustomEditor(typeof(RouteVisualizer))]

public class RootGizmo : Editor{
    private Route route;
    private RouteVisualizer visualizer;

    private void OnEnable() {
        visualizer = (RouteVisualizer)target;
        route = visualizer.route;
    }

    /*private void OnSceneGUI() {
        if(route == null  || !route.showGizmo) {
            return;
        }

        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(10, 10, 150, 50));
        if(GUILayout.Button(route.showGizmo ? "Hide Gizmos" : "Show Gizmos")) {
            route.showGizmo = !route.showGizmo;
            SceneView.RepaintAll();
        }
        GUILayout.EndArea();
    }*/

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        if(GUILayout.Button("Toggle Gizmozzz")) {
            route.showGizmo = !route.showGizmo;
        }
    }
}
