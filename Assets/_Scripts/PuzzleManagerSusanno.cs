using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerSusanno : MonoBehaviour
{
    public int anzahlLichter = 3;

    public GameObject pollerStart;
    public GameObject pollerLinks;
    public GameObject pollerMitte1;
    public GameObject pollerMitte2;
    public GameObject pollerRechts;
    public GameObject lightMangaer;
    public GameObject tempel;


    public GameObject plattformLinks;
    public GameObject plattformMitte;
    public GameObject plattformRechts;

    public bool[] gameState = new bool[5];

    public GameObject electroBahnLinks;
    public GameObject electroBahnMitte;
    public GameObject electroBahnRechts;

    public bool restart = false;
    private bool puzzleSolved = false;
    public bool puzzleActivated = false;

    public Image puzzleObject;

    void Update()
    {
        if (restart)
        {
            PuzzleStart();
        }

        if (gameState[0] || gameState[4])
        {
            //LichtStart is On; Move Middle
            plattformMitte.GetComponent<PlatformController>().plattformDoesMove = true;
            electroBahnMitte.SetActive(true);

        }
        else
        {
            plattformMitte.GetComponent<PlatformController>().plattformDoesMove = false;
            electroBahnMitte.SetActive(false);

        }

        if (gameState[2] || gameState[1])
        {
            //Licht Mitte1 is on; Move Links
            plattformLinks.GetComponent<PlatformController>().plattformDoesMove = true;
            electroBahnLinks.SetActive(true);
        }
        else
        {
            plattformLinks.GetComponent<PlatformController>().plattformDoesMove = false;
            electroBahnLinks.SetActive(false);
        }

        if (gameState[3] || gameState[4])
        {
            //Licht Mitte2 is on; Move Rechts
            plattformRechts.GetComponent<PlatformController>().plattformDoesMove = true;
            electroBahnRechts.SetActive(true);
        }
        else
        {
            plattformRechts.GetComponent<PlatformController>().plattformDoesMove = false;
            electroBahnRechts.SetActive(false);
        }

        if (gameState[1] && gameState[4])
        //if(gameState[0])
        {
            puzzleSolved = true;
            pollerLinks.transform.GetChild(0).gameObject.SetActive(true);
            pollerMitte1.transform.GetChild(0).gameObject.SetActive(true);
            pollerMitte2.transform.GetChild(0).gameObject.SetActive(true);
            pollerRechts.transform.GetChild(0).gameObject.SetActive(true);
            pollerStart.transform.GetChild(0).gameObject.SetActive(true);

            plattformLinks.GetComponent<PlatformController>().plattformDoesMove = true;
            plattformRechts.GetComponent<PlatformController>().plattformDoesMove = true;
            plattformMitte.GetComponent<PlatformController>().plattformDoesMove = true;

            electroBahnMitte.SetActive(true);
            electroBahnLinks.SetActive(true);
            electroBahnRechts.SetActive(true);
            Destroy(tempel);
            

        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && puzzleActivated)
        {
            if (Physics.Raycast(ray, out hit, 2f))
            {
                //Poller
                if (hit.transform.name.Contains("Poller") && !puzzleSolved)
                {
                    int result = System.Int32.Parse(Regex.Match(hit.transform.name, @"\d+").Value);
                    if (!gameState[result] && anzahlLichter < 1)
                    {
                        return;
                    }
                    if (gameState[result])
                    {
                        anzahlLichter++;
                        Debug.Log(anzahlLichter);
                        lightMangaer.transform.GetChild(anzahlLichter - 1).gameObject.SetActive(true);
                    }
                    else
                    {
                        anzahlLichter--;
                        Debug.Log(anzahlLichter);
                        lightMangaer.transform.GetChild(anzahlLichter).gameObject.SetActive(false);
                    }
                    gameState[result] = !gameState[result];
                    hit.transform.GetChild(0).gameObject.SetActive(gameState[result]);

                }
                if (puzzleSolved && hit.transform.name.Contains("PuzzleTempel"))
                {
                    hit.transform.GetChild(0).gameObject.SetActive(false);
                    puzzleObject.GetComponent<Image>().enabled = true;
                    PlayMakerFSM.BroadcastEvent("PuzzleSolved");
                }
            }
        }


    }

    public void PuzzleStart()
    {

        gameState = new bool[5];
        anzahlLichter = 3;
        lightMangaer.transform.GetChild(0).gameObject.SetActive(true);
        lightMangaer.transform.GetChild(1).gameObject.SetActive(true);
        lightMangaer.transform.GetChild(2).gameObject.SetActive(true);
        pollerLinks.transform.GetChild(0).gameObject.SetActive(false);
        pollerMitte1.transform.GetChild(0).gameObject.SetActive(false);
        pollerMitte2.transform.GetChild(0).gameObject.SetActive(false);
        pollerRechts.transform.GetChild(0).gameObject.SetActive(false);
        pollerStart.transform.GetChild(0).gameObject.SetActive(false);
        plattformMitte.GetComponent<PlatformController>().transform.position = plattformMitte.GetComponent<PlatformController>().start.position;
        plattformLinks.GetComponent<PlatformController>().transform.position = plattformLinks.GetComponent<PlatformController>().start.position;
        plattformRechts.GetComponent<PlatformController>().transform.position = plattformRechts.GetComponent<PlatformController>().start.position;

    }

    public void activatePuzzle()
    {
        puzzleActivated = true;
    }
}
