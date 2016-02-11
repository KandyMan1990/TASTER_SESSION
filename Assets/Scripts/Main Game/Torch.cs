using UnityEngine;

public class Torch : MonoBehaviour
{
    //private variables are only accessible within this script
    #region Private Variables
    ////a Light component we will use to control the game objects component
    private Light torchLight;
    //an AudioSource component we will use to control the game objects component
    private AudioSource audioSource;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //Get a reference to the components attached to the game object so we can control them
        torchLight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the F key is pressed, toggle whether the light is enabled or not and play the audio clip
        //attached to the AudioSource component
        if (Input.GetKeyDown(KeyCode.F))
        {
            torchLight.enabled = !torchLight.enabled;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
    #endregion
}