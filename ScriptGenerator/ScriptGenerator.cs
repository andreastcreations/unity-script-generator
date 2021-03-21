#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The static class that generates the C# script file.
    /// </summary>
    /// <remarks>
    /// Most variable and method names in this class are self-explainatory.
    /// This is why this script doesn't have many comments. It just prints strings on a script in a way that looks good.
    /// Nothing fancy.
    /// </remarks>
    public static class ScriptGenerator
    {
        // These static variables are used to add indentation or any space between variables, properties and methods.
        // We want our script to look nice.
        public static bool hasNamespaceAdded;
        public static bool hasVariablesOrProperties;
        public static bool hasCustomCodeOutsideMethods;
        public static bool hasUnityMethods;
        public static bool hasCustomMethods;

        /// <summary>
        /// The main static method that is used from the editor script to generate the script.
        /// </summary>
        /// <param name="scriptTemplate">The script template we want to generate.</param>
        public static void GenerateScript(ScriptTemplate scriptTemplate)
        {
            string scriptName = scriptTemplate.DeclarationStructProperty.GetScriptName();
            string copyPath = $"{Application.dataPath}/{scriptName}.cs";

            if (File.Exists(copyPath))
            {
                Debug.LogWarning($"The file with name {scriptName}.cs already exists on that path!");
                return;
            }

            hasNamespaceAdded = scriptTemplate.DeclarationStructProperty.HasNamespaceAdded();
            hasVariablesOrProperties = scriptTemplate.VariablesStructProperty != null && scriptTemplate.VariablesStructProperty.Count != 0;
            hasCustomCodeOutsideMethods = !string.IsNullOrEmpty(scriptTemplate.CustomCodeStructProperty.customCode);
            hasUnityMethods = scriptTemplate.UnityMethodsStructProperty != null && scriptTemplate.UnityMethodsStructProperty.Count > 0;
            hasCustomMethods = scriptTemplate.CustomMethodsStructProperty != null && scriptTemplate.CustomMethodsStructProperty.Count > 0;

            using (StreamWriter script = new StreamWriter(copyPath))
            {
                AddUsingStatements(script, scriptTemplate);
                AddScriptBeginning(script, scriptTemplate);
                AddVariables(script, scriptTemplate);
                AddCustomCodeOutsideMethods(script, scriptTemplate);
                AddDefaultMethods(script, scriptTemplate);
                AddCustomMethods(script, scriptTemplate);
                AddSpaceInCaseScriptIsEmpty(script);
                AddScriptEnding(script);
            }

            AssetDatabase.Refresh();
        }

        public static void AddUsingStatements(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            if (scriptTemplate.DeclarationStructProperty.IsEditor())
            {
                script.WriteLine("using UnityEditor;");
            }
            script.WriteLine("using UnityEngine;");
            script.WriteLine("");
        }

        public static void AddScriptBeginning(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            string tab;

            if (hasNamespaceAdded)
            {
                tab = "\t";

                script.WriteLine($"namespace {scriptTemplate.DeclarationStructProperty.nameSpace}");
                script.WriteLine("{");
            }
            else
            {
                tab = string.Empty;
            }

            if (scriptTemplate.DeclarationStructProperty.summary != string.Empty)
            {
                AddXMLSummary(script, scriptTemplate, tab);
            }

            script.WriteLine($"{tab}{scriptTemplate.DeclarationStructProperty.GetScriptDeclaration()}");
            script.WriteLine($"{tab}{{");
        }

        public static void AddXMLSummary(StreamWriter script, ScriptTemplate scriptTemplate, string tab)
        {
            script.WriteLine($"{tab}/// <summary>");

            string[] comment = ExtensionMethods.GetTextAreaFieldWithLineBreaks(scriptTemplate.DeclarationStructProperty.summary);

            for (int i = 0; i < comment.Length; i++)
            {
                script.WriteLine($"{tab}/// {comment[i]}");
            }

            script.WriteLine($"{tab}/// </summary>");
        }

        public static void AddScriptEnding(StreamWriter script)
        {
            if (hasNamespaceAdded)
            {
                script.WriteLine("\t}");
                script.WriteLine("}");
            }
            else
            {
                script.WriteLine("}");
            }
        }

        public static void AddVariables(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            if (!hasVariablesOrProperties)
            {
                return;
            }

            string tab = (hasNamespaceAdded) ? "\t\t" : "\t";

            for (int i = 0; i < scriptTemplate.VariablesStructProperty.Count; i++)
            {
                if (scriptTemplate.VariablesStructProperty[i].serrializeOption != SerializeOptionPopUp.None)
                {
                    script.WriteLine($"{tab}{scriptTemplate.VariablesStructProperty[i].GetSerializationOptionString()}");
                }

                script.WriteLine($"{tab}{scriptTemplate.VariablesStructProperty[i].GetVariableString(i)}");
            }

            bool addTab = false;

            for (int i = 0; i < scriptTemplate.VariablesStructProperty.Count; i++)
            {
                if (scriptTemplate.VariablesStructProperty[i].addProperty)
                {
                    // Add space between variables and properties
                    if (!addTab)
                    {
                        script.WriteLine($"{tab}");
                        addTab = true;
                    }

                    script.WriteLine($"{tab}{scriptTemplate.VariablesStructProperty[i].GetPropertyString(i)}");
                }
            }
        }

        public static void AddCustomCodeOutsideMethods(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            if (!hasCustomCodeOutsideMethods)
            {
                return;
            }

            string tab = (hasNamespaceAdded) ? "\t\t" : "\t";

            if (hasVariablesOrProperties)
            {
                script.WriteLine($"{tab}");
            }

            string[] customCode = ExtensionMethods.GetTextAreaFieldWithLineBreaks(scriptTemplate.CustomCodeStructProperty.customCode);

            for (int i = 0; i < customCode.Length; i++)
            {
                script.WriteLine($"{tab}{customCode[i]}");
            }
        }

        public static void AddDefaultMethods(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            string tab = (hasNamespaceAdded) ? "\t\t" : "\t";

            if (!hasUnityMethods)
            {
                return;
            }

            if (hasVariablesOrProperties || hasCustomCodeOutsideMethods)
            {
                script.WriteLine($"{tab}");
            }

            for (int i = 0; i < scriptTemplate.UnityMethodsStructProperty.Count; i++)
            {
                script.WriteLine($"{tab}{scriptTemplate.UnityMethodsStructProperty[i].GetMethodString()}");
                script.WriteLine($"{tab}{{");

                if (scriptTemplate.UnityMethodsStructProperty[i].hasCustomCode)
                {
                    string[] customCode = ExtensionMethods.GetTextAreaFieldWithLineBreaks(
                                                           scriptTemplate.UnityMethodsStructProperty[i].customCodeStruct.customCode
                                                           );

                    for (int j = 0; j < customCode.Length; j++)
                    {
                        script.WriteLine($"{tab}\t{customCode[j]}");
                    }
                }
                else
                {
                    script.WriteLine($"{tab}\t");
                }

                script.WriteLine($"{tab}}}");
                if (i == scriptTemplate.UnityMethodsStructProperty.Count - 1)
                {
                    return;
                }
                script.WriteLine($"{tab}");
            }
        }
        public static void AddCustomMethods(StreamWriter script, ScriptTemplate scriptTemplate)
        {
            string tab = (hasNamespaceAdded) ? "\t\t" : "\t";

            if (!hasCustomMethods)
            {
                return;
            }

            if (hasVariablesOrProperties || hasCustomCodeOutsideMethods || hasUnityMethods)
            {
                script.WriteLine($"{tab}");
            }

            for (int i = 0; i < scriptTemplate.CustomMethodsStructProperty.Count; i++)
            {
                script.WriteLine($"{tab}{scriptTemplate.CustomMethodsStructProperty[i].GetMethodString(i)}");
                script.WriteLine($"{tab}{{");

                if (scriptTemplate.CustomMethodsStructProperty[i].hasCustomCode)
                {
                    string[] customCode = ExtensionMethods.GetTextAreaFieldWithLineBreaks(
                                                           scriptTemplate.CustomMethodsStructProperty[i].customCodeStruct.customCode
                                                           );

                    for (int j = 0; j < customCode.Length; j++)
                    {
                        script.WriteLine($"{tab}\t{customCode[j]}");
                    }
                }
                else
                {
                    script.WriteLine($"{tab}\t");
                }

                script.WriteLine($"{tab}}}");
                if (i == scriptTemplate.CustomMethodsStructProperty.Count - 1)
                {
                    return;
                }
                script.WriteLine($"{tab}");
            }
        }

        private static void AddSpaceInCaseScriptIsEmpty(StreamWriter script)
        {
            string tab = (hasNamespaceAdded) ? "\t\t" : "\t";

            if (!hasVariablesOrProperties && !hasCustomCodeOutsideMethods && !hasUnityMethods && !hasCustomMethods)
            {
                script.WriteLine($"{tab}");
            }
        }
    }
#endif
}
