using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInDirection : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    public float magnitude;
    void Start()
    {
        rb.AddForce(direction);
    }


}
