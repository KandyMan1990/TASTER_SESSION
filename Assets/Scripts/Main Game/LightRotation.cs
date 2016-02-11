using UnityEngine;

public class LightRotation : MonoBehaviour
{
    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //variable to determine rotation speed, assigned in the inspector
    public float rotationSpeed = 20f;
    #endregion

    #region Unity Methods
    // Update is called once per frame
    void Update()
    {
        //rotate the object on the y axis by the value specified in the inspector
        //value specified is how many units per second it will move
        gameObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    #endregion
}