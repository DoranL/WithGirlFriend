using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerBody;
    public float distance = 5.0f;
    public float sensitivity = 100.0f;

    private float yRotation = 0f;
    private float xRotation = 0f;
    private CinemachineTransposer transposer;

    void Start()
    {
        // 커서 잠금
        Cursor.lockState = CursorLockMode.Locked;

        // Cinemachine Transposer 설정 가져오기
        //transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        // 초기 오프셋 설정
        SetCameraPosition();

        // LookAt 설정
        virtualCamera.LookAt = playerBody;
    }


    void Update()
    {
        // 마우스 입력 처리
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation += mouseY;

        // 카메라 위치 업데이트
        SetCameraPosition();
    }

    public void SetCameraPosition()
    {
        // 플레이어의 위치를 기준으로 카메라 오프셋 설정
        Vector3 offset = new Vector3(0, 4.5f, -distance);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 rotatedOffset = rotation * offset;

        // Transposer의 Follow Offset 설정
        //transposer.m_FollowOffset = rotatedOffset;
    }
}
