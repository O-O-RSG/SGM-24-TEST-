using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������� �� ��,��� ������ ����� ��������� CharacterController
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    private Animator animator;
    //������ �� ������, ������������ �������� ���������� ����������� (������)
    [SerializeField] Transform target;
    public float moveSpeed = 6.0f;
    //�������� ������ 
    public float jumpSpeed = 15.0f;
    //����������
    public float gravity = -9.8f;
    //���������� �������� 
    public float terminalVelocity = -10.0f;
    //����������� �������� �������
    public float minFall = -1.5f;

    private float vertSpeed;

    private CharacterController charController;

    public float rotSpeed = 15.0f;
    //����� ��� �������� ������ � ������������ ����� ���������
    private ControllerColliderHit contact;

    private Transform platformParent;

    private void Start()
    {
        //�������������� ���������� ������������ ��������
        vertSpeed = minFall;
        //��� ������� � ������ �������������� �����������
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //�������� � ������� (0, 0, 0), ���������� �������� ���������� ��������
        Vector3 movement = Vector3.zero;
        
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        //�������� �������������� ������ ��� ������� ������ �� ���������
        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            //��cc�������� ����������� ������
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            //���������� ������� ������ � ������ �����������, ����� �������� ��������������� ������ ��������
            movement = (right * horInput) + (forward * vertInput);
            movement *= moveSpeed;
            //������������ ������ ������� moveSpeed 
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            //�������� ������ ����������� ������� LookRotation(), ������� ��������� ����������, ��������� � ���� �����������
            Quaternion direction = Quaternion.LookRotation(movement);
            //Quaternion.Lerp() ��������� ������� ������� �� �������� ��������� � ������� � ���������� �������������
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed*Time.deltaTime);
        }
        bool hitGround = false;
        RaycastHit hit;
        //���������, ������ �� ��������
        
        if (vertSpeed < 0 && Physics.Raycast(transform.position,Vector3.down,out hit))
        {
            //����������, � ������� ������������ ���������(������ ������� �������� ����� �������)
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
        // ������ �������� Speed,������� ������� � �����������
        animator.SetFloat("Speed",movement.sqrMagnitude);
        //�������� isGrounded ���������� CharacterController ���������,������������� �� ���������� � ������������?
        //��������. ������ �������� �������� isGrounded ������� ��������� �����������
        if (hitGround) 
        {
            if(Input.GetButtonDown("Jump")) 
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
                //��������� ��������
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            //���� �������� �� ����� �� �����������, ��������� ����������, ���� �� ����� ���������� ���������� ��������
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            //���� �� ������������� �� ������� 
            if (contact != null)
            {
                //����������� ��������
                animator.SetBool("Jumping", true);
            }
            //��� �� ������������ �����������, �� ������� � ��� �������������
            if (charController.isGrounded)
            {
                //������� �������� � ����������� �� ����, ������� �� �������� � ����� ��������
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        //����������� �������� �� ��������� 
        movement.y = vertSpeed;
        //����� �� �������� �� ������� ������ �������� �� deltaTime
        movement *= Time.deltaTime;
        //������� �������� ���������
        charController.Move(movement);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlyingPlatform")
        {
            platformParent = other.transform;
            transform.SetParent(platformParent);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FlyingPlatform")
        {
            transform.SetParent(null);
            platformParent = null;
        }
    }
}
