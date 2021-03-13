using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectorController : MonoBehaviour {


    public List<TextAsset> levels = new List<TextAsset>();

    public Button levelButtonPrefab;

    int spacing = 40;

	// Use this for initialization
	void Start () {

        Canvas currentCanvas = FindObjectOfType<Canvas>();

        int x = spacing;
        int y = (int)currentCanvas.GetComponent<RectTransform>().rect.height - spacing;
        int width = (int)currentCanvas.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < levels.Count; i++)
        {
            x += spacing;
            if (x > width - spacing)
            {
                x = spacing;
                y -= spacing;
            }


            Button newButton = (Button)Instantiate(levelButtonPrefab, new Vector3(x, y, 0), Quaternion.identity);

            newButton.transform.SetParent(currentCanvas.transform, true);

            newButton.GetComponent<LevelButton>().level = levels[i];
            newButton.GetComponent<LevelButton>().levelNumber = i + 1;

        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
