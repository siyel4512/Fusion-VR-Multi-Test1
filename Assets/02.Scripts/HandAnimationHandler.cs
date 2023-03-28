using Fusion;
using HurricaneVR.Framework.Core.HandPoser;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandAnimationHandler : NetworkBehaviour
{
    public HVRHandAnimator hvrHandAnimator;
    public NetworkObject networkObject;
    public TMP_Text fingerInputState;
    public float[] fingerCurlSource = new float[5];

    public override void FixedUpdateNetwork()
    {
        if (hvrHandAnimator.FingerCurlSource == null)
        {
            return;
        }

        for (int i = 0; i < fingerCurlSource.Length; i++)
        {
            fingerCurlSource[i] = hvrHandAnimator.FingerCurlSource[i];
        }

        if (networkObject.HasInputAuthority)
        {
            RPC_GetFingerInputValue(fingerCurlSource);
        }

        if (networkObject.HasInputAuthority)
        {
            RPC_SetHandPos(fingerCurlSource);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_GetFingerInputValue(float[] _fingerCurlSource)
    {
        fingerInputState.text = _fingerCurlSource[0] + " / " + _fingerCurlSource[1] + " / " + _fingerCurlSource[2] + " / " + _fingerCurlSource[3] + " / " + _fingerCurlSource[4];
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SetHandPos(float[] _fingerCurlSource)
    {
        hvrHandAnimator.UpdateFingerCurls(_fingerCurlSource);
        hvrHandAnimator.UpdatePoser();
    }
}
