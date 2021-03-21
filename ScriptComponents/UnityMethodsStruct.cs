namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of a Unity method in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct UnityMethodsStruct
    {
        public UnityMethodNamesPopUp unityMethodName;

        public bool hasCustomCode;
        public CustomCodeStruct customCodeStruct;

        /// <summary>
        /// Similar to Unity's OnValidate. It gets called in the OnValidate of a MonoBehaviour class.
        /// </summary>
        public void OnValidate()
        {
            if (!hasCustomCode)
            {
                customCodeStruct.customCode = string.Empty;
            }
        }

        /// <summary>
        /// Gets the full declaration of the method.
        /// </summary>
        /// <returns>The declaration of the method in a <see cref="string"/> format.</returns>
        public string GetMethodString()
        {
            if (unityMethodName == UnityMethodNamesPopUp.None)
            {
                return string.Empty;
            }

            return $"private void {unityMethodName.ToString()}()";
        }
    }
#endif
}
