using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    private void Start()
    {
        lastCheckpointKey = SaveManager.Instance.saveSetup.lastCheckpoint;
    }

    public void SaveCheckpoint(int i)
    {
        if (i < lastCheckpointKey) return;

        lastCheckpointKey = i;
        SaveManager.Instance.saveSetup.playerPosition = GetCheckpointSpawnPosition();
        SaveManager.Instance.Save();
    }

    public Vector3 GetCheckpointSpawnPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public int GetLastCheckpoint()
    {
        return lastCheckpointKey;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }
}
