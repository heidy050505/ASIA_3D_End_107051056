using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.2f;

    /// <summary>
    /// 玩家是否進入感應區
    /// </summary>
    public bool playerInArea;

    public enum NPCState
    {
        FristDialog, Missioning, Finish
    }
    
    public NPCState state = NPCState.FristDialog;

    /* 協同程序
    private void Start()
    {
        //啟動協同程序
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        print("嗨");
        yield return new WaitForSeconds(1.5f);
        print("一點五秒後");
        yield return new WaitForSeconds(2);
        print("兩秒後");
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Robot Kyle")
        {
            playerInArea = true;
            StartCoroutine(Dialog());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Robot Kyle")
        {
            playerInArea = false;
            StopDialog();
        }
    }
    /// <summary>
    /// 停止對話
    /// </summary>
    private void StopDialog()
    {
        dialog.SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// 開始對話
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dialog()
    {
        dialog.SetActive(true);
        textContent.text = "";

        textName.text = name;

        string dialogString = data.dialougA;

        switch (state)
        {
            case NPCState.FristDialog:
                dialogString = data.dialougA;
                break;
            case NPCState.Missioning:
                dialogString = data.dialougB;
                break;
            case NPCState.Finish:
                dialogString = data.dialougC;
                break;
        }

        for (int i = 0; i< dialogString.Length; i++)
	    {
            textContent.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
	    }
    }


}
