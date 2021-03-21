using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The property drawer of the Custom Methods struct.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomMethodsStruct))]
    public class CustomMethodsStructDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float constantHeight = EditorGUIUtility.singleLineHeight * 8.5f;
            float variableHeight = 0f; // Total height depends on the methodParametersProp.arraySize and hasCustomCodeProp.boolValue.

            SerializedProperty methodParametersProp = property.FindPropertyRelative("methodParameters");
            SerializedProperty hasCustomCodeProp = property.FindPropertyRelative("hasCustomCode");
            SerializedProperty customCodeStructProp = property.FindPropertyRelative("customCodeStruct");

            if (methodParametersProp.isArray)
            {
                if (methodParametersProp.isExpanded)
                {
                    // The numbers were trial and error. Couldn't find anywhere how this works.
                    if (methodParametersProp.arraySize == 0)
                    {
                        variableHeight = 50f;
                    }
                    else
                    {
                        // There is some weird dependency between the arraySize and the total height.
                        // Again, coundn't find any information on this but it works perfectly.
                        // At first I just added "2" for every array item. Seems like EditorGUIUtility.singleLineHeight = 20.
                        // And by the way, I have no idea if this "2" is pixels or just bananas. Seriously, though...
                        // At least now it looks like it depends on the singleLineHeight and not on an arbitrary number.
                        float h = 0.1f * EditorGUIUtility.singleLineHeight;

                        for (int i = 0; i < methodParametersProp.arraySize; i++)
                        {
                            variableHeight += EditorGUI.GetPropertyHeight(methodParametersProp.GetArrayElementAtIndex(i)) + h;
                        }
                        variableHeight += 30f;
                    }
                }
            }

            if (hasCustomCodeProp.boolValue)
            {
                variableHeight += EditorGUI.GetPropertyHeight(customCodeStructProp);
            }

            return constantHeight + variableHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty accessModifierProp = property.FindPropertyRelative("accessModifier");
            SerializedProperty methodModifierProp = property.FindPropertyRelative("methodModifier");
            SerializedProperty returnTypeProp = property.FindPropertyRelative("returnType");
            SerializedProperty nameProp = property.FindPropertyRelative("name");
            SerializedProperty methodParametersProp = property.FindPropertyRelative("methodParameters");
            SerializedProperty hasCustomCodeProp = property.FindPropertyRelative("hasCustomCode");
            SerializedProperty customCodeStructProp = property.FindPropertyRelative("customCodeStruct");

            string headerLabel = (string.IsNullOrEmpty(nameProp.stringValue)) ? "Unnamed Method": nameProp.stringValue;

            float widthDivByTwo = (position.width / 2) - 2;
            float height = EditorGUIUtility.singleLineHeight;
            float additionalHeight = EditorGUI.GetPropertyHeight(methodParametersProp);

            GUIStyle headerStyle = new GUIStyle(EditorStyles.toolbar)
            {
                fontSize = EditorStyles.foldoutHeader.fontSize,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };

            Rect header = new Rect(position.x, position.y + 5, position.width, height);
            Rect accessModifier = new Rect(position.x, position.y + 35, widthDivByTwo, height);
            Rect methodModifier = new Rect(position.x + widthDivByTwo + 3, position.y + 35, widthDivByTwo, height);
            Rect returnType = new Rect(position.x, position.y + 55, position.width, height);
            Rect name = new Rect(position.x, position.y + 75, position.width, height);
            Rect methodParameters = new Rect(position.x, position.y + 95, position.width, height);
            Rect hasCustomCode = new Rect(position.x, position.y + 105 + additionalHeight, position.width, height);
            Rect customCodeStruct = new Rect(position.x, position.y + 125 + additionalHeight, position.width, EditorGUI.GetPropertyHeight(customCodeStructProp));

            EditorGUI.LabelField(header, headerLabel, headerStyle);
            EditorGUI.PropertyField(accessModifier, accessModifierProp, GUIContent.none);
            EditorGUI.PropertyField(methodModifier, methodModifierProp, GUIContent.none);
            EditorGUI.PropertyField(returnType, returnTypeProp);
            EditorGUI.PropertyField(name, nameProp);
            EditorGUI.PropertyField(methodParameters, methodParametersProp);
            EditorGUI.PropertyField(hasCustomCode, hasCustomCodeProp);
            if (hasCustomCodeProp.boolValue)
            {
                EditorGUI.PropertyField(customCodeStruct, customCodeStructProp);
            }

            EditorGUI.EndProperty();
        }
    }
}
