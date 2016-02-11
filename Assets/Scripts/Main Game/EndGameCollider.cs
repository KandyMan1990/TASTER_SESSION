#region Using Statements
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class EndGameCollider : MonoBehaviour
{
    #region Unity Methods
    //usable unity method should a game object have a trigger volume attached
    void OnTriggerEnter()
    {
        //if the game is complete (all pages collected)
        if (AppData.Data.GameComplete)
        {
            //load the main menu scene
            SceneManager.LoadSceneAsync("Menu");
        }
    }
    #endregion
}