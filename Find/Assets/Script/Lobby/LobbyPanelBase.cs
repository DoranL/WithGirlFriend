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

    //만든 패널 애니메이션을 실행하는 파트
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
