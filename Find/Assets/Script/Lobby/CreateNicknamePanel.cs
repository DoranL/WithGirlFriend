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
        //AddListener를 통해 createNickNameField를 클릭하면 OnClickCreateNickName() 함수를 호출
        createNickNameFieldBtn.onClick.AddListener(OnClickCreateNicknameBtn);
        //inputField 값이 변경 시마다 OnInputValueChanged() 함수 호출
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        Debug.Log("InitPanel");
    }

    private void OnInputValueChanged(string arg0)
    {
        //플레이어로부터 입력받은 inputField 값이 지정해 준 MAX_CHAR_FOR_NICKNAME(2)보다 길이가 길면 true
        //createNickNameField를 활성화(= CreateNickName 버튼)
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
            //MiddleSectionPanel은 join room / create room을 할 수 있는 패널
            lobbyUiManager.ShowPanel(LobbyPanelType.MiddleSectionPannel);
            Debug.Log("OnclickCreatedNickNameBtn");
        }
    }
}
