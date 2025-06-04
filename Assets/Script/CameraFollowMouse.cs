using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{

    public float panSpeed = 5f; // 카메라 이동 속도
    public float panBorderThickness = 10f; // 화면 가장자리 영역 두께 (픽셀)
    public float panLimitMinX; // 카메라 이동 제한 최소 X 좌표
    public float panLimitMaxX; // 카메라 이동 제한 최대 X 좌표

    private float initialYPos; // 카메라의 초기 Y축 위치 (고정될 Y축)

    void Start()
    {
        initialYPos = transform.position.y; // 스크립트 시작 시 현재 Y축 위치를 고정
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // 마우스 위치 가져오기 (Legacy Input System)
        Vector2 mousePosition = Input.mousePosition;

        // --- Input System 패키지를 사용하는 경우 아래 주석 해제 ---
        // Vector2 mousePosition = Mouse.current.position.ReadValue();

        // 화면 오른쪽 이동 (X축)
        if (mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        // 화면 왼쪽 이동 (X축)
        if (mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // Y축은 Start()에서 저장한 초기 위치로 고정
        pos.y = initialYPos;

        // 카메라 이동 제한 (X축만)
        pos.x = Mathf.Clamp(pos.x, panLimitMinX, panLimitMaxX);

        transform.position = pos;
    }
}
