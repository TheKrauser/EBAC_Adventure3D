using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public List<ParticleClass> particles;

    public void SpawnParticle(string particleName, Vector3 position, float timeToDestroy, int size)
    {
        var particle = particles.Find(p => p.particleName == particleName);
        var obj = Instantiate(particle.particlePrefab, position, Quaternion.identity);
        var s = new Vector3(size, size, size);
        obj.transform.localScale = s;
        Destroy(obj, timeToDestroy);
    }
}

[System.Serializable]
public class ParticleClass
{
    public string particleName;
    public GameObject particlePrefab;
}
