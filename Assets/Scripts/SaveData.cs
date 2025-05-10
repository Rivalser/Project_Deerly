using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string currentSceneName;
    public PlayerData playerData;
    public List<EnemyData> enemyDataList = new List<EnemyData>();
}

[Serializable]
public class PlayerData
{
    public float health;
    public Vector3 position;
}

[Serializable]
public class EnemyData
{
    public string enemyID;
    public float health;
    public Vector3 position;
}
