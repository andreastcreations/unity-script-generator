namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of a method's parameter in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct MethodParameters
    {
        public ParameterModifierPopUp parameterModifier;
        public string returnType;
        public string name;

        /// <summary>
        /// Gets the full declaration of the method's parameter.
        /// </summary>
        /// <param name="index">This exists in case we have a list of unnamed parameters.</param>
        /// <returns>The full declaration of the method's parameter in a <see cref="string"/> format.</returns>
        public string GetMethodParameterString(int index = 0)
        {
            string parameter = string.Empty;

            if (returnType == string.Empty)
            {
                returnType = "object";
            }

            if (name == string.Empty)
            {
                name = $"unnamedVariable{index}";
            }
            else
            {
                name = name.RemoveWhiteSpace();
            }

            if (parameterModifier != ParameterModifierPopUp.None)
            {
                parameter = $"{GetParameterModifierString()} {returnType} {name}";
            }
            else
            {
                parameter = $"{returnType} {name}";
            }

            return parameter;
        }

        /// <summary>
        /// Gets the parameter's modifier (<see langword="ref"/>, <see langword="in"/>, <see langword="out"/>).
        /// </summary>
        /// <returns>The parameter's modifier in a <see cref="string"/> format.</returns>
        private string GetParameterModifierString()
        {
            if (parameterModifier != ParameterModifierPopUp.None)
            {
                return parameterModifier.ToString().ToLower();
            }
            else
            {
                return string.Empty;
            }
        }
    }
#endif
}
