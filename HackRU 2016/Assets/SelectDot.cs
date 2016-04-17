using UnityEngine;
using System.Collections;

public class SelectDot : MonoBehaviour
    {
    public GameObject linetarget;
    LineRenderer line1;
    LineRenderer line2;
    LineRenderer line3;
    LineRenderer line;
    public GameObject lineobject1;
    public GameObject lineobject2;
    public GameObject lineobject3;
    public GameObject orb1;
    public GameObject orb2;
    public GameObject orb3;
    public Material fade1;
    public Material fade2;
    public Material fade3;
    public Material glow1;
    public Material glow2;
    public Material glow3;
    public AudioSource beep;
    public AudioSource victory;
    // Use this for initialization
    void Start()
        {
        line1 = lineobject1.GetComponent<LineRenderer>();
        line2 = lineobject2.GetComponent<LineRenderer>();
        line3 = lineobject3.GetComponent<LineRenderer>();
        line1.enabled = false;
        line2.enabled = false;
        line3.enabled = false;
      
        }

    // Update is called once per frame
    void Update()
        {
        line1 = lineobject1.GetComponent<LineRenderer>();
        line2 = lineobject2.GetComponent<LineRenderer>();
        line3 = lineobject3.GetComponent<LineRenderer>();
        line.SetPosition(0, linetarget.transform.position);
        }
    void OnTriggerEnter(Collider other)
        {
        if ((other.gameObject.tag == "1") && (line1.enabled == false && line2.enabled == false && line3.enabled == false))
            {
            line1.enabled = true;
            orb1.GetComponent<Renderer>().material = glow1;
            line = lineobject1.GetComponent<LineRenderer>();
            beep.Play();
            }
        else if ((other.gameObject.tag == "1") && (line == lineobject1.GetComponent<LineRenderer>()))
            {
            line1.enabled = false;
            line2.enabled = false;
            orb1.GetComponent<Renderer>().material = fade1;
            orb2.GetComponent<Renderer>().material = fade2;
            orb3.GetComponent<Renderer>().material = fade3;
            }

        else if ((other.gameObject.tag == "2") && (line == lineobject1.GetComponent<LineRenderer>()))
            {
            
            line2.enabled = true;
            orb2.GetComponent<Renderer>().material = glow2;
            line = lineobject2.GetComponent<LineRenderer>();
            beep.Play();
            }
        else if ((other.gameObject.tag == "2") && (line == lineobject2.GetComponent<LineRenderer>() || (line == lineobject3.GetComponent<LineRenderer>())))
            {
            line1.enabled = false;
            line2.enabled = false;
            line3.enabled = false;
            orb1.GetComponent<Renderer>().material = fade1;
            orb2.GetComponent<Renderer>().material = fade2;
            orb3.GetComponent<Renderer>().material = fade3;
            }
        else if ((other.gameObject.tag == "3") && (line3.enabled == false && line == lineobject2.GetComponent<LineRenderer>()))
            {
            line3.enabled = true;
            orb3.GetComponent<Renderer>().material = glow3;
            line = lineobject3.GetComponent<LineRenderer>();
            beep.Play();
            }
        else if ((other.gameObject.tag == "3") && (line == lineobject3.GetComponent<LineRenderer>() || (line == lineobject1.GetComponent<LineRenderer>())))
            {
            line3.enabled = false;
            line2.enabled = false;
            line1.enabled = false;
            orb1.GetComponent<Renderer>().material = fade1;
            orb2.GetComponent<Renderer>().material = fade2;
            orb3.GetComponent<Renderer>().material = fade3;
          
            }
        else if ((other.gameObject.tag == "1") && (line == lineobject3.GetComponent<LineRenderer>()))
            {
            Debug.Log("Victory");
            victory.Play();
            line = null;
          
            }
        }
    }
    
