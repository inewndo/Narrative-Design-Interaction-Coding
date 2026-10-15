using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "NPCProfile")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public string startNode;
    public enum dialoguePhase { begin, comeback, questStarted, questCompleted, rewardGiven}
    public dialoguePhase currentPhase;

    public void DataReset()
    {
        currentPhase = dialoguePhase.begin;
    }
}