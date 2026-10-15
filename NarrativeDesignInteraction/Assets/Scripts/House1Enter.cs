using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEnter : MonoBehaviour
{
    public Transform houseReturn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.instance.LoadLocationData(houseReturn.position, houseReturn.rotation);
            //go to 'next' scene
            SceneManager.LoadScene(1);
        }
    }
}
