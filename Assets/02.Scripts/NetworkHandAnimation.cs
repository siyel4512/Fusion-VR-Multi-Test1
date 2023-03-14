using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using HurricaneVR.Framework.Core.HandPoser;
using ExitGames.Client.Photon.StructWrapping;

public class NetworkHandAnimation : NetworkBehaviour
{
    public HVRHandAnimator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        RPC_SetComponets();
    }

    public override void FixedUpdateNetwork()
    {
        if (gameObject.activeSelf)
        {
            Debug.LogError("[SY] FixedUpdateNetwork() Call!!!");
            RPC_UpdateAnimation();
        }
    }

    [Rpc]
    private void RPC_SetComponets()
    {
        anim = GetComponent<HVRHandAnimator>();
    }

    [Rpc]
    private void RPC_UpdateAnimation()
    {
        //anim.UpdateState();
    }
}
