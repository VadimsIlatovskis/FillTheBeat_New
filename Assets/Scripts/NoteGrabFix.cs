using UnityEngine;
using System.Collections;

public class NoteGrabFix : MonoBehaviour
{
    [Header("Настройки")]
    public float enableColliderDelay = 0.15f;

    private Collider[] colliders;
    private Rigidbody rb;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        // Пока держим ноту — отключаем её коллайдеры,
        // чтобы она не застревала в руке
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    public void OnRelease()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        StartCoroutine(EnableCollidersAfterDelay());
    }

    private IEnumerator EnableCollidersAfterDelay()
    {
        yield return new WaitForSeconds(enableColliderDelay);

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
}