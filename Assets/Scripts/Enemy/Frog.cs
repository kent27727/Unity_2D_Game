using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    Sprite _defaultSprite;
    int _jumpsRemaining;

    [SerializeField] int _Jumps = 2;
    [SerializeField] float _jumpDelay = 3;
    [SerializeField] Vector2 _jumpForce;
    [SerializeField] Sprite _jumpSprite;


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb= GetComponent<Rigidbody2D>();
        _spriteRenderer= GetComponent<SpriteRenderer>();
        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
        _defaultSprite = _spriteRenderer.sprite;
        _jumpsRemaining = _Jumps;

    }

    void Jump()
    {
        if ( _jumpsRemaining <= 0 )
        {
            _jumpForce *= new Vector2(-1, 1);
            _jumpsRemaining = _Jumps;
        }

        _jumpsRemaining--;

        _rb.AddForce(_jumpForce);
        _spriteRenderer.flipX = _jumpForce.x > 0;
        _spriteRenderer.sprite = _jumpSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _defaultSprite;
        _audioSource.Play();

        if (collision.collider.CompareTag("Player"))
            SceneManager.LoadScene(0);

    }
}
