using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleBrain : MonoBehaviour
{
    public ParticleSystem particles;

    void Update()
    {
        if (transform.parent == null) StartCoroutine(fart());
    }
    IEnumerator fart()
    {
        yield return new WaitForSeconds(1f);
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
