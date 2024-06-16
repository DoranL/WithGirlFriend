using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion.Sockets;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] public Transform[] spawnPoints;

    //List<NetWorkController> playerPrefab = new List<NetWorkController>();
    //bool isBotsSpawned = false;

    public override void Spawned()
    {
        if(Runner.IsServer)
        {
            foreach(var item in Runner.ActivePlayers)
            {
                SpawnPlayer(item);
            }
        }
    }

    //void SpawnBots()
    //{
    //    if(isBotsSpawned)
    //    {
    //        return;
    //    }
    //    int numberOfBotsToSpawn = 10;

    //    Debug.Log($"Number of bots to spawn{numberOfBotsToSpawn}Bot Spawn count {playerPrefab.Count}");

    //    for(int i =0; i<numberOfBotsToSpawn; i++)
    //    {
    //        playerPrefab spawnedBotsPlayer = Runner.Spawn(playerPrefab, Utils.GetRandomSpawnPoint(), Quaternion.identity, null, InitializeBeforeSpawn);
    //    }
    //}

    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer && HasStateAuthority)
        {
            var index = playerRef % spawnPoints.Length; 
            var spawnPoint = spawnPoints[index].transform.position;
            //Runner.SpawnÀº ³×Æ®¿öÅ© °´Ã¼ ¸¸µë
            var playerObject = Runner.Spawn(playerNetworkPrefab, spawnPoint, Quaternion.identity, playerRef);

            Runner.SetPlayerObject(playerRef, playerObject);
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }

    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                Runner.Despawn(playerNetworkObject);
            }

            //Reset player object
            Runner.SetPlayerObject(playerRef, null);
        }
    }
}
