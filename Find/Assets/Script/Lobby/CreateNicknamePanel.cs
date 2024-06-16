using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNicknamePanel : LobbyPanelBase
{
    [Header("CreateNickNamePanel Vars")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button createNickNameFieldBtn;
    private const int MAX_CAHR_FOR_NICKNAME = 2;

    public override void InitPanel(LobbyUiManager lobbyUiManager)
    {
        base.InitPanel(lobbyUiManager);

        createNickNameFieldBtn.interactable = false;
        //AddListener�� ���� createNickNameField�� Ŭ���ϸ� OnClickCreateNickName() �Լ��� ȣ��
        createNickNameFieldBtn.onClick.AddListener(OnClickCreateNicknameBtn);
        //inputField ���� ���� �ø��� OnInputValueChanged() �Լ� ȣ��
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        Debug.Log("InitPanel");
    }

    private void OnInputValueChanged(string arg0)
    {
        //�÷��̾�κ��� �Է¹��� inputField ���� ������ �� MAX_CHAR_FOR_NICKNAME(2)���� ���̰� ��� true
        //createNickNameField�� Ȱ��ȭ(= CreateNickName ��ư)
        createNickNameFieldBtn.interactable = arg0.Length >= MAX_CAHR_FOR_NICKNAME;
        Debug.Log("OnInputValueChanged");
    }


    private void OnClickCreateNicknameBtn()
    {
        var nickName = inputField.text;
        if (nickName.Length >= MAX_CAHR_FOR_NICKNAME)
        {
            GlobalManager.Instance.networkController.SetPlayerNickname(nickName);

            base.ClosePanel();  
            //MiddleSectionPanel�� join room / create room�� �� �� �ִ� �г�
            lobbyUiManager.ShowPanel(LobbyPanelType.MiddleSectionPannel);
            Debug.Log("OnclickCreatedNickNameBtn");
        }
    }
}
