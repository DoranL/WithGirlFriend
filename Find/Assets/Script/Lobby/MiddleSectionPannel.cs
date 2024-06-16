using Fusion;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPannel : LobbyPanelBase
{
    [Header("MiddleSectionPanel Vars")]
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRandomArgBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomInputField;
    [SerializeField] private TMP_InputField createRoomInputField;
    private NetWorkController networkRunnerController;
    public override void InitPanel(LobbyUiManager uiManager)
    {
        base.InitPanel(uiManager);
        Debug.Log("InitPanel_Middle");

        networkRunnerController = GlobalManager.Instance.networkController;
        joinRandomRoomBtn.onClick.AddListener(JoinRandomRoom);
        //람다식 사용
        joinRandomArgBtn.onClick.AddListener(()=>CreateRoom(GameMode.Client, joinRoomInputField.text));
        createRoomBtn.onClick.AddListener(()=>CreateRoom(GameMode.Host, createRoomInputField.text));
    }

    private void CreateRoom(GameMode mode, string field)
    {
        if (field.Length >= 2)
        {
            Debug.Log(message: $"-----------{mode}--------");
            GlobalManager.Instance.networkController.StartGame(mode, field);
        }
    }
    private void JoinRandomRoom()
    {
        Debug.Log(message: $"-----------joinRandomRoom!--------");
        GlobalManager.Instance.networkController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }
}
