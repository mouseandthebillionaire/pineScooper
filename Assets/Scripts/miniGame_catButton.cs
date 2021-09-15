using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame_catButton : MonoBehaviour
{

	public Sprite[] catImages;
    
	// Use this for initialization
	void Start(){
		miniGame.cb = this;
	}

	void OnMouseUpAsButton()
	{
		miniGame.S.ResetGame();
		LoadCat(0);

	}

	public void LoadCat(int img){
		GetComponent<SpriteRenderer>().sprite = catImages[img];
	}
}
