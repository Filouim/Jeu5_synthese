using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour
{
    [SerializeField] private float vitesseMouvement = 20.0f;
    [SerializeField] private float vitesseRotation = 3.0f;
    [SerializeField] private float impulsionSaut = 30.0f;
    [SerializeField] private float gravite = 0.2f;
    [SerializeField] private GameObject _force;
    [SerializeField] private Camera _vision;
    [SerializeField] private GameObject _tortue;

    public AudioSource _saut;
    public AudioSource _marche;

    private GameManager _gameManager;
    private GameObject _laTortue;
    private float vitesseSaut;
    private Vector3 directionsMouvement = Vector3.zero;

    Animator animator;
    CharacterController controller;
    // Start is called before the first frame update
    void Awake()
    {
        _saut = GetComponent<AudioSource>();
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
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * vitesseRotation * Time.deltaTime, 0); //permet de deplacer le personnage vers l'avant a l'aide des fleches haut et bas du clavier en cliquant sur ces derniere
        float vitesse = Input.GetAxis("Vertical") * vitesseMouvement; //permet de changer l'orientation du personnage a l'aide des fleches droite et gauche du clavier en cliquant sur ces dernieres
        if(Input.GetAxis("Horizontal") > 0)
        {
            _marche.Play();
        }
        animator.SetBool("enCourse", vitesse > 0); //permet de changer l'animation du personnage vers la course en verifiant la vitesse du personnage en changeant le bool
        directionsMouvement = new Vector3(0, 0, vitesse); //permet de savoir la direction dans laquelle le personnage se dirige a l'aide de sa vitesse en la "mettant" dans l'axe des z 
        directionsMouvement = transform.TransformDirection(directionsMouvement); //permet d'obtenir la vrai direction vers ou le perso se dirige en changeant les donnees de la variable directions en world space 

        if(Input.GetButton("Jump") && controller.isGrounded){
            
            vitesseSaut = impulsionSaut; //permet au perso de sauter dans le cas au il touche le sol en changeant la valeur en y de vitesseSaut (qui est une variable... qui varie) par impulsionSaut
        } 

        if(Input.GetButton("Jump"))
        {
            _saut.Play();
        }
        
        animator.SetBool("enSaut", !controller.isGrounded && vitesseSaut >- impulsionSaut); //permet au perso de changer d'animation a l'aide de la variable isGrouded ce qui lui permet de verifier si le personnage touche le sol ou non
        directionsMouvement.y += vitesseSaut; //permet au personnage de sauter dans les airs a l'aide de vitesseSaut en donnant cette valeur a la valeur en y de directionMouvement

        if (!controller.isGrounded) vitesseSaut -= gravite; //permet au perso de subir de la gravite a l'aide de isGrounded et de la gravite, en soustrayant la valeur de vitesseSaut par la gravite a chaque frames

        controller.Move(directionsMouvement * Time.deltaTime); //Deplace le personnage selon la direction auxquel il se deplace
        if(vitesse >= 0)
        {
            _vision.fieldOfView = 60 + 4 * vitesse; //change le FOV de la camera selon la vitesse du personnage
        }

        _force.transform.localScale = new Vector3(2f*vitesse, 2f*vitesse, 2f*vitesse); //Change la taille du champ de force selon la vitesse du personnage

        if(transform.position.y < -100f)
        {
            RespawnJoueur();
            _gameManager.BonusMalusOxygene(-25f);
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
        vitesseSaut = 0f; //S'arrange pour que le joueur ne tombe pas a des vitesse halucinantes
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
}
