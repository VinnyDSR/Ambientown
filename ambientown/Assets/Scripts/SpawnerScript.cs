using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("Spawner Point")]
    public GameObject spawnPoint;

    [Header("Player Female 1")]
    public GameObject PlayerFemale1Pink;
    public GameObject PlayerFemale1Green;
    public GameObject PlayerFemale1Yellow;

    [Header("Player Female 2")]
    public GameObject PlayerFemale2Pink;
    public GameObject PlayerFemale2Green;
    public GameObject PlayerFemale2Yellow;

    [Header("Player Female 3")]
    public GameObject PlayerFemale3Pink;
    public GameObject PlayerFemale3Green;
    public GameObject PlayerFemale3Yellow;

    [Header("Player Male 1")]
    public GameObject PlayerMale1Red;
    public GameObject PlayerMale1Green;
    public GameObject PlayerMale1Blue;

    [Header("Player Male 2")]
    public GameObject PlayerMale2Red;
    public GameObject PlayerMale2Green;
    public GameObject PlayerMale2Blue;

    [Header("Player Male 3")]
    public GameObject PlayerMale3Red;
    public GameObject PlayerMale3Green;
    public GameObject PlayerMale3Blue;

    private int SpawnNumber;
    private int Gender;
    private int Clothes;

    void Start()
    {
        SpawnNumber = PlayerPrefs.GetInt("SelectedSkin");
        Gender = PlayerPrefs.GetInt("Gender");
        Clothes = PlayerPrefs.GetInt("Clothes");

        Vector3 spawnPointPosition = new Vector3();
        spawnPointPosition = spawnPoint.transform.position;

        if (Gender == 1)
        {
            switch (SpawnNumber)
            {
                case 1:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerMale1Red, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerMale1Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerMale1Blue, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerMale1Red, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                case 2:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerMale2Red, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerMale2Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerMale2Blue, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerMale2Red, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                case 3:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerMale3Red, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerMale3Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerMale3Blue, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerMale3Red, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                default:
                    Debug.Log("Não existe este personagem!");
                    break;
            }
        }
        else if (Gender == 2)
        {
            switch (SpawnNumber)
            {
                case 1:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerFemale1Pink, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerFemale1Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerFemale1Yellow, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerFemale1Pink, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                case 2:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerFemale2Pink, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerFemale2Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerFemale2Yellow, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerFemale2Pink, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                case 3:
                    switch (Clothes)
                    {
                        case 1:
                            Instantiate(PlayerFemale3Pink, spawnPointPosition, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(PlayerFemale3Green, spawnPointPosition, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(PlayerFemale3Yellow, spawnPointPosition, Quaternion.identity);
                            break;
                        default:
                            Instantiate(PlayerFemale3Pink, spawnPointPosition, Quaternion.identity);
                            Debug.Log("Não existe esta roupa!");
                            break;
                    }
                    break;
                default:
                    Debug.Log("Não existe este personagem!");
                    break;
            }
        }
        Debug.Log("Gender =" + Gender);
        Debug.Log("SeletedSkin =" + SpawnNumber);
        Debug.Log("Clothes =" + Clothes);
    }
}