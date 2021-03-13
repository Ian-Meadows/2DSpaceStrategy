using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public TextAsset level;

    public int levelNumber;

    // Use this for initialization
    void Start () {

        GetComponentInChildren<Text>().text = ""+levelNumber;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        FindObjectOfType<LevelSwitcher>().switchLevel(level);
    }
}
