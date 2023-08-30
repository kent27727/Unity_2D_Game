using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _jumpEndTime;
    [SerializeField] private float _jumpVelocity=5f;
    [SerializeField] private float _jumpDuration=0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = rb.velocity.y;

        if (Input.GetButtonDown("Jump"))
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }

        if (Input.GetButton("Jump") && _jumpEndTime > Time.time)
        {
            vertical = _jumpVelocity;
        }
            

        rb.velocity= new Vector2(horizontal, vertical);
    }
}
