using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExample fsm = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
            if (fsm.stateMachine.dictState == null) return;

            var keys = fsm.stateMachine.dictState.Keys.ToArray();
            var values = fsm.stateMachine.dictState.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
            }
        }
    }
#endif
}
