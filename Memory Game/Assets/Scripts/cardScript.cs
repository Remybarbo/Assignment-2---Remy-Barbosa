// Import necessary Unity libraries
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Define public class for card object
public class cardScript : MonoBehaviour {

    // Define public static boolean variable
    // This will be used to determine if the card should be flipped
    public static bool DO_NOT = false;

    // Define private integer variables
    // These will be used to store the state and value of the card
    [SerializeField]
    private int _state;
    [SerializeField]
    private int _cardValue;

    // Define private boolean variable
    // This will be used to check if the card has been initialized
    [SerializeField]
    private bool _initialized = false;

    // Define private sprite variables
    // These will be used to store the card's face and back sprites
    private Sprite _cardBack;
    private Sprite _cardFace;

    // Define private game object variable
    // This will be used to access the game manager object
    private GameObject _manager;

    // Function called when card object is created
    void Start(){
        // Set the initial state of the card to be face down
        _state = 1;

        // Find the game manager object and store it in the _manager variable
        _manager = GameObject.FindGameObjectWithTag ("Manager");
    }

    // Public function to set up the card's graphics
    public void setupGraphics() {
        // Get the card back and face sprites from the game manager
        _cardBack = _manager.GetComponent<gameManager> ().getCardBack ();
        _cardFace = _manager.GetComponent<gameManager> ().getCardFace (_cardValue);

        // Flip the card over to show the back
        flipcard ();
    }

    // Public function to flip the card over
    public void flipcard() {
        // If the card is face down, set the state to face up
        if (_state == 0)
            _state = 1;
        // If the card is face up, set the state to face down
        else if (_state == 1)
            _state = 0;

        // Update the card's sprite based on its state
        // If the card is face down and not marked as DO_NOT flip, show the card back
        if (_state == 0 && !DO_NOT)
            GetComponent<Image> ().sprite = _cardBack;
        // If the card is face up and not marked as DO_NOT flip, show the card face
        else if (_state == 1 && !DO_NOT)
            GetComponent<Image> ().sprite = _cardFace;
    }

    // Public property to get or set the card's value
    public int cardValue {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    // Public property to get or set the card's state
    public int state {
        get { return _state; }
        set { _state = value; }
    }

    // Public property to check if the card has been initialized
    public bool initialized {
        get { return _initialized; }
        set { _initialized = value; }
    }

    // Public function to pause briefly before flipping the card back over
    public void falseCheck() {
        StartCoroutine (pause ());
    }

    // Coroutine to pause briefly and then flip the card back over
    IEnumerator pause() {
        yield return new WaitForSeconds(0.3F);
        // If the card is face down, show the card back
        if(_state == 0)
            GetComponent<Image>().sprite = _cardBack;
        // If the card is face up, show
		else if(_state == 1)
			GetComponent<Image>().sprite = _cardFace;
		DO_NOT = false;
	}
}
