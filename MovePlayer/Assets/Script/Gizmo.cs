using UnityEngine;

public class Gizmo : MonoBehaviour
{
    private Vector3 targetOffset = new Vector3(0,0,4f);
    void OnDrawGizmos()
    { // transform.position은 현재 오브젝트의 월드 위치입니다.
        Vector3 origin = transform.position;
        Vector3 targetPosition = origin + targetOffset;
        Vector3 targetDirection = targetOffset.normalized;

        // Gizmos.color는 이후에 그릴 기즈모 도형의 색상을 지정하는 프로퍼티입니다.
        // Color.yellow는 유니티가 미리 제공하는 노란색 값입니다.
        Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere는 Scene 뷰에 구체를 그려 특정 위치를 표시하는 메서드입니다.
        Gizmos.DrawSphere(targetPosition, 0.15f);

        // Color.blue는 유니티가 미리 제공하는 파란색 값입니다.
        Gizmos.color = Color.blue;
        // Gizmos.DrawLine은 Scene 뷰에 두 점을 잇는 선을 그리는 메서드입니다.
        // transform.forward는 현재 오브젝트가 바라보는 앞 방향입니다.
        Gizmos.DrawLine(origin, origin + transform.forward * 3f);

    }
}
