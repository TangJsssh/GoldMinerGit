using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    //对属性进行初始化
    //顶部
    private Transform UIMenu;
    private Transform UIMenuWeapon;
    private Transform UIMenuFood;
    private Transform UITabName;
    private Transform UICloseBtn;
    //中部
    private Transform UICenter;
    private Transform UIScrollview;
    private Transform UIDetailPanel;
    private Transform UILeftBtn;
    private Transform UIRightBtn;
    //底部
    private Transform UIDeletePanel;
    private Transform UIDeleteBackBtn;
    private Transform UIDelelteInfoText;
    private Transform UIDeleteConfirmBtn;

    private Transform UIBottomMenus;
    private Transform UIDeleteBtn;
    private Transform UIDetailBtn;

    public GameObject PackageUIItemPrefab;
    private void Awake()
    {
        base.Awake();
        InitUI();
    }
    private void Start()
    {
        RefreshUI();
    }
    private void RefreshUI()
    {
        RefreshScroll();
    }
    private void RefreshScroll()
    {
        //清理容器中物品
        RectTransform scrollContent = UIScrollview.GetComponent<ScrollRect>().content;
        for(int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        foreach(packageLocalItem localData in GameManager.Instance.GetSortPackageLocalData()){
            Transform PackageUIItem =Instantiate(PackageUIItemPrefab.transform,scrollContent) as Transform;
            packageCell packageCell=PackageUIItem.GetComponent<packageCell>();
            packageCell.Refresh(localData, this);
        }
    }
    private void InitUI()
    {
        InitUIName();
        InitClick();
    }
    private void InitUIName()
    {
        UIMenu = transform.Find("topCenter/Menus");
        UIMenuWeapon = transform.Find("topCenter/Menus/weapon");
        UIMenuFood = transform.Find("topCenter/Menus/food");
        UITabName = transform.Find("leftTop/TabName");
        UICloseBtn = transform.Find("rightTop/Close");
        UICenter = transform.Find("center");
        UIScrollview = transform.Find("center/Scroll View");
        UIDetailPanel = transform.Find("center/DetailPanel");
        UILeftBtn = transform.Find("center/PageTurningLeft/icon");
        UIRightBtn = transform.Find("center/PageTurningRight/icon");
        UIDeletePanel = transform.Find("bottom/DeletePanel");
        UIDeleteBackBtn = transform.Find("bottom/DeletePanel/Back");
        UIDelelteInfoText = transform.Find("bottom/DeletePanel/InfoText");
        UIDeleteConfirmBtn = transform.Find("bottom/DeletePanel/ConfirmBtn");
        UIBottomMenus = transform.Find("bottom/BotttomMenus");
        UIDeleteBtn = transform.Find("bottom/BotttomMenus/DeleteBtn");
        UIDetailBtn = transform.Find("bottom/BotttomMenus/DetailBtn");
        UIDeletePanel.gameObject.SetActive(false);
        UIBottomMenus.gameObject.SetActive(true);
    }

    private void InitClick()
    {
        UIMenuWeapon.GetComponent<Button>().onClick.AddListener(OnClickWeapon);
        UIMenuFood.GetComponent<Button>().onClick.AddListener(OnClickFood);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UILeftBtn.GetComponent<Button>().onClick.AddListener(OnClickLeft);
        UIRightBtn.GetComponent<Button>().onClick.AddListener(OnClickRight);
        UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnClickDeleteBack);
        UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(OnClickDeleteConfirmBtn);
        UIDeleteBtn.GetComponent<Button>().onClick.AddListener(OnClickDelete);
        UIDetailBtn.GetComponent<Button>().onClick.AddListener(OnClickDetailBtn);

    }

    private void OnClickDetailBtn()
    {
        print("1");
    }

    private void OnClickDelete()
    {
        print("1");
    }

    private void OnClickDeleteConfirmBtn()
    {
        print("1");
    }

    private void OnClickDeleteBack()
    {
        print("1");
    }

    private void OnClickRight()
    {
        print("1");
    }

    private void OnClickLeft()
    {
        print("1");
    }

    private void OnClickClose()
    {
        ClosePanel();
        //UIManager.Instance.ClosePanel(UIConst.PackagePanel);

    }

    private void OnClickFood()
    {
        print("1");
    }

    private void OnClickWeapon()
    {
        print("1");
    }
}
