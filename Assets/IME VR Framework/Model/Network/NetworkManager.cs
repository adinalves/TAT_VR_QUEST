using System.Net;
using System.Net.Sockets;
using UnityEngine;

/*
 * OBJETO QUE GERENCIA A RECEPÇÃO E O ENVIO DE DADOS VIA CONEXÃO TCP
 */
public class NetworkManager : MonoBehaviour {

    //REFERÊNCIA PÚBLICA AO CONTROLADOR DA CENA
    public GameObject sceneControllerReference;

    //VARIÁVEIS DE CONTROLE
    //Thread do servidor TCP
    private ServerThread serverThread;
    //Listener das ações relacionadas à rede
    private static NetworkListenerInterface networkListener;

    //MÉTODOS DO CICLO DE VIDA DO GameObject ASSOCIADO PARA A MANIPULAÇÃO DO SERVIDOR
    void Start()
    {
        //Iniciar listener
        networkListener = sceneControllerReference.GetComponent<NetworkListenerInterface>();

        //Iniciar a Thread do servidor TCP em segundo plano
        Application.runInBackground = true;
        serverThread = new ServerThread(networkListener, getIPAddress());
        serverThread.Start();
    }

    void OnDisable()
    {
        //Parar a Thread do servidor TCP
        serverThread.Stop();
    }

    //MÉTODO ESTÁTICO PARA ENVIO DE DADOS
    public static void sendMessage(string destinationIP, string message)
    {
        new ClientConnectionThread(destinationIP, message, networkListener).Start();
    }

    //MÉTODO DE LEITURA DO IP DA MÁQUINA HOSPEDEIRA
    public string getIPAddress()
    {
        //Se for a versão a ser embarcada no Terminal do Instrutor, retornar o IP de loopback
        if (Settings.INSTRUCTOR_VERSION)
            return "127.0.0.1";
        //Caso contrário, verificar o IP da máquina
        else
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }

            }
            return localIP;
        }
    }
}