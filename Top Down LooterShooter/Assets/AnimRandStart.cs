using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandStart : MonoBehaviour
{
    void Awake()
    {
        Animator Anim = GetComponent<Animator>();
        AnimatorStateInfo state = Anim.GetCurrentAnimatorStateInfo(0);
        Anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
