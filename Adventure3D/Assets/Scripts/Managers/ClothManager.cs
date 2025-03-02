using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothType
{
    SPEED,
    STRENGTH,
    INVINCIBLE
}

public class ClothManager : Singleton<ClothManager>
{
    public List<ClothSetup> clothSetups;

    public ClothSetup GetSetupByType(ClothType clothType)
    {
        return clothSetups.Find(setup => setup.clothType == clothType);
    }
}

[System.Serializable]
public class ClothSetup
{
    public ClothType clothType;
    public Texture2D texture;
}
