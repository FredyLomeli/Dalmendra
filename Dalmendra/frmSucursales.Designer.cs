namespace Dalmendra
{
    partial class frmSucursales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSucursales));
            this.dgvSucursales = new System.Windows.Forms.DataGridView();
            this.lblNombreSucursal = new System.Windows.Forms.Label();
            this.txtNombreSucursal = new System.Windows.Forms.TextBox();
            this.tspSucursales = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCerrar = new System.Windows.Forms.ToolStripButton();
            this.gpbSucursales = new System.Windows.Forms.GroupBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblIDE = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.lblbd = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.cdgColor = new System.Windows.Forms.ColorDialog();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblSetColor = new System.Windows.Forms.Label();
            this.pbxColorSuc = new System.Windows.Forms.PictureBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsbEditar = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbdelete = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelar = new System.Windows.Forms.ToolStripButton();
            this.tsbTest = new System.Windows.Forms.ToolStripButton();
            this.tsbOrdenar = new System.Windows.Forms.ToolStripButton();
            this.tsbInicio = new System.Windows.Forms.ToolStripButton();
            this.tsbSubir = new System.Windows.Forms.ToolStripButton();
            this.tsbBajar = new System.Windows.Forms.ToolStripButton();
            this.tsbFinal = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSucursales)).BeginInit();
            this.tspSucursales.SuspendLayout();
            this.gpbSucursales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColorSuc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSucursales
            // 
            this.dgvSucursales.AllowDrop = true;
            this.dgvSucursales.AllowUserToAddRows = false;
            this.dgvSucursales.AllowUserToDeleteRows = false;
            this.dgvSucursales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSucursales.Location = new System.Drawing.Point(156, 19);
            this.dgvSucursales.MultiSelect = false;
            this.dgvSucursales.Name = "dgvSucursales";
            this.dgvSucursales.ReadOnly = true;
            this.dgvSucursales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSucursales.Size = new System.Drawing.Size(471, 261);
            this.dgvSucursales.TabIndex = 0;
            this.dgvSucursales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSucursales_CellClick);
            this.dgvSucursales.SelectionChanged += new System.EventHandler(this.dgvSucursales_SelectionChanged);
            // 
            // lblNombreSucursal
            // 
            this.lblNombreSucursal.AutoSize = true;
            this.lblNombreSucursal.Location = new System.Drawing.Point(9, 40);
            this.lblNombreSucursal.Name = "lblNombreSucursal";
            this.lblNombreSucursal.Size = new System.Drawing.Size(88, 13);
            this.lblNombreSucursal.TabIndex = 1;
            this.lblNombreSucursal.Text = "Nombre Sucursal";
            // 
            // txtNombreSucursal
            // 
            this.txtNombreSucursal.Enabled = false;
            this.txtNombreSucursal.Location = new System.Drawing.Point(9, 56);
            this.txtNombreSucursal.Name = "txtNombreSucursal";
            this.txtNombreSucursal.Size = new System.Drawing.Size(139, 20);
            this.txtNombreSucursal.TabIndex = 2;
            // 
            // tspSucursales
            // 
            this.tspSucursales.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.tsbEditar,
            this.tsbGuardar,
            this.tsbdelete,
            this.tsbCancelar,
            this.toolStripSeparator1,
            this.tsbTest,
            this.toolStripSeparator2,
            this.tsbOrdenar,
            this.tsbInicio,
            this.tsbSubir,
            this.tsbBajar,
            this.tsbFinal,
            this.toolStripSeparator3,
            this.tsbCerrar});
            this.tspSucursales.Location = new System.Drawing.Point(0, 0);
            this.tspSucursales.Name = "tspSucursales";
            this.tspSucursales.Size = new System.Drawing.Size(666, 25);
            this.tspSucursales.TabIndex = 3;
            this.tspSucursales.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCerrar
            // 
            this.tsbCerrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCerrar.Name = "tsbCerrar";
            this.tsbCerrar.Size = new System.Drawing.Size(43, 22);
            this.tsbCerrar.Text = "Cerrar";
            this.tsbCerrar.Click += new System.EventHandler(this.tsbCerrar_Click);
            // 
            // gpbSucursales
            // 
            this.gpbSucursales.Controls.Add(this.lblSetColor);
            this.gpbSucursales.Controls.Add(this.pbxColorSuc);
            this.gpbSucursales.Controls.Add(this.lblColor);
            this.gpbSucursales.Controls.Add(this.btnColor);
            this.gpbSucursales.Controls.Add(this.lblID);
            this.gpbSucursales.Controls.Add(this.lblIDE);
            this.gpbSucursales.Controls.Add(this.txtContraseña);
            this.gpbSucursales.Controls.Add(this.lblContraseña);
            this.gpbSucursales.Controls.Add(this.txtUsuario);
            this.gpbSucursales.Controls.Add(this.lblUsuario);
            this.gpbSucursales.Controls.Add(this.txtDB);
            this.gpbSucursales.Controls.Add(this.lblbd);
            this.gpbSucursales.Controls.Add(this.txtServidor);
            this.gpbSucursales.Controls.Add(this.lblServidor);
            this.gpbSucursales.Controls.Add(this.dgvSucursales);
            this.gpbSucursales.Controls.Add(this.txtNombreSucursal);
            this.gpbSucursales.Controls.Add(this.lblNombreSucursal);
            this.gpbSucursales.Location = new System.Drawing.Point(12, 28);
            this.gpbSucursales.Name = "gpbSucursales";
            this.gpbSucursales.Size = new System.Drawing.Size(642, 294);
            this.gpbSucursales.TabIndex = 4;
            this.gpbSucursales.TabStop = false;
            this.gpbSucursales.Text = "Sucursales";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(36, 19);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 13);
            this.lblID.TabIndex = 12;
            // 
            // lblIDE
            // 
            this.lblIDE.AutoSize = true;
            this.lblIDE.Location = new System.Drawing.Point(9, 19);
            this.lblIDE.Name = "lblIDE";
            this.lblIDE.Size = new System.Drawing.Size(21, 13);
            this.lblIDE.TabIndex = 11;
            this.lblIDE.Text = "ID:";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Enabled = false;
            this.txtContraseña.Location = new System.Drawing.Point(9, 218);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.Size = new System.Drawing.Size(139, 20);
            this.txtContraseña.TabIndex = 10;
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Location = new System.Drawing.Point(9, 202);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(61, 13);
            this.lblContraseña.TabIndex = 9;
            this.lblContraseña.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(9, 176);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(139, 20);
            this.txtUsuario.TabIndex = 8;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(9, 160);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 7;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtDB
            // 
            this.txtDB.Enabled = false;
            this.txtDB.Location = new System.Drawing.Point(9, 135);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(139, 20);
            this.txtDB.TabIndex = 6;
            // 
            // lblbd
            // 
            this.lblbd.AutoSize = true;
            this.lblbd.Location = new System.Drawing.Point(9, 119);
            this.lblbd.Name = "lblbd";
            this.lblbd.Size = new System.Drawing.Size(75, 13);
            this.lblbd.TabIndex = 5;
            this.lblbd.Text = "Base de datos";
            // 
            // txtServidor
            // 
            this.txtServidor.Enabled = false;
            this.txtServidor.Location = new System.Drawing.Point(9, 96);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(139, 20);
            this.txtServidor.TabIndex = 4;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(9, 80);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(46, 13);
            this.lblServidor.TabIndex = 3;
            this.lblServidor.Text = "Servidor";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(10, 242);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(93, 13);
            this.lblColor.TabIndex = 14;
            this.lblColor.Text = "Color de Sucursal:";
            // 
            // lblSetColor
            // 
            this.lblSetColor.AutoSize = true;
            this.lblSetColor.Location = new System.Drawing.Point(61, 18);
            this.lblSetColor.Name = "lblSetColor";
            this.lblSetColor.Size = new System.Drawing.Size(0, 13);
            this.lblSetColor.TabIndex = 16;
            this.lblSetColor.Visible = false;
            // 
            // pbxColorSuc
            // 
            this.pbxColorSuc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxColorSuc.Location = new System.Drawing.Point(9, 258);
            this.pbxColorSuc.Name = "pbxColorSuc";
            this.pbxColorSuc.Size = new System.Drawing.Size(100, 22);
            this.pbxColorSuc.TabIndex = 15;
            this.pbxColorSuc.TabStop = false;
            // 
            // btnColor
            // 
            this.btnColor.Enabled = false;
            this.btnColor.Image = global::Dalmendra.Properties.Resources.google_wallet___147_1;
            this.btnColor.Location = new System.Drawing.Point(115, 258);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(35, 22);
            this.btnColor.TabIndex = 13;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevo.Image = global::Dalmendra.Properties.Resources.plus_circle___1425_;
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(23, 22);
            this.tsbNuevo.Text = "Nuevo";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // tsbEditar
            // 
            this.tsbEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditar.Enabled = false;
            this.tsbEditar.Image = global::Dalmendra.Properties.Resources.pen___1319_;
            this.tsbEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditar.Name = "tsbEditar";
            this.tsbEditar.Size = new System.Drawing.Size(23, 22);
            this.tsbEditar.Text = "Editar";
            this.tsbEditar.Click += new System.EventHandler(this.tsbEditar_Click);
            // 
            // tsbGuardar
            // 
            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGuardar.Enabled = false;
            this.tsbGuardar.Image = global::Dalmendra.Properties.Resources.save_item___1410_;
            this.tsbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Size = new System.Drawing.Size(23, 22);
            this.tsbGuardar.Text = "Guardar";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);
            // 
            // tsbdelete
            // 
            this.tsbdelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbdelete.Enabled = false;
            this.tsbdelete.Image = global::Dalmendra.Properties.Resources.delete___1487_;
            this.tsbdelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbdelete.Name = "tsbdelete";
            this.tsbdelete.Size = new System.Drawing.Size(23, 22);
            this.tsbdelete.Text = "Eliminar";
            this.tsbdelete.Click += new System.EventHandler(this.tsbdelete_Click);
            // 
            // tsbCancelar
            // 
            this.tsbCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelar.Enabled = false;
            this.tsbCancelar.Image = global::Dalmendra.Properties.Resources.close___1511_;
            this.tsbCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelar.Name = "tsbCancelar";
            this.tsbCancelar.Size = new System.Drawing.Size(23, 22);
            this.tsbCancelar.Text = "Cancelar";
            this.tsbCancelar.Click += new System.EventHandler(this.tsbCancelar_Click);
            // 
            // tsbTest
            // 
            this.tsbTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTest.Enabled = false;
            this.tsbTest.Image = global::Dalmendra.Properties.Resources.charger___696_;
            this.tsbTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTest.Name = "tsbTest";
            this.tsbTest.Size = new System.Drawing.Size(23, 22);
            this.tsbTest.Text = "Probar Conexión";
            this.tsbTest.Click += new System.EventHandler(this.tsbTest_Click);
            // 
            // tsbOrdenar
            // 
            this.tsbOrdenar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOrdenar.Image = global::Dalmendra.Properties.Resources.database_system___1800_;
            this.tsbOrdenar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrdenar.Name = "tsbOrdenar";
            this.tsbOrdenar.Size = new System.Drawing.Size(23, 22);
            this.tsbOrdenar.Text = "Ordenar";
            this.tsbOrdenar.Click += new System.EventHandler(this.tsbOrdenar_Click);
            // 
            // tsbInicio
            // 
            this.tsbInicio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInicio.Enabled = false;
            this.tsbInicio.Image = global::Dalmendra.Properties.Resources.arrow_up_up__272_;
            this.tsbInicio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInicio.Name = "tsbInicio";
            this.tsbInicio.Size = new System.Drawing.Size(23, 22);
            this.tsbInicio.Text = "Enviar al inicio";
            this.tsbInicio.Click += new System.EventHandler(this.tsbInicio_Click);
            // 
            // tsbSubir
            // 
            this.tsbSubir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSubir.Enabled = false;
            this.tsbSubir.Image = global::Dalmendra.Properties.Resources.arrow_up___267_;
            this.tsbSubir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSubir.Name = "tsbSubir";
            this.tsbSubir.Size = new System.Drawing.Size(23, 22);
            this.tsbSubir.Text = "Subir";
            this.tsbSubir.Click += new System.EventHandler(this.tsbSubir_Click);
            // 
            // tsbBajar
            // 
            this.tsbBajar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBajar.Enabled = false;
            this.tsbBajar.Image = global::Dalmendra.Properties.Resources.arrow_down___269_;
            this.tsbBajar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBajar.Name = "tsbBajar";
            this.tsbBajar.Size = new System.Drawing.Size(23, 22);
            this.tsbBajar.Text = "Bajar";
            this.tsbBajar.Click += new System.EventHandler(this.tsbBajar_Click);
            // 
            // tsbFinal
            // 
            this.tsbFinal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFinal.Enabled = false;
            this.tsbFinal.Image = global::Dalmendra.Properties.Resources.arrow_down_down__272_;
            this.tsbFinal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFinal.Name = "tsbFinal";
            this.tsbFinal.Size = new System.Drawing.Size(23, 22);
            this.tsbFinal.Text = "Enviar al final";
            this.tsbFinal.Click += new System.EventHandler(this.tsbFinal_Click);
            // 
            // frmSucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 331);
            this.Controls.Add(this.gpbSucursales);
            this.Controls.Add(this.tspSucursales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSucursales";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sucursales";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSucursales_FormClosed);
            this.Load += new System.EventHandler(this.frmSucursales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSucursales)).EndInit();
            this.tspSucursales.ResumeLayout(false);
            this.tspSucursales.PerformLayout();
            this.gpbSucursales.ResumeLayout(false);
            this.gpbSucursales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxColorSuc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSucursales;
        private System.Windows.Forms.Label lblNombreSucursal;
        private System.Windows.Forms.TextBox txtNombreSucursal;
        private System.Windows.Forms.ToolStrip tspSucursales;
        private System.Windows.Forms.GroupBox gpbSucursales;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label lblbd;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripButton tsbEditar;
        private System.Windows.Forms.ToolStripButton tsbdelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTest;
        private System.Windows.Forms.ToolStripButton tsbCancelar;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCerrar;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblIDE;
        private System.Windows.Forms.ToolStripButton tsbOrdenar;
        private System.Windows.Forms.ToolStripButton tsbSubir;
        private System.Windows.Forms.ToolStripButton tsbBajar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbInicio;
        private System.Windows.Forms.ToolStripButton tsbFinal;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ColorDialog cdgColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.PictureBox pbxColorSuc;
        private System.Windows.Forms.Label lblSetColor;
    }
}