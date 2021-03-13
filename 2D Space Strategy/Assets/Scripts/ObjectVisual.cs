using UnityEngine;
using System.Collections;

public class ObjectVisual : MonoBehaviour {

    public bool isEnemy;

    public SpawnerController.ShipType shipeType = new SpawnerController.ShipType();

    public Sprite Fighter;
    public Sprite Corvette;
    public Sprite AntiFighterCorvette;

    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isEnemy)
        {
            spriteRenderer.color = new Color(1, 0, 0);
        }
        else
        {
            spriteRenderer.color = new Color(0, 0, 1);
        }

        SetSprite();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetSprite()
    {
        if (shipeType == SpawnerController.ShipType.Fighter)
        {
            spriteRenderer.sprite = Fighter;
        }
        if (shipeType == SpawnerController.ShipType.Corvette)
        {
            spriteRenderer.sprite = Corvette;
        }
        if (shipeType == SpawnerController.ShipType.AntiFighterCorvette)
        {
            spriteRenderer.sprite = AntiFighterCorvette;
        }
    }

}
