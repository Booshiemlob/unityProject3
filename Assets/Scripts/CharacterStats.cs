using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //TODO Expand on the stats contained below, make sure you have some that are class-specific.
    public string myName;
    public Sprite mySprite;
    public int health;
    public int physicalDamage;
    public int magicDamage;
    public int physicalArmour;
    public int magicArmour;
    //purely for outputting total damage in one clean number
    public int totalDamage;



    #region VFX & SFX
    public void ShowDamage()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("StopDamage", 0.2f);
    }

    void StopDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    #endregion
}
