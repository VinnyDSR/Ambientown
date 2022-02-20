using UnityEngine;

public class StopSound : MonoBehaviour
{
    public class FallingPlatform : MonoBehaviour
    {

        public float fallingTime;

        private TargetJoint2D target;
        private BoxCollider2D boxColl;

        void Start()
        {
            target = GetComponent<TargetJoint2D>();
            boxColl = GetComponent<BoxCollider2D>();
            FindObjectOfType<AudioManager>().Play("PlattformSound");
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Invoke("Falling", fallingTime);
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == 9)
            {
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().StopPlaying("PlattformSound");
            }
        }

        void Falling()
        {
            target.enabled = false;
            boxColl.isTrigger = true;
        }
    }
}
