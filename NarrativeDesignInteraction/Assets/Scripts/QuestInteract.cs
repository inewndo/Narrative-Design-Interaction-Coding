using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QuestInteract : MonoBehaviour
{

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
            
        }
    }

    public void DestroyRats() 
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
    }
    
}
