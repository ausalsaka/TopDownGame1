using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    public float seconds;
    private SpriteRenderer rendara;
    public void Start()
    {
        if(gameObject.GetComponent<SpriteRenderer>() != null) rendara = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Delete(seconds));
    }

    IEnumerator Delete(float time)
    {
        if(rendara != null)
        {
            yield return new WaitForSeconds(time - 10f);
            rendara.enabled = true;
            yield return new WaitForSeconds(1f);
            rendara.enabled = false;
            yield return new WaitForSeconds(1f);
            rendara.enabled = true;
            yield return new WaitForSeconds(1f);
            rendara.enabled = false;
            yield return new WaitForSeconds(1f);
            rendara.enabled = true;
            yield return new WaitForSeconds(1f);
            rendara.enabled = false;
            yield return new WaitForSeconds(1f);
            rendara.enabled = true;
            yield return new WaitForSeconds(1f);
            rendara.enabled = false;
            yield return new WaitForSeconds(.5f);
            rendara.enabled = true;
            yield return new WaitForSeconds(.5f);
            rendara.enabled = false;
            yield return new WaitForSeconds(.5f); //3.5
            rendara.enabled = true;
            yield return new WaitForSeconds(.5f); //4
            rendara.enabled = false;
            yield return new WaitForSeconds(.25f); //4.25
            rendara.enabled = true;
            yield return new WaitForSeconds(.25f); //4.5
            rendara.enabled = false;
            yield return new WaitForSeconds(.25f); //4.75
            rendara.enabled = true;
            yield return new WaitForSeconds(.25f);
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

    }

}
