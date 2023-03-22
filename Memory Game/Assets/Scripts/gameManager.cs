using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour {

	public Sprite[] cardFace;// An array of sprites representing the faces of the cards
	public Sprite cardBack; // The sprite for the back of the cards
	public GameObject[] cards;// An array of card game objects
	public GameObject gameTime;// The game object displaying the game time

	private bool _init  = false; // A private bool indicating whether the cards have been initialized
	private int _matches = 8; // The number of matches remaining


	void Update () {
		// If the cards have not been initialized, initialize them
		if (!_init)
			initializeCards ();
			// When the left mouse button is released, check the selected cards
		if (Input.GetMouseButtonUp (0))
			checkCards ();

	}
// Initializes the cards by assigning each one a card value and setting up its graphics
	void initializeCards() {
		// We need to assign a card value to each card twice, since there are two of each card
		for (int id = 0; id < 2; id++) {
			for (int i = 1; i < 9; i++) {
				  // Randomly choose an un-initialized card and assign it a card value
				bool test = false;
				int choice = 0;
				while (!test) {
					choice = Random.Range (0, cards.Length);
					test = !(cards [choice].GetComponent<cardScript> ().initialized);
				}
				cards [choice].GetComponent<cardScript> ().cardValue = i;
				cards [choice].GetComponent<cardScript> ().initialized = true;
			}
		}
		// Set up the graphics for each card
		foreach (GameObject c in cards)
			c.GetComponent<cardScript> ().setupGraphics ();
			// Set _init to true so that we don't initialize the cards again
		if (!_init)
			_init = true;
	}
// Returns the sprite for the back of the cards
	public Sprite getCardBack() {
		return cardBack;
	}
  // Returns the sprite for the front of a card with the given card value
	public Sprite getCardFace(int i) {
		return cardFace[i - 1];
	}
// Checks the currently selected cards to see if they match
	void checkCards() {
		List<int> c = new List<int> ();
		// Find the indices of the currently selected cards
		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<cardScript> ().state == 1)
				c.Add (i);
		}
		// If two cards are currently selected, compare them
		if (c.Count == 2)
			cardComparison (c);
	}
// Compares the selected cards and updates their states
	void cardComparison(List<int> c){
		// Disable card clicking while the comparison is being made
		cardScript.DO_NOT = true;

		int x = 0;
		// If the selected cards match, set their state to 2 and decrement the number of matches remaining
		if (cards [c [0]].GetComponent<cardScript> ().cardValue == cards [c [1]].GetComponent<cardScript> ().cardValue) {
			x = 2;
			_matches--;
			// If all matches have been found,
			// Loop through a list of card objects and update their state property
			// and call falseCheck method on each card
			if (_matches == 0)
				SceneManager.LoadScene("menuScene");
		}


		for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<cardScript> ().state = x;
			cards [c [i]].GetComponent<cardScript> ().falseCheck ();
		}

	}
// Reload the game scene
	public void reGame(){
		SceneManager.LoadScene ("gameScene");
	}
// Reload the menu scene
	public void reMenu(){
		SceneManager.LoadScene ("menuScene");
	}
}
