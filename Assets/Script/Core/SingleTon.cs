using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon : MonoBehaviour
{
    private static SingleTon instance;
    public static SingleTon Instance
    {
        get
        {
            if (instance == null)
            {
                SingleTon singleTon = FindObjectOfType<SingleTon>();
                if (singleTon == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = "SingleTon";
                    gameObj.AddComponent<SingleTon>();
                  
                }
                instance= singleTon;
                DontDestroyOnLoad(instance.gameObject);
            
            }
            return instance;
        }
    }
    public int testI = 0;
}
