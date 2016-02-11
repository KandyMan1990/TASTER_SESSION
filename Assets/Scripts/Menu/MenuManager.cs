//additional using statement to access the scene manager class
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        //enable the cursor so we can select buttons in this scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    #region public methods
    //method to change scene
    public void ChangeScene(int i)
    {
        //load in the background the number which represents the scene we want to load
        //number found in build order settings
        SceneManager.LoadSceneAsync(i);
    }

    //this method is declared static so we don't have to create a MenuManager object
    //we can just type MenuManager.QuitGame(); to call this method
    //this method is used to quit the game during gameplay
    public static void QuitGame()
    {
        //if we are in the Editor, we have to set the isPlaying value to false in order to stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //if we are not in the Editor, we must be playing the final game
        //we can just call Application Quit to exit the game
#endif
        Application.Quit();
    }

    //buttons cannot call static methods
    //we create a wrapper for our static QuitGame method so the button in the UI can quit the game
    public void Quit()
    {
        QuitGame();
    }
    #endregion
}