using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public string coinID;

    public void Awake()
    {
        coinID = this.name + "_" + transform.position.ToString();

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Collection();
            //Debug.Log(coinID);
        }
    }

    //this method is separated from OnTriggerEnter method in order to comparntmentilize code in case this functional behavior needs to be called from another class
    public void Collection()
    {
        //line below calls a method in the GameController singleton
        // We use th instance variable of that singleton to access the class data
        GameController.instance.CoinCollect(coinID);

        //turn off components of this object to simulate pick up
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
