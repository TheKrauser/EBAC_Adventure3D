using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public void SaveCheckpoint(int i)
    {
        if (i < lastCheckpointKey) return;

        lastCheckpointKey = i;
    }

    public Vector3 GetCheckpointSpawnPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }
}
