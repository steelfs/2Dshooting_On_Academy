using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pool_Object_Type
{
    Player_Bullet = 0,
    Enemy_Boss,
    Enemy_BossBullet,
    Enemy_BossMissile,
    Enemy_Asteroid,
    Enemy_Asteroid_Mini,
    Enemy_Curve,
    Enemy_Fighter,
    Enemy_Strike,
}

public class Factory : Singleton<Factory>
{
    public GameObject PlayerBullet;
    public GameObject Boss;
    public GameObject BossBullet;
    public GameObject BossMissile;
    public GameObject Asteroid;
    public GameObject Asteroid_Mini;
    public GameObject Fighter;
    public GameObject Curve;
    public GameObject Strike;
    public GameObject GetObject(Pool_Object_Type type)
    {
        GameObject result;
        switch (type)
        {
            case Pool_Object_Type.Player_Bullet:
                result = Instantiate(PlayerBullet);
                break;
            case Pool_Object_Type.Enemy_Boss:
                result = Instantiate(Boss);
                break;
            case Pool_Object_Type.Enemy_BossBullet:
                result = Instantiate(BossBullet);
                break;
            case Pool_Object_Type.Enemy_BossMissile:
                result = Instantiate(BossMissile);
                break;
            case Pool_Object_Type.Enemy_Asteroid:
                result = Instantiate(Asteroid);
                break;
            case Pool_Object_Type.Enemy_Asteroid_Mini:
                result = Instantiate(Asteroid_Mini);
                break;
            case Pool_Object_Type.Enemy_Fighter:
                result = Instantiate(Fighter);
                break;
            case Pool_Object_Type.Enemy_Strike:
                result = Instantiate(Strike);
                break;
            case Pool_Object_Type.Enemy_Curve:
                result = Instantiate(Curve);
                break;
            default:
                result = new GameObject();
                break;
        }
      
        return result;     
    }
}
