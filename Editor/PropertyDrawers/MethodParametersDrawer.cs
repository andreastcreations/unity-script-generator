using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer for the Method Parameters struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(MethodParameters))]
    public class MethodParametersDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2.5f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty parameterModifierProp = property.FindPropertyRelative("parameterModifier");
            SerializedProperty returnTypeProp = property.FindPropertyRelative("returnType");
            SerializedProperty nameProp = property.FindPropertyRelative("name");

            float height = EditorGUIUtility.singleLineHeight;
            float widthDivByThree = (position.width / 3) - 2;

            Rect parameterModifierLabel = new Rect(position.x, position.y, widthDivByThree, height);
            Rect returnTypeLabel = new Rect(position.x + widthDivByThree + 3, position.y, widthDivByThree, height);
            Rect nameLabel = new Rect(position.x + 2 * (widthDivByThree + 3), position.y, widthDivByThree, height);
            Rect parameterModifier = new Rect(position.x, position.y + 20, widthDivByThree, height);
            Rect returnType = new Rect(position.x + widthDivByThree + 3, position.y + 20, widthDivByThree, height);
            Rect name = new Rect(position.x + 2 * (widthDivByThree + 3), position.y + 20, widthDivByThree, height);

            EditorGUI.LabelField(parameterModifierLabel, "Modifier");
            EditorGUI.LabelField(returnTypeLabel, "Type");
            EditorGUI.LabelField(nameLabel, "Name");
            EditorGUI.PropertyField(parameterModifier, parameterModifierProp, GUIContent.none);
            EditorGUI.PropertyField(returnType, returnTypeProp, GUIContent.none);
            EditorGUI.PropertyField(name, nameProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
