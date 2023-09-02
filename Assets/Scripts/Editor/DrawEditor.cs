using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Draw))]
public class DrawEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Draw draw = (Draw)target;
        if (GUILayout.Button("AdjustHand"))
        {
            draw.AdjustHand();
        }
    }
}
