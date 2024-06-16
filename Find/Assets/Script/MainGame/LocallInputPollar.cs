using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocallInputPollar : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private PlayerController player;

    public static LocallInputPollar inputPollar;

    private NetworkRunner runner;

    //public List<Player> runningPlayers = new List<Player>();

    public NetworkPrefabRef playerPrefab;

    //private float yaw;
    //public float Yaw
    //{
    //    get
    //    {
    //        return yaw;
    //    }
    //    set
    //    {
    //        yaw = value;
    //        if (yaw > 0)
    //        {
    //            yaw = 0;
    //        }
    //        if (yaw < 0)
    //        {
    //            yaw = 360f;
    //        }
    //    }
    //}

    //private float pitch;
    //public float Pitch
    //{
    //    get
    //    {
    //        return pitch;
    //    }
    //    set
    //    {
    //        pitch = value;

    //        pitch = Mathf.Clamp(pitch, -80, 80);
    //    }
    //}

    private void Awake()
    {
        if (inputPollar == null)
        {
            inputPollar = this;
        }
        else if (inputPollar != this)
        {
            Destroy(gameObject); 
        }
    }

    //void Update()
    //{
    //    yaw += Input.GetAxis("Mouse X");
    //    pitch -= Input.GetAxis("Mouse Y");
    //}

    public override void Spawned()
    {
        if (Runner.LocalPlayer == Object.InputAuthority)
        {
            Runner.AddCallbacks(this);
        }
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    //Only if local we get input callback, no need to check
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (runner != null && runner.IsRunning)
        {

            var data = player.GetPlayerNetworkInput();
            
            //var dataInput = new PlayerData();

            //dataInput.buttons.Set(InputButtons.forward, Input.GetKey(KeyCode.W));
            //dataInput.buttons.Set(InputButtons.back, Input.GetKey(KeyCode.S));
            //dataInput.buttons.Set(InputButtons.right, Input.GetKey(KeyCode.D));
            //dataInput.buttons.Set(InputButtons.left, Input.GetKey(KeyCode.A));
            //dataInput.buttons.Set(InputButtons.jump, Input.GetKey(KeyCode.Space));

            //dataInput.pitch = pitch;
            //dataInput.yaw = yaw;

            //input.Set(dataInput);
            input.Set(data);
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //runningPlayers.Add(new Player(player, null));
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //runningPlayers.Remove(new Player(player, null));
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        if (!runner.IsServer)
        {
            return;
        }

        //foreach (var player in runningPlayers)
        //{
        //    var obj = runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player.playerRef);

        //    var controller = obj.GetComponent<CharacterController>();

        //    controller.enabled = false;
        //    obj.transform.position = new Vector3(0, 10, 0);
        //    controller.enabled = true;
        //}
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
}
