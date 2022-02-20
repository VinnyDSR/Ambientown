using UnityEngine;

public class RedCar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("CarSound");
    }

    void OnDestroy()
    {
        FindObjectOfType<AudioManager>().StopPlaying("CarSound");
    }


}
