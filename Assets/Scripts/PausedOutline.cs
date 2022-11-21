using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausedOutline : MonoBehaviour
{
    [SerializeField]
    private Color colour;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        textMesh.outlineColor = colour;
    }
}
