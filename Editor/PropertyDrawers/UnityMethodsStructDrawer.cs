using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer for the Unity Methods struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(UnityMethodsStruct))]
    public class UnityMethodsStructDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float constantHeight = EditorGUIUtility.singleLineHeight * 2.5f;
            float variableHeight = 0f; // Total height depends on the "hasCustomCode" value.

            SerializedProperty hasCustomCodeProp = property.FindPropertyRelative("hasCustomCode");
            SerializedProperty customCodeStructProp = property.FindPropertyRelative("customCodeStruct");

            if (hasCustomCodeProp.boolValue)
            {
                variableHeight += EditorGUI.GetPropertyHeight(customCodeStructProp);
            }

            return constantHeight + variableHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty unityMethodNameProp = property.FindPropertyRelative("unityMethodName");
            SerializedProperty hasCustomCodeProp = property.FindPropertyRelative("hasCustomCode");
            SerializedProperty customCodeStructProp = property.FindPropertyRelative("customCodeStruct");

            float height = EditorGUIUtility.singleLineHeight;

            Rect unityMethodName = new Rect(position.x, position.y, position.width, height);
            Rect hasCustomCode = new Rect(position.x, position.y + 20, position.width, height);
            Rect customCodeStruct = new Rect(position.x, position.y + 40, position.width, EditorGUI.GetPropertyHeight(customCodeStructProp));

            EditorGUI.PropertyField(unityMethodName, unityMethodNameProp, new GUIContent("Method Name"));
            EditorGUI.PropertyField(hasCustomCode, hasCustomCodeProp);
            if (hasCustomCodeProp.boolValue)
            {
                EditorGUI.PropertyField(customCodeStruct, customCodeStructProp);
            }

            EditorGUI.EndProperty();
        }
    }
}
