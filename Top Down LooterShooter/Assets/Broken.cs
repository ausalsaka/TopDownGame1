using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    public Rigidbody2D rb;
    public int gravityScale;
    public float duration;
    void Awake()
    {
        StartCoroutine(gravity());
    }

    IEnumerator gravity()
    {
        rb.gravityScale = gravityScale;
        yield return new WaitForSeconds(duration);
        rb.gravityScale = 0;
    }
}
