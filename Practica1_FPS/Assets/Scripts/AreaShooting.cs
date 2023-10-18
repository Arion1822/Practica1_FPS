using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaShooting : MonoBehaviour
{
    public GameObject diana1;
    public GameObject diana2;
    public GameObject diana3;
    public GameObject diana4;
    public GameObject diana5;
    public GameObject diana6;

    private float points;

    private bool movingUpDown = true;
    private bool movingRigthLeft = true;
    public float movementSpeed;
    private float yMin = 4.0f; 
    private float yMax = 10.0f; 

    private float xMin = 4.0f; 
    private float xMax = 10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    


    }

    // Update is called once per frame
    void Update()
    {
    
        //shootingAreaMovementRightLeft();
        shootingAreaMovementUpDown();
       //shootingAreaMovementCircle();
        
    
    }



private void shootingAreaMovementUpDown()
{
    Vector3 currentPosition1 = diana6.transform.position;
    Vector3 currentPosition2 = diana3.transform.position;

    if (movingUpDown)
    {
        currentPosition1.y += movementSpeed * Time.deltaTime;
        currentPosition2.y += movementSpeed * Time.deltaTime;
        if (currentPosition1.y >= yMax&& currentPosition2.y >= yMax )
        {
            currentPosition1.y = yMax;
            currentPosition2.y = yMax;
            movingUpDown = false;
        }
    
    }
    else
    {
        currentPosition1.y -= movementSpeed * Time.deltaTime;
       currentPosition2.y -= movementSpeed * Time.deltaTime;
        if (currentPosition1.y <= yMin && currentPosition2.y <= yMin)
        {
            currentPosition1.y = yMin;
            currentPosition2.y = yMin;
            movingUpDown = true;
        }
    
    }

    diana6.transform.position = currentPosition1;
    diana3.transform.position = currentPosition2;
}


private void shootingAreaMovementRightLeft()
{
    Vector3 currentPosition3 = diana4.transform.position;

    if (movingRigthLeft)
    {
    
        currentPosition3.x += movementSpeed * Time.deltaTime;
       
        if (currentPosition3.x >= xMax)
        {
            currentPosition3.x = xMax;
            movingRigthLeft = false;
        }
    }
    else
    {
       
        currentPosition3.x -= movementSpeed * Time.deltaTime;
        
        if (currentPosition3.x <= xMin)
        {
            currentPosition3.x = xMin;
            movingRigthLeft = true;
        }
    }


    diana4.transform.position= currentPosition3;
}

private void shootingAreaMovementCircle()
{
    Vector3 currentPosition4 = diana5.transform.position;

    if (movingRigthLeft)
    {
    
        currentPosition4.x += movementSpeed * Time.deltaTime;
        currentPosition4.y += movementSpeed * Time.deltaTime;
        currentPosition4.z += movementSpeed * Time.deltaTime;
       
        if (currentPosition4.x >= xMax)
        {
            currentPosition4.x = xMax;
            movingRigthLeft = false;
        }
    }
    else
    {
       
        currentPosition4.x -= movementSpeed * Time.deltaTime;
        currentPosition4.y -= movementSpeed * Time.deltaTime;
        currentPosition4.z -= movementSpeed * Time.deltaTime;
        if (currentPosition4.x <= xMin)
        {
            currentPosition4.x = xMin;
            movingRigthLeft = true;
        }
    }


    diana5.transform.position= currentPosition4;
}
}
