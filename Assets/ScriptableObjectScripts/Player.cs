using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject {
    public string Name;
    public float Charisma = 1f;
    public float Intoxication = 1f;
    public float Skill = 1f;
    public float Luck = 1f;

    public void UpdateAttribute(string attribute, float value) {
        attribute = attribute.ToLower();
        switch (attribute) {
            case "charisma+":
                Charisma += value;
                break;
            case "charisma-":
                Charisma -= value;
                break;
            case "intoxication+":
                Intoxication += value;
                break;
            case "intoxication-":
                Intoxication -= value;
                break;
            case "skill+":
                Skill += value;
                break;
            case "skill-":
                Skill -= value;
                break;
            case "luck+":
                Luck += value;
                break;
            case "luck-":
                Luck -= value;
                break;
            default:
                break;
        }
    }
}
