using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseIntExit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}