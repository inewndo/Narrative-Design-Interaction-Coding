using UnityEngine;

public class HouseIntEnter : MonoBehaviour
{

    private void Start()
    {
        GameController.instance.SpawnThirdPersonPrefab(this.transform.position, this.transform.rotation);
    }
}
