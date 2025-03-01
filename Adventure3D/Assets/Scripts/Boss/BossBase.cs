using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum BossAction
{
    INIT,
    IDLE,
    WALK,
    ATTACK,
    DEATH
}

public class BossBase : MonoBehaviour, IDamageable
{
    private StateMachine<BossAction> stateMachine;

    public HealthBase healthBase;
    public GunBase gunBase;
    public Player player;
    public AnimationBase animationBase;

    public float startAnimationDuration = 0.5f;
    public Ease startAnimationEase = Ease.OutBack;

    public float speed = 5f;
    public List<Transform> waypoits;

    public int attackAmount = 5;
    public float timeBetweenAttacks = 1f;

    private bool isDead = false;
    private Vector3 lookPos;

    private void Awake()
    {
        Init();

        healthBase.OnKill += HealthBase_OnBossKill;
    }

    private void Start()
    {

    }

    private void HealthBase_OnBossKill(HealthBase healthBase)
    {
        isDead = true;
        Debug.Log("Death");
        SwitchState(BossAction.DEATH);
    }

    private void Update()
    {
        if (isDead) return;

        lookPos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(lookPos);
    }

    private void Init()
    {
        stateMachine = new StateMachine<BossAction>();
        stateMachine.Init();

        stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
        stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
    }

    public void GoToRandomPoint(Action OnArrive = null)
    {
        StartCoroutine(GoToPointCoroutine(waypoits[UnityEngine.Random.Range(0, waypoits.Count)], OnArrive));
    }

    IEnumerator GoToPointCoroutine(Transform point, Action OnArrive = null)
    {
        while (Vector3.Distance(transform.position, point.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }

        OnArrive?.Invoke();
    }

    public void StartAttack(Action OnEndCallback = null)
    {
        StartCoroutine(StartAttackCoroutine(OnEndCallback));
    }

    public IEnumerator StartAttackCoroutine(Action OnEndCallback)
    {
        int attacks = 0;

        while (attacks < attackAmount)
        {
            gunBase.Shoot();
            attacks++;
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        OnEndCallback?.Invoke();
    }

    public void StopAttack()
    {

    }

    public void SwitchState(BossAction state)
    {
        stateMachine.SwitchState(state, this);
    }

    public void StartInitAnimation()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), startAnimationDuration).SetEase(startAnimationEase);
    }

    public void StartBoss()
    {
        StartCoroutine(StartBossCoroutine());
    }

    public IEnumerator StartBossCoroutine()
    {
        SwitchState(BossAction.INIT);
        yield return new WaitForSeconds(2f);
        SwitchState(BossAction.WALK);
    }

    public void PlayAnimationByTrigger(AnimationType type)
    {
        animationBase.PlayAnimationByTrigger(type);
    }

    public void Damage(float damage)
    {
        Debug.Log("Damaged boss in " + damage);
        healthBase.Damage(damage);
    }

    [NaughtyAttributes.Button]
    public void SwitchInit()
    {
        SwitchState(BossAction.INIT);
    }

    [NaughtyAttributes.Button]
    public void SwitchWalk()
    {
        SwitchState(BossAction.WALK);
    }

    [NaughtyAttributes.Button]
    public void SwitchAttack()
    {
        SwitchState(BossAction.ATTACK);
    }
}
