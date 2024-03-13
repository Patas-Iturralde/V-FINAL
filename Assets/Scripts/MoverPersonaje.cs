using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverPersonaje : MonoBehaviour
{
    public bool vivo = true;
    public float velocidadMov = 5.0f;
    public float velocidadRot = 200.0f;
    public float x, y;
    private Animator animator;

    public BoxCollider armas;

    public GameObject camaraCapturado;
    public GameObject mensajeCapturado;

    public AudioSource pasos;
    public AudioSource golpe;
    public AudioSource muerte;

    private bool Hactivo;
    private bool Vactivo;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        DesactivarArmas();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo == true)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            transform.Rotate(0, x * Time.deltaTime * velocidadRot, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidadMov);

            animator.SetFloat("VelX", x);
            animator.SetFloat("VelY", y);

            if (Input.GetMouseButtonDown(0))
            {
                golpe.Play();
                //animator.SetBool("Ataque", true);
                animator.Play("Ataque");
            }
            
            if (Input.GetButtonDown("Horizontal"))
            {
                Hactivo = true;
                pasos.Play();    
            }

            if (Input.GetButtonDown("Vertical"))
            {
                Vactivo = true;
                pasos.Play();
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                Hactivo = false;
                if(Vactivo == false)
                {
                    pasos.Pause();
                }
                
            }

            if (Input.GetButtonUp("Vertical"))
            {
                Vactivo = false;
                if (Hactivo == false)
                {
                    pasos.Pause();
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Llave")
        {
            Debug.Log("Tengo la llave");
            GetComponent<Abrirpuerta>().enabled = true;
        }

        if (other.tag == "Salida")
        {
            Debug.Log("Toco la puerta");
            
        }
        if (other.gameObject.tag == "Garra")
        {
            muerte.Play();
            vivo = false;
            // Inicia la corrutina
            StartCoroutine(WaitForAnimationAndDestroy());

            // Reproduce la animación
            animator.Play("Morir");
            animator.SetBool("Morir", true);
            
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

    IEnumerator WaitForAnimationAndDestroy()
    {
        // Espera a que termine la animación
        yield return new WaitForSeconds(2);

        // Destruye el objeto
        Destroy(gameObject);
        camaraCapturado.SetActive(true);
        mensajeCapturado.SetActive(true);
    }

}
