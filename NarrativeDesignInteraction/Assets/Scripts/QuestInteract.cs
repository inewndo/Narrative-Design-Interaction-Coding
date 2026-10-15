using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QuestInteract : MonoBehaviour
{
    public UpdateDialoguePhase ChangePhase;
    public string ratID;

    public void Awake()
    {
        ratID = this.name + "_" + transform.position.ToString();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyRats();
            if(GameController.instance.totalRats == 5)
            {
                ChangePhase.Invoke("questCompleted");
                Debug.Log("Quest Completed");
            }
        }
    }

    public void DestroyRats() 
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
    }
    
}

[System.Serializable]
public class UpdateDialoguePhase : UnityEvent<string>
{

}