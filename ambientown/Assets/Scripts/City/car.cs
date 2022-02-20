using UnityEngine;

public class car : MonoBehaviour
{


    //public AudioSource _as;
    //public AudioClip[] audioClipArray;
    //private int index;

    //public GameObject[] myCar;



    private


    void Awake()
    {
        //_as = GetComponent<AudioSource>();

    }


    // Start is called before the first frame update
    void Start()
    {
        //_as.clip = audioClipArray[0];
        //_as.Play();


        FindObjectOfType<AudioManager>().Play("CarSound");
    }

    //Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Black_Car") == null && GameObject.Find("Blue_Car") == null)

        {
            FindObjectOfType<AudioManager>().StopPlaying("CarSound");

        }





    }

    //void OnDestroy()
    //{

    //    if (GameObject.Find("Black_Car") == null && GameObject.Find("Blue_Car") == null)

    //    { 
    //        FindObjectOfType<AudioManager>().StopPlaying("CarSound");
    //    }

    //}

}
