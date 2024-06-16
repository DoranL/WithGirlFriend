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
        // Ŀ�� ���
        Cursor.lockState = CursorLockMode.Locked;

        // Cinemachine Transposer ���� ��������
        //transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        // �ʱ� ������ ����
        SetCameraPosition();

        // LookAt ����
        virtualCamera.LookAt = playerBody;
    }


    void Update()
    {
        // ���콺 �Է� ó��
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation += mouseY;

        // ī�޶� ��ġ ������Ʈ
        SetCameraPosition();
    }

    public void SetCameraPosition()
    {
        // �÷��̾��� ��ġ�� �������� ī�޶� ������ ����
        Vector3 offset = new Vector3(0, 4.5f, -distance);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 rotatedOffset = rotation * offset;

        // Transposer�� Follow Offset ����
        //transposer.m_FollowOffset = rotatedOffset;
    }
}
