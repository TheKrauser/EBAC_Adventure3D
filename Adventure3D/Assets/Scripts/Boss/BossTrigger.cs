using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossTrigger : MonoBehaviour
{
    public BossBase boss;
    public GameObject bossCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.StartBoss();
            bossCamera.SetActive(true);
            Destroy(gameObject);
        }
    }
}
