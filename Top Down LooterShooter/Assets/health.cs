using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    int value;
    public GameObject particles;
    public GameObject lights;
    Vector3 scaleMult;

    private void Awake()
    {
        value = Random.Range(5, 20);
        scaleMult = new Vector3 (value/5, value/5, 1);
        transform.localScale = Vector3.Scale(transform.localScale, scaleMult);
        particles.transform.localScale= Vector3.Scale(particles.transform.localScale, scaleMult);
        lights.transform.localScale = Vector3.Scale(lights.transform.localScale, scaleMult);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            player.pickUpHealth(value);
            Destroy(gameObject);
        }
    }
}
