using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorLocationBehavior : MonoBehaviour {

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
