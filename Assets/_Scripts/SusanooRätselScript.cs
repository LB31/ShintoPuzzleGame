using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SusanooRätselScript : MonoBehaviour
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

    public Image puzzleObject;

    void Start()
    {
        gameState[0] = false; //Start
        gameState[1] = false; //Links
        gameState[2] = false; //Mitte1
        gameState[3] = false; //Mitte2
        gameState[4] = false; //Rechts
    }

   
    void Update()
    {
        if(restart)
        {
            gameState[0] = false;
            gameState[1] = false;
            gameState[2] = false; 
            gameState[3] = false;
            gameState[4] = false;
            plattformMitte.GetComponent<PlatformController>().transform.position = plattformMitte.GetComponent<PlatformController>().start.position;
            plattformLinks.GetComponent<PlatformController>().transform.position = plattformLinks.GetComponent<PlatformController>().start.position;
            plattformRechts.GetComponent<PlatformController>().transform.position = plattformRechts.GetComponent<PlatformController>().start.position;

            //player transport
        }

        if(gameState[0] || gameState[4])
        {
            //LichtStart is On; Move Middle
            plattformMitte.GetComponent<PlatformController>().plattformDoesMove = true;
            
        }
        else
        {
            plattformMitte.GetComponent<PlatformController>().plattformDoesMove = false;

        }

        if(gameState[2] || gameState[1])
        {
            //Licht Mitte1 is on; Move Links
            plattformLinks.GetComponent<PlatformController>().plattformDoesMove = true;
        }
        else
        {
            plattformLinks.GetComponent<PlatformController>().plattformDoesMove = false;
        }

        if(gameState[3] || gameState[4])
        {
            //Licht Mitte2 is on; Move Rechts
            plattformRechts.GetComponent<PlatformController>().plattformDoesMove = true;
        }
        else
        {
            plattformRechts.GetComponent<PlatformController>().plattformDoesMove = false;
        }

        if (gameState[1] && gameState[4])
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

            Material M =  tempel.GetComponent<MeshRenderer>().materials[1];
            Destroy(M);
            
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out hit))
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
                        lightMangaer.transform.GetChild(anzahlLichter-1).gameObject.SetActive(true);
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
                if(puzzleSolved && hit.transform.name.Contains("PuzzleTempel"))
                {
                    hit.transform.GetChild(0).gameObject.SetActive(false);
                    puzzleObject.GetComponent<Image>().enabled = true;
                }
            }
        }


    }
}
