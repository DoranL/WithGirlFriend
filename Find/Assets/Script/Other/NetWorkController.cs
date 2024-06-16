using Fusion;
using Fusion.Sockets;
using NanoSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetWorkController : MonoBehaviour, INetworkRunnerCallbacks
{
    public event Action OnStartedRunnerConnection;
    public event Action OnPlayerJoinedSucessfully;

    public string LocalPlayerNickname { get; private set; }

    [SerializeField] private NetworkRunner networkPrefab;

    private NetworkRunner networkRunnerInstance;

    //public List<PlayerPrefs> players;

    public void ShutDownRunner()
    {
        networkRunnerInstance.Shutdown();
    }

    public void SetPlayerNickname(string str)
    {
        LocalPlayerNickname = str;
    }
    public async void StartGame(GameMode mode, string roomName)
    {
        OnStartedRunnerConnection.Invoke();

        if (networkRunnerInstance == null)
        {
            networkRunnerInstance = Instantiate(networkPrefab);
        }

        networkRunnerInstance.AddCallbacks(this);
        //�ش� Ŭ���̾�Ʈ���� �Է�
        //ProvideInput means that player is recording and sending inputs to the server
        networkRunnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            PlayerCount = 5,
            
            SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>(),
            //ObjectPool = networkRunnerInstance.GetComponent<ObjectPollingManager>()
        };

        var result = await networkRunnerInstance.StartGame(startGameArgs);
        
        if (result.Ok)
        {
            const string SCENE_NAME = "MainGame";
            networkRunnerInstance.SetActiveScene(SCENE_NAME);
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
        //debug finished
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectRequest");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnDisconnectedFromServer");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("OnHostMigration");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log("OnInput");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("OnInputMissing");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        OnPlayerJoinedSucessfully?.Invoke();
        Debug.Log("OnPlayerJoined");
        //�κ񿡼� create/join/randomjoin�� ���� �� LoadingCavnas - Start()���� OnPlayerJoinedSucessfully��
        //���������� �߰��ƴ��� Ȯ�� �߰��Ǹ� OnPlayerJoinedSucessfully�� null�� �ƴϱ� ������ Invoke�� ȣ���� => Invoke�� ������ �ð��� �����Ƿ� ��� 
        //LoadingCanvas - OnPlayerJoinedSucessfully() 
        //players.Add(player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
        //players.Remove(player);
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("OnReliableDataReceived");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadDone");

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadStart");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnSessionListUpdated");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutdown");

        const string LOBBY_SCENE = "Lobby";
        SceneManager.LoadScene(LOBBY_SCENE);
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("OnUserSimulationMessage");
    }
}
