using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

namespace TLab.InputField
{
    public class TLabKey : MonoBehaviour
    {
        [SerializeField] private string current;
        [SerializeField] private string lowercase;
        [SerializeField] private string uppercase;
        [SerializeField] private string lowercaseDisp;
        [SerializeField] private string uppercaseDisp;
        [SerializeField] private TextMeshProUGUI keyText;

        private List<string> inputBuffer;
        private bool isShiftOn = false;

#if UNITY_EDITOR
        public void Initialize()
        {
            string[] split = this.gameObject.name.Split("_");
            if(split.Length == 2)
            {
                lowercase = split[0];
                uppercase = split[1];

                lowercaseDisp = lowercase;
                uppercaseDisp = uppercase;

                current = lowercase;
            }
        }
#endif

        public void SetKeyInputBuffer(List<string> inputBuffer)
        {
            this.inputBuffer = inputBuffer;
        }

        public void Press()
        {
            inputBuffer.Add(current);
        }

        public void ShiftPressed()
        {
            isShiftOn = !isShiftOn;
            current = isShiftOn ? uppercase : lowercase;
            keyText.text = isShiftOn ? uppercaseDisp : lowercaseDisp;
        }
    }
}
