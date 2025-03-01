using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public CharacterController characterController;
    public Animator animator;

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;

    public int health;
    private int currentHealth;
    public bool isDead = false;

    public KeyCode keyJump = KeyCode.Space;
    public float jumpSpeed = 15f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private void Start()
    {
        currentHealth = health;
    }

    void Update()
    {
        transform.Rotate(0, !isDead ? Input.GetAxis("Horizontal") : 0 * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = !isDead ? Input.GetAxis("Vertical") : 0;
        var speedVector = transform.forward * inputAxisVertical * speed;
        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;
        characterController.Move(speedVector * Time.deltaTime);

        if (inputAxisVertical != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        speedVector = transform.forward * inputAxisVertical * speed;
        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(keyJump))
            {
                if (isDead) return;
                
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                if (isDead) return;

                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        animator.SetBool("isDead", isDead);
    }

    public void Damage(float damage)
    {
        currentHealth -= (int)damage;

        if (currentHealth <= 0) isDead = true;
    }

    /*private void Movement()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;
        vSpeed -= gravity * Time.deltaTime; speedVector.y = vSpeed;
        characterController.Move(speedVector * Time.deltaTime);

        if (inputAxisVertical != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Jump(float inputAxisVertical)
    {
        var speedVector = transform.forward * inputAxisVertical * speed;
        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }
        }
        vSpeed = gravity * Time.deltaTime;
    }

    private void Run()
    {
        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }
    }*/
}
