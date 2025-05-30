namespace Proyeto
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
            txtDestinatario = new TextBox();
            txtFirmante = new TextBox();
            dtpFecha = new DateTimePicker();
            btnGenerarCarta = new Button();
            rtbResultado = new RichTextBox();
            lblDestinatario = new Label();
            lblFirmante = new Label();
            lblFecha = new Label();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtDPI = new TextBox();
            txtTelefono = new TextBox();
            SuspendLayout();
            // 
            // txtDestinatario
            // 
            txtDestinatario.Location = new Point(144, 28);
            txtDestinatario.Name = "txtDestinatario";
            txtDestinatario.Size = new Size(125, 27);
            txtDestinatario.TabIndex = 0;
            // 
            // txtFirmante
            // 
            txtFirmante.Location = new Point(144, 79);
            txtFirmante.Name = "txtFirmante";
            txtFirmante.Size = new Size(125, 27);
            txtFirmante.TabIndex = 1;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(144, 271);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(125, 27);
            dtpFecha.TabIndex = 2;
            // 
            // btnGenerarCarta
            // 
            btnGenerarCarta.Location = new Point(91, 326);
            btnGenerarCarta.Name = "btnGenerarCarta";
            btnGenerarCarta.Size = new Size(94, 29);
            btnGenerarCarta.TabIndex = 4;
            btnGenerarCarta.Text = "Generar Carta";
            btnGenerarCarta.UseVisualStyleBackColor = true;
            btnGenerarCarta.Click += btnGenerarCarta_Click;
            // 
            // rtbResultado
            // 
            rtbResultado.Location = new Point(323, 12);
            rtbResultado.Name = "rtbResultado";
            rtbResultado.ReadOnly = true;
            rtbResultado.Size = new Size(465, 426);
            rtbResultado.TabIndex = 6;
            rtbResultado.Text = "";
            // 
            // lblDestinatario
            // 
            lblDestinatario.AutoSize = true;
            lblDestinatario.Location = new Point(29, 31);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(90, 20);
            lblDestinatario.TabIndex = 7;
            lblDestinatario.Text = "Destinatario";
            // 
            // lblFirmante
            // 
            lblFirmante.AutoSize = true;
            lblFirmante.Location = new Point(42, 82);
            lblFirmante.Name = "lblFirmante";
            lblFirmante.Size = new Size(67, 20);
            lblFirmante.TabIndex = 8;
            lblFirmante.Text = "Firmante";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(46, 271);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(47, 20);
            lblFecha.TabIndex = 9;
            lblFecha.Text = "Fecha";
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(46, 134);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(56, 20);
            lblMotivo.TabIndex = 10;
            lblMotivo.Text = "Motivo";
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(144, 129);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(125, 27);
            txtMotivo.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 228);
            label1.Name = "label1";
            label1.Size = new Size(32, 20);
            label1.TabIndex = 14;
            label1.Text = "DPI";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 176);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 13;
            label2.Text = "Telefono";
            // 
            // txtDPI
            // 
            txtDPI.Location = new Point(144, 223);
            txtDPI.Name = "txtDPI";
            txtDPI.Size = new Size(125, 27);
            txtDPI.TabIndex = 11;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(144, 173);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(125, 27);
            txtTelefono.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(txtDPI);
            Controls.Add(txtTelefono);
            Controls.Add(lblMotivo);
            Controls.Add(lblFecha);
            Controls.Add(lblFirmante);
            Controls.Add(lblDestinatario);
            Controls.Add(rtbResultado);
            Controls.Add(btnGenerarCarta);
            Controls.Add(dtpFecha);
            Controls.Add(txtMotivo);
            Controls.Add(txtFirmante);
            Controls.Add(txtDestinatario);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDestinatario;
        private TextBox txtFirmante;
        private DateTimePicker dtpFecha;
        private Button btnGenerarCarta;
        private RichTextBox rtbResultado;
        private Label lblDestinatario;
        private Label lblFirmante;
        private Label lblFecha;
        private Label lblMotivo;
        private TextBox txtMotivo;
        private Label label1;
        private Label label2;
        private TextBox txtDPI;
        private TextBox txtTelefono;
    }
}
