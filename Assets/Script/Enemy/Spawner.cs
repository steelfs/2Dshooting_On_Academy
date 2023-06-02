using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    //������ ������Ʈ ������
    public GameObject spawnTarget;

    //���� ������
    public float rangeY = 4;
    public float rangeX = 0.5f;


    public float interval = 0.5f;
    Player player;
    
    protected virtual EnemyBase Spawn()
    {
        GameObject obj = Instantiate(spawnTarget);
        obj.transform.position = new Vector3(transform.position.x, Random.Range(rangeY, -rangeY), 0);

        EnemyBase enemy = obj.GetComponent<EnemyBase>();
        enemy.OnInitialize();
        enemy.onDie += player.AddScore;
        return obj.GetComponent<EnemyBase>();
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(interval); // interval�� ������ �ð���ŭ ��ٸ���
        }
 
    }
    public void TestSpawn()
    {
        Spawn();
       
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color= Color.green;
        Gizmos.DrawLine(new Vector3( transform.position.x - rangeX, transform.position.y - rangeY, 0), new Vector3(transform.position.x - rangeX, transform.position.y + rangeY, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - rangeX, transform.position.y + rangeY, 0), new Vector3(transform.position.x + rangeX, transform.position.y + rangeY, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + rangeX, transform.position.y + rangeY, 0), new Vector3(transform.position.x + rangeX, transform.position.y - rangeY, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + rangeX, transform.position.y - rangeY, 0), new Vector3(transform.position.x - rangeX, transform.position.y - rangeY, 0));

    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(SpawnCoroutine());
    }


}
