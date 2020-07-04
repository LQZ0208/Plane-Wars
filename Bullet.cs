using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]//给子弹添加碰撞体
[RequireComponent(typeof(Rigidbody2D))]//给子弹一个刚体
public class Bullet : MonoBehaviour
{

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;//把trigger勾选
        GetComponent<Rigidbody2D>().gravityScale = 0;//让子弹不受重力影响
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "player":
                if (this.name== "EnemyBullet")
                {
                    collision.SendMessage("Damage",Random.Range(5,20),SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (this.name== "PlayerBullet")
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:
                if (this.name!=collision.name)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
