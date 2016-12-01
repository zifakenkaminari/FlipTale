using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(StageTrigger))]
public class StageTriggerEditor : Editor {

    bool isHorizontal, isVertical;


    public override void OnInspectorGUI () {
        DrawDefaultInspector ();

        EditorGUILayout.LabelField ("Direction");
        EditorGUI.indentLevel++;
        StageTrigger myTarget = (StageTrigger)target;
        isHorizontal = EditorGUILayout.Toggle ("Horizontal", isHorizontal);
        isVertical = EditorGUILayout.Toggle ("Vertical", isVertical);

        if (myTarget.getDirection () == 0) {        // horizontal
            if (isVertical) {
                myTarget.setDirection (1);
                isHorizontal = false;
            } else {
                isHorizontal = true;
            }
        } 
        else if (myTarget.getDirection () == 1) {
            if (isHorizontal) {
                myTarget.setDirection (0);
                isVertical = false;
            } else {
                isVertical = true;
            }
        }
        else {
            Debug.Assert (false);
        }
            
        if (GUI.changed) {
            EditorUtility.SetDirty ((StageTrigger)target);
            EditorSceneManager.MarkSceneDirty (SceneManager.GetActiveScene());
        }
    }
}
