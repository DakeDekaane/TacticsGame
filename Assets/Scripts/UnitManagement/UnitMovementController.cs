using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementController : MonoBehaviour
{
    private Tile tmpTile;
    public float speed = 100.0f;
    private Vector3 tmpPosition;
    private Vector3 heading;
    public bool moving;
    public IEnumerator FollowPath() {

        //We're already on the target tile.
        if(GraphAStar.instance.path.Count == 0) {
            moving = false;
            yield break;
        }

        //Moving to target tile.
        moving = true;
        tmpTile =  GraphAStar.instance.path.Pop();
        tmpPosition = new Vector3(tmpTile.transform.position.x,transform.position.y,tmpTile.transform.position.z);
        heading = tmpPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(heading);
        while(GraphAStar.instance.path.Count >= 0) {
            Debug.Log("Current position:" + transform.position);
            Debug.Log("Target position:" + tmpPosition);
            if (transform.position == tmpPosition) {
                if(GraphAStar.instance.path.Count == 0) {
                    moving = false;
                    yield break;
                }
                tmpTile =  GraphAStar.instance.path.Pop();
                tmpPosition = new Vector3(tmpTile.transform.position.x,transform.position.y,tmpTile.transform.position.z);
                heading = tmpPosition - transform.position;
                transform.rotation = Quaternion.LookRotation(heading);
            }
            //transform.position += transform.forward * Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position,tmpPosition,speed*Time.deltaTime);
            yield return null;
        }
        //moving = false;
        // do {
        //     tmpTile = GraphAStar.instance.path.Pop();
        //     tmpPosition = new Vector3(tmpTile.transform.position.x,transform.position.y,tmpTile.transform.position.z);
        //     heading = tmpPosition - transform.position;
        //     transform.rotation = Quaternion.LookRotation(heading);
        //     Debug.Log("Start point: " + transform.position);
        //     Debug.Log("Target point: " + tmpPosition);
        //     Debug.Log("Step point: " + transform.forward * speed * Time.deltaTime);


        //     while(transform.position != tmpPosition) {
        //         transform.position += transform.forward * Time.deltaTime * speed;
        //         yield return null;
        //     }
            
        //     //transform.position = new Vector3(tmpTile.transform.position.x,transform.position.y,tmpTile.transform.position.z);

        // } while (GraphAStar.instance.path.Count > 0);        
    }
}
