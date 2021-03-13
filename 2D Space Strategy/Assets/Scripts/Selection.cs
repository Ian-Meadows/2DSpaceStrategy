using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {


    BasicObject selected;

	// Use this for initialization
	void Start () {

        selected = GetComponentInParent<BasicObject>();

        transform.localScale = new Vector3(selected.size, selected.size, selected.size);

	}
	
	// Update is called once per frame
	void Update () {

        if (selected == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!selected.isSelected)
            {
                Destroy(gameObject);
            }
            
        }

        


	}
}
