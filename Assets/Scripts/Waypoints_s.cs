using UnityEngine;
using System.Collections;

public class Waypoints_s : MonoBehaviour {

	//Name of the icon image
	public string iconName = "wayIcon.psd";
	//Radius of each way point - use for checking the collision detection with the enemy
	public float radius = 1.0f;
	//Toggle this to make the enemy move by order from the first index to last index (Looping)
	public bool orderDirection = false;
	
	//Get all the transfrom of the waypoint - including the the parent
	private Transform [] waypoints;
	//Current waypoint index
	private int int_wayIndex;
	//Next waypoint index
	private int int_nextIndex;
	//Length of all waypoints
	private int int_wayLength;
	//Movement direction of the enemy to next waypoint
	private Vector3 v3_direction;
	//Checking if the enemy hit the waypoint
	private bool b_isHitRadius;
	
	//Set up all parameters before Initailize
	public void Awake() {
		//Get all Transforms of the gameObject include the children and the transform of this gameObject
		waypoints = gameObject.GetComponentsInChildren<Transform>();
		//Set up the length of all transform
		int_wayLength = waypoints.Length;
		int_wayIndex = 0;
		int_nextIndex = 1;
		//Checking the orderDirection if it?s false means the AI isn?t moving by order, 
		//so using the random index of waypoint
		if(orderDirection == false) {
			int int_randomWay = Random.Range(0, int_wayLength);
			//Checking to make sure that the waypoint length is more than 1
			if (int_wayLength > 1) {
				//Use Random Index
				while (int_wayIndex == int_randomWay) {
					int_randomWay = Random.Range(0, int_wayLength);
				}
			}
			int_nextIndex = int_randomWay;
		}
		//Set the direction to zero
		v3_direction = Vector3.zero;
		//To ignore the first waypoint at the beginning of the game
		b_isHitRadius = true;
	}
	
	public Vector3 StartPosition() {
		return waypoints[0].position;
	} 
	
	//Return the direction of the enemy toward the next waypoint
	public Vector3 GetDirection( Transform _AI ) {
		if (Vector3.Distance(_AI.position, waypoints[int_nextIndex].position) <= radius) {
			//Only check once when the AI hit the way point
			if (!b_isHitRadius) {
				b_isHitRadius = true;
				//Update the current way index
				int_wayIndex = int_nextIndex;
				//Get Direction by order
				if (orderDirection == true) {
					//Get the next way index
					int_nextIndex = (int_nextIndex + 1) % int_wayLength;
				} else {
					int int_randomWay = Random.Range(0, int_wayLength);
					//Checking to make sure that the waypoint length is more than 1
					if (int_wayLength > 1) {
						//Use Random Index
						while (int_wayIndex == int_randomWay) {
							int_randomWay = Random.Range(0, int_wayLength);
						}
					}
					int_nextIndex = int_randomWay;
				}
			}
		} else {
			b_isHitRadius = false;
		}
		
		//Get Direction from the current position of the character to the next way point
		//Make sure that the y position equal to the waypoint y position
		Vector3 v3_currentPosition = new Vector3(_AI.position.x, waypoints[int_nextIndex].position.y, _AI.position.z);
		v3_direction = (waypoints[int_nextIndex].position - v3_currentPosition).normalized;
		
		//Debug.Log("v3_currentPosition is " + v3_currentPosition);
		//Debug.Log("(waypoints[int_nextIndex].position is " + waypoints[int_nextIndex].position);
		
		return v3_direction;
	}
	
	//To get the direction from current position of the enemy to the player
	public Vector3 GetDirectionToPlayer (Transform _AI, Transform _player ) {
		//Make sure that the y position equal to the waypoint y position
		Vector3 v3_currentPosition = new Vector3(_AI.position.x, waypoints[int_wayIndex].position.y, _AI.position.z);
		Vector3 v3_playerPosition = new Vector3(_player.position.x, waypoints[int_wayIndex].position.y, _player.position.z);
		v3_direction = (v3_playerPosition - v3_currentPosition).normalized;
		
		return v3_direction;
		
	}
	
	//Checking if the enemy is away from the target waypoint in the specific distance or not
	public bool AwayFromWaypoint (Transform _AI, float _distance) {
		if (Vector3.Distance(_AI.position, waypoints[int_nextIndex].position) >= _distance) {
			return true;
		} else {
			return false;
		}
	}
	
	//Draw Gizmos and Directional line
	public void OnDrawGizmos() {
		//Get all Transform of this game objects include the children and the transform of this gameobject
		Transform [] waypointGizmos = gameObject.GetComponentsInChildren<Transform>();
		if (waypointGizmos != null) {
			if (orderDirection == true) {
				//Draw line by the order of each waypoint 0,1,2,3,...
				for (int i = 0; i < waypointGizmos.Length; i++) {
					Gizmos.color = Color.red;
					//Get the next way point
					int n = (i + 1) % waypointGizmos.Length;
					Gizmos.DrawLine(waypointGizmos[i].position, waypointGizmos[n].position);
					Gizmos.DrawIcon(waypointGizmos[i].position, iconName);
					Gizmos.color = Color.green;
					Gizmos.DrawWireSphere(waypointGizmos[i].position, radius);
				}
			} else {
				//Draw line from one point to every points except itself
				for (int j = 0; j < waypointGizmos.Length; j++) {
					for (int k = j; k < waypointGizmos.Length; k++) {
						Gizmos.color = Color.red;
						Gizmos.DrawLine(waypointGizmos[j].position, waypointGizmos[k].position);
					}
					Gizmos.DrawIcon(waypointGizmos[j].position, iconName);
					Gizmos.color = Color.green;
					Gizmos.DrawWireSphere(waypointGizmos[j].position, radius);
				}
			}
		}
	}

}
