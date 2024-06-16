using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

// �� ����� ���� ��Ʈ��ũ ȯ�濡�� �÷��̾ ī�޶� �����ϴ� ���� 
// MonoBehaviour -> NetworkBehviour
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private readonly int isMovingHash = Animator.StringToHash("isWalk");

    public void RenderVisual(Vector3 velocity)
    {
        //var isMoving = velocity.x > 0.1f || velocity.x < -0.1f;
        var isMoving = Input.GetKey(KeyCode.W);

        if (velocity.magnitude>=0.1f)
        {
            //float targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
            animator.SetBool(isMovingHash, isMoving);
        }
        else
        {
            animator.SetBool(isMovingHash, isMoving);
        }
    }
}
