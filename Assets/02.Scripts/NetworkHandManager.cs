using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkHandManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        RPC_TEST();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_TEST()
    {
        Debug.Log("RPC_TEST() »£√‚");
    }
}
