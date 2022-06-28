using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] protected float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //simple camera movement
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -80);

        /*adding require width and height to the camera*/
        //transform.position = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, -80);
        
        //smooting the camera using the lerp method
        float xTarget = target.transform.position.x + xOffset;
        float yTarget = target.transform.position.y + yOffset;

        float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
        float yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);


        transform.position = new Vector3(xNew, yNew, -80);
    }

}

