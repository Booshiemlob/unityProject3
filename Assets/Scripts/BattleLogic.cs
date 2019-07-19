using System.Collections;
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
    public GameObject[] heroSP = new GameObject[4];
    public GameObject[] enemySP = new GameObject[4];

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

    void SpawnIn()
    {
        //TODO Write your code to spawn in the prefabs, you will need to use arrays/lists and loops to accomplish this. 
        for (int heroSpawn = 0; heroSpawn < 3; heroSpawn++)
        {
            GameObject spawn = Instantiate(heroSP[Random.Range(0, 3)], spawnPoints[heroSpawn]) as GameObject;
        }

        for (int enemySpawn = 3; enemySpawn < 6; enemySpawn++)
        {
            GameObject spawn = Instantiate(enemySP[Random.Range(0, 3)], spawnPoints[enemySpawn]) as GameObject;
        }
        
    }

    void Attack()
    {

        //TODO Rewrite the code below to work for three heroes & three monsters (choosing one per side each round)

        /* The following code serves as an example of combat, but it is far too simplistic and does not meet all
         * the requirements, you will need to modify this heavily based on the system you want to implement.
         */

            int numbo = Random.Range(0, 1);
            string log = null;

        //Hero or monster hits based on a flat 50% chance
        

            //actually modifies damage value
            //heroes[0].health -= monsters[1].damage;

            heroes[0].physicalArmour -= monsters[1].physicalDamage;
            heroes[0].magicArmour -= monsters[1].magicDamage;
            monsters[1].physicalArmour -= heroes[0].physicalDamage;
            monsters[1].magicArmour -= heroes[0].magicDamage;

            monsters[1].totalDamage = (heroes[0].physicalArmour -= monsters[1].physicalDamage) + (heroes[0].magicArmour -= monsters[1].magicDamage);
            heroes[0].totalDamage = (monsters[1].physicalArmour -= heroes[0].physicalDamage) + (monsters[1].magicArmour -= heroes[0].magicDamage);

        
        //determines who attacks first
        if (numbo == 0) { 
        //hero start
        if (heroes[0].physicalArmour == 0)
        {
            heroes[0].health -= monsters[1].physicalDamage;
        }

        if (heroes[0].magicArmour == 0)
        {
            heroes[0].health -= monsters[1].magicDamage;
        }
        //HERO runs function controlling SFX and VFX
        heroes[0].ShowDamage();

        //HERO writes the result to the output string
        log = "The " + monsters[1].myName + " hits the " + heroes[0].myName + " for " + monsters[1].totalDamage + " damage! It has " + heroes[0].health + " HP remaining";
        }
        else { 
        //monster start
        if (monsters[1].physicalArmour == 0)
        {
            monsters[1].health -= heroes[0].physicalDamage;
        }

        if (monsters[1].magicArmour == 0)
        {
            monsters[1].health -= heroes[0].magicDamage;
        }
        //MONSTER runs function controlling SFX and VFX
        monsters[1].ShowDamage();

        //MONSTER writes the result to the output string
        log = "The " + heroes[0].myName + " hits the " + monsters[1].myName + " for " + monsters[1].totalDamage + " damage! It has " + monsters[1].health + " HP remaining";

        }

        //These end the game if either character's hp drops below 0
        if (monsters[1].health <= 0)
        {
            Destroy(monsters[1].gameObject);

            log = "Victory! The " + monsters[1].myName + " has been defeated!";

            //This must be called when combat finishes.
            CancelInvoke();
        }
        else if (heroes[0].health <= 0)
        {
            Destroy(heroes[0].gameObject);

            log = "Defeat! The " + heroes[0].myName + " has been defeated!";

            //This must be called when combat finishes.
            CancelInvoke();
        }

        //Writes the assigned string to the screen
        ouputLog.OutputText(log);
    }
}
