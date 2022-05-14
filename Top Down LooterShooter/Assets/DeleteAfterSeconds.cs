using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    public float seconds;
    void Awake()
    {
        StartCoroutine(delete(seconds));
    }
    IEnumerator delete(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
