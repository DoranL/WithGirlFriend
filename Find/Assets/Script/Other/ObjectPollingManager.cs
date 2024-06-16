using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollingManager : MonoBehaviour//, INetworkObjectPool
{
    //private Dictionary<NetworkObject, List<NetworkObject>> prefabsInstantiate = new Dictionary<NetworkObject, List<NetworkObject>>();
   
    //private void Start()
    //{
    //    if(GlobalManager.Instance != null)
    //    {
    //        GlobalManager.Instance.objectPoolingManager = this;
    //    }
    //}

    //public NetworkObject AcquireInstance(NetworkRunner runner, NetworkPrefabInfo info)
    //{
    //    NetworkObject networkObject = null;
    //    NetworkProjectConfig.Global.PrefabTable.TryGetPrefab(info.Prefab, out var prefab);
    //    prefabsInstantiate.TryGetValue(prefab, out var networkObjects);

    //    bool foundMatch = false;
    //    if(networkObjects?.Count > 0)
    //    {
    //        foreach(var item in networkObjects)
    //        {
    //            if(item != null && item.gameObject.activeSelf == false)
    //            {
    //                networkObject = item;

    //                foundMatch = true;
    //                break;
    //            }
    //        }
    //    }

    //    if(foundMatch == false)
    //    {
    //        networkObject = CreateObjectInstance(prefab);
    //    }
    //    return networkObject;
    //}

    //private NetworkObject CreateObjectInstance(NetworkObject prefab)
    //{
    //    var obj = Instantiate(prefab);

    //    if(prefabsInstantiate.TryGetValue(prefab, out var instanceData))
    //    {
    //        instanceData.Add(obj);
    //    }
    //    else
    //    {
    //        var list = new List<NetworkObject>{ obj };
    //        prefabsInstantiate.Add(prefab, list);
    //    }

    //    return obj;
    //}

    //public void ReleaseInstance(NetworkRunner runner, NetworkObject instance, bool isSceneObject)
    //{
    //    instance.gameObject.SetActive(false);
    //}

    //public void RemoveNetworkObjectDic(NetworkObject obj)
    //{
    //    if (prefabsInstantiate.Count > 0)
    //    {
    //        foreach (var item in prefabsInstantiate)
    //        {
    //            foreach(var networkObject in item.Value)
    //            {
    //                if(networkObject == obj)
    //                {
    //                    item.Value.Remove(networkObject);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
}
