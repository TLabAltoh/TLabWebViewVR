using UnityEngine;

namespace TLab.InputField
{
    public class TLabInputFieldBase : MonoBehaviour
    {
        [Header("Keyborad")]
        [SerializeField] protected TLabVKeyborad m_keyborad;

        #region KEY_EVENT

        public virtual void OnBackSpacePressed() { }

        public virtual void OnEnterPressed() { }

        public virtual void OnSpacePressed() { }

        public virtual void OnTabPressed() { }

        public virtual void OnShiftPressed() { }

        public virtual void OnSymbolPressed() { }

        public virtual void OnKeyPressed(string input) { }

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        public virtual void OnFocus(bool active)
        {
            m_keyborad.SwitchInputField(active ? this : null);
        }

        #endregion FOUCUS_EVENT

        protected virtual void Start() { }

        protected virtual void Update() { }
    }
}