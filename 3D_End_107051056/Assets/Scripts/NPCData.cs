using UnityEngine;

[CreateAssetMenu(fileName = "NPC資料", menuName = "Wang/NPC 資料")]
public class NPCData : ScriptableObject
{
    [Header("第一段對話"), TextArea(1, 5)]
    public string dialougA;
    [Header("第二段對話")]
    public string dialougB;
    [Header("第三段對話")]
    public string dialougC;
    [Header("任務項目需求數量")]
    public int count;
    [Header("已經取得項目數量")]
    public int countCurrent;
}
