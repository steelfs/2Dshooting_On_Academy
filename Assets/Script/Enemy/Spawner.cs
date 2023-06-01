using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnTarget;
    public float rangeY = 4;
    public float rangeX = 0.5f;
    public float interval = 0.5f;

    
    protected virtual void Spawn()
    {
        GameObject obj = Instantiate(spawnTarget);
        obj.transform.position = new Vector3(transform.position.x, Random.Range(rangeY, -rangeY), 0);
        Debug.Log("스포너 스폰 함수");
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(interval); // interval에 지정된 시간만큼 기다린다
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
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
