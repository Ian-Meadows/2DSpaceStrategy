using UnityEngine;
using System.Collections;

public class LevelSwitcher : MonoBehaviour {


    TextAsset level;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Application.loadedLevelName == "Game")
        {
            FindObjectOfType<MapCreator>().level = level;

            Destroy(gameObject);
        }

	}

    public void switchLevel(TextAsset level)
    {
        DontDestroyOnLoad(gameObject);

        this.level = level;

        Application.LoadLevel("Game");

    }
}
