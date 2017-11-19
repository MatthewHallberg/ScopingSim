using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorLocationBehavior : MonoBehaviour {

	/// <summary>
	/// This script handles placing tumors for level 3.
	/// Tumors are placed based on predetermined locations
	/// because the tumors are 2D and need to have a specific size, location,
	/// and rotation to look half way decent. 
	/// This script goes on the parent transform of all these locations. All the  
	/// locations are the children of this transform (they are just empty gameobjects
	/// that we can instantiate tumors and make them children) so the tumors then inherit the correct
	/// rotation, location, and scale of their parent. 
	/// </summary>

	private static TumorLocationBehavior _instance;
	public static TumorLocationBehavior Instance {
		get {
			return _instance;
		}
	}

	public GameObject IdentifyButton;
	public List<GameObject> TumorTypes = new List<GameObject>();
	private GameObject currentTumor;

	void Awake(){
		_instance = this;
	}
	
	public void PlaceRandomTumor(){
		//choose random tumor index
		int randTumorIndex = UnityEngine.Random.Range(0,TumorTypes.Count);
		//choose random location
		int randLocationIndex = UnityEngine.Random.Range(0,transform.childCount);
		//create tumor
		currentTumor = Instantiate(TumorTypes[randTumorIndex],transform.GetChild(randLocationIndex));
		//set defaults
		currentTumor.transform.localPosition = Vector3.zero;
		currentTumor.transform.localEulerAngles = Vector3.zero;
		currentTumor.transform.localScale = Vector3.one;
	}

	public void DestroyTumor(){
		Destroy (currentTumor);
	}
}
