using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class NPCInteract : MonoBehaviour
{
    //TODO
    //Get story data to set the phase of our Yarnspinner scripts
    //Set story data from yarnspinner scripts

    public TextMeshProUGUI promptText;
    public string interactText;
    public bool canInteract;
    public DialogueRunner npcDialogue;
    public NPCCamRig npcCamRig;
    public NPCData storyData;

    public void Start()
    {
        //setting this variable look sa bit static for our system design taste but we cam [ractive a bti of deliberate design because we know we are creating a systemized NPC rig and interaction for multiple isntances
        npcCamRig = transform.GetComponentInChildren<NPCCamRig>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetText(interactText);
            canInteract = true;
        }
    }
    void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Start NPC dialogue");
                SetText("");
                npcCamRig.MovePlayer();
                npcDialogue.StartDialogue(storyData.startNode);
                canInteract = false;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetText("");
            canInteract = false;
        }
    }

    public void SetText(string txt)
    {
        promptText.text = txt;
    }

    //This YarnCommand/method sets the dialogue phase variable in my YarnSpinner script
    //We use it in our LoadPhase node to route the story to its correct story path
    [YarnCommand("SetPhase")]
    public void SetStoryPhase()
    {
        InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vStore.SetValue("$dialoguePhase", storyData.currentPhase.ToString());
    }

    //this YarnCommand/method gets a string form yarnSpinner and converts it to the phase we are in our npcData scriptableObejct
    //It also contains an error reporting in case we pass a string that does not correspond to any dialoguephase
    [YarnCommand("GetPhase")]
    public void GetStoryPhase(string phase)
    {
        if (phase == "comeback")
        {
            storyData.currentPhase = NPCData.dialoguePhase.comeback;
        }
        else if (phase == "questStarted")
        {
            storyData.currentPhase = NPCData.dialoguePhase.questStarted;
        }
        else if (phase == "questCompleted")
        {
            storyData.currentPhase = NPCData.dialoguePhase.questCompleted;
        }
        else if (phase == "rewardGiven")
        {
            storyData.currentPhase = NPCData.dialoguePhase.rewardGiven;
        }
        else
        {
            Debug.LogError("The string you passed in the YarnScript does not match any current dialogue phase in the NPC Data Scriptable Object");
        }

        if (phase == "npc2_comeback")
        {
            storyData.currentPhase = NPCData.dialoguePhase.comeback;
        }
        else if (phase == "npc2_questStarted")
        {
            storyData.currentPhase = NPCData.dialoguePhase.questStarted;
        }
        else if (phase == "npc2_questCompleted")
        {
            storyData.currentPhase = NPCData.dialoguePhase.questCompleted;
        }
        else if (phase == "npc2_rewardGiven")
        {
            storyData.currentPhase = NPCData.dialoguePhase.rewardGiven;
        }
        else
        {
            Debug.LogError("The string you passed in the YarnScript does not match any current dialogue phase in the NPC Data Scriptable Object");
        }
    }

    public void OnApplicationQuit()
    {
        storyData.DataReset();
    }
}