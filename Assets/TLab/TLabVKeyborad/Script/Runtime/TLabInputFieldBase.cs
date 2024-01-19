using UnityEngine;

namespace TLab.InputField
{
    public class TLabInputFieldBase : MonoBehaviour
    {
        [Header("Keyborad")]
        [SerializeField] protected TLabVKeyborad m_keyborad;

        [Header("Option")]
        [SerializeField] protected bool m_activeOnAwake = false;

        public bool inputFieldIsActive => m_keyborad.inputFieldBase == this;

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

        protected virtual void SwitchInputField(bool active) => m_keyborad.SwitchInputField(active ? this : null);

        public virtual void OnFocus(bool active) => SwitchInputField(active);

        public virtual void OnFocus() => SwitchInputField(!inputFieldIsActive);

        #endregion FOUCUS_EVENT

        protected virtual void Start()
        {
            if (m_activeOnAwake)
            {
                m_keyborad.HideKeyborad(false);
                m_keyborad.SwitchInputField(this);
            }
        }

        protected virtual void Update() { }
    }
}