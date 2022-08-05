using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffScript : MonoBehaviour
{
    [SerializeField] public float duration = 10f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && !player.buffs[1])
        {
            player.PickUpBuff(1, duration);
            Destroy(gameObject);
        }
    }
}
