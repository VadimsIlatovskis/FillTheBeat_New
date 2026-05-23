using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [Header("Проверка")]
    public bool noteCheckmark;

    [Header("Респаун")]
    public GameObject notePrefab;
    public Transform respawnPoint;

    private Rigidbody rb;
    private bool alreadyUsed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsUsed()
    {
        return alreadyUsed;
    }

    public void SnapToSocket(Transform socketPoint)
    {
        alreadyUsed = true;

        transform.SetParent(socketPoint);

        transform.position = socketPoint.position;
        transform.rotation = socketPoint.rotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    public void DestroyAndRespawn()
    {
        if (alreadyUsed)
            return;

        alreadyUsed = true;

        if (notePrefab != null && respawnPoint != null)
        {
            Instantiate(notePrefab, respawnPoint.position, respawnPoint.rotation);
        }

        Destroy(gameObject);
    }
}