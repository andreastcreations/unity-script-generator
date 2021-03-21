using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer for the Variables struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(VariablesStruct))]
    public class VariablesStructDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 7.2f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty accessProp = property.FindPropertyRelative("access");
            SerializedProperty typeProp = property.FindPropertyRelative("type");
            SerializedProperty nameProp = property.FindPropertyRelative("name");
            SerializedProperty additionalModifierProp = property.FindPropertyRelative("additionalModifier");
            SerializedProperty serrializeOptionProp = property.FindPropertyRelative("serrializeOption");
            SerializedProperty addPropertyProp = property.FindPropertyRelative("addProperty");
            SerializedProperty propertyOptionProp = property.FindPropertyRelative("propertyOption");

            float height = EditorGUIUtility.singleLineHeight;

            Rect access = new Rect(position.x, position.y, position.width, height);
            Rect type = new Rect(position.x, position.y + 20, position.width, height);
            Rect name = new Rect(position.x , position.y + 40, position.width, height);
            Rect additionalModifier = new Rect(position.x, position.y + 65, position.width, height);
            Rect serrializeOption = new Rect(position.x, position.y + 85, position.width, height);
            
            float widthDivByTwo = (position.width / 2) - 2;

            Rect addProperty = new Rect(position.x, position.y + 105, widthDivByTwo, height);
            Rect propertyOption = new Rect(position.x + widthDivByTwo + 3, position.y + 105, widthDivByTwo, height);

            EditorGUI.PropertyField(access, accessProp);
            EditorGUI.PropertyField(type, typeProp);
            EditorGUI.PropertyField(name, nameProp);
            EditorGUI.PropertyField(additionalModifier, additionalModifierProp, new GUIContent("Additional Modifier"));
            EditorGUI.PropertyField(serrializeOption, serrializeOptionProp, new GUIContent("Serialization Options"));
            EditorGUI.PropertyField(addProperty, addPropertyProp, new GUIContent("Add Property"));
            EditorGUI.PropertyField(propertyOption, propertyOptionProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
