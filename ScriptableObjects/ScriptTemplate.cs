#if UNITY_EDITOR
using System.Collections.Generic;
#endif
using UnityEngine;

namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    /// <summary>
    /// The scriptable object that holds all the data of a script.
    /// </summary>
    [CreateAssetMenu(fileName = "New C# Template", menuName = "C# Script Template", order = 81)] // Right below the standard C# script.
    public class ScriptTemplate : ScriptableObject
    {
        [SerializeField]
        private DeclarationStruct _declarationStruct;
        [SerializeField]
        private VariablesStruct[] _variablesStruct;
        [SerializeField]
        private CustomCodeStruct _customCodeStruct;
        [SerializeField]
        private UnityMethodsStruct[] _unityMethodsStruct;
        [SerializeField]
        private CustomMethodsStruct[] _customMethodsStruct;

        public DeclarationStruct DeclarationStructProperty => _declarationStruct;
        public List<VariablesStruct> VariablesStructProperty
        {
            get
            {
                List<VariablesStruct> listOfVariables = new List<VariablesStruct>();

                if (_variablesStruct != null)
                {
                    for (int i = 0; i < _variablesStruct.Length; i++)
                    {
                        listOfVariables.Add(_variablesStruct[i]);
                    }
                }

                return listOfVariables;
            }
        }
        public CustomCodeStruct CustomCodeStructProperty => _customCodeStruct;
        public List<UnityMethodsStruct> UnityMethodsStructProperty
        {
            get
            {
                List<UnityMethodsStruct> listOfUnityMethods = new List<UnityMethodsStruct>();

                if (_unityMethodsStruct != null)
                {
                    for (int i = 0; i < _unityMethodsStruct.Length; i++)
                    {
                        listOfUnityMethods.Add(_unityMethodsStruct[i]);
                    }
                }

                return listOfUnityMethods;
            }
        }
        public List<CustomMethodsStruct> CustomMethodsStructProperty
        {
            get
            {
                List<CustomMethodsStruct> listOfCustomMethods = new List<CustomMethodsStruct>();

                if (_customMethodsStruct != null)
                {
                    for (int i = 0; i < _customMethodsStruct.Length; i++)
                    {
                        listOfCustomMethods.Add(_customMethodsStruct[i]);
                    }
                }

                return listOfCustomMethods;
            }
        }

        /// <summary>
        /// Running OnValidate method from all structs.
        /// </summary>
        private void OnValidate()
        {
            _declarationStruct.OnValidate();

            // If the script is static, Unity methods are not allowed because it cannot inherit from MonoBehaviour.
            // Also, all variables, properties and custom methods will be static.
            bool isStatic = _declarationStruct.typeModifier == TypeModifierPopUp.Static;

            for (int i = 0; i < _variablesStruct.Length; i++)
            {
                _variablesStruct[i].OnValidate(isStatic);
            }

            if (isStatic)
            {
                _unityMethodsStruct = new UnityMethodsStruct[0];
            }
            else
            {
                for (int i = 0; i < _unityMethodsStruct.Length; i++)
                {
                    _unityMethodsStruct[i].OnValidate();
                }
            }

            for (int i = 0; i < _customMethodsStruct.Length; i++)
            {
                _customMethodsStruct[i].OnValidate(isStatic);
            }
        }

        /// <summary>
        /// Resets the current scriptable object.
        /// </summary>
        public void Reset()
        {
            _declarationStruct.ResetDeclarationStruct();
            _variablesStruct = new VariablesStruct[0];
            _customCodeStruct.ResetCustomCodeStruct();
            _unityMethodsStruct = new UnityMethodsStruct[0];
            _customMethodsStruct = new CustomMethodsStruct[0];
        }
    }
#endif
}
