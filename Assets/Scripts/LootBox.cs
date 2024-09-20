using UnityEngine;

public class LootBox : MonoBehaviour
{
    public float interactRadius = 2f; 
    public HUD hud; 
    public int[] scorePoints = new int[] { 1000, 2000, 3000, 4000, 5000, 6000 };
    private bool isPlayerInRange = false;
    private PlayerStats playerStats;

    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }
    }

    void Update()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius);

        isPlayerInRange = false; 

        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.CompareTag("Player"))
            {
                isPlayerInRange = true;

                
                hud.ShowInteractText("Press 'F' to interact");

                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    UseLootBox(); 
                }
                break;
            }
        }

        
        if (!isPlayerInRange)
        {
            hud.HideInteractText();
        }
    }

    void UseLootBox()
    {
        if (playerStats != null)
        {
            playerStats.GetPoints(scorePoints[Random.Range(0,5)]); 
        }

        Debug.Log("Used and destroyed the loot box");
        hud.HideInteractText();
        Destroy(gameObject); 
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
