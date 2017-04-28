#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(StageEnter))]
public class StageEnterEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        StageEnter myTarget = (StageEnter)target;        
        EditorGUILayout.LabelField ("Can Enter");
        EditorGUI.indentLevel++;
        myTarget.canEnter [0] = EditorGUILayout.Toggle ("Front", myTarget.canEnter[0]);
        myTarget.canEnter[1] = EditorGUILayout.Toggle ("Back", myTarget.canEnter [1]);
        if (GUI.changed) {
            EditorUtility.SetDirty (myTarget);
            EditorSceneManager.MarkSceneDirty (SceneManager.GetActiveScene());
        }
    }

}
#endif
