using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class packageCell : MonoBehaviour
{
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;

    private packageLocalItem packagelocalData;
    private packageTableItem packagetableItem;
    private PackagePanel uiParent;
    private void Awake()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UIIcon = transform.Find("Top/icon");
        UIHead = transform.Find("Top/Head");
        UINew = transform.Find("Top/new");
        UISelect = transform.Find("select");
        UILevel = transform.Find("Button/LevelText");
        UIStars = transform.Find("Button/stars");
        UIDeleteSelect = transform.Find("DeleteSelect");

        UIDeleteSelect.gameObject.SetActive(false);
    }

    //ˢ����Ʒ״̬
    public void Refresh(packageLocalItem packagelocalData,PackagePanel uiParent)
    {
        //���ݳ�ʼ��
        this.packagelocalData = packagelocalData;
        this.packagetableItem=GameManager.Instance.GetPackageLocalItemByID(packagelocalData.id);
        this.uiParent = uiParent;
        //�ȼ���Ϣ
        UILevel.GetComponent<Text>().text="Lv."+this.packagelocalData.level.ToString();
        //�Ƿ����»��
        UINew.gameObject.SetActive(this.packagelocalData.isNew);
        //��Ʒ��Ƭ
        Texture2D t = (Texture2D)Resources.Load(this.packagetableItem.imagePath);
        Sprite temp=Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0,0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //ˢ���Ǽ�
        RefreshStars();
    }
    public void RefreshStars()
    {
        for(int i=0;i<UIStars.childCount;i++)
        {
            Transform star = UIStars.GetChild(i);
            if (this.packagetableItem.star > i)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }

}
