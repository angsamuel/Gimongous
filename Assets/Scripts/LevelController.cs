﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//can manage waypoints
public class LevelController : MonoBehaviour {
    public LevelGenerator levelGenerator;
	public float alertRadius;
	public List<UnitController> enemyControllers;
    public List<string> teams;

    public Hashtable teamTable;

    public PlayerInputController pic;
    [HideInInspector]public Unit playerUnit;
    float leaveLevelDistance = 0;
	void Awake(){
		enemyControllers = new List<UnitController> ();
        teams = new List<string>();
        teamTable = new Hashtable();
        playerUnit = pic.playerUnit;
        
	}
	// Use this for initialization
	void Start () {
        StartCoroutine(TableConstructionRoutine());
    }

	public void AddEnemyController(UnitController u){
		enemyControllers.Add (u);
	}

	public void AlertNearbyEnemies(Vector3 position, string team, Unit target){
		for (int i = 0; i < enemyControllers.Count; i++) {
			if (Vector3.Distance (position, enemyControllers[i].unit.transform.position) < alertRadius) {
                if(enemyControllers[i].unit.team == team && !enemyControllers[i].HasPatrolRoute()){
                    enemyControllers [i].target = target;
                }

			}
		}
	}

	// Update is called once per frame
	void Update () {
	}

    public IEnumerator TableConstructionRoutine()
    {
        yield return new WaitForSeconds(10);
        BuildTeamTables();
    }

    void BuildTeamTables()
    {
        for(int i = 0; i<enemyControllers.Count; i++)
        {
            if (!teams.Contains(enemyControllers[i].unit.team))
            {
                teams.Add(enemyControllers[i].unit.team);
            }
        }

        List<List<Unit>> unitLists = new List<List<Unit>>();

        for(int i = 0; i<teams.Count; i++)
        {
            unitLists.Add(new List<Unit>());
        }

        for(int i = 0; i<enemyControllers.Count; i++)
        {
            for(int j = 0; j<teams.Count; j++)
            {
                if(enemyControllers[i].unit.team != teams[j])
                {
                    unitLists[j].Add(enemyControllers[i].unit);
                }
            }
        }

        for (int i = 0; i < teams.Count; i++)
        {
            teamTable.Add(teams[i],unitLists[i]);
        }

    }

    public Unit GetPlayerUnit(){
        return playerUnit;
    }
}
