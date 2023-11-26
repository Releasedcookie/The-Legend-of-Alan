using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Call Componants 
    private Rigidbody2D rbody;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    // Move Speeds
    [SerializeField] private float jumpHeight = 14f;
    [SerializeField] private float moveSpeed = 7f;
    private float dirX = 0f;

    // UI Movements
    private bool moveLeftBool = false;
    private bool moveRightBool = false;

    private enum MovementState { idle, running, jumping, falling }
    public bool MoveEnabled = true;

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (MoveEnabled == true)
        {

            // ================ Heyboard / Joystick Controls ================
            dirX = Input.GetAxisRaw("Horizontal");

            // Player Move Left and Right
            rbody.velocity = new Vector2(dirX * moveSpeed, rbody.velocity.y);

            // Player Jump
            if (Input.GetButtonDown("Jump") && isGrounded() == true)
            {
                jumpSoundEffect.Play();
                rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
            }


            if (moveRightBool == true)
            {
                rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);
            }
            if (moveLeftBool == true)
            {
                rbody.velocity = new Vector2(-moveSpeed, rbody.velocity.y);
            }
        }
        UpdateAnimationState();

    }



    private void UpdateAnimationState()
    {
        MovementState state;

        if (rbody.velocity.x > 0f) // If moving forward
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (rbody.velocity.x < 0f) // If moving backwards
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void disableMovement()
    {
        MoveEnabled = false;
        sprite.flipX = false;
    }
    public void enableMovement()
    {
        MoveEnabled = true;
    }

    public void ButtonControl_moveLeft()
    {
        moveLeftBool = true;
    }
    public void ButtonControl_moveRight()
    {
        moveRightBool = true;
    }
    public void ButtonControl_jump()
    {
        if (isGrounded())
        {
            jumpSoundEffect.Play();
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
        }
    }
    public void ButtonControl_StopLeft()
    {
        moveLeftBool = false;
    }
    public void ButtonControl_StopRight()
    {
        moveRightBool = false;
    }
}
