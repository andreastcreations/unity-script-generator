using UnityEditor;
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// The editor script of the Script Template scriptable object.
    /// </summary>
    [CustomEditor(typeof(ScriptTemplate))]
    public class ScriptTemplateEditor : Editor
    {
        private ScriptTemplate _scriptTemplate;

        private SerializedProperty _declarationStruct;
        private SerializedProperty _variablesStruct;
        private SerializedProperty _customCodeStruct;
        private SerializedProperty _unityMethodsStruct;
        private SerializedProperty _customMethodsStruct;

        private bool showCustomCode = false;

        private static int tabsTop = 0;
        private string[] tabTitles = new string[] { "Declarations", "Unity Methods", "Custom Methods" };

        void OnEnable()
        {
            _scriptTemplate = (ScriptTemplate)target;

            _declarationStruct = serializedObject.FindProperty("_declarationStruct");
            _variablesStruct = serializedObject.FindProperty("_variablesStruct");
            _customCodeStruct = serializedObject.FindProperty("_customCodeStruct");
            _unityMethodsStruct = serializedObject.FindProperty("_unityMethodsStruct");
            _customMethodsStruct = serializedObject.FindProperty("_customMethodsStruct");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.Space(10);

            using (var boxLast = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button("Create Script"))
                {
                    if (_scriptTemplate.DeclarationStructProperty.GetScriptName() == string.Empty)
                    {
                        Debug.LogWarning("Add a name to your script!");
                        return;
                    }

                    ScriptGenerator.GenerateScript(_scriptTemplate);
                }
                if (GUILayout.Button("Reset"))
                {
                    Reset();
                }
            }

            GUILayout.Space(10);

            using (var box = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.PropertyField(_declarationStruct, true);
            }

            GUILayout.Space(20);

            using (var box = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                tabsTop = GUILayout.Toolbar(tabsTop, tabTitles);

                switch (tabsTop)
                {
                    case 0:
                        EditorGUILayout.PropertyField(_variablesStruct, new GUIContent("Add Variables"));

                        showCustomCode = EditorGUILayout.BeginFoldoutHeaderGroup(showCustomCode, "Add Custom Code", EditorStyles.foldoutHeader);
                        if (showCustomCode)
                        {
                            EditorGUILayout.PropertyField(_customCodeStruct);
                        }
                        
                        break;
                    case 1:
                        EditorGUILayout.PropertyField(_unityMethodsStruct);
                        break;
                    case 2:
                        EditorGUILayout.PropertyField(_customMethodsStruct);
                        break;
                }
            }

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                Repaint();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void Reset()
        {
            if (_scriptTemplate != null)
            {
                _scriptTemplate.Reset();
            }
        }
    }
}
