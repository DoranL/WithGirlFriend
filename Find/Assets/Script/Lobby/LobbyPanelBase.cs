using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    [field: SerializeField, Header("LobbyPanel Vars")]
    public LobbyPanelType panelType { get; private set; }
    [SerializeField] private Animator panelAnimator;

    protected LobbyUiManager lobbyUiManager;

    public enum LobbyPanelType
    {
        None,
        CreateNicknamePanel,
        MiddleSectionPannel
    }

    public virtual void InitPanel(LobbyUiManager uiManager)
    {
        lobbyUiManager = uiManager;
    }

    //���� �г� �ִϸ��̼��� �����ϴ� ��Ʈ
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        const string POP_IN_CLIP_NAME = "In";
        panelAnimator.Play(POP_IN_CLIP_NAME);
    }
    public void ClosePanel()
    {
        const string POP_OUT_CLIP_NAME = "Out";
        StartCoroutine(Utils.PlayAnimAndSetState(gameObject, panelAnimator, POP_OUT_CLIP_NAME, false));
    }
}
