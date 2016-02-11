#region Using Statements
//additional using statements to get access to Unity's UI components and access the generic lists
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
#endregion

public class AppData : MonoBehaviour
{
    //singleton is used to ensure there is only ever one instance of a class present in the program
    #region Singleton
    //private instance of the class
    private static AppData instance;

    //constructor used when creating the object for the first time
    private AppData()
    {
        //calls the ResetData method
        ResetData();
    }

    //public object other classes will use to get access to the instance
    public static AppData Data
    {
        //what to do when an object tries to access this object
        get
        {
            //if the instance does not exist
            if (instance == null)
            {
                //find the object in the scene that has this script attached to it and assign it to
                //the private instance so we can use it
                instance = FindObjectOfType<AppData>();
            }
            //if the instance does exist, return instance
            return instance;
        }
    }
    #endregion

    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //a list that contains Page objects, these are assigned in the inspector
    public List<Page> Pages = new List<Page>();

    //reference to the Collected Pages text UI component in the scene
    public Text CollectedPages;

    //reference to the Total Pages text UI component in the scene
    public Text TotalPages;

    //reference to the UI panel component in the scene
    public CanvasGroup UIPanel;

    //reference to the Page panel component in the scene
    public CanvasGroup PagePanel;

    //reference to the Name text UI component in the scene used to name a page when displayed
    public Text Name;

    //reference to the Message text UI component in the scene used to set the message of a page
    public Text Message;

    //reference to the Example text UI component in the scene used to set the example text of a page
    public Text Example;

    //reference to the Instructions panel component in the scene
    public CanvasGroup InstructionsPanel;

    //reference to the Instructions text UI component in the scene used to give gameplay instructions
    public Text Instructions;

    //list of AudioSource objects used to manipulate the music as the game progresses
	public List<AudioSource> Music = new List<AudioSource> ();
    #endregion

    //private variables are only accessible within this script
    #region Private Variables
    //stores the number of pages collected
    private int numberOfPagesCollected;

    //stores whether the game has been complete or not
    private bool gameComplete;
    #endregion

    #region Methods
    //public method to get and set the number of pages collected
    public int NumberOfPagesCollected
    {
        get { return numberOfPagesCollected; }
        set { numberOfPagesCollected = value; }
    }

    //public method to return the value of gameComplete
    //no set method as only this class will be able to change whether the game is complete or not
    public bool GameComplete
    {
        get { return gameComplete; }
    }

    //method to increment the number of pages collected variable
    //called when the user collects a page
    public void PageCollected()
    {
        //number of pages collected + 1
        NumberOfPagesCollected++;

        //set the collected pages text to the number of pages collected variable value
        CollectedPages.text = numberOfPagesCollected.ToString();

        //if the number of pages collected is equal to the total number of pages availabe to collect
        if (numberOfPagesCollected == Pages.Capacity)
        {
            //set game complete to true
            gameComplete = true;
        }

        //call the fade audio method and pass in the number of pages collected so we know what to do with the music
		FadeAudio (NumberOfPagesCollected);
    }

    //private method used to set the instance of this class to default values
    //called when the instance is first created and every time a new game is started
    private void ResetData()
    {
        //set number of pages collected to 0 and game complete to false
        numberOfPagesCollected = 0;
        gameComplete = false;

        //set the volume of all the music to 0
        for (int i = 0; i < Music.Count; i++)
        {
            Music[i].volume = 0;
        }
    }

    //method used to fade the instructions panel
    public void FadeInstructionsPanel(string s)
    {
        //set the text to the string we passed in
        Instructions.text = s;

        //start a Corouine using the FadeInPanel method
        StartCoroutine(FadeInPanel());
    }
	
    //method used to fade in or out the audio, the index of the music track in the list is passed in so we know
    //which track to fade
	private void FadeAudio(int index)
	{
        //switch statement used to save on multiple if statements, condition is the index passed in
        //music was set up to fade in and out this way at the request of the audio designer
		switch (index)
		{
            //if the index is 5, call the fade audio blueprint method and pass it the index to fade in and fade out index 2
		case 5:
			StartCoroutine(FadeAudioBlueprint(index, 2));
			break;
            //if the index is 6, call the fade audio blueprint method and pass it the index to fade in and fade out index 4
        case 6:
			StartCoroutine(FadeAudioBlueprint(index, 4));
			break;
            //if the index is 8, call the fade audio blueprint method and pass it the index to fade in and fade out index 3
        case 8:
			StartCoroutine(FadeAudioBlueprint(index, 3));
			break;
            //if the index is anything else, call the fade audio blueprint method and pass it the index to fade in
            //no track is faded out
        default:
			StartCoroutine(FadeAudioBlueprint(index));
			break;
		}
	}
    #endregion

