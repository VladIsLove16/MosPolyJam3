using UnityEngine;

public class MoveMechanic
{
    private Vector2 movementVector;
    private float movementSpeed;
    private Rigidbody2D rigidbody2D;
    public float DistanceTraveled { get; private set; }
    private float maxDistance;

    public MoveMechanic(Vector2 movementVector, float movementSpeed, Rigidbody2D rigidbody, float maxDistance)
    {
        this.movementVector = movementVector.normalized;
        this.movementSpeed = movementSpeed;
        this.rigidbody2D = rigidbody;
        this.maxDistance = maxDistance;
        DistanceTraveled = 0f;
        rigidbody.gravityScale = 0;
    }

    public bool FixedUpdate()
    {
        Vector2 movementForce = movementVector * movementSpeed;
        rigidbody2D.velocity = movementForce * Time.fixedDeltaTime;

        // Track distance and check against maxDistance
        DistanceTraveled += rigidbody2D.velocity.magnitude * Time.fixedDeltaTime;
        return DistanceTraveled >= maxDistance;
    }

    public void ChangeMoveSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        movementVector = newDirection.normalized;
    }
}
