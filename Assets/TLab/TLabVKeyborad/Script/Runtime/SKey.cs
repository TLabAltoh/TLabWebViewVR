using UnityEngine;

namespace TLab.InputField
{
    public enum SKeyCode
    {
        BACKSPACE,
        TAB,
        SYMBOL,
        SPACE,
        SHIFT,
        RETURN
    }

    public class SKey : KeyBase
    {
        [SerializeField] private SKeyCode m_sKey;

        public override void OnPress()
        {
            keyborad.OnSKeyPress(m_sKey);
        }

        public override void OnShift()
        {
            m_upperDisp.SetActive(true);
            m_lowerDisp.SetActive(true);
        }

#if UNITY_EDITOR
        public override void Setup()
        {
            base.Setup();

            string name = gameObject.name;
            switch (name)
            {
                case "BACKSPACE":
                    m_sKey = SKeyCode.BACKSPACE;
                    break;
                case "RETURN":
                    m_sKey = SKeyCode.RETURN;
                    break;
                case "SHIFT":
                    m_sKey = SKeyCode.SHIFT;
                    break;
                case "SPACE":
                    m_sKey = SKeyCode.SPACE;
                    break;
                case "SYMBOL":
                    m_sKey = SKeyCode.SYMBOL;
                    break;
                case "TAB":
                    m_sKey = SKeyCode.TAB;
                    break;
            }

            m_lowerDisp = transform.GetChild(0).gameObject;
            m_upperDisp = transform.GetChild(0).gameObject;
        }
#endif
    }
}
