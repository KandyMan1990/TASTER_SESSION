#region Using Statements
//additional using statements to allow access to UI elements and scene manager class
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#endregion

public class GameOver : MonoBehaviour
{
    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //public reference to a text component, set in the inspector
    public Text text;
    #endregion

    //private variables are only accessible within this script
    #region Private Variables
    //private reference to the CanvasGroup component attached to the object
    CanvasGroup canvas;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //get a reference to the CanvasGroup component and store it in canvas to be manipulated later
        canvas = GetComponent<CanvasGroup>();
        
        //run the FadeIn method as a Coroutine
        //this allows the method to take place over multiple frams instead of 1
        //allows the fade to be smooth and not jump from 0 to 1 in 1 frame
        StartCoroutine(FadeIn());
    }
    #endregion

    #region Coroutines
    //method that will fade in the scene after it has loaded in the background
    IEnumerator FadeIn()
    {
        //set the UI alpha channel to 0 so we can't see it
        canvas.alpha = 0;

        //while the alpha channel is not set to 1
        while (canvas.alpha < 1)
        {
            //add 0.25 to the alpha channel over 1 second
            //this gives us 4 seconds of fade in time
            canvas.alpha += 0.25f * Time.deltaTime;

            //break out of the method and return to this point on the next frame
            //as the break is in the while loop, the loop will continue on the next frame unless alpha is 1 or more
            yield return 0;
        }

        //in case the alpha value went above 1 at the end of the loop, set it back to 1 (fully visible)
        canvas.alpha = 1;

        //unload the game scene from memory
        SceneManager.UnloadScene("Finished Game");

        //local variable set to equal the colour of the text we assigned in the inspector
        Color c = text.color;

        //while the alpha channel of the texts colour is greater than 0
        while (text.color.a > 0)
        {
            //remove 0.25 from the alpha channel over 1 second to fade the text out
            //this should give us 4 seconds of fade time
            c.a -= 0.25f * Time.deltaTime;

            //set the colour of the text to our local colour variable
            //this must be done in this way as we cannot adjust the alpha channel of the text variable manually,
            //we can only assign it a colour variable
            text.color = c;

            //break out of the method and return to this point in the next frame
            yield return 0;
        }

        //load in the background the first scene in the build order, in this case, the main menu
        SceneManager.LoadSceneAsync("Menu");
    }
    #endregion
}