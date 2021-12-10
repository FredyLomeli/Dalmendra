namespace Dalmendra
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sucursalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.existenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConCodigo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSinCodigo = new System.Windows.Forms.ToolStripMenuItem();
            this.categoriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrActualizacion = new System.Windows.Forms.Timer(this.components);
            this.porCategoriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sucursalesToolStripMenuItem,
            this.existenciasToolStripMenuItem,
            this.categoriaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sucursalesToolStripMenuItem
            // 
            this.sucursalesToolStripMenuItem.Name = "sucursalesToolStripMenuItem";
            this.sucursalesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.sucursalesToolStripMenuItem.Text = "Sucursales";
            this.sucursalesToolStripMenuItem.Click += new System.EventHandler(this.sucursalesToolStripMenuItem_Click);
            // 
            // existenciasToolStripMenuItem
            // 
            this.existenciasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porCategoriasToolStripMenuItem,
            this.tsmConCodigo,
            this.tsmSinCodigo});
            this.existenciasToolStripMenuItem.Name = "existenciasToolStripMenuItem";
            this.existenciasToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.existenciasToolStripMenuItem.Text = "Reportes";
            // 
            // tsmConCodigo
            // 
            this.tsmConCodigo.Name = "tsmConCodigo";
            this.tsmConCodigo.Size = new System.Drawing.Size(177, 22);
            this.tsmConCodigo.Text = "Listado con Codigo";
            this.tsmConCodigo.Click += new System.EventHandler(this.tsmConCodigo_Click);
            // 
            // tsmSinCodigo
            // 
            this.tsmSinCodigo.Name = "tsmSinCodigo";
            this.tsmSinCodigo.Size = new System.Drawing.Size(177, 22);
            this.tsmSinCodigo.Text = "Listado sin Codigo";
            this.tsmSinCodigo.Click += new System.EventHandler(this.tsmSinCodigo_Click);
            // 
            // categoriaToolStripMenuItem
            // 
            this.categoriaToolStripMenuItem.Name = "categoriaToolStripMenuItem";
            this.categoriaToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.categoriaToolStripMenuItem.Text = "Categoria";
            this.categoriaToolStripMenuItem.Click += new System.EventHandler(this.categoriaToolStripMenuItem_Click);
            // 
            // tmrActualizacion
            // 
            this.tmrActualizacion.Enabled = true;
            this.tmrActualizacion.Interval = 60000;
            this.tmrActualizacion.Tick += new System.EventHandler(this.tmrActualizacion_Tick);
            // 
            // porCategoriasToolStripMenuItem
            // 
            this.porCategoriasToolStripMenuItem.Name = "porCategoriasToolStripMenuItem";
            this.porCategoriasToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.porCategoriasToolStripMenuItem.Text = "Por Categorias";
            this.porCategoriasToolStripMenuItem.Click += new System.EventHandler(this.porCategoriasToolStripMenuItem_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 425);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmPrincipal";
            this.Text = "Dalmendra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sucursalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem existenciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmConCodigo;
        private System.Windows.Forms.ToolStripMenuItem tsmSinCodigo;
        private System.Windows.Forms.Timer tmrActualizacion;
        private System.Windows.Forms.ToolStripMenuItem categoriaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porCategoriasToolStripMenuItem;

    }
}

