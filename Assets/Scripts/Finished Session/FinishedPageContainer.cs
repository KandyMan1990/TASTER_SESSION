#region Using Statements
//additional using statements to have access to UI components and Unity's FirstPersonController contained within
//Unity's Standard Assets
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
#endregion

public class FinishedPageContainer : MonoBehaviour
{
    //private variables are only accessible within this script
    #region Private Variables
    //bool to determine if the player is viewing a page
    private bool viewingPage = false;

    //bool to determine if the game is complete
    private bool isComplete = false;
    #endregion

    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //text variable to get a reference to the Collect Text UI component in the games UI
    public Text CollectText;

    //first person controller variable to get a reference to the FirstPersonController component in the game
    public FirstPersonController controller;

    //FinishedPlayer variable to get a reference to the FinishedPlayer component in the game
    //this would be the regular player class used during the session
    //FinishedPlayer class was only created to demo the finished class whilst leaving the original class available
    //to be edited during the session
    public FinishedPlayer player;
    #endregion

    #region Unity Methods
    //trigger methods are available to all objects that have a collider attached to them
    //this method is called when leaving the trigger
    void OnTriggerExit()
    {
        //when leaving the trigger volume, disable the Collect Text UI component in the games UI
        CollectText.enabled = false;
    }

    //trigger methods are available to all objects that have a collider attached to them
    //this method is called when the game object stays within the trigger volume for more than 1 frame
    void OnTriggerStay()
    {
        //if the player is not viewing a page
        if (!viewingPage)
        {
            //enable the Collect Text UI component in the the scene
            CollectText.enabled = true;

            //if the player presses the E key
            if (Input.GetKeyDown(KeyCode.E))
            {
                //set viewing page variable to true
                viewingPage = true;

                //disable the character controller so the user can't move
                controller.enabled = false;

                //stop the players health from decaying whilst viewing a page
                player.CanUpdate(false);

                //call the PageCollected method
                AppData.Data.PageCollected();

                //create a page variable and assign the earliest available page in the pages list to it
                Page currentPage = AppData.Data.Pages[AppData.Data.NumberOfPagesCollected - 1];

                //assign the current page variables to the UI components in the AppData class
                AppData.Data.Name.text = currentPage.Name;
                AppData.Data.Message.text = currentPage.Message;
                AppData.Data.Example.text = currentPage.Example;

                //switch the alpha of the Page UI panel and the general UI panel in the scene to show the page
                AppData.Data.PagePanel.alpha = 1f;
                AppData.Data.UIPanel.alpha = 0f;
            }
        }

        //if the player is viewing a page
        if (viewingPage)
        {
            //disable the Collect Text UI component
            CollectText.enabled = false;

            //if the player presses the R key
            //(the R key is used here as this method is called multiple times a frame
            //if we use the E key again, the page will appear and disappear before the player even sees it)
            if (Input.GetKeyDown(KeyCode.R))
            {
                //set viewing page variable to false
                viewingPage = false;

                //enable the controller so the player can move again
                controller.enabled = true;

                //enable the health decay function now the player is not viewing a page
                player.CanUpdate(true);

                //switch the alpha of the two UI panels again so the user doesn't see the page anymore
                AppData.Data.PagePanel.alpha = 0f;
                AppData.Data.UIPanel.alpha = 1f;

                //if AppData GameComplete is true and local variable isComplete is false
                if (AppData.Data.GameComplete && !isComplete)
                {
                    //set isComplete to true
                    //this prevents this section of code running infinitely
                    isComplete = true;

                    //call the fadeInstructionsPanel method and set its text to the string below
                    AppData.Data.FadeInstructionsPanel("Get back to the lighthouse");
                }

                //destroy the game object this script is attached to
                Destroy(gameObject);
            }
        }
    }
    #endregion
}