    #region Unity Methods
    void Start()
    {
        //call the reset method
        ResetData();

        //set the instructions panels alpha channel to 0
        InstructionsPanel.alpha = 0f;

        //set the page panels alpha channel to 0
        PagePanel.alpha = 0f;

        //set the UI panels alpha channel to 1
        UIPanel.alpha = 1f;

        //set the Collected Pages UI text component to the number of pages collected variable (should be 0 to start)
        CollectedPages.text = numberOfPagesCollected.ToString();

        //Set the Total pages UI text component to the length of the pages list
        TotalPages.text = Pages.Capacity.ToString();

        //Call the fade instructions panel method and pass in the string below to show
        FadeInstructionsPanel("Collect the pages.\nTip: The perimeter is usually a good place to start...");

        //Call the fade audio method passing in the number of pages collected variable (should be 0)
        FadeAudio(NumberOfPagesCollected);
    }
    #endregion

    #region Coroutines

    //used to fade in music
    IEnumerator FadeAudioBlueprint(int fadeInIndex)
    {
        //set volume of the music track to 0
        Music[fadeInIndex].volume = 0;

        //while the volume is less than 1
        while (Music[fadeInIndex].volume < 1f)
        {
            //add 0.25 to the volume over 1 second, giving us 4 seconds until it is at max volume
            Music[fadeInIndex].volume += 0.25f * Time.deltaTime;

            //break out of the method and return to the while loop on the next frame
            yield return 0;
        }
        //just in case the volume was above 1 at the end of the loop, set volume to 1
        Music[fadeInIndex].volume = 1f;
    }

    //overloaded method for FadeAudioBlueprint, allows us to also fade out another music track
    IEnumerator FadeAudioBlueprint(int fadeInIndex, int fadeOutIndex)
    {
        //set volume of the music track to be faded in to 0
        //set volume of the music track to be faded out to 1
        Music[fadeInIndex].volume = 0;
        Music[fadeOutIndex].volume = 1f;

        //while the volume is less than 1 on the track to fade in
        while (Music[fadeInIndex].volume < 1f)
        {
            //add 0.25 to the volume of the track to be faded in over 1 second, giving us 4 seconds until it is at max volume
            //subtract 0.25 from the volume of the track to be faded out over 1 second, giving us 4 seconds until it is at 0
            Music[fadeInIndex].volume += 0.25f * Time.deltaTime;
            Music[fadeOutIndex].volume -= 0.25f * Time.deltaTime;

            //break out of the method and return to the while loop on the next frame
            yield return 0;
        }

        //just in case the volume of the track to fade in was above 1 at the end of the loop, set volume to 1
        Music[fadeInIndex].volume = 1f;

        //just in case the volume of the track to fade out was below 0 at the end of the loop, set volume to 0
        Music[fadeOutIndex].volume = 0f;
    }

    //used to fade in a UI panel
    IEnumerator FadeInPanel()
    {
        //while the Instructions panels alpha channel is less than 1
        while (InstructionsPanel.alpha < 1f)
        {
            //add 0.25 to the alpha channel over 1 second, giving us 4 seconds until it is maxed out
            InstructionsPanel.alpha += 0.25f * Time.deltaTime;

            //break out of the method and return at this point on the next frame
            yield return 0;
        }

        //set the alpha channel to 1 in case it went slightly over during the loop
        InstructionsPanel.alpha = 1f;

        //start the fade out panel method
        StartCoroutine(FadeOutPanel());
    }

    //used to fade out a UI panel
    IEnumerator FadeOutPanel()
    {
        //while the alpha channel of the instructions panel is greater than 0
        while (InstructionsPanel.alpha > 0f)
        {
            //subtract 0.25 from the alpha channel over 1 second, giving us 4 seconds until it has faded completely
            InstructionsPanel.alpha -= 0.25f * Time.deltaTime;

            //break out of the method and return to this point on the next frame
            yield return 0;
        }

        //just in case the alpha goes below 0, set it to 0
        InstructionsPanel.alpha = 0f;
    }
    #endregion
}