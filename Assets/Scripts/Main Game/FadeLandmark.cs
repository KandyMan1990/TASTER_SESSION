using UnityEngine;

public class FadeLandmark : MonoBehaviour
{
    //private variables are only accessible within this script
    #region Private Variables
    //variables needed to manipulate the material of the landmark
    private float emission;
    private Color baseColor;
    private Material mat;
    private Renderer rend;
    private Color finalColor;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //get the Renderer component this script is attached to and store it in the local renderer variable
        rend = GetComponent<Renderer>();

        //set the local material variable to the material attached to the renderer of the game object this script is attached to
        mat = rend.material;

        //set our baseColor variable to green
        baseColor = new Color(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //bounce the emission colour between 0.5 and 1
        emission = Mathf.PingPong(Time.time * 0.5f, 1.0f);

        //assign the final color variable based on the baseColor and emission variables
        finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        //set the materials emission color variable to our finalColor variable
        mat.SetColor("_EmissionColor", finalColor);
    }
    #endregion
}