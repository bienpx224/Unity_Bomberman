using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    private PhotonView _photonView;
    private Animator _animator;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
    }
    public enum Direction
    {
        IDLE, UP, DOWN, RIGHT, LEFT
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKey(inputUp))
            {
                SetDirectionAnimator(Direction.UP);
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirectionAnimator(Direction.DOWN);
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirectionAnimator(Direction.LEFT);
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirectionAnimator(Direction.RIGHT);
            }
            else
            {
                SetDirectionAnimator(Direction.IDLE);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
    private void SetDirectionAnimator(Direction d)
    {
        switch (d)
        {
            case Direction.IDLE:
            direction = Vector2.zero;
                _animator.SetBool("onIdle", true);
                _animator.SetBool("onUp", false);
                _animator.SetBool("onDown", false);
                _animator.SetBool("onLeft", false);
                _animator.SetBool("onRight", false);
                break;
            case Direction.UP:
            direction = Vector2.up;
                _animator.SetBool("onIdle", false);
                _animator.SetBool("onUp", true);
                _animator.SetBool("onDown", false);
                _animator.SetBool("onLeft", false);
                _animator.SetBool("onRight", false);
                break;
            case Direction.DOWN:
            direction = Vector2.down;
                _animator.SetBool("onIdle", false);
                _animator.SetBool("onUp", false);
                _animator.SetBool("onDown", true);
                _animator.SetBool("onLeft", false);
                _animator.SetBool("onRight", false);
                break;
            case Direction.LEFT:
            direction = Vector2.left;
                _animator.SetBool("onIdle", false);
                _animator.SetBool("onUp", false);
                _animator.SetBool("onDown", false);
                _animator.SetBool("onLeft", true);
                _animator.SetBool("onRight", false);
                break;
            case Direction.RIGHT:
            direction = Vector2.right;
                _animator.SetBool("onIdle", false);
                _animator.SetBool("onUp", false);
                _animator.SetBool("onDown", false);
                _animator.SetBool("onLeft", false);
                _animator.SetBool("onRight", true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }
    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        _animator.SetBool("onIdle",false);
        _animator.SetBool("onLeft",false);
        _animator.SetBool("onRight",false);
        _animator.SetBool("onDown",false);
        _animator.SetBool("onUp",false);
        _animator.SetBool("onDeath",true);

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CheckWinState();
    }
}
