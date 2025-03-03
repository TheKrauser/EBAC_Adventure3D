using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public int key = 1;
    private string checkpointKey = "CheckpointKey";
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return;

        if (other.CompareTag("Player"))
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        ToggleCheckpoint(true);
    }

    private void ToggleCheckpoint(bool isOn)
    {
        if (isOn)
        {
            meshRenderer.material.SetColor("_Color", Color.green);
            meshRenderer.material.SetColor("_EmissionColor", Color.green);
            hasActivated = true;
            AudioManager.Instance.PlaySound("Checkpoint");
            SaveCheckpoint();
        }
        else
        {
            meshRenderer.material.SetColor("_Color", Color.grey);
            meshRenderer.material.SetColor("_EmissionColor", Color.grey);
            hasActivated = false;
        }
    }

    private void SaveCheckpoint()
    {
        if (SaveManager.Instance.saveSetup.lastCheckpoint > key) return;

        SaveManager.Instance.saveSetup.lastCheckpoint = key;
        CheckpointManager.Instance.SaveCheckpoint(key);
    }

    [NaughtyAttributes.Button]
    public void TurnOn()
    {
        ToggleCheckpoint(true);
    }

    [NaughtyAttributes.Button]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(checkpointKey);
    }
}
