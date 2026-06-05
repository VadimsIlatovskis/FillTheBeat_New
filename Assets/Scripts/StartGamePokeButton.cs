using UnityEngine;
using Oculus.Interaction;

public class StartGamePokeButton : MonoBehaviour
{
    public SongGapManager songManager;
    public GameObject startMenu;

    private PokeInteractable pokeInteractable;
    private bool hasStarted = false;

    private void Awake()
    {
        pokeInteractable = GetComponent<PokeInteractable>();
    }

    private void Update()
    {
        if (hasStarted)
            return;

        if (pokeInteractable.State == InteractableState.Select)
        {
            hasStarted = true;

            if (songManager != null)
                songManager.StartGame();

            if (startMenu != null)
                startMenu.SetActive(false);
        }
    }
}