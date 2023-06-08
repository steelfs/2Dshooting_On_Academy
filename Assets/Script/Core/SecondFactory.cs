using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFactory : Singleton<SecondFactory>
{
    public GameObject randomEnemy;
    public enum PoolObjType
    {
        PlayerBullet,
        Boss,
        EnemyBullet,
        EnemyCurve,
        EnemyFighter,
        EnemyAsteroid,
        EnemyAsteroidMini,
    }
    public GameObject GetObject(PoolObjType type)
    {
        GameObject obj;
        switch (type)
        {
            case PoolObjType.PlayerBullet:
                obj = Instantiate(randomEnemy);
                break;
            case PoolObjType.Boss:
                obj = Instantiate(randomEnemy);
                break; 
            case PoolObjType.EnemyBullet:
                obj = Instantiate(randomEnemy);
                break;
            case PoolObjType.EnemyCurve:
                obj = Instantiate(randomEnemy);
                break;
            case PoolObjType.EnemyFighter:
                obj = Instantiate(randomEnemy);
                break;
            case PoolObjType.EnemyAsteroid:
                obj = Instantiate(randomEnemy);
                break;
            case PoolObjType.EnemyAsteroidMini:
                obj = Instantiate(randomEnemy);
                break;
            default:
                obj = new GameObject();
                break;
        }
        return obj;
    }
}
