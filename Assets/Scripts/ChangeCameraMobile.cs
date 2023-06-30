using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraMobile : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void ChangeCameraPos(int pos)
    {
        animator.SetInteger("Position", pos);
    }
}
