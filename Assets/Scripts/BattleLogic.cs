using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    //TODO Change the following variables to arrays or lists
    public List<CharacterStats> heroes = new List<CharacterStats>();

    public List<CharacterStats> monsters = new List<CharacterStats>();

    //TODO Use the follwing spawn points as a reference point for where to spawn heroes and monsters respectively
    public Transform[] spawnPoints = new Transform[6];

    /* To output any text to the screen, simply call the "Output Text" function on
     * the script referenced below, and pass a string to it, it will write the string
     * to the screen over time.
     */
    public WriteText ouputLog;

    //TODO Create more prefabs below, to represent more classes/monsters that may be spawned
    public GameObject[] heroSP;

    public GameObject[] enemySP;

    //Basic SFX for game events
    public AudioClip hurt, atack;

    private void Start()
    {
        //This will call the SpawnIn function when the game starts (currently does nothing)
        SpawnIn();

        //This will repeat the Attack function once every four seconds indefinitely
        InvokeRepeating("Attack", 4, 4);

        //An example of how to write a string to the screen"
        //ouputLog.OutputText("A " + monsters[1].myName + " approaches!");
        heroes[0].physicalArmour = 0;
        monsters[1].physicalDamage = 0;
    }

    private void SpawnIn()
    {
        //TODO Write your code to spawn in the prefabs, you will need to use arrays/lists and loops to accomplish this.
        
		//Commemnted out because this is for random and I wanted to spawn specific units rather than randomly from an array of enemies and heroes.
		/*for (int heroSpawn = 0; heroSpawn < 3; heroSpawn++)
        {
            GameObject spawn = Instantiate(heroSP[Random.Range(0, 3)], spawnPoints[heroSpawn]) as GameObject;
        }

        for (int enemySpawn = 3; enemySpawn < 6; enemySpawn++)
        {
            GameObject spawn = Instantiate(enemySP[Random.Range(0, 3)], spawnPoints[enemySpawn]) as GameObject;
        }*/

        for (int i = 0; i < 3; i++)
        {
            GameObject heroSpawn = Instantiate(heroSP[i], spawnPoints[i]) as GameObject;
            GameObject monsterSpawn = Instantiate(enemySP[i], spawnPoints[i + 3]) as GameObject;

            heroes[i] = heroSpawn.GetComponent<CharacterStats>();
            monsters[i] = monsterSpawn.GetComponent<CharacterStats>();
        }
    }

    private void Attack()
    {
        //TODO Rewrite the code below to work for three heroes & three monsters (choosing one per side each round)

        /* The following code serves as an example of combat, but it is far too simplistic and does not meet all
         * the requirements, you will need to modify this heavily based on the system you want to implement.
         */

        int numbo = Random.Range(0, 2);
        string log = null;

        //Hero or monster hits based on a flat 50% chance

        //actually modifies damage value
        //heroes[0].health -= monsters[1].damage;

        //monsters attack
        if (numbo == 0)
        {
            int i = Random.Range(0, monsters.Count);         

            int heroToAttack = Random.Range(0, heroes.Count);
            while (!heroes[heroToAttack].gameObject.activeSelf)
            {
                heroToAttack = Random.Range(0, heroes.Count);
            }

            //damage calculation
            int physDmg = monsters[i].physicalDamage - heroes[heroToAttack].physicalArmour;
            if (physDmg < 0) physDmg = 0;
            int magDmg = monsters[i].physicalDamage - heroes[heroToAttack].physicalArmour;
            if (magDmg < 0) magDmg = 0;

            monsters[i].totalDamage = physDmg + magDmg;

            //reduce armors
            heroes[heroToAttack].physicalArmour -= monsters[i].physicalDamage;
            if (heroes[heroToAttack].physicalArmour < 0) heroes[heroToAttack].physicalArmour = 0;
            heroes[heroToAttack].magicArmour -= monsters[i].magicDamage;
            if (heroes[heroToAttack].magicArmour < 0) heroes[heroToAttack].magicArmour = 0;

            //reduce hp of hero
			heroes[heroToAttack].ShowDamage();
            heroes[heroToAttack].health -= monsters[i].totalDamage;
            log = "The " + monsters[i].myName + " hits the " + heroes[heroToAttack].myName + " for " +
                monsters[i].totalDamage + " damage! It has " + heroes[heroToAttack].health + " HP remaining";
        }
        //heroes attack
        else
        {
            int i = Random.Range(0, heroes.Count);          
            int monsterToAttack = Random.Range(0, monsters.Count);
           

            //damage calculation
            int physDmg = heroes[i].physicalDamage - monsters[monsterToAttack].physicalArmour;
            if (physDmg < 0) physDmg = 0;
            int magDmg = heroes[i].magicDamage - monsters[monsterToAttack].magicArmour;
            if (magDmg < 0) magDmg = 0;

            heroes[i].totalDamage = physDmg + magDmg;

            //reduce armor
            monsters[monsterToAttack].physicalArmour -= heroes[i].physicalDamage;
            if (monsters[monsterToAttack].physicalArmour < 0) monsters[monsterToAttack].physicalArmour = 0;
            monsters[monsterToAttack].magicArmour -= heroes[i].magicDamage;
            if (monsters[monsterToAttack].magicArmour < 0) monsters[monsterToAttack].magicArmour = 0;

            //reduces hp of monster
            monsters[monsterToAttack].ShowDamage();
            monsters[monsterToAttack].health -= heroes[i].totalDamage;
            log = "The " + heroes[i].myName + " hits the " + monsters[monsterToAttack].myName + " for " +
                heroes[i].totalDamage + " damage! It has " + monsters[monsterToAttack].health + " HP remaining";
        }

        bool allDead = true;
        for (int i = 0; i < monsters.Count; i++)
        {
            CharacterStats currentMonster = monsters[i];
            if (monsters[i].health <= 0)
            {
                log = "The " + currentMonster.myName + " has been defeated!";
                //monsters[i].gameObject.SetActive(false);
                monsters.Remove(currentMonster);
                Destroy(currentMonster.gameObject);
            }
            else
            {
                allDead = false;
            }
            
        }

        if (allDead)
        {
            log = "Victory! The lair of monsters has been cleansed!";
            CancelInvoke();
        }

        

        allDead = true;
        for (int i = 0; i < heroes.Count; i++)
        {
            CharacterStats currentHero = heroes[i];
            if (heroes[i].health <= 0)
            {

                heroes.Remove(currentHero);
                Destroy(currentHero.gameObject);
                log = "The " + currentHero.myName + " has fallen!";
               
                    
            }
            else
            {
                allDead = false;
            }
          
        }

        if (allDead)
        {
            log = "Defeat! All the heroes have vanquished!";
            CancelInvoke();
        }

        //Writes the assigned string to the screen
        ouputLog.OutputText(log);
    }
}