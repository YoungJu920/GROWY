using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] GameObject target;         // 카메라가 따라갈 대상
    [SerializeField] float moveSpeed;           // 카메라 속도
    [SerializeField] BoxCollider2D boundary;    // 바운더리
    
    private Vector3 targetPosition;             // 대상의 현재 위치값
    private Vector3 minBound;                   // BoxCollider 영역의 최소 xyz 값
    private Vector3 maxBound;                   // BoxCollider 영역의 최대 xyz 값

    private float halfWidht;                    // 카메라 가로 사이즈의 반
    private float halfHeight;                   // 카메라 세로 사이즈의 반

    // Start is called before the first frame update
    void Start()
    {
        minBound = boundary.bounds.min;
        maxBound = boundary.bounds.max;

        halfHeight = GetComponent<Camera>().orthographicSize;   
        halfWidht = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            //// 단순하게 따라가기 (이렇게 하면 그래픽 안흔들림)
            // this.transform.position = targetPosition;

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidht, maxBound.x - halfWidht);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBoundary(BoxCollider2D newBoundary)
    {
        boundary = newBoundary;
        minBound = boundary.bounds.min;
        maxBound = boundary.bounds.max;
    }
}
