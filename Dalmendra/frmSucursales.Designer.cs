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
            this.dgvSucursales = new System.Windows.Forms.DataGridView();
            this.lblNombreSucursal = new System.Windows.Forms.Label();
            this.txtNombreSucursal = new System.Windows.Forms.TextBox();
            this.tspSucursales = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsbEditar = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbdelete = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvSucursales)).BeginInit();
            this.tspSucursales.SuspendLayout();
            this.gpbSucursales.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSucursales
            // 
            this.dgvSucursales.AllowUserToAddRows = false;
            this.dgvSucursales.AllowUserToDeleteRows = false;
            this.dgvSucursales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSucursales.Location = new System.Drawing.Point(156, 19);
            this.dgvSucursales.MultiSelect = false;
            this.dgvSucursales.Name = "dgvSucursales";
            this.dgvSucursales.ReadOnly = true;
            this.dgvSucursales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSucursales.Size = new System.Drawing.Size(471, 242);
            this.dgvSucursales.TabIndex = 0;
            this.dgvSucursales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSucursales_CellClick);
            // 
            // lblNombreSucursal
            // 
            this.lblNombreSucursal.AutoSize = true;
            this.lblNombreSucursal.Location = new System.Drawing.Point(9, 56);
            this.lblNombreSucursal.Name = "lblNombreSucursal";
            this.lblNombreSucursal.Size = new System.Drawing.Size(88, 13);
            this.lblNombreSucursal.TabIndex = 1;
            this.lblNombreSucursal.Text = "Nombre Sucursal";
            // 
            // txtNombreSucursal
            // 
            this.txtNombreSucursal.Enabled = false;
            this.txtNombreSucursal.Location = new System.Drawing.Point(9, 72);
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
            this.tsbCerrar});
            this.tspSucursales.Location = new System.Drawing.Point(0, 0);
            this.tspSucursales.Name = "tspSucursales";
            this.tspSucursales.Size = new System.Drawing.Size(662, 25);
            this.tspSucursales.TabIndex = 3;
            this.tspSucursales.Text = "toolStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbTest
            // 
            this.tsbTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTest.Enabled = false;
            this.tsbTest.Image = global::Dalmendra.Properties.Resources.database_system___1800_;
            this.tsbTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTest.Name = "tsbTest";
            this.tsbTest.Size = new System.Drawing.Size(23, 22);
            this.tsbTest.Text = "Probar Conexión";
            this.tsbTest.Click += new System.EventHandler(this.tsbTest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.gpbSucursales.Size = new System.Drawing.Size(638, 275);
            this.gpbSucursales.TabIndex = 4;
            this.gpbSucursales.TabStop = false;
            this.gpbSucursales.Text = "Sucursales";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(35, 31);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 13);
            this.lblID.TabIndex = 12;
            // 
            // lblIDE
            // 
            this.lblIDE.AutoSize = true;
            this.lblIDE.Location = new System.Drawing.Point(9, 31);
            this.lblIDE.Name = "lblIDE";
            this.lblIDE.Size = new System.Drawing.Size(21, 13);
            this.lblIDE.TabIndex = 11;
            this.lblIDE.Text = "ID:";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Enabled = false;
            this.txtContraseña.Location = new System.Drawing.Point(9, 234);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.Size = new System.Drawing.Size(139, 20);
            this.txtContraseña.TabIndex = 10;
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Location = new System.Drawing.Point(9, 218);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(61, 13);
            this.lblContraseña.TabIndex = 9;
            this.lblContraseña.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(9, 192);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(139, 20);
            this.txtUsuario.TabIndex = 8;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(9, 176);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 7;
            this.lblUsuario.Text = "Usuario";
            // 
            // txtDB
            // 
            this.txtDB.Enabled = false;
            this.txtDB.Location = new System.Drawing.Point(9, 151);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(139, 20);
            this.txtDB.TabIndex = 6;
            // 
            // lblbd
            // 
            this.lblbd.AutoSize = true;
            this.lblbd.Location = new System.Drawing.Point(9, 135);
            this.lblbd.Name = "lblbd";
            this.lblbd.Size = new System.Drawing.Size(75, 13);
            this.lblbd.TabIndex = 5;
            this.lblbd.Text = "Base de datos";
            // 
            // txtServidor
            // 
            this.txtServidor.Enabled = false;
            this.txtServidor.Location = new System.Drawing.Point(9, 112);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(139, 20);
            this.txtServidor.TabIndex = 4;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(9, 96);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(46, 13);
            this.lblServidor.TabIndex = 3;
            this.lblServidor.Text = "Servidor";
            // 
            // frmSucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 315);
            this.Controls.Add(this.gpbSucursales);
            this.Controls.Add(this.tspSucursales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
    }
}