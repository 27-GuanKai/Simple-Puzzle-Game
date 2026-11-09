using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode sprintkey = KeyCode.LeftShift;
    public KeyCode crouchkey = KeyCode.C;

    [Header("Player Orientation")]
    public Transform Orientaion;

    [Header("Movement")]
    public float MoveSpeed;
    public float walkspeed;
    public float runspeed;
    public float CrouchSpeed;

    [Header("Friction with floor")]
    public float groundDrag;

    [Header("Grounded?")]
    public float playerHeight;
    public LayerMask whatisfloor;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    [Header("Jumping")]
    public float Jumpforce;
    public float jumpcooldown;
    public float airmulti;
    bool readytojump;

    [Header("Crouching")]
    public float crouchYscale;
    float startYscale;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    //Bool for HeadBob Speed Checks
    public bool Air_Check;
    public bool Crouch_Check;
    public bool Walk_Check;
    public bool Run_Check;
    public PickupScript PickupScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readytojump = true;

        startYscale = transform.localScale.y;
    }
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // Jump
        if (Input.GetKey(jumpkey) && readytojump && grounded)
        {
            readytojump = false;

            jump();

            Invoke(nameof(ResetJump), jumpcooldown);
        }
        // Crouch
        if (Input.GetKeyDown(crouchkey) && PickupScript.pickedup == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            Crouch_Check = true;
        }
        // Uncrouch
        if (Input.GetKeyUp(crouchkey) && PickupScript.pickedup == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
            Crouch_Check = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        
    }
    private void Update()
    {
        MyInput();
        speedControl();
        statehandler();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisfloor);

        if (grounded)
        {
            rb.drag = groundDrag;
            print("Grounded");
            Air_Check = false;
        }
        else if (!grounded)
        {
            print("Aerial");
            rb.drag = .9f;
            Air_Check = true;
        }
    }
    void MovePlayer()
    {
        moveDirection = Orientaion.forward * verticalInput + Orientaion.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        }
        if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * MoveSpeed * 10f*airmulti, ForceMode.Force);
        }
    }
    void speedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatvel.magnitude > MoveSpeed)
        {
            Vector3 limitedvel = flatvel.normalized * MoveSpeed;
            rb.velocity = new Vector3(limitedvel.x, rb.velocity.y, limitedvel.z);
        }
    }
    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * Jumpforce, ForceMode.Impulse);

    }
    private void ResetJump()
    {
        readytojump = true;
    }

    public enum MovementState
    {
        walking,
        sprinting,
        Crouching,
        air
    }
    void statehandler()
    {
        // Mode (Crouch)
        if (Input.GetKey(crouchkey))
        {
            state = MovementState.Crouching;
            MoveSpeed = CrouchSpeed;
        }
        // Mode (Sprint)
        else if (grounded && Input.GetKey(sprintkey))
        {
            state = MovementState.sprinting;
            MoveSpeed = runspeed;
            Run_Check = true;
            Walk_Check = false;
        }
        // Mode (Walk)
        else if (grounded)
        {
            state = MovementState.walking;
            MoveSpeed = walkspeed;
            Run_Check = false;
            Walk_Check = true;
        }
        
        // Mode (Aerial)
        else if(!grounded)
        {
            state = MovementState.air;
            Air_Check = false;
        }
    }
}
