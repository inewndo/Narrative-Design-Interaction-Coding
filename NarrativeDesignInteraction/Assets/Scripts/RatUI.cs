using TMPro;
using UnityEngine;

public class RatUI : MonoBehaviour
{
    public TextMeshProUGUI ratAmt;

    public void Awake()
    {
        GameController.instance.LoadRatUI(this);
    }

    public void RatUpdate(int rats)
    {
        ratAmt.text = rats.ToString();
    }
}
