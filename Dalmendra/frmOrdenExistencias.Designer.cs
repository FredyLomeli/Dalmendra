namespace Dalmendra
{
    partial class frmOrdenExistencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenExistencias));
            this.gpbOrdenarArticulos = new System.Windows.Forms.GroupBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvOrdenExistencias = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCategorias = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSucursales = new System.Windows.Forms.ComboBox();
            this.tspSucursales = new System.Windows.Forms.ToolStrip();
            this.tsbCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOrdenar = new System.Windows.Forms.ToolStripButton();
            this.tsbInicio = new System.Windows.Forms.ToolStripButton();
            this.tsbSubir = new System.Windows.Forms.ToolStripButton();
            this.tsbBajar = new System.Windows.Forms.ToolStripButton();
            this.tsbFinal = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCerrar = new System.Windows.Forms.ToolStripButton();
            this.gpbOrdenarArticulos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenExistencias)).BeginInit();
            this.tspSucursales.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbOrdenarArticulos
            // 
            this.gpbOrdenarArticulos.Controls.Add(this.btnConsultar);
            this.gpbOrdenarArticulos.Controls.Add(this.dgvOrdenExistencias);
            this.gpbOrdenarArticulos.Controls.Add(this.label2);
            this.gpbOrdenarArticulos.Controls.Add(this.cmbCategorias);
            this.gpbOrdenarArticulos.Controls.Add(this.label1);
            this.gpbOrdenarArticulos.Controls.Add(this.cmbSucursales);
            this.gpbOrdenarArticulos.Location = new System.Drawing.Point(13, 28);
            this.gpbOrdenarArticulos.Name = "gpbOrdenarArticulos";
            this.gpbOrdenarArticulos.Size = new System.Drawing.Size(593, 465);
            this.gpbOrdenarArticulos.TabIndex = 0;
            this.gpbOrdenarArticulos.TabStop = false;
            this.gpbOrdenarArticulos.Text = "Ordenar Articulos";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(496, 31);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 5;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgvOrdenExistencias
            // 
            this.dgvOrdenExistencias.AllowUserToAddRows = false;
            this.dgvOrdenExistencias.AllowUserToDeleteRows = false;
            this.dgvOrdenExistencias.AllowUserToResizeColumns = false;
            this.dgvOrdenExistencias.AllowUserToResizeRows = false;
            this.dgvOrdenExistencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenExistencias.Location = new System.Drawing.Point(6, 74);
            this.dgvOrdenExistencias.MultiSelect = false;
            this.dgvOrdenExistencias.Name = "dgvOrdenExistencias";
            this.dgvOrdenExistencias.ReadOnly = true;
            this.dgvOrdenExistencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenExistencias.Size = new System.Drawing.Size(581, 385);
            this.dgvOrdenExistencias.TabIndex = 4;
            this.dgvOrdenExistencias.SelectionChanged += new System.EventHandler(this.dgvOrdenExistencias_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Categoria :";
            // 
            // cmbCategorias
            // 
            this.cmbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategorias.DropDownWidth = 165;
            this.cmbCategorias.FormattingEnabled = true;
            this.cmbCategorias.Location = new System.Drawing.Point(316, 33);
            this.cmbCategorias.Name = "cmbCategorias";
            this.cmbCategorias.Size = new System.Drawing.Size(163, 21);
            this.cmbCategorias.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sucursal :";
            // 
            // cmbSucursales
            // 
            this.cmbSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSucursales.FormattingEnabled = true;
            this.cmbSucursales.Location = new System.Drawing.Point(77, 33);
            this.cmbSucursales.Name = "cmbSucursales";
            this.cmbSucursales.Size = new System.Drawing.Size(165, 21);
            this.cmbSucursales.TabIndex = 0;
            // 
            // tspSucursales
            // 
            this.tspSucursales.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tspSucursales.Size = new System.Drawing.Size(618, 25);
            this.tspSucursales.TabIndex = 15;
            this.tspSucursales.Text = "toolStrip1";
            // 
            // tsbCancelar
            // 
            this.tsbCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
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
            this.tsbOrdenar.Enabled = false;
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
            // frmOrdenExistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 504);
            this.Controls.Add(this.tspSucursales);
            this.Controls.Add(this.gpbOrdenarArticulos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrdenExistencias";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existencias";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrdenExistencias_FormClosing);
            this.Load += new System.EventHandler(this.frmOrdenExistencias_Load);
            this.gpbOrdenarArticulos.ResumeLayout(false);
            this.gpbOrdenarArticulos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenExistencias)).EndInit();
            this.tspSucursales.ResumeLayout(false);
            this.tspSucursales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbOrdenarArticulos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategorias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSucursales;
        private System.Windows.Forms.DataGridView dgvOrdenExistencias;
        private System.Windows.Forms.ToolStrip tspSucursales;
        private System.Windows.Forms.ToolStripButton tsbCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbOrdenar;
        private System.Windows.Forms.ToolStripButton tsbInicio;
        private System.Windows.Forms.ToolStripButton tsbSubir;
        private System.Windows.Forms.ToolStripButton tsbBajar;
        private System.Windows.Forms.ToolStripButton tsbFinal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCerrar;
        private System.Windows.Forms.Button btnConsultar;
    }
}