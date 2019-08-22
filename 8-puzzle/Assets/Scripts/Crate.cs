using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : CrateBehavior
{
    public Transform CrateLid;

    public override void CrateOpen(RpcArgs args)
    {
        StartCoroutine(Activate());
        BMSLogger.DebugLog("CrateActivated");
    }

    IEnumerator Activate()
    {
        for(int i = 0; i <= 20; i++)
        {
            CrateLid.rotation = Quaternion.Slerp(Quaternion.Euler(-90f, 0f, 0f), Quaternion.Euler(-0f, 0f, 0f), i / 20f);
            yield return null;
        }
//        yield return new WaitForSeconds(stopTime);
    }

}
