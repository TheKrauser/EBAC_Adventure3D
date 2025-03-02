using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>, IDamageable
{
    public CharacterController characterController;
    public Animator animator;
    public UI_Updater uiHealth;

    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;
    public bool isInvincible = false;

    public int health;
    private int currentHealth;
    public bool isDead = false;
    public float timeToRespawn = 4f;
    private Vector3 initialPosition;

    public KeyCode keyJump = KeyCode.Space;
    public float jumpSpeed = 15f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [SerializeField] private ClothChanger clothChanger;

    private void Start()
    {
        ResetHealth();
        uiHealth.UpdateValue((float)currentHealth / health);
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, (!isDead ? Input.GetAxis("Horizontal") : 0) * turnSpeed * Time.deltaTime, 0);

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

    public void ResetHealth()
    {
        currentHealth = health;
        uiHealth.UpdateValue((float)currentHealth / health);
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float targetSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = targetSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void TurnInvincibility(float duration)
    {
        StartCoroutine(TurnInvincibilityCoroutine(duration));
    }

    private IEnumerator TurnInvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    private IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangePlayerTexture(setup);
        yield return new WaitForSeconds(duration);
        clothChanger.ResetTexture();
    }

    public void Damage(float damage)
    {
        if (currentHealth <= 0) return;
        if (isInvincible) return;

        ShakeCamera.Instance.Shake(2f, 2f, 0.2f);
        currentHealth -= (int)damage;
        uiHealth.UpdateValue((float)currentHealth / health);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            StartCoroutine(RespawnCoroutine());
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(timeToRespawn);
        Respawn();
    }

    public void Respawn()
    {
        if (!CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = initialPosition;
        }
        else
        {
            transform.position = CheckpointManager.Instance.GetCheckpointSpawnPosition();
        }

        isDead = false;
        currentHealth = health;
        uiHealth.UpdateValue((float)currentHealth / health);
    }
}
