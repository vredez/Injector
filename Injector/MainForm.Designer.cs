namespace Injector
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.listView_procs = new System.Windows.Forms.ListView();
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList_icons = new System.Windows.Forms.ImageList(this.components);
            this.groupBox_dll = new System.Windows.Forms.GroupBox();
            this.button_dllpath = new System.Windows.Forms.Button();
            this.textBox_dllpath = new System.Windows.Forms.TextBox();
            this.button_injectload = new System.Windows.Forms.Button();
            this.tabControl_mode = new System.Windows.Forms.TabControl();
            this.tabPage_inject = new System.Windows.Forms.TabPage();
            this.button_refresh = new System.Windows.Forms.Button();
            this.tabPage_load = new System.Windows.Forms.TabPage();
            this.button_exepath = new System.Windows.Forms.Button();
            this.textBox_exepath = new System.Windows.Forms.TextBox();
            this.label_exe = new System.Windows.Forms.Label();
            this.groupBox_dll.SuspendLayout();
            this.tabControl_mode.SuspendLayout();
            this.tabPage_inject.SuspendLayout();
            this.tabPage_load.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_procs
            // 
            this.listView_procs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_procs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_name});
            this.listView_procs.FullRowSelect = true;
            this.listView_procs.HideSelection = false;
            this.listView_procs.Location = new System.Drawing.Point(6, 6);
            this.listView_procs.MultiSelect = false;
            this.listView_procs.Name = "listView_procs";
            this.listView_procs.Size = new System.Drawing.Size(429, 204);
            this.listView_procs.SmallImageList = this.imageList_icons;
            this.listView_procs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_procs.TabIndex = 0;
            this.listView_procs.UseCompatibleStateImageBehavior = false;
            this.listView_procs.View = System.Windows.Forms.View.Details;
            this.listView_procs.SelectedIndexChanged += new System.EventHandler(this.OnDataSelection);
            this.listView_procs.MouseEnter += new System.EventHandler(this.OnMouseEnterListview);
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "Name";
            this.columnHeader_name.Width = 417;
            // 
            // imageList_icons
            // 
            this.imageList_icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList_icons.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList_icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox_dll
            // 
            this.groupBox_dll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_dll.Controls.Add(this.button_dllpath);
            this.groupBox_dll.Controls.Add(this.textBox_dllpath);
            this.groupBox_dll.Location = new System.Drawing.Point(12, 289);
            this.groupBox_dll.Name = "groupBox_dll";
            this.groupBox_dll.Size = new System.Drawing.Size(449, 74);
            this.groupBox_dll.TabIndex = 1;
            this.groupBox_dll.TabStop = false;
            this.groupBox_dll.Text = "DLL";
            // 
            // button_dllpath
            // 
            this.button_dllpath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_dllpath.Location = new System.Drawing.Point(411, 49);
            this.button_dllpath.Name = "button_dllpath";
            this.button_dllpath.Size = new System.Drawing.Size(32, 19);
            this.button_dllpath.TabIndex = 1;
            this.button_dllpath.Text = "...";
            this.button_dllpath.UseVisualStyleBackColor = true;
            this.button_dllpath.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // textBox_dllpath
            // 
            this.textBox_dllpath.AllowDrop = true;
            this.textBox_dllpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_dllpath.Location = new System.Drawing.Point(6, 21);
            this.textBox_dllpath.Name = "textBox_dllpath";
            this.textBox_dllpath.Size = new System.Drawing.Size(437, 22);
            this.textBox_dllpath.TabIndex = 0;
            this.textBox_dllpath.TextChanged += new System.EventHandler(this.OnDataSelection);
            this.textBox_dllpath.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.textBox_dllpath.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            // 
            // button_injectload
            // 
            this.button_injectload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_injectload.Enabled = false;
            this.button_injectload.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_injectload.Location = new System.Drawing.Point(12, 374);
            this.button_injectload.Name = "button_injectload";
            this.button_injectload.Size = new System.Drawing.Size(449, 47);
            this.button_injectload.TabIndex = 2;
            this.button_injectload.Text = "Inject";
            this.button_injectload.UseVisualStyleBackColor = true;
            this.button_injectload.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // tabControl_mode
            // 
            this.tabControl_mode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_mode.Controls.Add(this.tabPage_inject);
            this.tabControl_mode.Controls.Add(this.tabPage_load);
            this.tabControl_mode.Location = new System.Drawing.Point(12, 12);
            this.tabControl_mode.Name = "tabControl_mode";
            this.tabControl_mode.SelectedIndex = 0;
            this.tabControl_mode.Size = new System.Drawing.Size(449, 271);
            this.tabControl_mode.TabIndex = 3;
            this.tabControl_mode.SelectedIndexChanged += new System.EventHandler(this.OnModeChanged);
            // 
            // tabPage_inject
            // 
            this.tabPage_inject.BackColor = System.Drawing.Color.White;
            this.tabPage_inject.Controls.Add(this.button_refresh);
            this.tabPage_inject.Controls.Add(this.listView_procs);
            this.tabPage_inject.Location = new System.Drawing.Point(4, 22);
            this.tabPage_inject.Name = "tabPage_inject";
            this.tabPage_inject.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_inject.Size = new System.Drawing.Size(441, 245);
            this.tabPage_inject.TabIndex = 0;
            this.tabPage_inject.Text = "Inject";
            // 
            // button_refresh
            // 
            this.button_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_refresh.Location = new System.Drawing.Point(6, 216);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(429, 23);
            this.button_refresh.TabIndex = 1;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // tabPage_load
            // 
            this.tabPage_load.BackColor = System.Drawing.Color.White;
            this.tabPage_load.Controls.Add(this.button_exepath);
            this.tabPage_load.Controls.Add(this.textBox_exepath);
            this.tabPage_load.Controls.Add(this.label_exe);
            this.tabPage_load.Location = new System.Drawing.Point(4, 22);
            this.tabPage_load.Name = "tabPage_load";
            this.tabPage_load.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_load.Size = new System.Drawing.Size(441, 245);
            this.tabPage_load.TabIndex = 1;
            this.tabPage_load.Text = "Load";
            // 
            // button_exepath
            // 
            this.button_exepath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_exepath.Location = new System.Drawing.Point(403, 34);
            this.button_exepath.Name = "button_exepath";
            this.button_exepath.Size = new System.Drawing.Size(32, 19);
            this.button_exepath.TabIndex = 3;
            this.button_exepath.Text = "...";
            this.button_exepath.UseVisualStyleBackColor = true;
            this.button_exepath.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // textBox_exepath
            // 
            this.textBox_exepath.AllowDrop = true;
            this.textBox_exepath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_exepath.Location = new System.Drawing.Point(77, 6);
            this.textBox_exepath.Name = "textBox_exepath";
            this.textBox_exepath.Size = new System.Drawing.Size(358, 22);
            this.textBox_exepath.TabIndex = 1;
            this.textBox_exepath.TextChanged += new System.EventHandler(this.OnDataSelection);
            this.textBox_exepath.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.textBox_exepath.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            // 
            // label_exe
            // 
            this.label_exe.AutoSize = true;
            this.label_exe.Location = new System.Drawing.Point(6, 9);
            this.label_exe.Name = "label_exe";
            this.label_exe.Size = new System.Drawing.Size(65, 13);
            this.label_exe.TabIndex = 0;
            this.label_exe.Text = "Executable:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 433);
            this.Controls.Add(this.tabControl_mode);
            this.Controls.Add(this.button_injectload);
            this.Controls.Add(this.groupBox_dll);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 378);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Injector";
            this.groupBox_dll.ResumeLayout(false);
            this.groupBox_dll.PerformLayout();
            this.tabControl_mode.ResumeLayout(false);
            this.tabPage_inject.ResumeLayout(false);
            this.tabPage_load.ResumeLayout(false);
            this.tabPage_load.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_procs;
        private System.Windows.Forms.ColumnHeader columnHeader_name;
        private System.Windows.Forms.GroupBox groupBox_dll;
        private System.Windows.Forms.Button button_dllpath;
        private System.Windows.Forms.TextBox textBox_dllpath;
        private System.Windows.Forms.Button button_injectload;
        private System.Windows.Forms.ImageList imageList_icons;
        private System.Windows.Forms.TabControl tabControl_mode;
        private System.Windows.Forms.TabPage tabPage_inject;
        private System.Windows.Forms.TabPage tabPage_load;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button button_exepath;
        private System.Windows.Forms.TextBox textBox_exepath;
        private System.Windows.Forms.Label label_exe;
    }
}