using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace ChatSimple
{
    public partial class Form1 : Form
    {
        private TcpClient cliente;
        private StreamReader reader;
        private StreamWriter writer;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_iniciar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Esta aplicacion " +
                "es el servidor?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (respuesta == DialogResult.Yes)
                {
                    int port = int.Parse(txt_Puerto.Text);
                    TcpListener server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    string ip = getIP();
                    rtx_mensajes.AppendText("Servidor iniciado en la IP y puerto: "
                        + ip + ":" + port + "\n");
                    
                    //Esperar a que un cliente se conecte de forma asincrona
                    cliente = await server.AcceptTcpClientAsync();
                    rtx_mensajes.AppendText("Cliente conectado!\n");

                    COnfigurarStreams();
                    _ = RecibirMensajes();


                }
                else
                {
                    string ip = txt_IP.Text;
                    int port = int.Parse(txt_Puerto.Text);

                    cliente = new TcpClient();
                    rtx_mensajes.AppendText("Conectando al servidor... \n");

                    await cliente.ConnectAsync(ip, port);
                    rtx_mensajes.AppendText("Conectado\n");

                    COnfigurarStreams();
                    _ = RecibirMensajes();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void COnfigurarStreams()
        {
            NetworkStream stream = cliente.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        private async Task RecibirMensajes()
        {
            try
            {
                while (cliente != null && cliente.Connected)
                {
                    string mensajeRecibido = await reader.ReadLineAsync();
                    if (mensajeRecibido != null)
                    {
                        rtx_mensajes.Invoke((MethodInvoker)delegate
                        {
                            rtx_mensajes.AppendText("Extraño: " + mensajeRecibido + "\n");
                        });
                    }
                }
            }
            catch (Exception)
            {
                rtx_mensajes.Invoke((MethodInvoker)delegate
                {
                    rtx_mensajes.AppendText("Cliente Desconectado \n");
                });
            }
        }

        private string getIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = "";
            IPHostEntry host = Dns.GetHostEntry(hostName);

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Filtra IPv4
                {
                    myIP = ip.ToString();
                    break;
                }
            }
            return myIP;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (cliente != null && cliente.Connected &&
                !string.IsNullOrWhiteSpace(txt_Mensaje.Text))
            {
                try
                {
                    string mensaje = txt_Mensaje.Text;
                    await writer.WriteLineAsync(mensaje);

                    rtx_mensajes.AppendText("Yo: " + mensaje + "\n");
                    txt_Mensaje.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar mensaje: " + ex.ToString());
                }

            }
            else
                MessageBox.Show("No hay clinetes conectados", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
