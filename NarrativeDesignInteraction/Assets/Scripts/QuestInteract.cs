using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QuestInteract : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public bool canInteract;
    public UpdateDialoguePhase ChangePhase;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetText("Press E to interact");
            canInteract = true;
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
        interactText.text = txt;
    }

    public void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {

                ChangePhase.Invoke("questCompleted");
                canInteract = false;
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