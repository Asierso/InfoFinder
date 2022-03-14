namespace InfoFinder
{
    partial class SearchMenu
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
        public void InitializeComponent()
        {
            this.searchButton = new System.Windows.Forms.Button();
            this.subjectTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filtersTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pagesUdc = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.versionTxt = new System.Windows.Forms.Label();
            this.programURL = new System.Windows.Forms.LinkLabel();
            this.activeSearchGroup = new System.Windows.Forms.GroupBox();
            this.activeSearch = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pagesUdc)).BeginInit();
            this.activeSearchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.searchButton.Location = new System.Drawing.Point(12, 217);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(436, 47);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Buscar";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // subjectTxt
            // 
            this.subjectTxt.Location = new System.Drawing.Point(147, 26);
            this.subjectTxt.Multiline = true;
            this.subjectTxt.Name = "subjectTxt";
            this.subjectTxt.Size = new System.Drawing.Size(263, 99);
            this.subjectTxt.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filtersTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pagesUdc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.subjectTxt);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 207);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurar búsqueda";
            // 
            // filtersTxt
            // 
            this.filtersTxt.Location = new System.Drawing.Point(147, 165);
            this.filtersTxt.Name = "filtersTxt";
            this.filtersTxt.Size = new System.Drawing.Size(263, 26);
            this.filtersTxt.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filtros";
            // 
            // pagesUdc
            // 
            this.pagesUdc.Location = new System.Drawing.Point(147, 133);
            this.pagesUdc.Name = "pagesUdc";
            this.pagesUdc.Size = new System.Drawing.Size(263, 26);
            this.pagesUdc.TabIndex = 4;
            this.pagesUdc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Páginas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temas a buscar";
            // 
            // versionTxt
            // 
            this.versionTxt.AutoSize = true;
            this.versionTxt.Location = new System.Drawing.Point(12, 267);
            this.versionTxt.Name = "versionTxt";
            this.versionTxt.Size = new System.Drawing.Size(63, 20);
            this.versionTxt.TabIndex = 3;
            this.versionTxt.Text = "Version";
            // 
            // programURL
            // 
            this.programURL.AutoSize = true;
            this.programURL.Location = new System.Drawing.Point(680, 267);
            this.programURL.Name = "programURL";
            this.programURL.Size = new System.Drawing.Size(96, 20);
            this.programURL.TabIndex = 4;
            this.programURL.TabStop = true;
            this.programURL.Text = "Repo y Help";
            this.programURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.programURL_LinkClicked);
            // 
            // activeSearchGroup
            // 
            this.activeSearchGroup.Controls.Add(this.activeSearch);
            this.activeSearchGroup.Location = new System.Drawing.Point(454, 4);
            this.activeSearchGroup.Name = "activeSearchGroup";
            this.activeSearchGroup.Size = new System.Drawing.Size(328, 260);
            this.activeSearchGroup.TabIndex = 5;
            this.activeSearchGroup.TabStop = false;
            this.activeSearchGroup.Text = "Busquedas activas";
            // 
            // activeSearch
            // 
            this.activeSearch.AutoScroll = true;
            this.activeSearch.Location = new System.Drawing.Point(6, 25);
            this.activeSearch.Name = "activeSearch";
            this.activeSearch.Size = new System.Drawing.Size(316, 229);
            this.activeSearch.TabIndex = 0;
            // 
            // SearchMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 291);
            this.Controls.Add(this.activeSearchGroup);
            this.Controls.Add(this.programURL);
            this.Controls.Add(this.versionTxt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.searchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SearchMenu";
            this.Text = "InfoFinder";
            this.Load += new System.EventHandler(this.SearchMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pagesUdc)).EndInit();
            this.activeSearchGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox subjectTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox filtersTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown pagesUdc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label versionTxt;
        private System.Windows.Forms.LinkLabel programURL;
        private System.Windows.Forms.GroupBox activeSearchGroup;
        private System.Windows.Forms.Panel activeSearch;
    }
}