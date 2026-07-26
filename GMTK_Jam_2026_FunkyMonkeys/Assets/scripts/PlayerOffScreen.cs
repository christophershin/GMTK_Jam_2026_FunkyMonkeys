using UnityEngine;

public class PlayerOffScreen : MonoBehaviour
{

    public Transform player;
    public float visibleAboveY = 5f;
    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (player == null)
            return;

        // Follow the player's X position
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        transform.position = pos;

        // Show or hide based on player's Y position
        bool shouldBeVisible = player.position.y > visibleAboveY;

        foreach (Renderer r in renderers)
        {
            r.enabled = shouldBeVisible;
        }
    }
}