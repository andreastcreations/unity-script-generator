using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of the script declaration field in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct DeclarationStruct
    {
        public string nameSpace;
        public AccessModifierPopUp accessModifier;
        public TypeModifierPopUp typeModifier;
        public TypePopUp type;
        public string scriptName;
        public InheritsFromPopUp inheritsFrom;
        public string inheritsFromCustom;
        [TextArea(3, 10)]
        public string summary;

        /// <summary>
        /// Similar to Unity's OnValidate. It is called in an OnValidate of a MonoBehaviour.
        /// </summary>
        public void OnValidate()
        {
            if (type == TypePopUp.Class)
            {
                if (typeModifier == TypeModifierPopUp.Static)
                {
                    // Cannot inherit from any class except Object
                    if (inheritsFrom == InheritsFromPopUp.MonoBehaviour ||
                        inheritsFrom == InheritsFromPopUp.ScriptableObject ||
                        inheritsFrom == InheritsFromPopUp.Editor)
                    {
                        inheritsFrom = InheritsFromPopUp.None;
                    }
                }
            }

            if (type == TypePopUp.Interface)
            {
                // Can only be public or internal. Set default to public.
                if (accessModifier == AccessModifierPopUp.Private ||
                    accessModifier == AccessModifierPopUp.Protected ||
                    accessModifier == AccessModifierPopUp.ProtectedInternal ||
                    accessModifier == AccessModifierPopUp.PrivateProtected)
                {
                    accessModifier = AccessModifierPopUp.Public;
                }

                // Can not be abstract, partial, sealed or static. Set to none.
                typeModifier = TypeModifierPopUp.None;

                // Can not inherit MonoBehaviour, ScriptableObject or Editor. Set default to none.
                if (inheritsFrom == InheritsFromPopUp.MonoBehaviour ||
                    inheritsFrom == InheritsFromPopUp.ScriptableObject ||
                    inheritsFrom == InheritsFromPopUp.Editor)
                {
                    inheritsFrom = InheritsFromPopUp.None;
                }
            }

            if (type == TypePopUp.ScriptableObject)
            {
                // Can not be static. Set default to none.
                if (typeModifier == TypeModifierPopUp.Static)
                {
                    typeModifier = TypeModifierPopUp.None;
                }

                // Obviously it has to inherit from ScriptableObject.
                inheritsFrom = InheritsFromPopUp.ScriptableObject;
            }

            if (type == TypePopUp.Struct)
            {
                // Can only be public or internal. Set default to public.
                if (accessModifier == AccessModifierPopUp.Private ||
                    accessModifier == AccessModifierPopUp.Protected ||
                    accessModifier == AccessModifierPopUp.ProtectedInternal ||
                    accessModifier == AccessModifierPopUp.PrivateProtected)
                {
                    accessModifier = AccessModifierPopUp.Public;
                }

                // Can only use sealed or none. Set default to none.
                if (typeModifier == TypeModifierPopUp.Abstract ||
                    typeModifier == TypeModifierPopUp.Sealed ||
                    typeModifier == TypeModifierPopUp.Static)
                {
                    typeModifier = TypeModifierPopUp.None;
                }

                // Can not inherit MonoBehaviour, ScriptableObject or Editor. Set default to none.
                if (inheritsFrom == InheritsFromPopUp.MonoBehaviour ||
                    inheritsFrom == InheritsFromPopUp.ScriptableObject ||
                    inheritsFrom == InheritsFromPopUp.Editor)
                {
                    inheritsFrom = InheritsFromPopUp.None;
                }
            }

            if (type == TypePopUp.Enum)
            {
                // Can only be public or internal. Set default to public.
                if (accessModifier == AccessModifierPopUp.Private ||
                    accessModifier == AccessModifierPopUp.Protected ||
                    accessModifier == AccessModifierPopUp.ProtectedInternal ||
                    accessModifier == AccessModifierPopUp.PrivateProtected)
                {
                    accessModifier = AccessModifierPopUp.Public;
                }

                // Can not be abstract, partial, sealed or static. Set to none.
                typeModifier = TypeModifierPopUp.None;

                // Can not inherit from anywhere. Set to none.
                inheritsFrom = InheritsFromPopUp.None;
            }
        }

        /// <summary>
        /// Resets the current struct.
        /// </summary>
        public void ResetDeclarationStruct()
        {
            nameSpace = string.Empty;
            accessModifier = AccessModifierPopUp.Public;
            typeModifier = TypeModifierPopUp.None;
            type = TypePopUp.Class;
            scriptName = string.Empty;
            inheritsFrom = InheritsFromPopUp.MonoBehaviour;
            inheritsFromCustom = string.Empty;
            summary = string.Empty;
        }

        /// <summary>
        /// Check if the current script is included in a <see langword="namespace"/>.
        /// </summary>
        /// <returns><see langword="true"/> or <see langword="false"/>.</returns>
        public bool HasNamespaceAdded() => (nameSpace != null && nameSpace != string.Empty) ? true : false;

        /// <summary>
        /// Check if the current script is an Editor script.
        /// </summary>
        /// <returns><see langword="true"/> or <see langword="false"/>.</returns>
        public bool IsEditor() => (inheritsFrom == InheritsFromPopUp.Editor) ? true : false;

        /// <summary>
        /// Check if the current script is declared as <see langword="static"/>.
        /// </summary>
        /// <returns><see langword="true"/> or <see langword="false"/>.</returns>
        public bool IsStatic() => (typeModifier == TypeModifierPopUp.Static) ? true : false;

        /// <summary>
        /// Gets the name of the script without white space.
        /// </summary>
        /// <returns>The name of the script in a <see cref="string"/> format.</returns>
        public string GetScriptName() => (scriptName != string.Empty) ? scriptName.Replace(" ", string.Empty) : scriptName;

        /// <summary>
        /// Gets the full declaration of the script.
        /// </summary>
        /// <returns>Te declaration of the script in a <see cref="string"/> format.</returns>
        public string GetScriptDeclaration()
        {
            string inheritance = GetInheritanceString();

            if (inheritance != string.Empty)
            {
                if (typeModifier != TypeModifierPopUp.None)
                {
                    return $"{GetAccessModifierString()} {GetTypeModifierString()} {GetTypeString()} {GetScriptName()} : {GetInheritanceString()}";
                }
                else
                {
                    return $"{GetAccessModifierString()} {GetTypeString()} {GetScriptName()} : {GetInheritanceString()}";
                }
            }
            else
            {
                if (typeModifier != TypeModifierPopUp.None)
                {
                    return $"{GetAccessModifierString()} {GetTypeModifierString()} {GetTypeString()} {GetScriptName()}";
                }
                else
                {
                    return $"{GetAccessModifierString()} {GetTypeString()} {GetScriptName()}";
                }
            }
        }

        /// <summary>
        /// Gets the access modifier of the script.
        /// </summary>
        /// <returns>The access modifier in a <see cref="string"/> format.</returns>
        private string GetAccessModifierString()
        {
            if (accessModifier == AccessModifierPopUp.ProtectedInternal)
            {
                return "protected internal";
            }
            else if (accessModifier == AccessModifierPopUp.PrivateProtected)
            {
                return "private protected";
            }
            else // all the rest
            {
                return accessModifier.ToString().ToLower();
            }
        }

        /// <summary>
        /// Gets the type modifier of the script.
        /// </summary>
        /// <returns>The type modifier in a <see cref="string"/> format.</returns>
        private string GetTypeModifierString() => (typeModifier == TypeModifierPopUp.None) ? string.Empty : typeModifier.ToString().ToLower();

        /// <summary>
        /// Gets the type of the script.
        /// </summary>
        /// <returns>The script's type in a <see cref="string"/> format.</returns>
        private string GetTypeString() => (type == TypePopUp.Class || type == TypePopUp.ScriptableObject) ? "class" : type.ToString().ToLower();

        /// <summary>
        /// Gets the inheritance of the script.
        /// </summary>
        /// <returns>The inheritance in a <see cref="string"/> format.</returns>
        private string GetInheritanceString()
        {
            if (inheritsFrom == InheritsFromPopUp.None)
            {
                return string.Empty;
            }
            else if (inheritsFrom == InheritsFromPopUp.Custom)
            {
                return inheritsFromCustom;
            }
            else // all the rest
            {
                return inheritsFrom.ToString();
            }
        }
    }
#endif
}
