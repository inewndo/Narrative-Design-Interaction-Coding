using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;

public class NPCCamRig : MonoBehaviour
{
    //TODO
    //DONE Switch between the different transforms to set what view our camera is
    //DONE Switch from tpc to camera rig using cinemachine priority
    //DONE Set when the switch will happen by having yarnspinner call the correct method
    //DONE Switch back from the NPC cam to tpc

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
