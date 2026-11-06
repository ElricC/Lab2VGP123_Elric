using System.Security.Claims;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public enum CameraState
    {
        None, 
        Follow, 
        Bound, 
        Transition
    }

    public CameraState currentState;
    public float minXPos;
    public float maxXPos;
    public Transform target;
    public float cameraSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No Target assigned as player. Please include valid reference in the camera follow script");
                return;
            }
            target = player.transform;
        } 
    }

    //regular update function is specifically linked to the computers tick rate - useful for input handling / rendering
    //fixed update runs on an exact fixed time (60FPS = 1/60 = 0.0166666s) - this is useful for physics as a proper fixed update that is decoumpled from rendering
    //allows for more consistent physics behaviour.
    //Late update is the last update in a frame after all fixed update and update cycles have run. Unity specifics that LateUpdate is generally useful for camera movement as it is the last thing that will happen in a frame.

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) //this is an exit condition if object need to be run doesn't exist.
        {
            return;
        }
        
        //we need to capture current position in order to modify the x position for this update
        Vector3 currentPos = transform.position;
        //modify to a clamp of x min and max values... if you wanted more functionality - here is where you would modify the camera positions
        currentPos.x = Mathf.Clamp(target.position.x , minXPos , maxXPos) ;

        //currentPos.y = Mathf.Clamp
        transform.position = Vector3.MoveTowards(transform.position, currentPos, cameraSpeed * Time.deltaTime);

        
    }
}
