using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //public variables are viewable and editable in the Inspector
    #region Public Variables
    //Tooltips are used to describe to the user what the component/variable in the inspector actually does
    //by hovering the mouse over the variable, the tooltip will be shown

    [Tooltip("Adjust how high the object moves up and down.  Higher is less motion")]
    public int FloatScale = 2;
    [Tooltip("Adjust how fast the object rotates.  Higher is quicker")]
    public int RotateScale = 50;
    [Tooltip("Adjust how much health this pickup will give to the player")]
    public float Health = 20f;
    [Tooltip("The player this pickup will affect")]
    public FinishedPlayer player;
    #endregion

    //private variables are only accessible within this script
    #region Private Variables
    //the position of the game object
    private Vector3 Position;
    #endregion

    #region Unity Methods
    // Use this for initialization
    void Start()
    {
        //assign the position of the game object this script is attached to to our local variable "Position"
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move our local "Position" up and down to the curve of a Sine wave based on the current time
        //and the scale assigned in the inspector
        //multiplied by Time.deltaTime to make it frame rate independent
        Position.y += Mathf.Sin(Time.time * FloatScale) * Time.deltaTime;

        //assign our local "Position" to the game objects position as we can't manipulate it directly
        transform.position = Position;

        //rotate the z axis of the game object this script is attached to by the value set in the inspector
        //multiplied by Time.deltaTime to make it frame rate independent
        transform.Rotate(new Vector3(0, 0, 1) * (RotateScale * Time.deltaTime));
    }

    //this method is available to all objects that have a collider attached to them
    void OnTriggerEnter(Collider other)
    {
        //if the game object that collides with this game object is tagged as "Player"
        if (other.gameObject.tag == "Player")
        {
            //set the players health by the value set in the inspector
            player.SetHealth(Health);

            //destroys the game object this script is attached to
            Destroy(gameObject);
        }
    }
    #endregion
}