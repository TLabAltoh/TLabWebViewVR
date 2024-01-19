using UnityEngine;

namespace TLab.InputField
{
    public class Key : KeyBase
    {
        [SerializeField] private string m_lower;
        [SerializeField] private string m_upper;

        public override void OnPress()
        {
            keyborad.OnKeyPress(keyborad.shift ? m_upper : m_lower);
        }

        private void SwitchDisp()
        {
            if (keyborad != null)
            {
                if (keyborad.gameObject.activeSelf)
                {
                    m_upperDisp.SetActive(keyborad.shift);
                    m_lowerDisp.SetActive(!keyborad.shift);
                }
            }
        }

        public override void OnShift()
        {
            SwitchDisp();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            SwitchDisp();
        }

#if UNITY_EDITOR
        public override void Setup()
        {
            base.Setup();

            string[] split = gameObject.name.Split(" ");
            switch (split.Length)
            {
                case 1:
                    m_lower = split[0];
                    m_upper = split[0];
                    break;
                case 2:
                    m_lower = split[0];
                    m_upper = split[1];
                    break;
            }

            m_lowerDisp = transform.GetChild(0).gameObject;
            m_upperDisp = transform.GetChild(1).gameObject;

            m_lowerDisp.SetActive(true);
            m_upperDisp.SetActive(false);
        }
#endif
    }
}
