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
    public GameObject particle;

    private void Start()
    {
        ResetHealth();
        initialPosition = transform.position;

        if (SaveManager.Instance.saveSetup.playerPosition != Vector3.zero)
            transform.position = SaveManager.Instance.saveSetup.playerPosition;

        currentHealth = SaveManager.Instance.saveSetup.health == 0 ? health : SaveManager.Instance.saveSetup.health;
        uiHealth.UpdateValue((float)currentHealth / health);
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
            particle.SetActive(true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            particle.SetActive(false);
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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public void ResetHealth()
    {
        if (isDead) return;

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

        ParticleManager.Instance.SpawnParticle("Blood", transform.position, 2f, 4);
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
        if (SaveManager.Instance.saveSetup.lastCheckpoint == 0)
        {
            Debug.Log(initialPosition);
            transform.position = initialPosition;
        }
        else
        {
            Debug.Log(SaveManager.Instance.saveSetup.playerPosition);
            transform.position = SaveManager.Instance.saveSetup.playerPosition;
        }

        isDead = false;
        currentHealth = health;
        uiHealth.UpdateValue((float)currentHealth / health);
    }
}
