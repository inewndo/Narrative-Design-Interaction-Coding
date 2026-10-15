using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;

public class NPCCamRig : MonoBehaviour
{
    public Transform nPCLoc;
    public Transform pCLoc;
    public Transform pcMoveLoc;
    public int pNum;
    public CinemachineCamera nPC_C_Cam;


    [YarnCommand("CamMove")]
    public void CamMove(string locName)
    {
        nPC_C_Cam.Priority = pNum;
        if (locName == "npc")
        {
            nPC_C_Cam.gameObject.transform.position = nPCLoc.position;
        }
        else if (locName == "pc")
        {
            nPC_C_Cam.gameObject.transform.position = pCLoc.position;
        }
        else if (locName == "return")
        {
            nPC_C_Cam.Priority = 0;
        }
    }

    public void MovePlayer()
    {
        GameController.instance.MovePlayerOnNPC(pcMoveLoc);
    }
}
