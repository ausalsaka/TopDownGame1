using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    public float health;
    [SerializeField]
    public GameObject remnants;
    [SerializeField]
    public GameObject[] drops;
    [SerializeField]
    public float dropRadius;
    private int dropCount;

    private void Start()
    {
        dropCount = drops.Length;
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) die();
    }

    void die()
    {
        spawner.mode = 1;
        Vector2 rand = new Vector2(Random.Range(transform.position.x - dropRadius, transform.position.x + dropRadius), Random.Range(transform.position.y - dropRadius, transform.position.y + dropRadius));
        for(int i = 0; i < dropCount; i++)
        {
            Instantiate(drops[i], rand, Quaternion.identity);
        }
        Instantiate(remnants, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
