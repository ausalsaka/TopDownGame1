using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTimerControllerScript : MonoBehaviour
{

    [HideInInspector] public int timers = 0;
    public GameObject[] BuffTimers;
    [HideInInspector]public float tempDur;

    public void StartBuffTimer(int buff, float duration)
    {
        if (timers == 0)
        {
            GameObject timer;
            timer = Instantiate(BuffTimers[buff], transform.position, Quaternion.identity) as GameObject;
            timer.GetComponent<BuffTimerScript>().maxTime = duration;
            timer.transform.SetParent(gameObject.transform);
            timers += 1;
        } else if (timers == 1)
        {
            GameObject timer;
            Vector2 pos = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y -40);
            timer = Instantiate(BuffTimers[buff], pos, Quaternion.identity) as GameObject;
            timer.GetComponent<BuffTimerScript>().maxTime = duration;
            timer.transform.SetParent(gameObject.transform);
            timers += 1;
        }

    }

}
