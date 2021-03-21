using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The struct that holds values of any custom code block in the Script Generator.
    /// </summary>
    [System.Serializable]
    public struct CustomCodeStruct
    {
        [TextArea(3, 50)]
        public string customCode;

        /// <summary>
        /// Resets the current struct.
        /// </summary>
        public void ResetCustomCodeStruct()
        {
            customCode = string.Empty;
        }
    }
#endif
}
