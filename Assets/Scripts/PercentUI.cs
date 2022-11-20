using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentUI : MonoBehaviour
{
    [SerializeField]
    private BatIcon batIcon;

    [SerializeField]
    private Color red, red2, orange, orange2, yellow, yellow2, green, green2;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        Color useColour, outlineColour;

        if (batIcon.percentage < 25)
        {
            useColour = red;
            outlineColour = red2;
        }
        else if (batIcon.percentage < 50)
        {
            useColour = orange;
            outlineColour = orange2;
        }
        else if (batIcon.percentage < 75)
        {
            useColour = yellow;
            outlineColour = yellow2;
        }
        else
        {
            useColour = green;
            outlineColour = green2;
        }

        text.color = useColour;
        text.outlineColor = outlineColour;
        text.text = Mathf.Round(batIcon.percentage) + "%";
    }
}
