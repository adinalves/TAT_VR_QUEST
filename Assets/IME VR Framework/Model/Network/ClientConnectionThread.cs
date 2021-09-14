using System.Threading;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClientConnectionThread {

    //ATRIBUTOS
    private string destinationIP;
    private string message;
    private NetworkListenerInterface networkListener;

    //CONSTRUTOR
    public ClientConnectionThread(string destinationIP, string message, NetworkListenerInterface networkListener)
    {
        this.destinationIP = destinationIP;
        this.message = message;
        this.networkListener = networkListener;
    }

    //MÉTODO DE INICIALIZAÇÃO DA Thread DE ENVIO DE DADOS
    public void Start()
    {
        Thread clientThread = new Thread(RunClientThread);
        clientThread.IsBackground = true;
        clientThread.Start();
    }

    //MÉTODO DE EXECUÇÃO DA Thread DE ENVIO DE DADOS
    private void RunClientThread()
    {
        try
        {
            TcpClient client = new TcpClient(destinationIP, Settings.REMOTE_PORT);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            Debug.Log("Dados enviados para " + destinationIP + ":" + Settings.REMOTE_PORT + "\n" + message);
            client.Close();
            networkListener.onReceiveAck(destinationIP, message);
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
            networkListener.onReceiveNack(destinationIP, message);
        }
    }
}