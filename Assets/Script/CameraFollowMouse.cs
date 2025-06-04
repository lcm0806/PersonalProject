using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{

    public float panSpeed = 5f; // ī�޶� �̵� �ӵ�
    public float panBorderThickness = 10f; // ȭ�� �����ڸ� ���� �β� (�ȼ�)
    public float panLimitMinX; // ī�޶� �̵� ���� �ּ� X ��ǥ
    public float panLimitMaxX; // ī�޶� �̵� ���� �ִ� X ��ǥ

    private float initialYPos; // ī�޶��� �ʱ� Y�� ��ġ (������ Y��)

    void Start()
    {
        initialYPos = transform.position.y; // ��ũ��Ʈ ���� �� ���� Y�� ��ġ�� ����
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // ���콺 ��ġ �������� (Legacy Input System)
        Vector2 mousePosition = Input.mousePosition;

        // --- Input System ��Ű���� ����ϴ� ��� �Ʒ� �ּ� ���� ---
        // Vector2 mousePosition = Mouse.current.position.ReadValue();

        // ȭ�� ������ �̵� (X��)
        if (mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        // ȭ�� ���� �̵� (X��)
        if (mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // Y���� Start()���� ������ �ʱ� ��ġ�� ����
        pos.y = initialYPos;

        // ī�޶� �̵� ���� (X�ุ)
        pos.x = Mathf.Clamp(pos.x, panLimitMinX, panLimitMaxX);

        transform.position = pos;
    }
}
