using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationBase animationBase;

    public Collider coll;

    public float startHealth = 10f;
    public float currentHealth;

    public float startAnimationDuration = 0.2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithSpawnAnimation = false;

    public bool lookAtPlayer = false;
    private Player player;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    protected virtual void Init()
    {
        ResetHealth();

        if (startWithSpawnAnimation) SpawnAnimation();
    }

    protected void ResetHealth()
    {
        currentHealth = startHealth;
    }

    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill()
    {
        if (coll != null) coll.enabled = false;
        PlayAnimationByTrigger(AnimationType.DEATH);
        Destroy(gameObject, 3f);
    }

    public void OnDamage(float damage)
    {
        if (currentHealth <= 0) return;
        ParticleManager.Instance.SpawnParticle("Electric", transform.position, 2f, 4);

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void PlayAnimationByTrigger(AnimationType type)
    {
        animationBase.PlayAnimationByTrigger(type);
    }

    public void SpawnAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    public void Damage(float damage)
    {
        OnDamage(damage);
    }

    public virtual void Update()
    {
        if (lookAtPlayer)
        {
            var targetPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            transform.LookAt(targetPos);
        }
    }
}
