namespace ChatSimple
{
    partial class Nombre
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txt_Nombre = new TextBox();
            btn_Aceptar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(247, 25);
            label1.TabIndex = 0;
            label1.Text = "Ingresa tu nombre de usuario";
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(12, 48);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(460, 31);
            txt_Nombre.TabIndex = 1;
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.Location = new Point(190, 85);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(112, 34);
            btn_Aceptar.TabIndex = 2;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = true;
            btn_Aceptar.Click += btn_Aceptar_Click;
            // 
            // Nombre
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 124);
            Controls.Add(btn_Aceptar);
            Controls.Add(txt_Nombre);
            Controls.Add(label1);
            Name = "Nombre";
            Text = "Nombre";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_Nombre;
        private Button btn_Aceptar;
    }
}