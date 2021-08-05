using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    bool estaenelsuelo;
    public SpriteRenderer spriterenderer;
    bool hasmuerto = false;
    public GameObject menumuerte;
    Rigidbody2D fisicas;
    Animator anim;
    public float salto;
    bool llave = false;

    // Start is called before the first frame update
    void Start()
    {
        fisicas = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(llave);
        if (Input.GetKey("right") && !hasmuerto)
        {
            transform.Translate(new Vector2(0.1f, 0));
            spriterenderer.flipX = false;
        }
        if (Input.GetKey("left") && !hasmuerto)
        {
            transform.Translate(new Vector2(-0.1f, 0));
            spriterenderer.flipX = true;
        }
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            anim.SetBool("estaandando", true);
        }
        else
        {
            anim.SetBool("estaandando", false);
        }
        if (Input.GetKey("up") && estaenelsuelo && !hasmuerto)
        {
            fisicas.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
        }
        if (transform.position.y <= -5f)
        {
            hasmuerto = true;
            menumuerte.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo")
        {
            estaenelsuelo = true;
            anim.SetBool("estaenelsuelo", true);
        }
        if (col.gameObject.tag == "CosasMalas")
        {
            hasmuerto = true;
            menumuerte.SetActive(true);
        }
        if (col.gameObject.tag == "Llave")
        {
            llave = true;
            Destroy(col.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo")
        {
            estaenelsuelo = false;
            anim.SetBool("estaenelsuelo", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Puerta")
        {
            if (llave)
            {
                SceneManager.LoadScene("HasGanado");
            }
        }
    }
}
