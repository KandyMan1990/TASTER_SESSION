#region Using Statements
//Additional using statements to access features such as UI components, changing scenes and getting a reference
//to the first person controller contained in Unity's Standard Assets package
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#endregion

public class FinishedPlayer : MonoBehaviour
{
    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //A variable we will use for the players health, initialised to 100 but can be changed in the inspector,
    //along with a description should the variable be highlighted in the Inspector
    [Tooltip("Adjust how much health the player has")]
    public float Health = 100f;

    //A variable we will use to determine how many seconds will pass before 1 health point is lost,
    //along with a description should the variable be highlighted in the Inspector
    [Tooltip("Sets how many seconds it takes to lose 1 health point")]
    public float DecayRate = 3f;

    //A variable we will use to store a reference to the health bar image used in the games UI,
    //along with a description should the variable be highlighted in the Inspector
    [Tooltip("Sets the image which will reflect the players current health")]
    public Image HealthBar;

    //A variable we will use to set what the colour of the health bar will be when health is at max,
    //along with a description should the variable be highlighted in the Inspector
    [Tooltip("Sets the colour which represents full health")]
    public Color StartColour;

    //A variable we will use to set what the colour of the health bar will be when health is 0,
    //along with a description should the variable be highlighted in the Inspector
    [Tooltip("Sets the colour which represents no health")]
    public Color EndColour;
    #endregion

    //private variables are only accessible within this script
    #region Private Variables
    //a variable that will get a reference to the FirstPersonController attached to this or a child object
    private FirstPersonController controller;

    //a bool variable that will determine whether the health can gradually decay or not
    private bool canUpdate;

    //a variable used to determine what the maximum health can be
    //used to stop health from going above 100% and used to set the length of the UI representation of health
    private float maxHealth;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //Hide the cursour during gameplay
        Cursor.visible = false;

        //when this script loads, find the FirstPersonController attached to this game object or its children
        //and store it in our FirstPersonController variable so we can manipulate it
        controller = GetComponentInChildren<FirstPersonController>();

        //when this script loads, set our canUpdate variables to true
        //When the game starts, health will begin to drain
        canUpdate = true;

        //set our maxHealth variable to equal whatever was set in the inspector
        maxHealth = Health;

        //if the StartColour variable has not been set in the inspector, default to green
        if (StartColour.Equals(new Color(0, 0, 0, 0)))
            StartColour = Color.green;

        //if the EndColour variable has not been set in the inspector, default to red
        if (EndColour.Equals(new Color(0, 0, 0, 0)))
            EndColour = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        //if our canUpdate variable is set to true, call the UpdateHealth methods
        if (canUpdate)
            UpdateHealth();

        //If the escape key is pressed, quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.QuitGame();
        }

        //sends a warning to the console telling the user they have not set the alpha channel of the colours
        if (StartColour.a == 0 || EndColour.a == 0)
            Debug.LogWarning("Alpha is set to 0, colour will never be displayed");
    }
    #endregion

    #region Private Methods
    //private method used to update the players health
    private void UpdateHealth()
    {
        //health will lose 1 hit point per whatever the DecayRate is set to in seconds
        //multiply by Time.deltaTime to make it frame rate independent
        Health -= (1f / DecayRate) * Time.deltaTime;

        //set the fill rate of the health bar to the percentage of health the player has remaining
        HealthBar.fillAmount = Health / maxHealth;

        //fade between the start colour and end colour based on the health bars fill amount
        HealthBar.color = Color.Lerp(EndColour, StartColour, HealthBar.fillAmount);
        
        //if health is less than or equal to 0 (i.e. the player has lost)
        if (Health <= 0)
        {
            //set canUpdate to false to prevent health being removed any further
            canUpdate = false;

            //disable movement of player
            controller.enabled = false;

            //load the game over scene
            SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
        }
    }
    #endregion

    #region Public Methods
    //public method used to determine the amount of health to add to the player
    public void SetHealth(float health)
    {
        //add the passed in variable 'health' to the players actual 'Health' variable
        //'health' should be more different to better distinguish between the parameter and the script variable 'Health'
        Health += health;

        //if the result of the above operation has the players health set greater than what was declared in the
        //inspector, set the health to equal our maxHealth variable
        if (Health > maxHealth)
            Health = maxHealth;
    }

    //public method used to enable or disable our health decay method
    public void CanUpdate(bool b)
    {
        //set canUpdate to the value of b
        canUpdate = b;
    }
    #endregion
}