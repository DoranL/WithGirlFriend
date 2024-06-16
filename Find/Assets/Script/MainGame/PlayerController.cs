using Cinemachine;
using Fusion;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WebSocketSharp;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject cam;
    //[SerializeField] private new Camera camera;

    public Vector2 rotationInput;

    [Header("Character Controller Settings")]
    [SerializeField] private float gMoon = 1.6f;
    [SerializeField] private float moveSpeed = 10.0f;
    //[SerializeField] private float jumpForce = 8.0f;

    //입력 받은 텍스트로 닉네임 설정
    [Networked(OnChanged =nameof(OnNicknameChanged))] private NetworkString<_8> playerName {  get; set; }
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    [Networked]
    [HideInInspector]
    private bool IsGrounded { get; set; }

    private float horizontal;
    private float vertical;
    private Rigidbody rigid;

    private PlayerMovementController playerMovementController;
    private CharacterController controller;
    private Vector3 moveDir;
    private float yRotate;

    private float yaw;
    public float Yaw
    {
        get
        {
            return yaw;
        }
        set
        {
            yaw = value;
            if (yaw > 0)
            {
                yaw = 0;
            }
            if (yaw < 0)
            {
                yaw = 70f;
            }
        }
    }

    private float pitch;
    public float Pitch
    {
        get
        {
            return pitch;
        }
        set
        {
            pitch = value;

            pitch = Mathf.Clamp(pitch, -80, 80);
        }
    }

    private void SetLocalObjects()
    {
        if (Runner.LocalPlayer.IsValid == Object.HasInputAuthority)
        {
            cam.SetActive(true);
            //camera.gameObject.SetActive(true);
            var nickName = GlobalManager.Instance.networkController.LocalPlayerNickname;
            RpcSetNickName(nickName);
        }
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName(NetworkString<_8> nickName)
    {
        playerName = nickName;
    }

    private static void OnNicknameChanged (Changed<PlayerController> changed)
    {
        //changed.LoadNew();
        //var newNickName = changed.Behaviour.playerName;

        //changed.LoadOld();
        //var oldNickName = changed.Behaviour.playerName;

        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    private void SetPlayerNickname(NetworkString<_8> nickname)
    {
        playerNameText.text = nickname + " " + Object.InputAuthority.PlayerId;
    }

    public override void Spawned()
    {
        if(!HasInputAuthority)
        {
            Destroy(cam.gameObject);
            return;
        }

        if(HasStateAuthority)
        {
            rigid = GetComponent<Rigidbody>();
            playerMovementController = GetComponent<PlayerMovementController>();
        }
        SetLocalObjects();
    }

    public void BeforeUpdate()
    {
        if(Runner.LocalPlayer.IsValid == Object.HasInputAuthority)
        {
            const string HORIZONTAL = "Horizontal";
            const string VERTICAL = "Vertical";

            yaw += Input.GetAxis("Mouse X");
            pitch -= Input.GetAxis("Mouse Y");

            horizontal = Input.GetAxis(HORIZONTAL);
            vertical = Input.GetAxis(VERTICAL);
        }
    }

    //Happens before anything else fusion does, network application, reconlation etc
    //called at the start of the fusion update loop, before the fusion simulation loop
    //it fires before fusion does any work, every screen refresh.
    //FixedUpdateNetwork를 통해 예측, 연산, 재예측 등을 함
    public override void FixedUpdateNetwork()
    {
        //will return false if
        //the client does not have state authority or input authority
        //the requested type of input does not exist in the simulation
        if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out var input))
        {
            //rigid.velocity = new Vector3(0, rigid.velocity.y, input.VerticalInput * moveSpeed);
            Vector3 pos = transform.position;
            pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
            transform.position = pos;
            pos += Vector3.down * gMoon * Time.deltaTime;

            InputMovement(input);
        }
    }

    public override void Render()
    {
        if(!Object.HasInputAuthority)
        {
            return;
        }

        playerMovementController.RenderVisual(rigid.velocity);
    }
    private void InputMovement(PlayerData input)
    {
        buttonsPrev = default;

        var pressed = input.NetworkButtons.GetPressed(buttonsPrev);
        var released = input.NetworkButtons.GetReleased(buttonsPrev);

        moveDir = Vector3.zero;

        if (input.NetworkButtons.WasPressed(buttonsPrev, InputButtons.forward))
        {
            moveDir = rigid.transform.forward;
            rigid.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            //moveDir += Vector3.forward;

            //inputDir += Vector2.up;
        }

        if (input.NetworkButtons.WasPressed(buttonsPrev, InputButtons.back))
        {
           // moveDir += Vector3.back;
            //inputDir -= Vector2.up;
        }

        if (input.NetworkButtons.WasPressed(buttonsPrev, InputButtons.right))
        {
            yRotate += 300f * Time.deltaTime;
            Quaternion newRotation = Quaternion.Euler(0, yRotate, 0);

            rigid.MoveRotation(newRotation);
        }

        if (input.NetworkButtons.WasPressed(buttonsPrev, InputButtons.left))
        {
            yRotate -= 300f * Time.deltaTime;
            Quaternion newRotation = Quaternion.Euler(0, yRotate, 0);

            rigid.MoveRotation(newRotation);
        }

        buttonsPrev = input.NetworkButtons;
    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
       //GlobalManager.Instance.objectPoolingManager.RemoveNetworkObjectDic(Object);
       Destroy(gameObject);
    }

    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        
        data.HorizontalInput = horizontal;
        data.VerticalInput = vertical;

        data.NetworkButtons.Set(InputButtons.forward, Input.GetKey(KeyCode.W));
        data.NetworkButtons.Set(InputButtons.back, Input.GetKey(KeyCode.S));
        data.NetworkButtons.Set(InputButtons.right, Input.GetKey(KeyCode.D));
        data.NetworkButtons.Set(InputButtons.left, Input.GetKey(KeyCode.A));

        data.pitch = pitch;
        data.yaw = yaw;

        return data;
    }
}
