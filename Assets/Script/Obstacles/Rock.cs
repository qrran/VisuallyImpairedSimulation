using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

	public GameObject rock; // Reference to the instantiated prefab

	// Start is called before the first frame update
	void Start()
    {
		Vector3 rockPosition = rock.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
