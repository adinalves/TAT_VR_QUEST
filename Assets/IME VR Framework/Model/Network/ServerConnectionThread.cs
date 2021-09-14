using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using UnityEngine;

public class ServerConnectionThread {

    //ATRIBUTOS
    private TcpListener tcpListener;
    private NetworkListenerInterface networkListener;

    //CONSTRUTOR
    public ServerConnectionThread(TcpListener tcpListener, NetworkListenerInterface networkListener)
    {
        this.tcpListener = tcpListener;
        this.networkListener = networkListener;
    }

    //MÉTODO DE INICIALIZAÇÃO DA Thread DE RECEPÇÃO DE DADOS
    public void Start()
    {
        //Cliente conectado
        TcpClient client = tcpListener.AcceptTcpClient();
        Debug.Log("Nova conexão estabelecida");

        //Ler o IP do cliente
        string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

        //Receber dados do cliente
        string message;
        using (NetworkStream stream = client.GetStream())
        {
            byte[] data = new byte[1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int numBytesRead;
                while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                {
                    ms.Write(data, 0, numBytesRead);
                }
                message = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
            }
        }
        networkListener.onReceiveMessage(clientIP, message);
        Debug.Log("Dados recebidos de " + clientIP + ":" + Settings.LOCAL_PORT + "\n" + message);

        //Encerrar a conexão com o cliente
        client.Close();
    }
}