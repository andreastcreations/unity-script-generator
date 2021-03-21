namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of any custom method in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct CustomMethodsStruct
    {
        public AccessModifierPopUp accessModifier;
        public MethodModifierPopUp methodModifier;
        public string returnType;
        public string name;
        public MethodParameters[] methodParameters;
        public bool hasCustomCode;
        public CustomCodeStruct customCodeStruct;

        /// <summary>
        /// Similar to Unity's OnValidate. It is called in an OnValidate of a MonoBehaviour.
        /// </summary>
        public void OnValidate(bool isStatic = false)
        {
            if (!hasCustomCode)
            {
                customCodeStruct.customCode = string.Empty;
            }

            if (isStatic)
            {
                methodModifier = MethodModifierPopUp.Static;
            }
        }

        /// <summary>
        /// Gets the full declaration of a method.
        /// </summary>
        /// <param name="index">This exists in case we have a list of unnamed methods.</param>
        /// <returns>The full declaration of a method in a <see cref="string"/> format.</returns>
        public string GetMethodString(int index = 0)
        {
            if (returnType == string.Empty)
            {
                returnType = "void";
            }

            if (name == string.Empty)
            {
                name = $"UnnamedMethod{index}";
            }
            else
            {
                name = name.RemoveWhiteSpace();
            }

            string parametersString = GetAllMethodParametersString();

            if (methodModifier != MethodModifierPopUp.None)
            {
                return $"{GetAccessModifierString()} {GetMethodModifierString()} {returnType} {name}({parametersString})";
            }
            else
            {
                return $"{GetAccessModifierString()} {returnType} {name}({parametersString})";
            }
        }

        /// <summary>
        /// Gets all the parameters of the method.
        /// </summary>
        /// <returns>All the parameters of the method in a <see cref="string"/> format.</returns>
        private string GetAllMethodParametersString()
        {
            if (methodParameters == null || methodParameters.Length == 0)
            {
                return string.Empty;
            }

            string parameters = string.Empty;

            for (int i = 0; i < methodParameters.Length; i++)
            {
                if (i == 0)
                {
                    parameters = methodParameters[i].GetMethodParameterString(i);
                }
                else
                {
                    parameters += $", {methodParameters[i].GetMethodParameterString(i)}";
                }
            }

            return parameters;
        }

        /// <summary>
        /// Gets the access modifier of the method.
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
        /// Gets any additional modifier of the method.
        /// </summary>
        /// <returns>The additional modifier in a <see cref="string"/> format.</returns>
        private string GetMethodModifierString()
        {
            if (methodModifier == MethodModifierPopUp.SealedOverride)
            {
                return "sealed override";
            }
            else if (methodModifier == MethodModifierPopUp.None)
            {
                return string.Empty;
            }
            else // all the rest
            {
                return methodModifier.ToString().ToLower();
            }
        }
    }
#endif
}
