using UnityEngine;
using System.Collections;

public class ArrowHandler : MonoBehaviour {


    public float Lifetime; //The float amount of seconds this arrow should remain in play before being destroyed
    public float DestroyDelay; //The delay after this object hits something before destroying it

    private TimerVar _lifeTimer; //If the arrow doesn't hit anything we need to destroy it after a time to save performance.
    private TimerVar _deathTimer; //Timer for when to destroy if the arrow hits another object while in flight.
    private TimerVar _colTimer; //We need this small timer to turn on the box collider for this object after firing.
    private bool _hitSomething; //Has this arrow hit an object? Used to destroy the arrow after a time.
    public bool IsActive; //Has this arrow been fired? Used to start lifetime calculations.

    private Rigidbody _rig; //This is a reference to the rigidbody attached to the arrow.

	// Use this for initialization
	void Start () {
        _rig = GetComponent<Rigidbody>(); //Grab a reference to the rigidbody so we can do extra fun physics stuff.

        //Set up our timers.
        _lifeTimer = new TimerVar(Lifetime, false);
        _deathTimer = new TimerVar(DestroyDelay, false);
	    
	}
	
	// Update is called once per frame
	void Update () {

        //Don't do any calculations until after the arrow has been fired.
        if (!IsActive)
            return;

        if(_hitSomething)
        {
            //Has this arrow hit something?
            //If yes, start the death routine
            if(_deathTimer.TimerDone)
            {
                //Check to see if the destroy delay is done.
                //If yes, destroy the object.

                Destroy(gameObject);
            }
            else
            {
                //If not, keep counting down.
                _deathTimer.TimerUpdate();
            }
        }
        else
        {
            

            //If not, keep checking the lifetime info
            if (_lifeTimer.TimerDone)
            {
                //Has this object exceeded it's allotted life time?
                //If yes, we destroy it.
                Destroy(gameObject);
            }
            else
            {
                //If no, we keep counting down.
                _lifeTimer.TimerUpdate();
            }
        }

	
	}

    void HitOjbect(GameObject obj)
    {
        //First we will check to see if the object has a SurfMaterial script
        SurfMaterial sF = obj.GetComponent<SurfMaterial>();
        if(sF != null)
        {
  
            switch(sF.SurfaceType)
            {
                case SurfMaterial.SurfType.Default:
                    //Place your desired behavior here for default surfaces
                    break;
                case SurfMaterial.SurfType.Metal:
                    //Metal surfaces will bounce our arrow
                    _hitSomething = true; //This will initialize the general destroy behavior we set up earlier.
                    break;
                case SurfMaterial.SurfType.Wood:
                    //Our arrow will stick to wood surfaces
                   
                    _rig.isKinematic = true;//Setting isKinematic to true stops physics handling and will simulate the arrow "sticking" to a surface.
                    break;
                case SurfMaterial.SurfType.Rock:
                    //Rock surfaces will break the arrow (no bounce)
                    Destroy(gameObject); //Destroy the object to simulate breakage.
                    //Alternatively, you may want to write your own code in here to instantiate a "broken arrow"
                    //prefab at this arrow's location before deleting. 
                    break;

                    //Normally, you'd have a "default" case in a switch statement, but since an
                    //enum can only be set to one of it's option variables, there's no need for
                    //that in this situation.
            }
        }

        //If therei's no SurfMaterial on this object, that's fine, we'll just apply the default behavior.
        _hitSomething = true;
    }

    void FixedUpdate()
    {

        //Any physics code should be run from the FixedUpdate() thread, where most physics calculations
        //are executed.

        //No more physics calucaltions if we're running the death code.
        if (_hitSomething)
            return;

        //This will make sure that the arrow properly points in the direction it's moving. Gives
        //it a more realistic look when bouncing and when the impulse force of the arrow fireing is 
        //low enough for the arrow to come back down.
        if (_rig.velocity != Vector3.zero)
        {
            Quaternion aRot = Quaternion.LookRotation(_rig.velocity.normalized);
            _rig.rotation = Quaternion.Slerp(_rig.rotation, aRot, Time.deltaTime * 15);
        }
     

    }

    void OnCollisionEnter(Collision col)
    {
        //This function must be on the gameObject that has the box collider attached to it.
        HitOjbect(col.gameObject);
    }
}



