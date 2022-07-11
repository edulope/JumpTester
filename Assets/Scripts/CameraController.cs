using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public bool useOffsetValues;

    public float rotationSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues){
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;


        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate(){

        pivot.transform.position = target.transform.position;

        //rotaciona target a partir da posicao do mouse
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;

        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;

        pivot.Rotate(0, horizontal, 0);

        if(invertY){
            pivot.Rotate(vertical, 0, 0);
        }
        else pivot.Rotate(-vertical, 0, 0);


        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f){
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

          if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle){
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //rotaciona a camera em funcao do target e offset
        float desiredYangle = pivot.eulerAngles.y;
        float desiredXangle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXangle, desiredYangle, 0);

        transform.position = target.position - (rotation*offset);
        
        if(transform.position.y < target.position.y){
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }


        transform.LookAt(target);
    }
}
