using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StageEnter))]
public class StageEnterEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        StageEnter myTarget = (StageEnter)target;        
        EditorGUILayout.LabelField ("Can Enter");
        EditorGUI.indentLevel++;
        myTarget.canEnter [0] = EditorGUILayout.Toggle ("Front", myTarget.canEnter[0]);
        myTarget.canEnter[1] = EditorGUILayout.Toggle ("Back", myTarget.canEnter [1]);
    }

}
