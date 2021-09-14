using UnityEngine;
using UnityEngine.UI;

public class CardboardController : MonoBehaviour
{

    //REFERÊNCIA PÚBLICA
    public GameObject sceneControllerReference;
    public GameObject cardboardGaze;
    public GameObject linkTestUI;

    //VARIÁVEL DE CONTROLE
    private SceneControllerInterface sceneController;

    //INICIALIZAR CONTROLADOR DA CENA
    void Start()
    {
        if (!Settings.VERSION_TYPE.Equals(Settings.VersionType.CARDBOARD))
            this.gameObject.transform.parent.gameObject.SetActive(false);
        else
        {
            sceneController = sceneControllerReference.GetComponent<SceneControllerInterface>();
            cardboardGaze.SetActive(sceneController.isCardboardGazeEnabled());
            if (linkTestUI != null)
            {
                linkTestUI.SetActive(sceneController.isCardboardGazeEnabled());
                GraphicRaycaster graphicRaycaster = linkTestUI.GetComponentInChildren<GraphicRaycaster>();
                if (graphicRaycaster != null)
                    graphicRaycaster.enabled = false;
                //GvrPointerGraphicRaycaster gvrPointerGraphicRaycaster = linkTestUI.GetComponentInChildren<GvrPointerGraphicRaycaster>();
                //if (gvrPointerGraphicRaycaster != null)
                //    gvrPointerGraphicRaycaster.enabled = true;
            }
        }
    }

    //GERENCIAR CLIQUES DE BOTÃO
    void Update()
    {
        //Se clicar na tela do dispositivo
        if (!sceneController.isCardboardGazeEnabled() && Input.GetMouseButtonDown(0))
            sceneController.onTouchCardboardScreen();

        //Se clicar no botão voltar do Android, fechar a aplicação
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}