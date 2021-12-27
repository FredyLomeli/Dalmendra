namespace Dalmendra
{
    partial class frmCategorias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCategorias));
            this.gpbSucursales = new System.Windows.Forms.GroupBox();
            this.ckbEstado = new System.Windows.Forms.CheckBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblIDE = new System.Windows.Forms.Label();
            this.txtPalabraClave = new System.Windows.Forms.TextBox();
            this.lblPalabraClave = new System.Windows.Forms.Label();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblNombreCategoria = new System.Windows.Forms.Label();
            this.tspSucursales = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsbEditar = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbdelete = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOrdenar = new System.Windows.Forms.ToolStripButton();
            this.tsbInicio = new System.Windows.Forms.ToolStripButton();
            this.tsbSubir = new System.Windows.Forms.ToolStripButton();
            this.tsbBajar = new System.Windows.Forms.ToolStripButton();
            this.tsbFinal = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCerrar = new System.Windows.Forms.ToolStripButton();
            this.gpbSucursales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.tspSucursales.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbSucursales
            // 
            this.gpbSucursales.Controls.Add(this.ckbEstado);
            this.gpbSucursales.Controls.Add(this.lblID);
            this.gpbSucursales.Controls.Add(this.lblIDE);
            this.gpbSucursales.Controls.Add(this.txtPalabraClave);
            this.gpbSucursales.Controls.Add(this.lblPalabraClave);
            this.gpbSucursales.Controls.Add(this.dgvCategorias);
            this.gpbSucursales.Controls.Add(this.txtDescripcion);
            this.gpbSucursales.Controls.Add(this.lblNombreCategoria);
            this.gpbSucursales.Location = new System.Drawing.Point(12, 29);
            this.gpbSucursales.Name = "gpbSucursales";
            this.gpbSucursales.Size = new System.Drawing.Size(528, 207);
            this.gpbSucursales.TabIndex = 5;
            this.gpbSucursales.TabStop = false;
            this.gpbSucursales.Text = "Categorias";
            // 
            // ckbEstado
            // 
            this.ckbEstado.AutoSize = true;
            this.ckbEstado.Enabled = false;
            this.ckbEstado.Location = new System.Drawing.Point(12, 138);
            this.ckbEstado.Name = "ckbEstado";
            this.ckbEstado.Size = new System.Drawing.Size(56, 17);
            this.ckbEstado.TabIndex = 13;
            this.ckbEstado.Text = "Activo";
            this.ckbEstado.UseVisualStyleBackColor = true;
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
            // txtPalabraClave
            // 
            this.txtPalabraClave.Enabled = false;
            this.txtPalabraClave.Location = new System.Drawing.Point(9, 112);
            this.txtPalabraClave.Name = "txtPalabraClave";
            this.txtPalabraClave.Size = new System.Drawing.Size(139, 20);
            this.txtPalabraClave.TabIndex = 4;
            // 
            // lblPalabraClave
            // 
            this.lblPalabraClave.AutoSize = true;
            this.lblPalabraClave.Location = new System.Drawing.Point(9, 96);
            this.lblPalabraClave.Name = "lblPalabraClave";
            this.lblPalabraClave.Size = new System.Drawing.Size(73, 13);
            this.lblPalabraClave.TabIndex = 3;
            this.lblPalabraClave.Text = "Palabra Clave";
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.AllowUserToResizeColumns = false;
            this.dgvCategorias.AllowUserToResizeRows = false;
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Location = new System.Drawing.Point(156, 19);
            this.dgvCategorias.MultiSelect = false;
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategorias.Size = new System.Drawing.Size(358, 171);
            this.dgvCategorias.TabIndex = 0;
            this.dgvCategorias.SelectionChanged += new System.EventHandler(this.dgvCategorias_SelectionChanged);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(9, 72);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(139, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // lblNombreCategoria
            // 
            this.lblNombreCategoria.AutoSize = true;
            this.lblNombreCategoria.Location = new System.Drawing.Point(9, 56);
            this.lblNombreCategoria.Name = "lblNombreCategoria";
            this.lblNombreCategoria.Size = new System.Drawing.Size(63, 13);
            this.lblNombreCategoria.TabIndex = 1;
            this.lblNombreCategoria.Text = "Descripción";
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
            this.tsbOrdenar,
            this.tsbInicio,
            this.tsbSubir,
            this.tsbBajar,
            this.tsbFinal,
            this.toolStripSeparator2,
            this.tsbCerrar});
            this.tspSucursales.Location = new System.Drawing.Point(0, 0);
            this.tspSucursales.Name = "tspSucursales";
            this.tspSucursales.Size = new System.Drawing.Size(550, 25);
            this.tspSucursales.TabIndex = 14;
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
            // frmCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 243);
            this.Controls.Add(this.tspSucursales);
            this.Controls.Add(this.gpbSucursales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCategorias";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categorias";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCategorias_FormClosed);
            this.Load += new System.EventHandler(this.frmCategorias_Load);
            this.gpbSucursales.ResumeLayout(false);
            this.gpbSucursales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.tspSucursales.ResumeLayout(false);
            this.tspSucursales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbSucursales;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblIDE;
        private System.Windows.Forms.TextBox txtPalabraClave;
        private System.Windows.Forms.Label lblPalabraClave;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblNombreCategoria;
        private System.Windows.Forms.ToolStrip tspSucursales;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripButton tsbEditar;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbdelete;
        private System.Windows.Forms.ToolStripButton tsbCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCerrar;
        private System.Windows.Forms.CheckBox ckbEstado;
        private System.Windows.Forms.ToolStripButton tsbOrdenar;
        private System.Windows.Forms.ToolStripButton tsbInicio;
        private System.Windows.Forms.ToolStripButton tsbSubir;
        private System.Windows.Forms.ToolStripButton tsbBajar;
        private System.Windows.Forms.ToolStripButton tsbFinal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}