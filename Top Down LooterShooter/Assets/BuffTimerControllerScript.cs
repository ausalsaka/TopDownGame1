using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTimerControllerScript : MonoBehaviour
{


    public GameObject[] BuffTimers;
    public GameObject AmmoBuffTimer;
    [HideInInspector]public float tempDur;


    public void StartAmmoBuffTimer(float duration)
    {
        GameObject timer;
        timer = Instantiate(AmmoBuffTimer, transform.position, Quaternion.identity) as GameObject;
        timer.GetComponent<BuffTimerScript>().maxTime = duration;
        timer.transform.parent = gameObject.transform;
    }

    public void StartBuffTimer(int buff, float duration)
    {
        GameObject timer;
        timer = Instantiate(BuffTimers[buff], transform.position, Quaternion.identity) as GameObject;
        timer.GetComponent<BuffTimerScript>().maxTime = duration;
        timer.transform.parent = gameObject.transform;
    }

}
