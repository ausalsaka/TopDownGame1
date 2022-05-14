using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    public float seconds;
    void Awake()
    {
        StartCoroutine(disable(seconds));
    }
    IEnumerator disable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
