using UnityEngine;

public class FanScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("FanSound");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
