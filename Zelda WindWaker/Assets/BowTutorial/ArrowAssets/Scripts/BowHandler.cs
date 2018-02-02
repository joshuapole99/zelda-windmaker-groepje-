using UnityEngine;
using System.Collections;

public class BowHandler : MonoBehaviour {

    public GameObject fabArrow; //Reference to the arrow prefab
    public GameObject gArrow; //Reference to the initial arrow. Note: You need to have an arrow on the bow already at start.

    public Transform pt_arrow; //Point on the bow to create the new arrow after firing.


    public float ReloadDelay; //This is an exposed variable in the inspector that allows you to set the reload delay seconds directly.
    private TimerVar ReloadTimer; //Simple timer to figure out when to reload the bow.
    private bool _isReloading; //Is the bow currently in the process of reloading? Set this to true directly after firing.

    public float ArrowForce; //Force to be applied to the arrow when firing
    public bool CanFire; //Simple boolean to see if we can currently fire the bow or not.


    void Start()
    {
        //First thing to do is create the timer
        ReloadTimer = new TimerVar(ReloadDelay, false);

        //This is just for organization purposes. By default
        //undeclared booleans are set to "false" at the start.
        //This just shows you how the initial values should be.
        _isReloading = false;
        CanFire = true;

        //It may be the case, depending on the situation, that you don't already have
        //an arrow loaded when the Start() is called. This will make sure it gets an arrow
        //no matter what.
        if(gArrow == null)
        {

            ReloadArrow();
        }
    }

	// Update is called once per frame
	void Update () {

        //In the editor window, we'll draw a nice red line to show the direction the 
        //bow is aiming. 
        Debug.DrawRay(transform.position, transform.forward * 300, Color.red);

	
        //Check first to make sure we can fire. No need to look for input
        //if nothing can be done about it.
        if(CanFire)
        {
            //Simple input code to get a keypress. Change this to whatever key you like
            if (Input.GetMouseButtonDown(0))
            {
               
                


                FireArrow(); //Function that actually fires the arrow.
            }
        }
        else
        {
            if(_isReloading)
            {
                if(ReloadTimer.TimerDone)
                {
                    //If the timer is done, we set the variables back to their default
                    //values and then call the Reload function
                    _isReloading = false; //We aren't reloading anymore. This should be called before anything else to prevent unwanted code loops.
                    ReloadTimer.Reset(); //TimerVar.Reset() resets all the TimerVar variables back to their initial values
                    ReloadArrow(); //Finally, call the reload function.
                }
                else
                {
                    //If the timer isn't done, we keep updating it's values.
                    ReloadTimer.TimerUpdate();
                }
            }
        }
       

	}

    /// <summary>
    /// Reload Arrow
    /// 
    /// This function will put a new arrow on the bow. It does not handle
    /// anything with timing, it's just for the instantiation and positioning
    /// of the arrow. We'll use a timer var to delay the reload function in
    /// the Update() thread.
    /// </summary>
    public void ReloadArrow()
    {
        //First we make sure there is an arrow prefab to instantiate.
        if(fabArrow != null)
        {
            //We instantiate(create via prefab) a new arrow and save it as the 
            //arrow GameObject "gArrow" to be fired when needed.
            gArrow = (GameObject)Instantiate(fabArrow, pt_arrow.position, pt_arrow.rotation);

            //Next step is to parent the arrow to the bow so that it moves and rotates with it properly.
            gArrow.transform.SetParent(transform); //I've heard it's better to use Transform.SetParent() rather than Transform.parent = transform.

            //Now that the new arrow is in. We can fire again
            CanFire = true;
        }
        else
        {
            //If it is null, then we need to have a little chat with ourselves.
            Debug.Log("Bow Error! You are trying to instantiate an object without a reference! Sent from: " + gameObject.name);
            //I usually like to toss in the name of the object that throws the error just so I have a more focused search area
            // when trying to identify a problem.
        }
    }


    public void FireArrow()
    {
        //Anytime you use a public variable, or any variable really, you should
        //always make sure the variable has been set before attempting to use it.
        //This will avoid infuriating errors. Alternatively, you may want to send 
        //some information to the output log if it is null so that you can see what
        //happened later.

        if(gArrow != null)
        {
           
            //Get the rigidbody component of the arrow.
            Rigidbody rArrow = gArrow.GetComponent<Rigidbody>();

            if (rArrow != null)
            {

                //First thing we want to do is de-parent the arrow from the bow
                //This will prevent the arrow from moving with the bow even after
                //it has been fired

                gArrow.transform.SetParent(null);

               // gArrow.transform.parent = null; //Not sure how you'd clear a parent using a function instead of this method. SetParent(null)? I don't know if that's okay. 

                //Setting isKinematic to false will allow physics to be applied to
                //the arrow.
                rArrow.isKinematic = false;

                //Grab a reference to the arrow handler.
                ArrowHandler aHand = gArrow.GetComponent<ArrowHandler>();
                //Small note: You can use ArrowHandler.cs to store your damage information
                //when the arrow hits its target, it can use the damage information stored in
                //the ArrowHandler component to apply damage to the target. Let me know if you
                //would like a quick demonstration of how this works.

                if(aHand != null)
                {
                    //Set the arrow handler to active since we are now firing
                    aHand.IsActive = true;
                }

                //This command will add velocity to a rigidbody using a vector
                //For our situation, we want the arrow to fly forward, so we multiply
                //the arrow's forward facing vector times the force amount.
                rArrow.AddForce(pt_arrow.forward * ArrowForce, ForceMode.Impulse);

                //ForceMode.Impulse is just one of several force modes. You might want to play around with those
                //to see if you can get better results with different options.

                //Since we aren't instantiating a new arrow directly after fire anymore
                //we need to tell the script not to allow firing until a new arrow has 
                //been loaded in.
                CanFire = false;

                //We've just fired the arrow, it's time to reload.
                _isReloading = true; //This will take care of everything to do with reloading.
            }
            else
            {
                Debug.Log("Bow Error! There is no rigidbody attached to the arrow object! Sent from: " + gArrow.name);
            }
        }
        else
        {
            //If it is null, then we need to have a little chat with ourselves.
            Debug.Log("Bow Error! The Arrow GameObject has not been set! Can't logically fire something that isn't there. Sent from: " + gameObject.name);
        }


    }
}
