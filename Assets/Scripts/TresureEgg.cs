using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureEgg : MonoBehaviour
{
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.Play("open");
        Debug.Log("HEYYYYY" + other);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.Play("close");
        Debug.Log("HEYYYYY" + other);
    }
}
