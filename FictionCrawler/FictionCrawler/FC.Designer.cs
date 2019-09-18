namespace FictionCrawler
{
    partial class Fiction
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.ListBox();
            this.cbSelect = new System.Windows.Forms.ComboBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.txtBookIntro = new System.Windows.Forms.TextBox();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbFiction = new System.Windows.Forms.PictureBox();
            this.rbNoEnd = new System.Windows.Forms.RadioButton();
            this.rbEnd = new System.Windows.Forms.RadioButton();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFiction)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(497, 150);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(497, 204);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 7;
            this.btnEnd.Text = "停止";
            this.btnEnd.UseVisualStyleBackColor = true;
            // 
            // Status
            // 
            this.Status.FormattingEnabled = true;
            this.Status.ItemHeight = 12;
            this.Status.Location = new System.Drawing.Point(12, 253);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(560, 196);
            this.Status.TabIndex = 10;
            // 
            // cbSelect
            // 
            this.cbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelect.FormattingEnabled = true;
            this.cbSelect.Items.AddRange(new object[] {
            "青春日常"});
            this.cbSelect.Location = new System.Drawing.Point(452, 25);
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.Size = new System.Drawing.Size(120, 20);
            this.cbSelect.TabIndex = 11;
            // 
            // gbInfo
            // 
            this.gbInfo.Controls.Add(this.txtBookIntro);
            this.gbInfo.Controls.Add(this.txtBookName);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Controls.Add(this.pbFiction);
            this.gbInfo.Location = new System.Drawing.Point(12, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(434, 235);
            this.gbInfo.TabIndex = 12;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "书籍信息";
            // 
            // txtBookIntro
            // 
            this.txtBookIntro.Location = new System.Drawing.Point(185, 56);
            this.txtBookIntro.Multiline = true;
            this.txtBookIntro.Name = "txtBookIntro";
            this.txtBookIntro.ReadOnly = true;
            this.txtBookIntro.Size = new System.Drawing.Size(243, 173);
            this.txtBookIntro.TabIndex = 3;
            // 
            // txtBookName
            // 
            this.txtBookName.Location = new System.Drawing.Point(185, 21);
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.ReadOnly = true;
            this.txtBookName.Size = new System.Drawing.Size(243, 21);
            this.txtBookName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "简介：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "书名：";
            // 
            // pbFiction
            // 
            this.pbFiction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFiction.Location = new System.Drawing.Point(7, 21);
            this.pbFiction.Name = "pbFiction";
            this.pbFiction.Size = new System.Drawing.Size(124, 140);
            this.pbFiction.TabIndex = 0;
            this.pbFiction.TabStop = false;
            // 
            // rbNoEnd
            // 
            this.rbNoEnd.AutoSize = true;
            this.rbNoEnd.Location = new System.Drawing.Point(513, 69);
            this.rbNoEnd.Name = "rbNoEnd";
            this.rbNoEnd.Size = new System.Drawing.Size(59, 16);
            this.rbNoEnd.TabIndex = 13;
            this.rbNoEnd.TabStop = true;
            this.rbNoEnd.Text = "未完结";
            this.rbNoEnd.UseVisualStyleBackColor = true;
            // 
            // rbEnd
            // 
            this.rbEnd.AutoSize = true;
            this.rbEnd.Location = new System.Drawing.Point(513, 104);
            this.rbEnd.Name = "rbEnd";
            this.rbEnd.Size = new System.Drawing.Size(59, 16);
            this.rbEnd.TabIndex = 13;
            this.rbEnd.TabStop = true;
            this.rbEnd.Text = "完  结";
            this.rbEnd.UseVisualStyleBackColor = true;
            // 
            // Fiction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.rbEnd);
            this.Controls.Add(this.rbNoEnd);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.cbSelect);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnStart);
            this.Name = "Fiction";
            this.Text = "小说爬虫";
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFiction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.ListBox Status;
        private System.Windows.Forms.ComboBox cbSelect;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox txtBookIntro;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbFiction;
        private System.Windows.Forms.RadioButton rbNoEnd;
        private System.Windows.Forms.RadioButton rbEnd;
    }
}

