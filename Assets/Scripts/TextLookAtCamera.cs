using UnityEngine;

public class TextLookAtCamera : MonoBehaviour
{
    [Header("Ссылки")]
    public Transform textObject;
    public Camera vrCamera;

    [Header("Настройки")]
    public bool copyCameraRotation = false;

    void LateUpdate()
    {
        if (textObject == null || vrCamera == null)
            return;

        if (copyCameraRotation)
        {
            // Полностью копирует rotation камеры
            textObject.rotation = vrCamera.transform.rotation;
        }
        else
        {
            // Смотрит на камеру
            Vector3 dir = textObject.position - vrCamera.transform.position;

            if (dir.sqrMagnitude > 0.001f)
            {
                textObject.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
}