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


    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = dolly.m_Position;
    }


}
