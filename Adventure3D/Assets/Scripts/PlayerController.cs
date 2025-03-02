using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody rb;
    public Collider coll;
    public Transform visuals;

    public float speed;
    public float jumpForce;

    private bool isGrounded;
    private bool isDead;

    public StateMachine<States> stateMachine;

    public enum States
    {
        IDLE,
        RUNNING,
        JUMPING,
        DEAD
    }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        visuals = GetComponentInChildren<Transform>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    private Vector3 movement;
    private Vector3 jumpFriction = new Vector3(0, 100f, 0);
    public void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        rb.velocity += movement * speed * Time.deltaTime;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        Debug.Log(rb.velocity.y);
        if (rb.velocity.y <= -1) rb.velocity -= jumpFriction * Time.deltaTime;
    }

    private void Init()
    {
        stateMachine = new StateMachine<States>();
        stateMachine.Init();

        stateMachine.RegisterStates(States.IDLE, new GMPlayerIdle());
        stateMachine.RegisterStates(States.RUNNING, new GMPlayerRunning());
        stateMachine.RegisterStates(States.JUMPING, new GMPlayerJumping());
        stateMachine.RegisterStates(States.DEAD, new GMPlayerDead());

        stateMachine.SwitchState(States.IDLE);
    }
}
