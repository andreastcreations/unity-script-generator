// A place to hold all enums

namespace ATMedia.CustomTools.ScriptGeneration
{
#if UNITY_EDITOR
    public enum AccessModifierPopUp
    {
        Public,
        Private,
        Protected,
        Internal,
        ProtectedInternal,
        PrivateProtected
    }

    public enum TypeModifierPopUp
    {
        None,
        Abstract,
        Partial,
        Sealed,
        Static
    }

    public enum TypePopUp
    {
        Class,
        Interface,
        ScriptableObject,
        Struct,
        Enum
    }

    public enum InheritsFromPopUp
    {
        MonoBehaviour,
        ScriptableObject,
        Editor,
        None,
        Custom
    }

    public enum VariableModifierPopUp
    {
        None,
        Const,
        ReadOnly,
        Static
    }

    public enum SerializeOptionPopUp
    {
        None,
        SerializeField,
        HideInInspector
    }

    public enum PropertyOptionPopUp
    {
        PublicGetPublicSet,
        PublicGetPrivateSet,
        PublicGet
    }

    public enum MethodModifierPopUp
    {
        None,
        Static,
        Partial,
        Abstract,
        Virtual,
        Sealed,
        SealedOverride,
        Override
    }

    public enum ParameterModifierPopUp
    {
        None,
        Ref,
        In,
        Out
    }

    public enum UnityMethodNamesPopUp
    {
        None,
        OnEnable,
        OnDisable,
        OnValidate,
        Awake,
        Start,
        FixedUpdate,
        Update,
        LateUpdate,
        OnDestroy
    }
#endif
}
