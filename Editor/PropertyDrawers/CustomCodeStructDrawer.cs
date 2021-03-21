using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer of the Custom Code struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomCodeStruct))]
    public class CustomCodeStructDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 13.7f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty customCodeProp = property.FindPropertyRelative("customCode");

            float height = EditorGUIUtility.singleLineHeight;

            Rect customCode = new Rect(position.x, position.y, position.width, 13 * height);

            EditorGUI.PropertyField(customCode, customCodeProp, new GUIContent("(What you see is what you get)"));

            EditorGUI.EndProperty();
        }
    }
}
