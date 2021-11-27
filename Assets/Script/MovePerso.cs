using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour
{
    [SerializeField] private float vitesseMouvement = 20.0f;

    [SerializeField] private float impulsionSaut = 30.0f;
    [SerializeField] private float gravite = 0.2f;
    [SerializeField] private GameObject _force;
    [SerializeField] private Camera _vision;
    [SerializeField] private GameObject _tortue;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    [SerializeField] private float rotation = 360;



    [SerializeField] private Transform _currentGameObject;

    public AudioSource _marche;

    private GameManager _gameManager;
    private GameObject _laTortue;

    private Vector3 directionsMouvement;

    private Vector3 velocity;



    Animator animator;
    CharacterController controller;
    // Start is called before the first frame update
    void Awake()
    {

        _marche = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        ApparaitreTortue();
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

        DoInput(turnAxis);

        animator.SetBool("enCourse", moveAxis != 0); //permet de changer l'animation du personnage vers la course en verifiant la vitesse du personnage en changeant le bool

        _force.transform.localScale = new Vector3(2f * vitesseMouvement, 2f * vitesseMouvement, 2f * vitesseMouvement); //Change la taille du champ de force selon la vitesse du personnage

        if (transform.position.y < -100f)
        {
            RespawnJoueur();
            _gameManager.SubirDegats(25f);
        }

        DeplacementDeLaTortue();
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

    /// <summary>
    /// Fait apparaitre et fait deplacer la tortue (a modifier eventuellement)
    /// </summary>
    private void ApparaitreTortue()
    {
        _laTortue = Instantiate(_tortue, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    /// <summary>
    /// Fait en sorte que la tortue suive le joueur
    /// </summary>
    private void DeplacementDeLaTortue()
    {
        _laTortue.transform.position = Vector3.MoveTowards(_laTortue.transform.position, transform.position, 7 * Time.deltaTime);
        _laTortue.transform.rotation = transform.rotation;
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

        switch (movez)
        {
            case -1:
                transform.localScale = new Vector3(1, 1, -1);
                break;

            case 1:
                transform.localScale = new Vector3(1, 1, 1);
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
