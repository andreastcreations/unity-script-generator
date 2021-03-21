using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer of the Declaration struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(DeclarationStruct))]
    public class DeclarationStructDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 9.2f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty nameSpaceProp = property.FindPropertyRelative("nameSpace");
            SerializedProperty accessModifierProp = property.FindPropertyRelative("accessModifier");
            SerializedProperty typeModifierProp = property.FindPropertyRelative("typeModifier");
            SerializedProperty typeProp = property.FindPropertyRelative("type");
            SerializedProperty scriptNameProp = property.FindPropertyRelative("scriptName");
            SerializedProperty inheritsFromProp = property.FindPropertyRelative("inheritsFrom");
            SerializedProperty inheritsFromCustomProp = property.FindPropertyRelative("inheritsFromCustom");
            SerializedProperty summaryProp = property.FindPropertyRelative("summary");

            float height = EditorGUIUtility.singleLineHeight;
            float widthDivByThree = (position.width / 3) - 2;

            Rect nameSpace = new Rect(position.x, position.y, position.width, height);
            Rect accessModifier = new Rect(position.x, position.y + 20, widthDivByThree, height);
            Rect typeModifier = new Rect(position.x + widthDivByThree + 3, position.y + 20, widthDivByThree, height);
            Rect type = new Rect(position.x + (2 * (widthDivByThree + 3)), position.y + 20, widthDivByThree, height);
            Rect scriptName = new Rect(position.x, position.y + 40, position.width, height);

            EditorGUI.PropertyField(nameSpace, nameSpaceProp, new GUIContent("Namespace"));
            EditorGUI.PropertyField(accessModifier, accessModifierProp, GUIContent.none);
            EditorGUI.PropertyField(typeModifier, typeModifierProp, GUIContent.none);
            EditorGUI.PropertyField(type, typeProp, GUIContent.none);
            EditorGUI.PropertyField(scriptName, scriptNameProp);

            Rect inheritsFrom;
            Rect inheritsFromCustom;

            if (inheritsFromProp.enumValueIndex == 4)
            {
                inheritsFrom = new Rect(position.x, position.y + 60, 2 * widthDivByThree, height);
                inheritsFromCustom = new Rect(position.x + (2 * (widthDivByThree + 3)), position.y + 60, widthDivByThree, height);

                EditorGUI.PropertyField(inheritsFrom, inheritsFromProp, new GUIContent("Inheritance"));
                EditorGUI.PropertyField(inheritsFromCustom, inheritsFromCustomProp, GUIContent.none);
            }
            else
            {
                inheritsFrom = new Rect(position.x, position.y + 60, position.width, height);

                EditorGUI.PropertyField(inheritsFrom, inheritsFromProp, new GUIContent("Inheritance"));
            }

            Rect summary = new Rect(position.x, position.y + 90, position.width, 4 * height);

            EditorGUI.PropertyField(summary, summaryProp, new GUIContent("Summary (XML Documentation)"));

            EditorGUI.EndProperty();
        }
    }
}
