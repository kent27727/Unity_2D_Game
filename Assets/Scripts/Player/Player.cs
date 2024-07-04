using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float _maxHorizontalSpeed = 5;
    [SerializeField] float _jumpVelocity = 5;
    [SerializeField] float _jumpDuration = 0.5f;
    [SerializeField] float _footOffSet = 0.35f;
    [SerializeField] Sprite _jumpSprite;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _footOffset = 0.5f;
    [SerializeField] float _acceleration = 10;

    public bool IsGrounded;

    float _jumpEndTime;
    float _horizontal;
    int _jumpsRemaining;

    SpriteRenderer _spriteRenderer;  
    Animator _animator; 
    AudioSource _audioSource;
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator =GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Gizmos.color = Color.red;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);        
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        //Draw Left Foot
        origin = new Vector2(transform.position.x - _footOffSet, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        //Draw Right Foot
        origin = new Vector2(transform.position.x + _footOffSet, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateGrounding();

        var horizontalInput = Input.GetAxis("Horizontal");
        
        var vertical = _rb.velocity.y;

        if (Input.GetButtonDown("Jump") && _jumpsRemaining>0)
        {
            _jumpEndTime = Time.time + _jumpDuration;
            _jumpsRemaining--;

           _audioSource.Play();
            _audioSource.pitch = _jumpsRemaining > 0 ? 1 : 1.2f;

        }
            

        if (Input.GetButton("Jump") && _jumpEndTime > Time.time)
            vertical = _jumpVelocity;

        var desiredHorizontal = horizontalInput * _maxHorizontalSpeed;
        _horizontal = Mathf.Lerp(_horizontal, desiredHorizontal, Time.deltaTime * _acceleration);
        _rb.velocity = new Vector2(_horizontal, vertical);
        UpdateSprite();
    }

    void UpdateGrounding()
    {
        IsGrounded = false;

        // Check Center
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        // Check Left
        origin = new Vector2(transform.position.x - _footOffSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        // Check Right
        origin = new Vector2(transform.position.x + _footOffSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        if (IsGrounded && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            _jumpsRemaining = 2;
        }

    }

    private void UpdateSprite()
    {
        _animator.SetBool("IsGrounded",IsGrounded);
        _animator.SetFloat("HorizontalSpeed",Math.Abs( _horizontal));

        if (_horizontal > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            _spriteRenderer.flipX = true;
    }
}