using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
        
    public float MaxX;//设定范围
    public float MaxY;
    public float MinX;
    public float MinY;
    public float MoveSpeed;//移动速度
    private GameObject bg;//背景
    public GameObject bullectPrefab;//子弹
    private Transform firePos;//子弹发射口
    public int BulletNumber;//特殊攻击的子弹数量
    public float angle;//特殊攻击子弹之间的角度
    public int hp;//血量值
    public AudioClip clips;//子弹音效
    public AudioSource gamoverAudio;//游戏结束音效
    

    void Start()
    {
        bg = GameObject.Find("BG");//找到BG背景图片
        firePos = transform.GetChild(0);//确定子弹发射口
        InvokeRepeating("Attack",0,0.5f);//延时调用函数发射子弹，第一个参数表示调用attack，第二个是开始时调用，第三个是每0.5秒调用一次
    }

    void Update()
    {
        bg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(0,Time.time/5));//五秒转一圈图
        MoveMent();
        RangAttack();
    }

    //控制飞机移动
    public void MoveMent()
        {
            float Horizontal = Input.GetAxis("Horizontal");//获取水平输入轴
            float Vertical = Input.GetAxis("Vertical");//获取垂直输入轴
            if (transform.position.x >= MaxX && Horizontal > 0)//如果当前x位置大于等于边界范围，且还在向这个方向运动，也就是说还按下了向右，就让水平输入值为0，也就不会再继续向右
            {
                Horizontal = 0;
            }
            else if (transform.position.x <= MinX && Horizontal < 0)//同理
            {
                Horizontal = 0;
            }
            else if (transform.position.y>= MaxY && Vertical > 0)
            {
                Vertical = 0;
            }
            else if (transform.position.y<= MinY && Vertical < 0)
            {
                Vertical = 0;
            }
            transform.Translate(Horizontal * MoveSpeed * Time.deltaTime, Vertical * Time.deltaTime * MoveSpeed,0);//移动
        }
    //发射子弹攻击
    public void Attack()
    {
        GameObject tempBullet = Instantiate(bullectPrefab,firePos.position,firePos.rotation);//生成实体子弹
        tempBullet.AddComponent<Bullet>();//让子弹以一定速度移动
        tempBullet.name = "PlayerBullet";//给玩家飞机发射的子弹命名
        AudioSource.PlayClipAtPoint(clips,firePos.position,0.5f);
    }
    //特殊攻击，大范围攻击
    private void RangAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i=-BulletNumber/2;i<BulletNumber/2+1;i++)
            {
                GameObject tempBullet = Instantiate(bullectPrefab, firePos.position, firePos.rotation);//生成实体子弹
                tempBullet.transform.Rotate(0,0,angle*i);//让子弹旋转一个角度
                tempBullet.AddComponent<Bullet>();//让子弹一定速度移动
                tempBullet.name = "PlayerBullet";//给玩家飞机发射的子弹命名
                AudioSource.PlayClipAtPoint(clips, firePos.position, 0.5f);
            }
        }
       
    }
    //主角受伤以及死亡结束游戏
    #region
    private void Damage(int damage)
    {
        if (hp>0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;//死亡
                gamoverAudio.Play();
                Invoke("Gameover",1f);//延迟1秒调用
                

            }
            else 
            {
                //受伤
            }

        }
    }

    private void Gameover()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion

}

