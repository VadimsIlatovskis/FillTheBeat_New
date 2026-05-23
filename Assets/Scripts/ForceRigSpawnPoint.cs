using UnityEngine;

public class ForceRigSpawnPoint : MonoBehaviour
{
    [Header("Что передвигаем")]
    public Transform playerRig;

    [Header("Куда ставим")]
    public Transform spawnPoint;

    private void Start()
    {
        if (playerRig == null || spawnPoint == null)
            return;

        playerRig.position = spawnPoint.position;
        playerRig.rotation = spawnPoint.rotation;
    }
}