using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class FinalScoreManager : MonoBehaviourPunCallbacks
{
    GameObject p1Text;
    GameObject p2Text;

    private int p1Score = 0;
    private int p2Score = 0;

    private void Start()
    {
        Debug.Log("scoremanager");
        p1Text = GameObject.Find("p1Score");
        p2Text = GameObject.Find("p2Score");
    }

    public void AddScore(bool isLocalPlayer)
    {
        Debug.Log("addscore");
        if (isLocalPlayer)
        {
            p1Score++;
            Debug.Log("P1 Score: " + p1Score);
        }
        else
        {
            p2Score++;
            Debug.Log("P2 Score: " + p2Score);
        }

        // RPC 호출하여 점수 동기화
        photonView.RPC("SyncScore", RpcTarget.All, p1Score, p2Score);
    }

    [PunRPC]
    private void SyncScore(int p1, int p2)
    {
        Debug.Log("sync");

        p1Text.GetComponent<TMP_Text>().text = p1.ToString();
        p2Text.GetComponent<TMP_Text>().text = p2.ToString();
    }

}
