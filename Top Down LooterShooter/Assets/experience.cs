using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class experience : MonoBehaviour
{
    private int value;
    private void Start()
    {
        value = Random.Range(5, 20);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            player.pickUpXp(value);
            Destroy(gameObject);
        }
    }


}
