using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private GameObject[] enemies;//用数组装满敌机
    void Start()
    {
        enemies = Resources.LoadAll<GameObject>("Enemys");
        InvokeRepeating("CreateEnemys",0,1.5f);
    }

    void Update()
    {
        
    }
    //随机生成敌机
    private void CreateEnemys()
    {
        int num = Random.Range(0,enemies.Length);
        GameObject enemy = Instantiate(enemies[num],new Vector3(Random.Range(-2.2f,2.2f),6,1),Quaternion.identity);//随机敌机，随机位置
        enemy.AddComponent<EnemyAI>();
        //随机速度
        int speeds=Random.Range(3,6);
        enemy.GetComponent<EnemyAI>().speed = speeds;
        enemy.tag = "Enemy";
    }
}
