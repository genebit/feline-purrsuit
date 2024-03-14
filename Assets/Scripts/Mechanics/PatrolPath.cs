#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Mechanics
{
    /// <summary>
    /// This component is used to create a patrol path, two points which enemies will move between.
    /// </summary>
    public partial class PatrolPath : MonoBehaviour
    {
        /// <summary>
        /// One end of the patrol path.
        /// </summary>
        public Vector2 startPosition, endPosition;

        /// <summary>
        /// Create a Mover instance which is used to move an entity along the path at a certain speed.
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public Mover CreateMover(float speed = 1) => new Mover(this, speed);

        void Reset()
        {
            startPosition = Vector3.left;
            endPosition = Vector3.right;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(PatrolPath))]
    public class PatrolPathGizmo : Editor
    {
        public void OnSceneGUI()
        {
            var path = target as PatrolPath;
            using (var cc = new EditorGUI.ChangeCheckScope())
            {
                var sp = path.transform.InverseTransformPoint(Handles.PositionHandle(path.transform.TransformPoint(path.startPosition), path.transform.rotation));
                var ep = path.transform.InverseTransformPoint(Handles.PositionHandle(path.transform.TransformPoint(path.endPosition), path.transform.rotation));
                if (cc.changed)
                {
                    sp.y = 0;
                    ep.y = 0;
                    path.startPosition = sp;
                    path.endPosition = ep;
                }
            }
            Handles.Label(path.transform.position, (path.startPosition - path.endPosition).magnitude.ToString());
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        static void OnDrawGizmo(PatrolPath path, GizmoType gizmoType)
        {
            var start = path.transform.TransformPoint(path.startPosition);
            var end = path.transform.TransformPoint(path.endPosition);
            Handles.color = Color.yellow;
            Handles.DrawDottedLine(start, end, 5);
            Handles.DrawSolidDisc(start, path.transform.forward, 0.1f);
            Handles.DrawSolidDisc(end, path.transform.forward, 0.1f);
        }
    }
#endif
}