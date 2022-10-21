using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objToMove;
    [SerializeField] private Vector2 rot;
    public CameraShake _cameraShake;
    
    
    void Update()
    {
        rot.x=Input.GetAxis("Mouse X");
        rot.y=Input.GetAxis("Mouse Y");
        transform.Rotate(-rot.y,0,0);
        objToMove.transform.Rotate(0,rot.x,0);
        
        if(Input.GetKeyUp("e")){
             _cameraShake.ShakeCamera(0.8f,0.5f,10);
            //_cameraShake.ShakeRotateCamera(3,3);
            

        }
       
    }
}

