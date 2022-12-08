using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public enum LevelMode
    {
        Default,
        Boss,
        Princess,
        Bonus
    }

    public LevelMode levelMode;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        levelMode = (LevelMode)EditorGUILayout.EnumPopup("Mode", levelMode);

        EditorGUILayout.Space();

        switch (levelMode)
        {
            case LevelMode.Boss:
                DisplayBossLevel();
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerStrength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Towers"));

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayBossLevel()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("giantBossID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("giantBossStrength"));
    }
}