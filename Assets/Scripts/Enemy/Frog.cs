using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Sprite _defaultSprite;

    [SerializeField] float _jumpDelay = 3;
    [SerializeField] Vector2 _jumpForce;
    [SerializeField] Sprite _jumpSprite;


    void Awake()
    {
        
        _rb= GetComponent<Rigidbody2D>();
        _spriteRenderer= GetComponent<SpriteRenderer>();
        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
        _defaultSprite = _spriteRenderer.sprite;

    }

    void Jump()
    {
        _rb.AddForce(_jumpForce);
        _jumpForce *= new Vector2(-1,1);
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        _spriteRenderer.sprite = _jumpSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _defaultSprite;
    }
}
