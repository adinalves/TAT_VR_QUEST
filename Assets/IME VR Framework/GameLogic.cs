using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameLogic : MonoBehaviour
{
    //ELEMENTOS DA UI
    public GameObject initialCanvas;
    public InputField nameInputField;
    public GameObject timerCanvas;
    public GameObject NovaAvaliacao;
    public GameObject CanvasRanking;
    public GameObject CanvasResultado;
    public GameObject Canvas;
    public GameObject Weapon;
    public Text timerText;
    public GameObject gunCanvas;
    public GameObject gun;
    public Button BotaoAvaliacao;
    public Button BotaoRanking;
    public Button BotaoBack;
    public Button BotaoNew;
    public Button BotaoDireita;
    public Button BotaoEsquerda;
    public Button BotaoVoltarTelaInicial;
    public Button BotaoNovaAvaliacaoResultado;
    public Button BotaoRankingResultado;

    //OCULUS QUEST

    // public GameObject initialCanvas;
   // public InputField nameInputField;
    public GameObject keyboardCanvas;
    public Text logText;
    private bool isRightTriggerReleased = true;
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";



    public float TimerStart;
    public int mao = 1;

    public GameObject alvo;
    public GameObject arma;
    
    public Text NameResultado;
    public Text TimeResultado;
    public Text AcertosResultado;
    public Text MencaoResultado;
    public Text DispersaoResultado;

    public TextAsset textJSON;

    public Text rankingNome;
    public Text rankingImpactos;
    public Text rankingTempo;
    public Text rankingDispersao;

    
   
   

    [System.Serializable]
    public class Player 
    {
        public string nome;
        public int acertos;
        public string tempo;
        public double dispersao;
    }
    
    [System.Serializable]
    public class PlayerList
    {
       


        public List<Player> player = new List<Player>();
        

    }


   

    public PlayerList myPlayerList = new PlayerList();
    public Player jogador = new Player();
    public string auxNome;
    public string auxImpactos;
    public string auxTempo;
    public string auxRankingDispersao;

    private StreamWriter escritor;
    [SerializeField] private string json;


    public void SerializarJSON()
    {
        
        
        json = JsonUtility.ToJson(myPlayerList);
        escritor = new StreamWriter(Application.dataPath + "/Json/Jogadores.json", false);        
        escritor.Write(json);
        escritor.Close();
    }




    //INICIALIZAÇÃO DA APLICAÇÃO
    void Start()
    {
       

        

       myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);

        Weapon.SetActive(false);
        Canvas.SetActive(false);
        
        //Leitura dos dados persistidos em JSON
       // DAO.getInstance().load();
        Button btnAvaliacao = BotaoAvaliacao.GetComponent<Button>();
		btnAvaliacao.onClick.AddListener(ExibeAvaliacao);
        
        Button btnVoltarTela = BotaoBack.GetComponent<Button>();
		btnVoltarTela.onClick.AddListener(Voltar); 

        Button btnNew = BotaoNew.GetComponent<Button>();
		btnNew.onClick.AddListener(Iniciar);

        Button btnRanking = BotaoRanking.GetComponent<Button>();
		btnRanking.onClick.AddListener(Ranking);

        Button btnDireita = BotaoDireita.GetComponent<Button>();
		btnDireita.onClick.AddListener(Direita);

        
        Button btnEsquerda = BotaoEsquerda.GetComponent<Button>();
		btnEsquerda.onClick.AddListener(Esquerda);

        Button btnVoltar = BotaoVoltarTelaInicial.GetComponent<Button>();
		btnVoltar.onClick.AddListener(VoltarTelaInicial);

        Button btnNovaAvaliacaoResultado = BotaoNovaAvaliacaoResultado.GetComponent<Button>();
		btnNovaAvaliacaoResultado.onClick.AddListener(NovaAvaliacaoResultado);

        Button btnRankingResultado = BotaoRankingResultado.GetComponent<Button>();
		btnRankingResultado.onClick.AddListener(Ranking);


    }

    
    public void onClickNameInputField()
    {
        // overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        // Debug.Log("TESTE ");
         overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //keyboardCanvas.SetActive(true);
       //initialCanvas.SetActive(true);
       
    }





    void NovaAvaliacaoResultado()
    {
        nameInputField.text = "";
        CanvasResultado.SetActive(false);
        NovaAvaliacao.SetActive(true);
        
    }

    void VoltarTelaInicial()
    {
        initialCanvas.SetActive(true);
        CanvasRanking.SetActive(false);
    }

    void Direita() {
        mao = 1; 
        Debug.Log("Mao direita");

    }

     void Esquerda() {
        mao = 0;
        Debug.Log("Mao esquerda");

    }

    void Iniciar() {
        
       
       NameResultado.text = nameInputField.text;

        jogador.nome = nameInputField.text;
        
        AcertosResultado.text = "-";
        MencaoResultado.text = "-";
        DispersaoResultado.text = "-";
        TimerStart = Time.time;
        timerCanvas.SetActive(true);
        
        NovaAvaliacao.SetActive(false);
       
       
        
        Invoke("InvokeAvaliacao", 0.4f);

    }

    void InvokeAvaliacao() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Weapon.SetActive(true);
       // Canvas.SetActive(true);
    }



   void ExibeAvaliacao(){
       
        nameInputField.text = "";
		initialCanvas.SetActive(false);
        NovaAvaliacao.SetActive(true);
	}

     void Ranking(){
       
         auxNome = "";
         auxImpactos = "";
         auxTempo = "";
         auxRankingDispersao = "";

   

        List<Player> aux = new List<Player>();
        aux = myPlayerList.player;

        aux.Sort((x, y) => {
                        int ret = y.acertos.CompareTo(x.acertos);
                        return ret != 0 ? ret : x.dispersao.CompareTo(y.dispersao);
                    });



       for(int i = 0; i < aux.Count; i++)
        {
            auxNome += aux[i].nome + "\n"; 
            auxImpactos += aux[i].acertos +  " (" + mencao(aux[i].acertos) + ")" + "\n"; 
            auxTempo += aux[i].tempo + "\n"; 
            auxRankingDispersao += aux[i].dispersao.ToString("F") + "\n";
        }

        rankingNome.text = auxNome;
        rankingImpactos.text = auxImpactos;
        rankingTempo.text = auxTempo;
        rankingDispersao.text = auxRankingDispersao;

        CanvasResultado.SetActive(false);
		initialCanvas.SetActive(false);
        CanvasRanking.SetActive(true);

	}

    void Voltar() {
        initialCanvas.SetActive(true);
        NovaAvaliacao.SetActive(false);

    }

    //ATUALIZAÇÃO DA APLICAÇÃO A CADA FRAME
    void Update()
    {
        
        //Se clicar com o gatilho direito, mostrar/esconder binóculo
        OVRInput.Update();
        if (isRightTriggerReleased && (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) >= 0.5f))
        {
            isRightTriggerReleased = false;
            logText.text = "APERTOU O GATILHO - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        }
        else
        {
            isRightTriggerReleased = true;
            logText.text = "GATILHO LIVRE - Posição " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        }

        // if(timerCanvas.activeSelf) {

        if(true) {
            
        int minutes = (int) (Time.time - TimerStart)/ 60;
        int seconds = (int) (Time.time - TimerStart) % 60;
        timerText.text = 
            ((minutes < 10) ? "0" + minutes : "" + minutes) + ":" +
            ((seconds < 10) ? "0" + seconds : "" + seconds); 
        }

       

        //     if(arma.GetComponent<Gun>().getBullet()<=0) {
            
        //     Invoke("CallFinalAvaliacao", 5f);

        //   CanvasResultado.SetActive(true);
        //   timerCanvas.SetActive(false);
        //   TimeResultado.text = timerText.text;
        //   Cursor.visible = true;
        //   Cursor.lockState = CursorLockMode.None;
            
        // }

    }

    public void CallFinalAvaliacao(){
    
            
            
            AcertosResultado.text = (alvo.GetComponent<Target>().getHit()).ToString();
           

            int hit = alvo.GetComponent<Target>().getHit();
            MencaoResultado.text = mencao(hit);

            double disp = alvo.GetComponent<Target>().Dispersao();
            DispersaoResultado.text = disp.ToString("F");
            Weapon.SetActive(false);
            Canvas.SetActive(false);
            myPlayerList.player.Add(new Player{nome=jogador.nome, acertos=hit, tempo = timerText.text, dispersao = disp});
            SerializarJSON();
          
            
    }

    
    public void OnClickUpButton()
    {
        gun.transform.Rotate(-1, 0, 0);
    }

    public void OnClickLeftButton()
    {
        gun.transform.Rotate(0, -1, 0);
    }

    public void OnClickRightButton()
    {
        gun.transform.Rotate(0, 1, 0);
    }

    public void OnClickDownButton()
    {
        gun.transform.Rotate(1, 0, 0);
    }

    public void OnClickFireButton()
    {
        Debug.Log("ATIROU!");
    }


    public string mencao(int hits){

        if(hits == 15)
        {
            return "E";
        }
        else if(hits==14 || hits==13){
            return "MB";
        }
        else if(hits<=12 && hits>=9) {
            return "B";
        }
        else if(hits<=6 && hits>=8){
            return "R";
        }
        else
        {
            return "I";
        }

    }


  
    
}


