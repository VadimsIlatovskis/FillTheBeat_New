using System.Collections;
using UnityEngine;

public class SnapRigHeadToStartPoint : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Camera vrCamera;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        Vector3 headToStartOffset = startPoint.position - vrCamera.transform.position;
        headToStartOffset.y = 0f;

        transform.position += headToStartOffset;

        float yawDifference =
            startPoint.eulerAngles.y - vrCamera.transform.eulerAngles.y;

        transform.RotateAround(
            vrCamera.transform.position,
            Vector3.up,
            yawDifference
        );
    }
}