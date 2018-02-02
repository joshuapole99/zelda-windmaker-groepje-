using UnityEngine;
using System.Collections;

/// <summary>
/// Simple script to hanlde material surfaces. This 
/// will allow you to handle different situations according to
/// what surface you set an object as. Rightn ow we're using it 
/// for arrow penetration/breakage, but it can also be used to
/// determine what sound to play for your character's footsteps.
/// </summary>
public class SurfMaterial : MonoBehaviour {

    public SurfType SurfaceType; //This is the public variable that will be accessed by other scripts.


    public enum SurfType
    {
        Default, //Whatever you want your default surface to be
        Rock,
        Wood,
        Metal //You can always add more surface types
    }
}
