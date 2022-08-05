using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBuffScript : MonoBehaviour
{
    [SerializeField]public float duration = 10f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && !player.buffs[0])
        {
            player.PickUpBuff(0, duration);
            Destroy(gameObject);
        }
    }
}
