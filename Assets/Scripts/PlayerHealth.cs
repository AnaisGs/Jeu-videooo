using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int maxLifePoints = 3;

    public int currentLifePoints = 3; 

    public bool isInvulnerable = false; 

    public float invulnerableTime = 2.25f;

    public float invulnerableFlash = 0.2f; 

    public SpriteRenderer sr; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     {
    sr = GetComponent<SpriteRenderer>(); // Trouve automatiquement le SpriteRenderer
    currentLifePoints = maxLifePoints;
}
    }

    public void Hurt(int damage = 1) {
        if (isInvulnerable)  { 
            return; 
            }

        currentLifePoints = currentLifePoints - damage; 
        if (currentLifePoints <=0) { 
            Debug.Log ("Fin de partie"); 
        } else  {
            StartCoroutine (Invulnerable());
           }  
    }

    IEnumerator Invulnerable() { 
        isInvulnerable = true; 
        Color startColor = sr.color;

        for (float i = 0;i <= invulnerableTime; i += invulnerableFlash) { 

            if (sr.color.a == 1) { 
                sr.color = Color.clear; 
            } else {
                sr.color = startColor; 
            }
            yield return new WaitForSeconds (invulnerableFlash);
         }   
         isInvulnerable = false;
         yield return null; 
    }
}