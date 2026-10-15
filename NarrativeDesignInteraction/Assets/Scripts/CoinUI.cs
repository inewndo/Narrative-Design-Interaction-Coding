using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinAmt;

    public void Awake()
    {
        GameController.instance.LoadCoinUI(this);
    }
    public void Start()
    {
        //GameController.instance.coinInventory = this;
    }

    public void CoinUpdate(int coins)
    {
        coinAmt.text = coins.ToString();
    }
}