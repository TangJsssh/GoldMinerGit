using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour
{
    public float speed = 5f;//������ת�ٶ�
    public int rotateDir;//��ת����
    private bool isMoveing;//�жϹ����Ƿ��ƶ�
    public float moveSpeed=10f;//�����ٶ�
    public float currentSpeed;//��ǰ�ٶ�
    private float moveTimer;//��ʱ��
    public bool ReturnToInitPos;//�ж��Ƿ��Ƿ���״̬
    private Vector3 originalPosition;//���ӳ�ʼλ��
    public SpriteRenderer sr;
    public Transform HookHeadTrans;
    public Glod glod;//ץ���Ľ��
    public int money=0;//��ǰ�ռ�����Ǯ
    public Text moneyText;
    public float gameTimer=60;//��Ϸ��ʱ��
    public Text timeText;
    public int goldMoney;//Ŀ����
    public Text goldText;
    public GameObject winPanel;//ʤ�����
    public GameObject losePanel;//ʧ�����
    public bool gameOver=false;//��Ϸ����
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
        //�����ʹ����乳��ֻ���ڵ�һ��ת�������һֱ��ת
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
    //���ӳ���
    private void HookMove()
    {
        //������ǰ�ƶ�
        HookHeadTrans.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        moveTimer += Time.deltaTime;
        if(moveTimer > 2)
        {
            ReturnToInitPos = true;
        }
        // �������ӵĳ�����ƥ�乳��ͷ����ʵ��λ��
        float newLength = Vector3.Distance(originalPosition, HookHeadTrans.position);
        sr.size = new Vector2(sr.size.x, -newLength);
        //sr.size=new Vector2(sr.size.x, sr.size.y+moveSpeed*Time.deltaTime);
    }
    //���ӷ���
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
                glod = null;//�����ٴ��������ٶ���
            }
            
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("1"))
        {
            glod= collision.GetComponent<Glod>();
            money += glod.prices;//���������ļ�ֵ����money
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
