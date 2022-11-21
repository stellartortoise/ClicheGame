using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BatIcon : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart dolly;
    
    private Slider slider;
    public float percentage;

    private float originalTrackLengthEndPoint = 248.646f;


    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = dolly.m_Position;
        percentage = (dolly.m_Position / (originalTrackLengthEndPoint + 10f)) * 100f;
        percentage = Mathf.Clamp(percentage, 0f, 100f);
    }


}
