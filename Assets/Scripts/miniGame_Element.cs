using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame_Element : MonoBehaviour {

    public bool poop;

	// Different Textures
	public Sprite buttonSprite;
    public Sprite[] emptyImages;
    public Sprite poopImage;

    // Use this for initialization
    void Start(){
		// Register in Grid
        int x = (int)transform.localPosition.x;
        int y = (int)transform.localPosition.y;
        miniGame.elements[x, y] = this;

		miniGame.S.ResetGame();

    }

	public void loadElement(){
		poop = Random.value < 0.15;
		if(poop){
			miniGame.S.numPoop++;
		}
		GetComponent<SpriteRenderer>().sprite = buttonSprite;
	}
   

    // Load Image
    public void loadTexture(int adjacentCount){
		if (poop)
            GetComponent<SpriteRenderer>().sprite = poopImage;
        else
            GetComponent<SpriteRenderer>().sprite = emptyImages[adjacentCount];
    }

    // Is it still covered?
    public bool isCovered(){
		return GetComponent<SpriteRenderer>().sprite.texture.name == "pineScooper_btn";
    }

    void OnMouseUpAsButton()
    {
        if (poop){
			// It's a poop!
			miniGame.uncoverPoop();
		} else {
			// Not a poop
			// show adjacent poop number
            int x = (int)transform.localPosition.x;
            int y = (int)transform.localPosition.y;
			loadTexture(miniGame.adjacentPoop(x, y));

			// uncover area without mines
			miniGame.FFuncover(x, y, new bool[miniGame.w, miniGame.h]);

			// find out if the game was won now
			if (miniGame.isFinished())
				miniGame.gameOver = true;           
        }
    }

}
