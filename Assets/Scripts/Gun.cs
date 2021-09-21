using UnityEngine;


public class Gun : MonoBehaviour {

    public Transform bulletPrefab;

    Animator    animator;
    AudioSource audioSource;

    FirstPersonCamera fpsCam;

    ShotEffects shootEffects;

    Transform shotSpawn;
    Transform shellSpawn;

    string gunName = "Pistol";
    public int bulletCount = 15;
    public GameObject CanvasResultado;

    //public Text logText;
    private bool isRightTriggerReleased = true;
    
    

    private void Awake() {

        animator    = transform.Find(gunName).GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();

        fpsCam     = transform.parent.GetComponent<FirstPersonCamera>();
        shotSpawn  = transform.Find("shotSpawn");
        shellSpawn = transform.Find("shellSpawn");

        shootEffects = GetComponent<ShotEffects>();

    }

    void Start() {
        
    }

    void Update() {
        
        
       

         //Se clicar com o gatilho direito, mostrar/esconder binóculo
        OVRInput.Update();
        if (isRightTriggerReleased && (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.5f))
        {
            isRightTriggerReleased = false;
           // logText.text = "APERTOU O GATILHO - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

             bulletCount--;
            
            animator.Play("Shoot", -1, 0);

            audioSource.Play();

            shootEffects.MuzzleFlash(shotSpawn.position, shotSpawn.rotation);
            shootEffects.Shell(shellSpawn.position, shellSpawn.rotation);

            // implementação do tiro vem aqui
            ShootBullet();
           // ShootRaycast();
        }
        else
        {
            isRightTriggerReleased = true;
            //logText.text = "GATILHO LIVRE - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        }









        if(CanvasResultado.activeSelf)
        {
            
            bulletCount = 15;
        

        }
        
        

    }

    void ShootBullet() {

        Transform bulletObj = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
        Destroy(bulletObj.gameObject, 10f);
        
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.GetForwardDirection(), out hitInfo, Mathf.Infinity, LayerMask.GetMask("hittable"))) {

            
            bulletObj.GetComponent<Bullet>().SetDirection((hitInfo.point - shotSpawn.position).normalized);
            

        } else {
            bulletObj.GetComponent<Bullet>().SetDirection(fpsCam.GetForwardDirection());
        }

    }


    void ShootRaycast() {

        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.GetForwardDirection(), out hitInfo, Mathf.Infinity, LayerMask.GetMask("hittable"))) {

            IShotHit hitted = hitInfo.transform.GetComponent<IShotHit>();
            if(hitted != null) {

                hitted.Hit(fpsCam.GetForwardDirection());

            }
        }
    }





    public int getBullet()
    {
        return bulletCount;
    }

   
   

}
