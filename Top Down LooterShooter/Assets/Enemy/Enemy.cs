using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float health = 100;
    public GameObject enemyDead;
    public GameObject[] drops;
    public float dropRadius = 0.1f;
    private bool canDamage = true;

    public void TakeDamage (float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

    }

    IEnumerator canDamageReset()
    {
        yield return new WaitForSeconds(1.5f);
        canDamage = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.name == "Player" && canDamage)
        {

            Player.health -= 20;
            Player.Health.value -= 20;
            Debug.Log(Player.health);
            canDamage = false;
            StartCoroutine(canDamageReset());

        }
    }

    void Die()
    {
        Vector2 rand = new Vector2(Random.Range(transform.position.x - dropRadius, transform.position.x + dropRadius), Random.Range(transform.position.y - dropRadius, transform.position.y + dropRadius));
        if (Random.Range(1, 100) >90)
        {
            Instantiate(drops[0], rand, Quaternion.identity);
        }
        else { Instantiate(drops[1], rand, Quaternion.identity); }
        Instantiate(enemyDead, transform.position, transform.rotation);
        Destroy(gameObject);
        spawner.currentEnemies -= 1;
    }

    

}
