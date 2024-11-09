using UnityEngine;

public class RandomBlockShaderController : MonoBehaviour
{
    public Material blockMaterial;  // The material using the RandomBlockGenerator shader
    public float blockSize = 8.0f;  // Block size property
    public float timeInterval = 1.0f;  // Time interval for block generation (in seconds)
    public float blockDuration = 1.0f;  // Duration for which the block remains visible (in seconds)
    public Color blockColor = Color.red;  // The block color

    private float timeSinceStart = 0.0f;  // Track elapsed time
    private float blockStartTime = -1.0f;  // The time when the block should appear
    private Vector2 blockPosition;  // Stores the block position

    void Update()
    {
        // Update the time since start, using Time.deltaTime to simulate elapsed time in seconds
        timeSinceStart += Time.deltaTime;

        // Check if the current time has passed the start time and is within the block duration
        if (blockStartTime < 0 || timeSinceStart > blockStartTime + timeInterval)
        {
            // Update blockStartTime to trigger a new block at intervals
            blockStartTime = timeSinceStart;
            blockPosition = CalculateBlockPosition();  // Calculate a new random block position

            // Send updated values to the shader
            if (blockMaterial != null)
            {
                blockMaterial.SetFloat("_CustomTimeSinceStart", timeSinceStart);  // Send time to the shader
                blockMaterial.SetFloat("_BlockSize", blockSize);  // Set the block size
                blockMaterial.SetFloat("_TimeInterval", timeInterval);  // Set the time interval
                blockMaterial.SetFloat("_BlockDuration", blockDuration);  // Set block duration
                blockMaterial.SetColor("_BlockColor", blockColor);  // Set the block color
            }
        }

        // Make sure the block is visible only during the block's duration
        if (timeSinceStart < blockStartTime + blockDuration)
        {
            // Continue to display the block
            blockMaterial.SetColor("_BlockColor", blockColor);  // Ensure block color remains active
        }
    }

    Vector2 CalculateBlockPosition()
    {
        // Generate screen coordinates based on a random function similar to the shader's logic
        float randomX = Mathf.Floor(Random.Range(0f, 1f) * blockSize) / blockSize;
        float randomY = Mathf.Floor(Random.Range(0f, 1f) * blockSize) / blockSize;

        return new Vector2(randomX, randomY);
    }

    // Visualize block position in the Scene view for debugging
    private void OnDrawGizmos()
    {
        // Draw a small sphere at the block position for debugging
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(blockPosition.x, blockPosition.y, 0f), 0.1f);
    }

    // Optionally, display the position in the Inspector
    void OnGUI()
    {
        GUILayout.Label("Block Position: " + blockPosition.ToString());
    }
}
