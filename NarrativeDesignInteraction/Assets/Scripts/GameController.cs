using NUnit.Framework;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public AddCoins coinUpdate;
    public AddRats ratUpdate;

    public int totalCoins = 0;
    public int totalRats = 0;

    public CoinUI coinInventory;
    public RatUI ratInventory;
    public GameObject ThirdPersonRig;
    public Transform mainStartingTransform;
    public Vector3 loadPos;
    public Quaternion loadRot;

    public List<string> coinsCollected;
    public List<string> ratsCollected;
    public UpdateDialoguePhase ChangePhase;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;

            //this is a built in action within the scenemanger that requires a listener method that takes two arguements- scene and loadscenemodes
            //this action is called whenever a new scene is loaded
            SceneManager.sceneLoaded += OnSceneLoad;
            DontDestroyOnLoad(this);
        }

    }

    //this is a reciever of the sceneloaded acion. It checks which scene has been loaded usining build index number
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            //the below logic chacks if the data has veen loaded into the laodPos
            //if loadPos is blank then game spawns player at mainStartingTransform
            //if loadPos has data then game will spawm player in the presaved spot
            if (loadPos == Vector3.zero)
            {
                //calls the instantiation method using dynamic arguements for the psotion and rotation
                SpawnThirdPersonPrefab(mainStartingTransform.position, mainStartingTransform.rotation);
            }
            else
            {
                //if has already presaved coordinates - use them
                SpawnThirdPersonPrefab(loadPos, loadRot);
            }
            //coinUpdate.AddListener(coinInventory.CoinUpdate);
            LoadCoins();
            
        }
    }

    //PUBLIC method we can call from outside this class tp instantiate our thirdpersonrig and rotation
    //it takes the arguemnt, the postion and rotation of where we want tp spawn Thirdpersonrig prefab
    public void SpawnThirdPersonPrefab(Vector3 pos, Quaternion rot)
    {
        Instantiate(ThirdPersonRig, pos, rot);
    }

    //this metho d is called bt the trigger of our loading zzone when we exit scene
    //so when we reload the 3d demo scene we use the loadPos and locationRot data
    public void LoadLocationData(Vector3 pos, Quaternion rot)
    {
        loadPos = pos;
        loadRot = rot;
    }

    public void CoinCollect(string coinID)
    {
        //this singleton is called by each individual coin prefab
        //adds coinui class method called coinUpdate as a listener to the coinUpdate event
        // event passes totalcoins int as an arguement for that coinupdate method
        totalCoins++;
        coinUpdate.Invoke(totalCoins);
        //this method stores coinID of each prefab upon collection
        //invokes th UI to add a coin to the string count (not working atm)
        coinsCollected.Add(coinID);
    }

    public void RatsCollect(string ratID)
    {
        
        totalRats++;
        ratUpdate.Invoke(totalRats);
        
        ratsCollected.Add(ratID);
    }

    public void SceneChangeCheck()
    {
        Debug.Log("You have changed scenes.Current Coin Amount:" + totalCoins.ToString());
    }

    public void LoadCoins()
    {
        CoinCollect[] allCoinsInScene = FindObjectsByType<CoinCollect>();
        if (coinsCollected != null)
        {
            //make comparison- if object with the same idea has already been added to the list of the collected coins - destroy it
            foreach (CoinCollect coin in allCoinsInScene)
            {
                if (coinsCollected.Contains(coin.coinID))
                {
                    Destroy(coin.gameObject);
                }
            }
        }
    }
    public void LoadRats()
    {
        QuestInteract[] allRatsInScene = FindObjectsByType<QuestInteract>();
        if (ratsCollected != null)
        {
            //make comparison- if object with the same idea has already been added to the list of the collected coins - destroy it
            foreach (QuestInteract rat in allRatsInScene)
            {
                if (ratsCollected.Contains(rat.ratID))
                {
                    Destroy(rat.gameObject);
                }
            }
        }

        if (totalRats == 5)
        {
            ChangePhase.Invoke("questCompleted");
            Debug.Log("Quest Completed");
        }
    }

    //this is a 'setter' method for my coinInventory variable
    public void LoadCoinUI(CoinUI ui)
    {
        coinInventory = ui;
    }
    public void LoadRatUI(RatUI ui)
    {
        ratInventory = ui;
    }

    //this is a method that is called by startDialogue event in DialogueRUnner class
    //we will toggle true false in the event system to call whether we want to move or not
    public void SetPlayerMovement(bool b)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();
        if (b)
        {
            tpc.enabled = false;
        }
        else
        {
            tpc.enabled = true;
        }
    }

    public void MovePlayerOnNPC(Transform newPos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = newPos.position;
    }
}

[System.Serializable]
public class AddCoins : UnityEvent<int>
{

}

[System.Serializable]
public class AddRats : UnityEvent<int>
{

}

[System.Serializable]
public class UpdateDialoguePhase : UnityEvent<string>
{

}
