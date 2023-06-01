
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed = 3.0f;
    public float waitTimeX = 1.0f;  
    GameObject explosion;

    //[SerializeField]
    public int score = 10; //적이 주는 점수
    public int Score => score;


    private void Awake()
    {
        explosion = transform.GetChild(0).gameObject;
        
    }
    // Start is called before the first frame update
   
    void Update()
    {
        OnMoveUpdate();
    }

    protected virtual void OnMoveUpdate()
    {
        transform.Translate(Time.deltaTime * speed * -transform.right);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        explosion.transform.SetParent(null);//부모 오브젝트와 같이 destroy되는것을 방지
        //explosion.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360));
        explosion.transform.Rotate(0,0, Random.Range(0, 360.0f));
        explosion.SetActive(true);
        Destroy(gameObject);

        //디자인 패턴은 코딩의 정석
    }
}

