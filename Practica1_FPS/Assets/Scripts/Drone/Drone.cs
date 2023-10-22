using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    
    public int maxLife = 100;
    public int currentLife;
    
    
    public float speed = 5f;

    public Character character;
    public float distance;
    
    private Transform[] patrolPoints; // Array to store patrol points

    private int currentTargetIndex = 0;
    
    public float detectionAngle = 45f;
    public float detectionDistance = 10f;
    public float rotationSpeed = 10f;

    public float maxChaseDistance = 40f;
    public float attackDistance = 10f;
    
    private float lastShootTime;

    public List<GameObject> itemsOnDie;

    public Image lifeBar;
    public Image life;
    public GameObject lifeBarPosition;
    
    //Borrar
    public float dis;
    
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
        GetComponent<Rigidbody>().isKinematic = true;
        
        currentLife = maxLife;
        
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
        float angleToPlayer = Vector3.Angle(transform.forward, character.transform.position - transform.position);
        Vector3 directionToPlayer = (character.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        dis = distanceToPlayer; 
        
        DrawLifeBar();  
        
        switch (currentState)
        {
            case DroneState.Idle:
                // Drone is floating in the air without movement.
                break;
            case DroneState.Patrol:
                //transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

                // Drone is moving in a predefined loop.
                break;
            case DroneState.Alert:
                float rotationStep = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationStep);

                // Check if the player is within the detection angle and distance
                if (angleToPlayer <= detectionAngle)
                {
                    // Transition to CHASE state if the player is detected
                    currentState = DroneState.Chase;
                }
                break;
            case DroneState.Chase:
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                
                
                Vector3 newPosition = new Vector3(transform.forward.x, 0, transform.forward.z) * speed * Time.deltaTime;
                transform.position += newPosition;

                // If the distance is greater than a certain distance, transition to Patrol state
                if (distanceToPlayer > maxChaseDistance)
                {
                    currentState = DroneState.Patrol;
                }
                // If the distance is smaller than a certain distance, transition to Attack state
                else if (distanceToPlayer < attackDistance)
                {
                    currentState = DroneState.Attack;
                }
                break;
            case DroneState.Attack:
                AttackPlayer();
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

    public void ReceiveDamage(int damage)
    {
        
        GetHit();
        
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentLife = 0;
        currentState = DroneState.Die;
        GetComponent<Rigidbody>().isKinematic = false;

        if (itemsOnDie != null)
        {
            int i = Random.Range(0, itemsOnDie.Count);
            Instantiate(itemsOnDie[i], transform.position, transform.rotation);    
        }
        
        Destroy(gameObject, 3f);

    }

    public void GetHit()
    {
        if (currentState != DroneState.Die)
        {
            
            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
            }
            
            
            DroneState previousState = currentState; // Store the previous state before transitioning to Hit state
            currentState = DroneState.Hit;

            // Start a coroutine to change the state back to Alert after 4 seconds
            StartCoroutine(ReturnToAlertStateAfterDelay(1f, previousState));
        }
    }
    
    private Coroutine hitCoroutine;

    private IEnumerator ReturnToAlertStateAfterDelay(float delay, DroneState previousState)
    {
        yield return new WaitForSeconds(delay);

        // Check if the current state is still Hit before transitioning back to Alert
        if (currentState == DroneState.Hit)
        {
            // Transition back to Alert state if the previous state was Idle or Patrol, else return to the previous state
            currentState = (previousState == DroneState.Idle || previousState == DroneState.Patrol) ? DroneState.Alert : previousState;
        }
    }
    
    private void AttackPlayer()
    {
        Vector3 directionToPlayer = (character.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, character.transform.position);

        // If the distance exceeds attackDistance, transition back to Chase state
        if (distanceToPlayer > attackDistance)
        {
            currentState = DroneState.Chase;
        }
        else
        {
            // Shoot the character once every two seconds if there are no obstacles
            if (Time.time - lastShootTime >= 2f)
            {
                // Perform a raycast to check for obstacles between the drone and character
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, attackDistance))
                {
                    if (hit.collider.tag.Equals("Player"))
                    {
                        
                        character.ReceiveDamage(20);
                        lastShootTime = Time.time;
                    }
                    else Debug.Log("Not Hitting player");
                }
            }
        }
    }



    private void DrawLifeBar()
    {
        lifeBar.gameObject.SetActive(true);
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(lifeBarPosition.transform.position);
        lifeBar.rectTransform.anchoredPosition = new Vector2(1920 * viewportPoint.x, 1080 * viewportPoint.y);
        float cL = currentLife;
        float mL = maxLife;
        life.fillAmount = cL/mL;

    }
    
    

    
    private void OnDrawGizmos()
    {
        // Set the color for the detection angle visualization
        Gizmos.color = Color.yellow;

        // Get the forward direction of the drone
        Vector3 forwardDirection = transform.forward;

        // Calculate the starting point of the detection angle visualization
        Vector3 startPoint = transform.position + Quaternion.Euler(0, -detectionAngle / 2, 0) * forwardDirection * detectionDistance;

        // Draw the detection angle visualization using a cone or field of view shape
        Gizmos.DrawRay(transform.position, startPoint - transform.position);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, detectionAngle, 0) * forwardDirection * detectionDistance);
        Gizmos.DrawLine(startPoint, transform.position + forwardDirection * detectionDistance);
    }
    
    

}
