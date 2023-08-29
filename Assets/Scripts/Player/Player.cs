using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var vertical = rb.velocity.y;

        if (Input.GetButtonDown("Fire1"))
            vertical = 5;

        rb.velocity = new Vector2(horizontal, vertical);
    }
}
