using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroid : Spawner
{
    //목적지 영역의 기준
    Transform destination;
    private void Awake()
    {
        destination = transform.GetChild(0);
    }

    //스폰지역 목적지역 그리기
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos(); //스폰지역 그리기

        Gizmos.color = Color.white;
        if (destination == null) //도착지점그리기
        {
            destination = transform.GetChild(0); //play전 오류 방지
        }
        Gizmos.DrawWireCube(destination.position, new Vector3(1, Mathf.Abs(rangeY) + Mathf.Abs(-rangeY), 1)); //mathf 맥스값을 구하는 함수
    }

    protected override EnemyBase Spawn()
    {
        EnemyBase enemy = base.Spawn();//일단 스폰


        // is : is 뒤의 타입으로 캐스팅 시도 실패하면 false 성공하면 true;
        // as = as 뒤의 타입으로 캐스팅 시도. 실패하면 null이고 성공하면 참조를 리턴
        EnemyAsteroid asteroid = enemy as EnemyAsteroid;
        if (asteroid != null)
        {
            Vector3 destPos = destination.position;
            destPos.y = Random.Range(rangeY, -rangeY);
            asteroid.Destination = destPos; //도착지점 설정

        }
        return asteroid;
    }




}
