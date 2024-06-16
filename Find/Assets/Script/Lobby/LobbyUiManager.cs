using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUiManager : MonoBehaviour
{
    [SerializeField] private LoadingCanvas loadingCanvasControllerPrefab;
    [SerializeField] private LobbyPanelBase[] lobbyPanels;

    private void Start()
    {
        foreach (var lobby in lobbyPanels) 
        {
            lobby.InitPanel(this);
        }

        Instantiate(loadingCanvasControllerPrefab);
    }

    //패널 타입을 전달받아 기존 LobbyPanel에서 설정한 패널 타입과 동일하면 해당 패널 타입을 보여줌
    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach (var lobby in lobbyPanels)
        {
            if(lobby.panelType == type)
            {
                lobby.ShowPanel();

                break;
            }
        }
    }
}
