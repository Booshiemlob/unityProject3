using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    //TODO Change the following variables to arrays or lists
    public CharacterStats heroes;
    public CharacterStats monsters;

    //TODO Use the follwing spawn points as a reference point for where to spawn heroes and monsters respectively
    public Transform[] spawnPoints = new Transform[6];

    /* To output any text to the screen, simply call the "Output Text" function on
     * the script referenced below, and pass a string to it, it will write the string
     * to the screen over time.
     */
    public WriteText ouputLog;

    //TODO Create more prefabs below, to represent more classes/monsters that may be spawned
    public GameObject knight, boar;

    //Basic SFX for game events
    public AudioClip hurt, atack;
    
    private void Start()
    {
        //This will call the SpawnIn function when the game starts (currently does nothing)
        SpawnIn();

        //This will repeat the Attack function once every four seconds indefinitely
        InvokeRepeating("Attack", 4, 4);

        //An example of how to write a string to the screen"
        ouputLog.OutputText("A " + monsters.myName + " approaches!");
    }

    void SpawnIn()
    {
        //TODO Write your code to spawn in the prefabs, you will need to use arrays/lists and loops to accomplish this. 
    }

    void Attack()
    {
        //TODO Rewrite the code below to work for three heroes & three monsters (choosing one per side each round)

        /* The following code serves as an example of combat, but it is far too simplistic and does not meet all
         * the requirements, you will need to modify this heavily based on the system you want to implement.
         */

        int simpleRandomChance = Random.Range(0, 2);
        string log = null;

        //Hero or monster hits based on a flat 50% chance
        if(simpleRandomChance == 0)
        {
            //actually modifies damage value
            heroes.health -= monsters.damage;

            //runs function controlling SFX and VFX
            heroes.ShowDamage();

            //writes the result to the output string
            log = "The " + monsters.myName + " hits the " + heroes.myName + " for " + monsters.damage + " damage! It has " + heroes.health + " HP remaining";
        }
        else
        {
            monsters.health -= heroes.damage;

            monsters.ShowDamage();

            log = "The " + heroes.myName + " hits the " + monsters.myName + " for " + heroes.damage + " damage! It has " + monsters.health + " HP remaining";
        }
        
        //These end the game if either character's hp drops below 0
        if (monsters.health <= 0)
        {
            Destroy(monsters.gameObject);

            log = "Victory! The " + monsters.myName + " has been defeated!";

            //This must be called when combat finishes.
            CancelInvoke();
        }
        else if (heroes.health <= 0)
        {
            Destroy(heroes.gameObject);

            log = "Defeat! The " + heroes.myName + " has been defeated!";

            //This must be called when combat finishes.
            CancelInvoke();
        }

        //Writes the assigned string to the screen
        ouputLog.OutputText(log);
    }
}
