namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of a variable field in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct VariablesStruct
    {
        public string access;
        public string type;
        public string name;

        public VariableModifierPopUp additionalModifier;
        public SerializeOptionPopUp serrializeOption;

        public bool addProperty;
        public PropertyOptionPopUp propertyOption;

        /// <summary>
        /// Similar to Unity's OnValidate. It gets called in the OnValidate of a MonoBehaviour class.
        /// </summary>
        public void OnValidate(bool isStatic = false)
        {
            if (isStatic)
            {
                additionalModifier = VariableModifierPopUp.Static;
            }
        }

        /// <summary>
        /// Gets the full declaration of a variable.
        /// </summary>
        /// <param name="index">This exists in case we have a list of unnamed variables.</param>
        /// <param name="isStatic">This is used in case the we declare our class as static.</param>
        /// <returns>The full declaration of a variable in a <see cref="string"/> format.</returns>
        public string GetVariableString(int index = 0, bool isStatic = false)
        {
            if (isStatic)
            {
                additionalModifier = VariableModifierPopUp.Static;
            }

            string additionalModifierString = GetAdditionalModifierString();

            if (access == string.Empty)
            {
                access = "private";
            }

            if (type == string.Empty)
            {
                type = "object";
            }

            if (name == string.Empty)
            {
                name = $"unnamedVariable{index}";
            }
            else
            {
                name = name.RemoveWhiteSpace();
            }
            
            if (additionalModifier != VariableModifierPopUp.None)
            {
                return $"{access} {additionalModifierString} {type} {name};";
            }
            else
            {
                return $"{access} {type} {name};";
            }
        }

        /// <summary>
        /// Gets the full declaration of a property based on a variable name.
        /// </summary>
        /// <param name="index">This exists in case we have a list of unnamed variables.</param>
        /// <param name="isStatic">This is used in case the we declare our class as static.</param>
        /// <returns>The full declaration of a property in a <see cref="string"/> format.</returns>
        public string GetPropertyString(int index = 0, bool isStatic = false)
        {
            string newAccess = "public";
            string newType = (type == string.Empty) ? "object" : type;
            string newName;

            if (name == string.Empty)
            {
                newName = (index < 0) ? "UnnamedProperty" : "UnnamedProperty" + index;
            }
            else
            {
                string tempName = name.ChangeFirstLetterToCaps().RemoveWhiteSpace();
                newName = (index < 0) ? tempName : tempName + index;
            }

            string addGetSet;

            if (propertyOption == PropertyOptionPopUp.PublicGetPublicSet)
            {
                addGetSet = "{ get; set; }";
            }
            else if (propertyOption == PropertyOptionPopUp.PublicGetPrivateSet)
            {
                addGetSet = "{ get; private set; }";
            }
            else
            {
                addGetSet = "{ get; }";
            }

            return $"{newAccess} {newType} {newName} {addGetSet}";
        }

        /// <summary>
        /// Gets the serialization option for a variable.
        /// </summary>
        /// <returns>The serialization of a variable in a <see cref="string"/> format.</returns>
        public string GetSerializationOptionString()
        {
            if (serrializeOption == SerializeOptionPopUp.SerializeField)
            {
                return "[SerializeField]";
            }
            else if (serrializeOption == SerializeOptionPopUp.HideInInspector)
            {
                return "[HideInInspector]";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets any additional modifier addet to the variable.
        /// </summary>
        /// <returns>The additional modifier of a variable in a <see cref="string"/> format.</returns>
        private string GetAdditionalModifierString()
        {
            if (additionalModifier == VariableModifierPopUp.Const)
            {
                return "const";
            }
            else if (additionalModifier == VariableModifierPopUp.ReadOnly)
            {
                return "readonly";
            }
            else if (additionalModifier == VariableModifierPopUp.Static)
            {
                return "static";
            }
            else
            {
                return string.Empty;
            }
        }
    }
#endif
}
