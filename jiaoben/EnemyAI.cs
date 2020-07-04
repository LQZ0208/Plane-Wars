using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]//给敌机添加碰撞体
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    //敌机的移动，速度随机
    public int speed;
    private GameObject bullectPrefab;
    private Transform firePos;
    void Start()
    {
        bullectPrefab = Resources.Load("Bullet")as GameObject;//bullectPrefab = Resources.Load<GameObject>("Bullet");这样也可以
        firePos = transform.GetChild(0);//敌机子弹发射口，由于我们把发射口旋转了180度，所以子弹是向下发射的。
        InvokeRepeating("Attack",0,0.49f);//延时调用函数发射子弹，第一个参数表示调用attack，第二个是开始时调用，第三个是每1秒调用一次
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    void Update()
    {
        transform.Translate(Vector3.down*Time.deltaTime*speed);//这是控制敌机下移的
    }
    public void Attack()
    {
        GameObject bullet = Instantiate(bullectPrefab, firePos.position, firePos.rotation);
        bullet.AddComponent<Bullet>();
        bullet.name = "EnemyBullet";//给敌方飞机发射的子弹命名

    }
    //敌方飞机遇到情况，1撞到wall，2撞到敌机。被子弹消灭在bullet类里
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.tag=="Player")
        {
            Destroy(gameObject);
            collision.SendMessage("Damage", 30, SendMessageOptions.DontRequireReceiver);
        }
    }
}
