using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
    {
    Vector3 initposition = new Vector3(-6.5f, -0.1f, 3);
    Quaternion dartrot = Quaternion.Euler(0, 90, 0);
    // Use this for initialization
    void Start()
        {

        }

    // Update is called once per frame
    void Update()
        {

        }
    void OnTriggerEnter(Collider other) // C#, type first, name in second
        {
        if (other.gameObject.tag == "Board")
            {
            Debug.Log("Grab");
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            StartCoroutine(Reset());
            }
        if (other.gameObject.tag == "Wall")
            {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            StartCoroutine(Reset());
            }
        }
    public IEnumerator Reset()
        {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Darts");
        }
    }
