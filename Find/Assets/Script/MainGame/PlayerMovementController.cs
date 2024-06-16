using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

// �� ����� ���� ��Ʈ��ũ ȯ�濡�� �÷��̾ ī�޶� �����ϴ� ���� 
// MonoBehaviour -> NetworkBehviour
public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private readonly int isMovingHash = Animator.StringToHash("isWalk");
    private bool init;


    private void Start()
    {
        init = true;
    }
    public void RenderVisual(Vector3 velocity)
    {
        if (!init) return;
        //var isMoving = velocity.x > 0.1f || velocity.x < -0.1f;
        var isMoving = Input.GetKey(KeyCode.W);

        if (velocity.magnitude>=0.1f)
        {
            animator.SetBool(isMovingHash, isMoving);
        }
        else
        {
            animator.SetBool(isMovingHash, isMoving);
        }
    }
}
