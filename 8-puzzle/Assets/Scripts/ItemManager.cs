using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : ItemManagerBehavior
{
    public static ItemManager instance;
    public List<GameObject> itemPrefabs;

    private void Awake()
    {
        instance = this;
    }

    public override void CreateItem(RpcArgs args)
    {
        int i = args.GetNext<int>();
        Vector3 pos = args.GetNext<Vector3>();

        Debug.Log("Created Item");
        Instantiate(itemPrefabs[i - 1], pos, Quaternion.identity);
    }
}
