using UnityEngine;

public class BoundarySetup : MonoBehaviour
{
    public GameObject ground;
    public GameObject topBoundary;

    void Start()
    {
        // Setup ground collider
        BoxCollider2D groundCollider = ground.AddComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, 1);
        ground.transform.position = new Vector2(0, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y);

        // Setup top boundary collider
        BoxCollider2D topCollider = topBoundary.AddComponent<BoxCollider2D>();
        topCollider.size = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, 1);
        topBoundary.transform.position = new Vector2(0, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
    }
}
