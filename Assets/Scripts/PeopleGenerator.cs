﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    public QueueManager cuemanager;
    public UIManagerScript uiManagerScript;
    public static List<GameObject> peopleQ;
    [HideInInspector] public bool spawningPeople { get; private set; } = true;
    GameStatusManager gm;
    public static float maxPeopleQLength = 5;

    private bool isFirstSpawn = true;

    private void Start()
    {
        gm = FindObjectOfType<GameStatusManager>().GetComponent<GameStatusManager>();

        uiManagerScript = FindObjectOfType<UIManagerScript>();

        Debug.Assert(peopleQ == null);
        peopleQ = new List<GameObject>();

        cuemanager = FindObjectOfType<QueueManager>();

        StartCoroutine("PersonSpawner");
    }

    public GameObject GeneratePerson()
    {
        GameObject person = new GameObject();
        Stats stats = person.AddComponent<Stats>();
        SpriteRenderer sr = person.AddComponent<SpriteRenderer>();
        Person ps = person.AddComponent<Person>();
        stats.dogBreed = Stats.breed.Human;
        ps.transform.name = "Person";

        gm.audioSrcs[2].Play();
        

        peopleQ.Add(person);
        updateQPos();
        //cuemanager.AddToQueue();

        // IF the count of the queue is one that means the VERY next person has to be show on the UI
  
        return person;
    }

    //public GameObject spawnPerson()
    //{
    //    GameObject person = GeneratePerson();
    //    GameObject ourPerson = Instantiate(person);
    //    peopleQ.Enqueue(ourPerson);
    //    return ourPerson;
    //}

    public static void updateQPos()
    {
        for (int i =0; i < peopleQ.Count; i++)
        {
            // peopleQ[i].GetComponent<SpriteRenderer>().sortingOrder = -1 * i;
        }
    }

    IEnumerator PersonSpawner()
    {
        while (true)
        { 
            if(peopleQ.Count >= maxPeopleQLength)
            { spawningPeople = false;
            }else
            { spawningPeople = true; }

            if(spawningPeople)
            {
                //remove the first immediate spawned person so that it lines up with the queue animation
                if (isFirstSpawn)
                {
                    //do nothing
                    isFirstSpawn = false;
                }
                else
                {
                    GeneratePerson();

                }

                yield return new WaitForSeconds(gm.DetermineWaitTime());
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
