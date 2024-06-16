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

    //�г� Ÿ���� ���޹޾� ���� LobbyPanel���� ������ �г� Ÿ�԰� �����ϸ� �ش� �г� Ÿ���� ������
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
