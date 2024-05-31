using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour
{
    public float speed = 5f;//钩子旋转速度
    public int rotateDir;//旋转方向
    private bool isMoveing;//判断钩子是否移动
    public float moveSpeed=10f;//钩子速度
    public float currentSpeed;//当前速度
    private float moveTimer;//计时器
    public bool ReturnToInitPos;//判断是否是返回状态
    private Vector3 originalPosition;//钩子初始位置
    public SpriteRenderer sr;
    public Transform HookHeadTrans;
    public Glod glod;//抓到的金块
    public int money=0;//当前收集到的钱
    public Text moneyText;
    public float gameTimer=60;//游戏计时器
    public Text timeText;
    public int goldMoney;//目标金额
    public Text goldText;
    public GameObject winPanel;//胜利面板
    public GameObject losePanel;//失败面板
    public bool gameOver=false;//游戏结束
    private void Start()
    {
        originalPosition = HookHeadTrans.transform.position;
        currentSpeed = moveSpeed;
        moneyText.text=money.ToString();
        goldText.text=goldMoney.ToString();
    }
    private void Update()
    {
        gameTimer-=Time.deltaTime;
        timeText.text=gameTimer.ToString("0");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isMoveing = true;
        }
        if(isMoveing)
        {
           if(ReturnToInitPos)
            {
                HookReturn();
                Debug.Log("returnToInitPos is true");
            }
            else
            {
                HookMove();
                Debug.Log("returnToInitPos is false");
            }
        }
        else
        {
            HookRotation();
            Debug.Log("HookRotation is true");
        }
        if(gameTimer<=0)
        {
            JudgeGameResult();
        }
        if(gameOver)
        {
            gameTimer = 0;
            return;
        }
        
    }
    private void HookRotation()
    {
        //如果不使用这句钩子只会在第一次转换方向后一直旋转
        float angle = (transform.eulerAngles.z + 180) % 360 - 180;
        if (angle<= -85)
        {
            rotateDir = 1;
        }
        else if (angle >= 85)
        {
            rotateDir = -1;
        }
        transform.Rotate(Vector3.forward * speed*Time.deltaTime*rotateDir);
    }
    //钩子出发
    private void HookMove()
    {
        //钩子向前移动
        HookHeadTrans.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        moveTimer += Time.deltaTime;
        if(moveTimer > 2)
        {
            ReturnToInitPos = true;
        }
        // 更新绳子的长度以匹配钩子头部的实际位置
        float newLength = Vector3.Distance(originalPosition, HookHeadTrans.position);
        sr.size = new Vector2(sr.size.x, -newLength);
        //sr.size=new Vector2(sr.size.x, sr.size.y+moveSpeed*Time.deltaTime);
    }
    //钩子返回
    private void HookReturn()
    {
        HookHeadTrans.position = Vector3.MoveTowards(HookHeadTrans.position, originalPosition, currentSpeed * Time.deltaTime);
        sr.size = new Vector2(sr.size.x, sr.size.y + currentSpeed * Time.deltaTime);
        if (Vector3.Distance(HookHeadTrans.position, originalPosition) < 0.01f)
        {
            moveTimer = 0;
            isMoveing = false;
            ReturnToInitPos=false;
            sr.size = new Vector2(sr.size.x, 0);
            currentSpeed = moveSpeed;
            moneyText.text = money.ToString();
            if (glod!=null) 
            {
                Destroy(glod.gameObject);
                glod = null;//避免再次引用销毁对象
            }
            
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("1"))
        {
            glod= collision.GetComponent<Glod>();
            money += glod.prices;//将勾到金块的价值传给money
            collision.transform.SetParent(HookHeadTrans);
            collision.transform.localEulerAngles = Vector3.zero;
            collision.transform.localPosition = Vector3.zero;

            ReturnToInitPos = true;
            currentSpeed = glod.slowSpeed;
            
        }
    }
    public void JudgeGameResult()
    {
        if(money>=goldMoney)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
        gameOver = true;
    }

}
