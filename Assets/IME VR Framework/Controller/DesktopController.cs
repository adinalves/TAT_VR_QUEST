using UnityEngine;
using UnityEngine.UI;

public class DesktopController : MonoBehaviour {

    //REFERÊNCIAS PÚBLICAS
    //public GameObject sceneControllerReference;
    //public GameObject linkTestUI;
    public float movementSpeed = 3.5f;

    //VARIÁVEIS DE CONTROLE
    private SceneControllerInterface sceneController;
    private float x;
    private float y;

    //INICIALIZAR CONTROLADOR DA CENA
    void Start()
    {
        //if (!Settings.VERSION_TYPE.Equals(Settings.VersionType.DESKTOP))
        //    this.gameObject.transform.parent.gameObject.active = false;
        //else
        //{
        //    sceneController = sceneControllerReference.GetComponent<SceneControllerInterface>();
        //    if (linkTestUI != null)
        //    {
        //        linkTestUI.SetActive(sceneController.isDesktopMousePointerEnabled());
        //        GraphicRaycaster graphicRaycaster = linkTestUI.GetComponentInChildren<GraphicRaycaster>();
        //        if (graphicRaycaster != null)
        //            graphicRaycaster.enabled = true;
        //        GvrPointerGraphicRaycaster gvrPointerGraphicRaycaster = linkTestUI.GetComponentInChildren<GvrPointerGraphicRaycaster>();
        //        if (gvrPointerGraphicRaycaster != null)
        //            gvrPointerGraphicRaycaster.enabled = false;
        //    }
        //}
    }

    //GERENCIAR CLIQUES DE BOTÃO
    void Update()
    {
        ////Se clicar com o botão esquerdo do mouse
        //if (!sceneController.isDesktopMousePointerEnabled() && Input.GetMouseButtonDown(0))
        //    sceneController.onClickDesktopMouseLeftButton();

        ////Se clicar com o botão do meio do mouse
        //if (Input.GetMouseButtonDown(2))
        //    sceneController.onClickDesktopMouseScrollButton();

        //Se segurar o botão direito, a câmera responde à movimentação do mouse
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * movementSpeed, -Input.GetAxis("Mouse X") * movementSpeed, 0));
            x = transform.rotation.eulerAngles.x;
            y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(x, y, 0);
        }
    }
}