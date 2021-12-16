using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour
{
    [SerializeField] private float vitesseMouvement = 20.0f;

    [SerializeField] private float impulsionSaut = 30.0f;
    [SerializeField] private float tempsInvincible = 5f;
    [SerializeField] private float gravite = 0.2f;
    [SerializeField] private GameObject _force;
    [SerializeField] private Camera _vision;
    [SerializeField] private GameObject _tortue;
    [SerializeField] private GameObject _boxAttaque;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    [SerializeField] private float rotation = 360;



    [SerializeField] private Transform _currentGameObject;

    public AudioSource _marche;

    private GameManager _gameManager;
    private GameObject _laTortue;



    public bool estInvincible = false;
    private Vector3 directionsMouvement;
    private Vector3 velocity;
    Animator animator;
    CharacterController controller;
    private Vector3 _tailleChamp;
    // Start is called before the first frame update
    void Awake()
    {

        _marche = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveAxis = Input.GetAxisRaw(moveInputAxis);
        float turnAxis = Input.GetAxisRaw(turnInputAxis);

        //Animation d'attaque du joueur, pas tres efficace
        float attaqueBouton = Input.GetAxisRaw("Jump");
        //Bouton pour augmenter la taille du champ de force (momentanement)
        bool boutonLavage = Input.GetKey(KeyCode.E);

        if (attaqueBouton > 0)
        {
            StartCoroutine(BoiteDureeVie());
        }

        if(boutonLavage)
        {
            StartCoroutine(ChampGrossi(boutonLavage));
        }
        else
        {
            StopCoroutine(ChampGrossi(boutonLavage));
        }

        DoInput(turnAxis);

        animator.SetBool("enCourse", moveAxis != 0); //permet de changer l'animation du personnage vers la course en verifiant la vitesse du personnage en changeant le bool

        //Change la taille du champ de force selon la vitesse du personnage

        if (transform.position.y < -100f)
        {
            RespawnJoueur();
            _gameManager.SubirDegats(25f);
        }

        // DeplacementDeLaTortue();
    }

    private IEnumerator BoiteDureeVie()
    {
        animator.SetBool("enAttaque", true);
        _boxAttaque.SetActive(true);
        float t = 0.0f;
        do
        {
            t += Time.deltaTime;
            if(t>=1.0f)
            {
                _boxAttaque.SetActive(false);
                animator.SetBool("enAttaque", false);
            }
            yield return null;
        } while(t <= 1.0f);
        yield return null;
    }

    //Permet d'augmenter la taille du champ de force, ce qui permet de laver plus de tarrain, plus vite
    private IEnumerator ChampGrossi(bool bouton)
    {
        float t = 0.0f;
        do
        {
            t += Time.deltaTime;
            _force.transform.localScale = new Vector3(10.0f * t, 10.0f * t, 10.0f * t);
            _tailleChamp = _force.transform.localScale;
            yield return null;
        } while (t <= 5.0f);
        _force.transform.localScale = new Vector3(0f,0f,0f);
        _tailleChamp = _force.transform.localScale;
        yield return null;
    }

    //Permet de faire reapparaitre le joueur au dessus de l'ile en plein milieu
    private void RespawnJoueur()
    {
        controller.enabled = false;
        transform.position = new Vector3(0f, 12f, 0f);
        ReactiveController();
    }

    //Reactive le CharacterController du personnage
    private void ReactiveController()
    {
        controller.enabled = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Ennemi")
        {
            if (!_gameManager._invincible)
            {
                StartCoroutine(Invincible());
                _gameManager.SubirDegats(10f);
            }
        }
    }

    private IEnumerator Invincible()
    {
        _gameManager.GetInvinvibilite(true);

        yield return new WaitForSeconds(tempsInvincible);

        _gameManager.GetInvinvibilite(false);
    }


    /*Code Felix -> changement de gestion de déplacement de personnage + vider fixedUpdate + flip 180 degrés */

    private void DoInput(float turn)
    {
        Movement();
        Turn(turn);
    }
    private void Movement()
    {

        float movez = Input.GetAxisRaw("Vertical");
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //champ de force ici
        switch (movez)
        {
            case -1:
                transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
                _force.transform.localScale = new Vector3(8f, 8f, 8f);
                break;

            case 1:
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                _force.transform.localScale = new Vector3(8f, 8f, 8f);
                break;

            default:
                _force.transform.localScale = _tailleChamp;
                break;

        }

        directionsMouvement = new Vector3(0, 0, movez);
        directionsMouvement = transform.TransformDirection(directionsMouvement);
        directionsMouvement *= vitesseMouvement;



        controller.Move(directionsMouvement * Time.deltaTime);
        velocity.y += gravite * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Turn(float inp)
    {
        transform.Rotate(0, inp * rotation * Time.deltaTime, 0);
    }
}
