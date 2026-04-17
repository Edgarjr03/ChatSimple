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
        private string miNombre = "Anónimo";

        private List<StreamWriter> clientesConectados = new List<StreamWriter>();

        private readonly object lockClientes = new object();

        private bool esServidor = false;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btn_iniciar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Esta aplicacion " +
                "es el Servidor?", "Sistema", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            try
            {
                if (respuesta == DialogResult.Yes)
                {
                    using (Nombre formNombre = new Nombre())
                    {
                        if (formNombre.ShowDialog() == DialogResult.OK)
                            miNombre = formNombre.NombreElegido;
                        else
                            return;
                    }
                    esServidor |= true;
                    int puerto = int.Parse(txt_Puerto.Text);
                    TcpListener listener = new TcpListener(IPAddress.Any, puerto);
                    listener.Start();


                    
                    rtx_mensajes.AppendText("Servidor iniciado en la direccion y puerto:  " + getIP() + ":" + puerto + "...\r\n");
                    


                    // Bucle infinito: El servidor nunca deja de aceptar clientes
                    while (true)
                    {
                        TcpClient nuevoCliente = await listener.AcceptTcpClientAsync();
                        rtx_mensajes.AppendText("¡Un nuevo cliente se ha unido a la sala!\r\n");

                        // Manejamos cada cliente en una tarea en segundo plano separada
                        _ = ManejarCliente(nuevoCliente);
                    }
                }
                else
                {
                    using (Nombre formNombre = new Nombre())
                    {
                        if (formNombre.ShowDialog() == DialogResult.OK)
                            miNombre = formNombre.NombreElegido;
                        else
                            return;
                    }

                    string ip = txt_IP.Text;
                    int port = int.Parse(txt_Puerto.Text);

                    cliente = new TcpClient();
                    rtx_mensajes.AppendText("Conectando al servidor...\r\n");
                    
                    await cliente.ConnectAsync(ip, port);
                    rtx_mensajes.AppendText("¡Conectado a la sala!\r\n");
                    



                    NetworkStream stream = cliente.GetStream();
                    reader = new StreamReader(stream);
                    writer = new StreamWriter(stream) { AutoFlush = true };

                      await writer.WriteLineAsync($"NOMBRE:{miNombre}");

                    _ = RecibirMensajes();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

        }

        private async Task ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            StreamReader clientReader = new StreamReader(stream);
            StreamWriter clientWriter = new StreamWriter(stream) { AutoFlush = true };

            string nombreCliente = "Desconocido"; // nombre provisional

            lock (lockClientes) { clientesConectados.Add(clientWriter); }

            try
            {
                while (cliente.Connected)
                {
                    string mensajeRecibido = await clientReader.ReadLineAsync();

                    if (mensajeRecibido == null) break;

                    // *** Si el cliente manda su nombre al conectarse ***
                    if (mensajeRecibido.StartsWith("NOMBRE:"))
                    {
                        nombreCliente = mensajeRecibido.Substring(7).Trim();

                        string anuncio = $"*** {nombreCliente} se unió a la sala ***";
                        rtx_mensajes.Invoke((MethodInvoker)delegate {
                            rtx_mensajes.AppendText(anuncio + "\r\n");
                        });
                        DifundirMensaje(anuncio);
                    }
                    else
                    {
                        // Mensaje normal: usamos el nombre que ya guardamos
                        string mensajeFormateado = $"{nombreCliente}: {mensajeRecibido}";

                        rtx_mensajes.Invoke((MethodInvoker)delegate {
                            rtx_mensajes.AppendText(mensajeFormateado + "\r\n");
                        });

                        DifundirMensaje(mensajeFormateado);
                    }
                }
            }
            catch (Exception)
            {
                // cliente desconectado inesperadamente
            }
            finally
            {
                lock (lockClientes) { clientesConectados.Remove(clientWriter); }
                rtx_mensajes.Invoke((MethodInvoker)delegate {
                    rtx_mensajes.AppendText($"*** {nombreCliente} abandonó la sala ***\r\n");
                });
                DifundirMensaje($"*** {nombreCliente} abandonó la sala ***");
                cliente.Close();
            }
        }

        private async void DifundirMensaje(string mensaje)
        {
            List<StreamWriter> copiaClientes;

            // Hacemos una copia rápida de la lista por seguridad
            lock (lockClientes) { copiaClientes = new List<StreamWriter>(clientesConectados); }

            foreach (var clientWriter in copiaClientes)
            {
                try
                {
                    await clientWriter.WriteLineAsync(mensaje);
                }
                catch
                {

                }
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
        private void ConfigurarStreams()
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
                        rtx_mensajes.Invoke((MethodInvoker)delegate {
                            rtx_mensajes.AppendText(mensajeRecibido + "\r\n");
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

        private async void btn_enviar_Click(object sender, EventArgs e)
        {
            string mensaje = txt_Mensaje.Text;
            if (string.IsNullOrWhiteSpace(mensaje)) return;

            try
            {
                if (esServidor)
                {
                    string mensajeFormateado = $"{miNombre}: {mensaje}";
                    rtx_mensajes.AppendText(mensajeFormateado + "\r\n");
                    DifundirMensaje(mensajeFormateado);
                }
                else if (cliente != null && cliente.Connected)
                {
                    // Mandamos solo el texto; el servidor le pone el nombre
                    await writer.WriteLineAsync(mensaje);
                    
                }

                txt_Mensaje.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar: " + ex.Message);
            }
        }
    }
}
