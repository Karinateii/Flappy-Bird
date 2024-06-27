using UnityEngine;

public class BoundarySetup : MonoBehaviour
{
    // Public GameObjects for ground and top boundary
    public GameObject ground;
    public GameObject topBoundary;

    // Start is called before the first frame update
    void Start()
    {
        // Setup the ground collider
        // Add a BoxCollider2D component to the ground GameObject
        BoxCollider2D groundCollider = ground.AddComponent<BoxCollider2D>();
        // Set the size of the ground collider to cover the screen width
        groundCollider.size = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, 1);
        // Position the ground at the bottom of the screen
        ground.transform.position = new Vector2(0, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y);

        // Setup the top boundary collider
        // Add a BoxCollider2D component to the topBoundary GameObject
        BoxCollider2D topCollider = topBoundary.AddComponent<BoxCollider2D>();
        // Set the size of the top boundary collider to cover the screen width
        topCollider.size = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, 1);
        // Position the top boundary at the top of the screen
        topBoundary.transform.position = new Vector2(0, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
    }
}
