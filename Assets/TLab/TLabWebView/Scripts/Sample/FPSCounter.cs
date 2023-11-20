using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField, HideInInspector] private TextMeshProUGUI m_counter;

    void Start()
    {
        m_counter = GetComponent<TextMeshProUGUI>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        m_counter.text = (1.0f / Time.deltaTime).ToString("0.000");
    }
}
