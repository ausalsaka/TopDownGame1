using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 25f;
    public Rigidbody2D rb;
    public ParticleSystem particles;
    public SpriteRenderer render;

    void Start()
    {
        StartCoroutine(range());
        rb.velocity = transform.up * speed;
    }
    private void Awake()
    {
        render.enabled = false;
        particles.Pause();
    }
    IEnumerator range()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Breakable obj = hitInfo.GetComponent<Breakable>();
        if(hitInfo.tag == "enemy" || hitInfo.tag == "breakable")
        {
            if (enemy != null) enemy.TakeDamage(damage);
            if (obj != null) obj.takeDamage(damage);
            particles.transform.parent = null;
            Destroy(gameObject);
        }
        if(hitInfo.name == "firePoint")
        {
            render.enabled = true;
            particles.Play();
        }

        if (hitInfo.name != "Bullet1(Clone)" && hitInfo.name != "Player" && hitInfo.tag != "DroppedLoot" && hitInfo.name != "firePoint")
        {
            particles.transform.parent = null;
            Destroy(gameObject);
        }
        
    }


}
