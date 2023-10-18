using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    
    public int maxLife = 100;
    public int currentLife;
    
    
    public float speed = 5f;

    public Character character;
    public float distance;
    
    private Transform[] patrolPoints; // Array to store patrol points

    private int currentTargetIndex = 0;
    
    public enum DroneState
    {
        Idle,
        Patrol,
        Alert,
        Chase,
        Attack,
        Hit,
        Die
    }

    public DroneState currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<Character>();
        
        Transform patrolRoute = transform.Find("Patrol Route");
        if (patrolRoute != null)
        {
            // Get all child transforms (patrol points) of the "Patrol Route" object
            patrolPoints = new Transform[patrolRoute.childCount];
            for (int i = 0; i < patrolRoute.childCount; i++)
            {
                patrolPoints[i] = patrolRoute.GetChild(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, character.transform.position);

        switch (currentState)
        {
            case DroneState.Idle:
                // Drone is floating in the air without movement.
                break;
            case DroneState.Patrol:
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

                // Drone is moving in a predefined loop.
                break;
            case DroneState.Alert:
                // Drone senses the player nearby and starts rotating to see the player.
                // If the player is seen, transition to Chase state. Otherwise, return to Patrol.
                break;
            case DroneState.Chase:
                // Drone approaches the player until a minimum distance (MIN).
                // If the player escapes beyond a maximum distance (MAX), return to Patrol.
                break;
            case DroneState.Attack:
                // Drone starts shooting at the player.
                // If the distance exceeds SHOOT_MAX, transition back to Chase state.
                break;
            case DroneState.Hit:
                // Drone gets hit by a bullet.
                // Transition to Alert state. If already in Alert, remain in Alert state.
                break;
            case DroneState.Die:
                // Drone loses all its life, gradually disappear (fade out).
                break;
        }
    }

}
