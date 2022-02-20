using Assets.Lineker.Assets.REST_Client.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    public GameObject table;
    public GameObject selector;
    public Text titleText;
    private GetProgressosMapa api;


    public Text[] nicknames;
    public Text[] scores;
    public Text[] positions;

    private string[] nicknameValues = new string[10];
    private string[] scoreValues = new string[10];

    void Start()
    {
        api = FindObjectOfType<GetProgressosMapa>();
        table.SetActive(false);
        selector.SetActive(true);
    }

    public void forestClick()
    {
        table.SetActive(true);
        selector.SetActive(false);
        setText("Forest", "Floresta");
        api.CallLeaderboard(1);
    }

    public void cityClick()
    {
        table.SetActive(true);
        selector.SetActive(false);
        setText("City", "Cidade");
        api.CallLeaderboard(2);
    }

    public void riverClick()
    {
        table.SetActive(true);
        selector.SetActive(false);
        setText("River", "Rio");
        api.CallLeaderboard(3);
    }

    public void xClick()
    {
        table.SetActive(false);
        selector.SetActive(true);
    }

    public void ShowLeaderboard(List<Leaderboard> leaderboard)
    {

        //Primeiro os apelidos e pontuações são zerados para não manter dados antigos
        for (int i = 0; i < 10; i++)
        {
            nicknameValues[i] = "";
            scoreValues[i] = "";
        }

        //Segundo cada apelido e pontuação da leaderboard são passados para os seus respectivos valores
        for (int i = 0; i < leaderboard.Count; i++)
        {
            nicknameValues[i] = $"{i + 1} \t - \t {leaderboard[i].Apelido}";
            scoreValues[i] = $"{leaderboard[i].Pontuacao}";
        }

        //Terceiro os apelidos e valores são passados para suas respectivas posições na tela
        for (int i = 0; i < 10; i++)
        {
            nicknames[i].text = nicknameValues[i];
            scores[i].text = scoreValues[i];
        }
    }

    private void setText(string en, string pt)
    {
        switch (PlayerPrefs.GetString("language"))
        {
            case "en":
                titleText.text = en;
                break;
            case "pt":
                titleText.text = pt;
                break;
            default:
                titleText.text = "Atribua um texto";
                break;
        }
    }
}