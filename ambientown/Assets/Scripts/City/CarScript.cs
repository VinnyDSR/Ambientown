using UnityEngine;

public class CarScript : MonoBehaviour
{

    public AudioSource _as;
    public AudioClip[] audioClipArray;
    private int index;




    void Awake()
    {
        _as = GetComponent<AudioSource>();

    }


    // Start is called before the first frame update
    void Start()
    {
        _as.clip = audioClipArray[0];
        _as.Play();


        //FindObjectOfType<AudioManager>().Play("CarSound");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        //FindObjectOfType<AudioManager>().StopPlaying("CarSound");

        _as.Stop();

    }






}
