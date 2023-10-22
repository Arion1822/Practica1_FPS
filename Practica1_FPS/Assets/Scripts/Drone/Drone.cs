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
    
    private Transform[] patrolPoints;
    
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

                if (distanceToPlayer > maxChaseDistance)
                {
                    currentState = DroneState.Patrol;
                }
                else if (distanceToPlayer < attackDistance)
                {
                    currentState = DroneState.Attack;
                }
                break;
            case DroneState.Attack:
                AttackPlayer();
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
        if (currentState == DroneState.Patrol || currentState == DroneState.Idle)
        {
            
            if (hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
            }
            
            currentState = DroneState.Hit;
            StartCoroutine(ReturnToAlertStateAfterDelay(1f));
        }
    }
    
    private Coroutine hitCoroutine;

    private IEnumerator ReturnToAlertStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentState == DroneState.Hit)
        {
            currentState = DroneState.Alert;
        }
    }
    
    private void AttackPlayer()
    {
        Vector3 directionToPlayer = (character.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        
        float distanceToPlayer = Vector3.Distance(transform.position, character.transform.position);
        
        if (distanceToPlayer > attackDistance)
        {
            currentState = DroneState.Chase;
        }
        else
        {
            if (Time.time - lastShootTime >= 2f)
            {
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
}
