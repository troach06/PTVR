using UnityEngine;
using System.Collections;
using Leap;
using System.Collections.Generic;
using Leap.Unity;

public class GrabRight : MonoBehaviour
{
    Quaternion dartrot = Quaternion.Euler(0, 90, 0);

    LeapProvider provider;
    public GameObject dart;
    public Transform lefthand;
    public Transform righthand;
    public AudioSource woosh;
   /// Vector3 modelpos = new Vector3(0, 0.6f, -9.5f);
  //  Quaternion modelrot = Quaternion.Euler(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        //woosh.enabled = false;
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        dart.GetComponent<Rigidbody>().isKinematic = true;
    }


    // Update is called once per frame
    void Update()
    {
        dart = GameObject.FindWithTag("Dart");
        Frame frame = provider.CurrentFrame; // controller is a Controller object
        List<Hand> hands = frame.Hands;
        for (int h = 0; h < hands.Count; h++)
        {
            Hand hand = hands[h];
            if (dart.transform.parent == righthand && hand.IsRight && hand.GrabStrength == 0)
            {
                dart.transform.parent = null;
                dart.GetComponent<Rigidbody>().isKinematic = false;
                dart.GetComponent<Rigidbody>().AddForce(-6f, 0f, 0f, ForceMode.Impulse);
                dart.transform.rotation = (dartrot);
               // model.transform.position = modelpos;
               // model.transform.rotation = modelrot;
               // woosh.enabled = false;
            }
        }
    }

    void OnTriggerStay(Collider other) // C#, type first, name in second
    {
        Frame frame = provider.CurrentFrame; // controller is a Controller object
        List<Hand> hands = frame.Hands;
        for (int h = 0; h < hands.Count; h++)
        {
            Hand hand = hands[h];
            if (other.gameObject.tag == "Dart")
            {
                if (hand.IsRight && hand.GrabStrength > 0.1)
                {
                    Debug.Log("Grab");
                    dart.GetComponent<Rigidbody>().isKinematic = true;
                    dart.transform.parent = righthand;
                    //woosh.enabled = true;
                }
            }
        }
    }
}


