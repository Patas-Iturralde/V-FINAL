using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Animator animator;

    public AudioSource muerte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Espada")
        {
            muerte.Play();
            // Inicia la corrutina
            Debug.Log("daño a enemigo");
            StartCoroutine(WaitForAnimationAndDestroy());

            // Reproduce la animación
            
        }
    }

    IEnumerator WaitForAnimationAndDestroy()
    {
        animator.Play("Morir");
        animator.SetBool("Morir", true);
        // Espera a que termine la animación
        yield return new WaitForSeconds(2);

        // Destruye el objeto
        Destroy(gameObject);
    }

}
