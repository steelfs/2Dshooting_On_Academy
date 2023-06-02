using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroid : Spawner
{
    //������ ������ ����
    Transform destination;
    private void Awake()
    {
        destination = transform.GetChild(0);
    }

    //�������� �������� �׸���
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos(); //�������� �׸���

        Gizmos.color = Color.white;
        if (destination == null) //���������׸���
        {
            destination = transform.GetChild(0); //play�� ���� ����
        }
        Gizmos.DrawWireCube(destination.position, new Vector3(1, Mathf.Abs(rangeY) + Mathf.Abs(-rangeY), 1)); //mathf �ƽ����� ���ϴ� �Լ�
    }

    protected override EnemyBase Spawn()
    {
        EnemyBase enemy = base.Spawn();//�ϴ� ����


        // is : is ���� Ÿ������ ĳ���� �õ� �����ϸ� false �����ϸ� true;
        // as = as ���� Ÿ������ ĳ���� �õ�. �����ϸ� null�̰� �����ϸ� ������ ����
        EnemyAsteroid asteroid = enemy as EnemyAsteroid;
        if (asteroid != null)
        {
            Vector3 destPos = destination.position;
            destPos.y = Random.Range(rangeY, -rangeY);
            asteroid.Destination = destPos; //�������� ����

        }
        return asteroid;
    }




}
