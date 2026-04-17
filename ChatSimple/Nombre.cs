using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChatSimple
{
    public partial class Nombre : Form
    {
        public string NombreElegido { get; private set; } = "Anónimo";
        public Nombre()
        {
            InitializeComponent();
        }
        

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            string nombre = txt_Nombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor ingresa un nombre.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación antes de entrar al chat
            DialogResult confirmacion = MessageBox.Show(
                $"¿Seguro que quieres usar el nombre \"{nombre}\"?",
                "Confirmar nombre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                // Acepta → entra al chat
                NombreElegido = nombre;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // Si elige No → vuelve al formulario para cambiar el nombre
        }
    }
}

