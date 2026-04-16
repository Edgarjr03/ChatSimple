namespace ChatSimple
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txt_IP = new TextBox();
            label2 = new Label();
            txt_Puerto = new TextBox();
            btn_iniciar = new Button();
            rtx_mensajes = new RichTextBox();
            label3 = new Label();
            label4 = new Label();
            txt_Mensaje = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(27, 25);
            label1.TabIndex = 1;
            label1.Text = "IP";
            // 
            // txt_IP
            // 
            txt_IP.Location = new Point(12, 41);
            txt_IP.Name = "txt_IP";
            txt_IP.Size = new Size(267, 31);
            txt_IP.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(285, 13);
            label2.Name = "label2";
            label2.Size = new Size(64, 25);
            label2.TabIndex = 3;
            label2.Text = "Puerto";
            // 
            // txt_Puerto
            // 
            txt_Puerto.Location = new Point(285, 41);
            txt_Puerto.Name = "txt_Puerto";
            txt_Puerto.Size = new Size(125, 31);
            txt_Puerto.TabIndex = 2;
            // 
            // btn_iniciar
            // 
            btn_iniciar.Location = new Point(430, 38);
            btn_iniciar.Name = "btn_iniciar";
            btn_iniciar.Size = new Size(154, 34);
            btn_iniciar.TabIndex = 4;
            btn_iniciar.Text = "Iniciar Server";
            btn_iniciar.UseVisualStyleBackColor = true;
            btn_iniciar.Click += btn_iniciar_Click;
            // 
            // rtx_mensajes
            // 
            rtx_mensajes.Location = new Point(12, 137);
            rtx_mensajes.Name = "rtx_mensajes";
            rtx_mensajes.Size = new Size(572, 205);
            rtx_mensajes.TabIndex = 5;
            rtx_mensajes.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 109);
            label3.Name = "label3";
            label3.Size = new Size(85, 25);
            label3.TabIndex = 6;
            label3.Text = "Mensajes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 361);
            label4.Name = "label4";
            label4.Size = new Size(77, 25);
            label4.TabIndex = 7;
            label4.Text = "Mensaje";
            // 
            // txt_Mensaje
            // 
            txt_Mensaje.Location = new Point(12, 389);
            txt_Mensaje.Name = "txt_Mensaje";
            txt_Mensaje.Size = new Size(454, 31);
            txt_Mensaje.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(472, 386);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 9;
            button1.Text = "Enviar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 450);
            Controls.Add(button1);
            Controls.Add(txt_Mensaje);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(rtx_mensajes);
            Controls.Add(btn_iniciar);
            Controls.Add(label2);
            Controls.Add(txt_Puerto);
            Controls.Add(label1);
            Controls.Add(txt_IP);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_IP;
        private Label label2;
        private TextBox txt_Puerto;
        private Button btn_iniciar;
        private RichTextBox rtx_mensajes;
        private Label label3;
        private Label label4;
        private TextBox txt_Mensaje;
        private Button button1;
    }
}
