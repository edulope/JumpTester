using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int max;
    public int current;

    public PlayerController player;

    public float invincibilityCoolDown;
    private float invincibilityCoolDownCounter;

    private bool rendererEnabled;
    public Renderer[] modelRenderer;
    public float flashLength;
    private float flashCounter;

    private bool isRespawning;
    public Vector3 respawnPoint;
    public float respawnLength;

    public TMP_Text healthText;

    public GameObject pontoDebug;

    // Start is called before the first frame update
    void Start()
    {
        current = max;
        atualizaVidas();
        player = FindObjectOfType<PlayerController>();
        modelRenderer = player.GetComponentsInChildren<Renderer>();

        respawnPoint = player.transform.position;
        isRespawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCoolDownCounter > 0){
            invincibilityCoolDownCounter = invincibilityCoolDownCounter - Time.deltaTime;

            flashCounter = flashCounter - Time.deltaTime;
            if(flashCounter <= 0){
                rendererEnabled =  !rendererEnabled;
                flashCounter =  flashLength;
            }
            
            if(invincibilityCoolDownCounter <= 0){
                rendererEnabled = true;
            }

            foreach(Renderer r in modelRenderer)
                r.enabled = rendererEnabled;

        }
         if(Input.GetButtonDown("Fire2")){
              //StartCoroutine("RespawnCo", pontoDebug.transform.position);
            }
    }

    public void Hurt(int damage, Vector3 hitDirection){
        if(invincibilityCoolDownCounter <= 0){
            current = current - damage;
            atualizaVidas();
            if(current <= 0){
                Respawn();
            }
            else{
                player.Knockback(hitDirection);

                invincibilityCoolDownCounter = invincibilityCoolDown;

                rendererEnabled = false;

                flashCounter =  flashLength;
            }
        }  
    }

    private void atualizaVidas(){
        string vidas = "Vidas:";
        for(int i = 0; i < current; i++){
            vidas = vidas + "O";
        }
        healthText.text = vidas;
    }

    public void HurtZone(int damage){
        if(!isRespawning){
            current = current - damage;
            atualizaVidas();
            if(current <= 0){
                Respawn();
            }
            else StartCoroutine("RespawnCo", player.lastGroundPosition);
        }
    }

    public void Respawn(){        
        if(!isRespawning){
            current = max;
            atualizaVidas();
            StartCoroutine("RespawnCo", respawnPoint);
        }
    }

    public IEnumerator RespawnCo(Vector3 position){

        isRespawning = true;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;
        player.gameObject.SetActive(true);

        CharacterController charController  = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = position;
        charController.enabled = true;

        invincibilityCoolDownCounter = invincibilityCoolDown;

        rendererEnabled = false;

        flashCounter = flashLength;

        player.Stun();
    }

    public void changeCheckPoint(Vector3 newCheckPoint){
        respawnPoint = newCheckPoint;
    }

    public void Heal(int recovery){
        current = current + recovery;
        if(current>max) current = max;
        atualizaVidas();
    }
}
