using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCrossfade : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private bool playAtStart = false;
    [SerializeField]
    private string[] animationState;
    [SerializeField]
    private int whichState;



    private void Start()
    {
        animator = GetComponent<Animator>();

        if (playAtStart == true)
        {
            animator.Play("Base Layer.Crossfade_end", -1); //animationState[whichState]
        }
    }
}
