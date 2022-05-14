using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(cull());
        animator.SetBool("cull", false);
    }
    IEnumerator cull()
    {
        yield return new WaitForSeconds(10f);
        animator.SetBool("cull", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
