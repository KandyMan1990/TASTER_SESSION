using UnityEngine;

public class Player : MonoBehaviour
{
    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //Hide the cursour during gameplay
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If the escape key is pressed, quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.QuitGame();
        }
    }
    #endregion
}