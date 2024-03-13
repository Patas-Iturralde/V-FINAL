using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NavMeshController : MonoBehaviour
{
    public float rangoVision = 50;
    public float rangoFOV = 30;
    private Vector3 jugadorDesdeIA;
    private float distanciaJugador = 0;
    public float angulo = 0;

    public bool veoJugador = false;


    public bool ataque;
    public Transform objetivo;
    private NavMeshAgent agente;
    public Animator animator;

    public BoxCollider armas;

    public AudioSource atacar;
    public AudioSource muerte;

    public bool vivo = true;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        DesactivarArmas();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo == true)
        {
            bool visto = false;
            distanciaJugador = Vector3.SqrMagnitude(transform.position - objetivo.position);
            if (distanciaJugador <= (rangoVision * rangoVision))
            {
                jugadorDesdeIA = objetivo.position - transform.position;
                angulo = Vector3.Angle(transform.forward, jugadorDesdeIA);
                if (angulo <= rangoFOV)
                {
                    visto = true;
                }
            }

            if (distanciaJugador <= (rangoVision * rangoVision) && angulo <= rangoFOV)
            {
                veoJugador = true;
                visto = true;
                if (Vector3.Distance(transform.position, objetivo.transform.position) > 2 && !ataque)
                {
                    agente.destination = objetivo.position;
                }
                else
                {
                    atacar.Play();
                    StartCoroutine(coolDown());
                    animator.Play("Ataque");
                }
            }

            if (veoJugador)
            {
                transform.LookAt(objetivo.position);
            }


            if (visto)
            {
                Debug.Log("Veo Jugador");
            }
            else
            {
                Debug.Log("No veo Jugador");
            }




            
        }
        
    }


    public void ActivarArmas()
    {
        armas.enabled = true;
    }

    public void DesactivarArmas()
    {
        armas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Espada")
        {
            muerte.Play();
            vivo = false;
            // Inicia la corrutina
            Debug.Log("daño a enemigo");
            StartCoroutine(WaitForAnimationAndDestroy());

            // Reproduce la animación

        }
    }

    IEnumerator WaitForAnimationAndDestroy()
    {
        animator.StopPlayback();
        animator.Play("Morir");
        animator.SetBool("Morir", true);
        // Espera a que termine la animación
        yield return new WaitForSeconds(2);

        // Destruye el objeto
        Destroy(gameObject);
    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(2);
    }

}
