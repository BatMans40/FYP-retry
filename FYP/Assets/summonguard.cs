using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target; // The target object to rotate around
    public float rotateSpeed = 21f; // Speed of rotation
    public float rotationRadius = 2f; // Radius of the circular path around the player

    private Vector3 offset; // Offset from the target

    void Start()
    {
        // Initialize the offset based on the rotation radius
        offset = new Vector3(rotationRadius, 0f, 0f);
    }

    void Update()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Calculate the new offset position around the target in a circular path
            offset = Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, Vector3.forward) * offset;
            Vector3 followPosition = target.position + offset;

            // Set the position of the object to the follow position immediately
            transform.position = followPosition;
        }
        else
        {
            // Log a warning message if the target is not assigned
            Debug.LogWarning("Target for rotation is not assigned!", this);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the "Enemy" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            // Destroy the enemy object
            Destroy(collision.gameObject);
        }
    }
}