using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ContactPoint2D[] listContacts = new ContactPoint2D[1];

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetContacts(listContacts);

            if (listContacts[0].normal.y < -0.5f){
                Destroy(gameObject);
            } else  {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.Hurt();
            }  
        }
    }
}

    
