using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class miniGame : MonoBehaviour {

	public GameObject mg;

	// The Grid itself
    public static int w = 7; // this is the width
    public static int h = 7; // this is the height
	public static miniGame_Element[,] elements = new miniGame_Element[w, h];

	public static bool  gameOver;

	public int          numPoop;

	// Cat Button
	public static miniGame_catButton cb;
    
	// Displays
	public Text         numPoopText;


	public static miniGame S;

	public void Awake()
	{
		S = this;
	}


	public void ResetGame(){
		gameOver = false;
		numPoop = 0;
		miniGame_timer.ResetTimer();
		foreach (miniGame_Element elem in elements){
			if(elem != null){
			    elem.loadElement();
			}
		}
		numPoopText.text = numPoop.ToString();
	}

	// Uncover all Mines
	public static void uncoverPoop(){
		foreach (miniGame_Element elem in elements)
            if (elem.poop)
                elem.loadTexture(0);
		cb.LoadCat(2);
		miniGame_AudioManager.S.GameOverSound();
		gameOver = true;
    }

	// Find out if a poop is at the coordinates
    public static bool poopAt(int x, int y){
        // Coordinates in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].poop;
        return false;
    }

	// Count adjacent poop for an element
    public static int adjacentPoop(int x, int y){
        int count = 0;

        if (poopAt(x, y + 1)) ++count; // top
		if (poopAt(x + 1, y + 1)) ++count; // top-right
		if (poopAt(x + 1, y)) ++count; // right
		if (poopAt(x + 1, y - 1)) ++count; // bottom-right
		if (poopAt(x, y - 1)) ++count; // bottom
		if (poopAt(x - 1, y - 1)) ++count; // bottom-left
		if (poopAt(x - 1, y)) ++count; // left
		if (poopAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

	// Flood Fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].loadTexture(adjacentPoop(x, y));

            // close to a mine? then no more work needed here
            if (adjacentPoop(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

	public static bool isFinished()
    {
        // Try to find a covered element that is no mine
        foreach (miniGame_Element elem in elements)
			if (elem.isCovered() && !elem.poop)
                return false;
        // There are none => all are mines => game won.
		cb.LoadCat(1);
		return true;

    }

	public void CloseWindow(){
		ResetGame();
		mg.SetActive(false);
	}

}
