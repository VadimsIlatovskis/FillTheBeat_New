using UnityEngine;

public class NoteAutoRespawn : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPoint;
    public float minY = -1f;
    public float checkDelay = 0.2f;

    private Rigidbody rb;
    private bool isRespawning = false;
    private NoteObject noteObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        noteObject = GetComponent<NoteObject>();
    }

    private void Update()
    {
        if (isRespawning)
            return;

        if (noteObject != null && noteObject.IsUsed())
            return;

        if (transform.position.y < minY)
        {
            StartCoroutine(Respawn());
        }
    }

    private System.Collections.IEnumerator Respawn()
    {
        isRespawning = true;

        yield return new WaitForSeconds(checkDelay);

        if (noteObject != null && noteObject.IsUsed())
        {
            isRespawning = false;
            yield break;
        }

        if (respawnPoint == null)
        {
            isRespawning = false;
            yield break;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        yield return null;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.Sleep();
        }

        isRespawning = false;
    }
}
