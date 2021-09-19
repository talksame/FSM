using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyScript : MonoBehaviour
{
    //�̵�, ����, �뽬 �����ϱ�.
    [SerializeField]


    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;

    private Rigidbody rigidbody;

    private Vector3 inputDirection = Vector3.zero;

    private bool isGrounded = false;
    //�����ɽ�Ʋ ȿ��
    public LayerMask groundlayermask;
    public float groundCheckDistance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        if ( inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
        }

        // Process Jump input

        if (Input.GetKeyDown("space"))
        {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        // Process dash Input
        if (Input.GetKeyDown("s"))
        {
            //�α��Լ��� ���ؼ� ���� ������ �� �ְ� ������.
            Vector3 dashVelocty = Vector3.Scale(transform.forward, dashDistance * new Vector3(
                (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime), 
                0,
                (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime)
                ));
            rigidbody.AddForce(dashVelocty, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        //������������ �̿�Ǳ� ������, ���Ӱ��� ������� ���������� Ȱ��Ǵ� ������Ʈ �Լ�.

        rigidbody.MovePosition( rigidbody.position + inputDirection * speed * Time.fixedDeltaTime);
        
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        
        if ( Physics.Raycast(transform.position + (Vector3.up * 0.1f),
            Vector3.down, out hitInfo, groundCheckDistance, groundlayermask))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }
    }
}
