using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed;     // ���������� �����, �������� �������� ����� ���������
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb; // ���������� �����������
    private SpriteRenderer sprite;
    private Animator animatorr;

    private bool grounded; // ������ ����������, ������������ ��� ����, ����� ���������� ��������� �������

    private States Stat // ��������� � ��������� �������� � ��������� ������������� ���������� ��� ��������������� ������ ��������
    {
        get { return (States)animatorr.GetInteger("status"); } 
        set { animatorr.SetInteger("status", (int)value); }
    }
    private void Awake() // �����, ���������� ��� ������� �������
    {
        rb = GetComponent<Rigidbody2D>(); // ���������� �������� ��������
        sprite = GetComponentInChildren<SpriteRenderer>(); // ���� ��������� �������� �������� �� �������� ��������
        animatorr = GetComponent<Animator>();
    }

    private void FixedUpdate() // �������, ���������� ����� ������������� ���������� ������
    {
        CheckGround();
    }

    private void Update() // �����, ���������� �� ������ �����
    {
        if (grounded) Stat = States.stand; // ��������������� �������� stand, ���� ������ ���������� ����� 1
        if (Input.GetButton("Horizontal")) // �������� �� ��������� ������ ��������
            Move(); 
        if (grounded && Input.GetButtonDown("Jump")) // �������� �� ������� ��������� ������� "������" � ������� ����������� ��� ��������
        {
            animatorr.SetTrigger("prepare_jump"); // ��������� �������� �� ��������������� �������� ���������� � ������
            Jump();
        }
        if (!grounded) Stat = States.jump_go; // �������� �� ���������� ����������� ��� ��������, � ��������� ������ ��������������� �������� ������
    }
    private void Move() // ���������� ������� ��������
    {
        if (grounded) Stat = States.move; // �������� �� ������� ����������� ��� �������� � ��������� �������� ��������

        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal"); // ���������� ����������, ������� �������� �������� ��������� ������� �� ��� �������
        transform.position = Vector3.MoveTowards(transform.position, horizontalInput + transform.position, Time.deltaTime * speed); // ��������� ���������� �� ������� � �����������, ������� ���������� �� �������� ������� �������, ����� ������� � ��������� �����������
        sprite.flipX = horizontalInput.x < 0.0f; // ��������� ������� �� ��� ������� ��� ������������� ����� �������� X
    }

    private void Jump() // ���������� ������� ������
    {
        rb.velocity = Vector2.up * jumpForce; // �������� ������� ��������, ������������� ����������� �����
    }

    private void CheckGround() // ���������� ������� �������� �� ���������� �� �����������
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f); // ���������� �������, ������ �������� ������� �� ���������� ���������� �������� � ������������ �������
        grounded = collider.Length > 1; // ������� �������� ������� ���������� � ����������� �� ������ �������
    }

}

public enum States // ��������� ������������, �������������� ����������
{
    stand,
    move,
    jump_go
}