namespace PassengerTransportApp
{
    partial class BusManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvBuses = new System.Windows.Forms.DataGridView();
            this.picBus = new System.Windows.Forms.PictureBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBus)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBuses
            // 
            this.dgvBuses.AllowUserToAddRows = false;
            this.dgvBuses.AllowUserToDeleteRows = false;
            this.dgvBuses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuses.Location = new System.Drawing.Point(12, 29);
            this.dgvBuses.MultiSelect = false;
            this.dgvBuses.Name = "dgvBuses";
            this.dgvBuses.ReadOnly = true;
            this.dgvBuses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuses.Size = new System.Drawing.Size(434, 409);
            this.dgvBuses.TabIndex = 0;
            this.dgvBuses.SelectionChanged += new System.EventHandler(this.dgvBuses_SelectionChanged);
            // 
            // picBus
            // 
            this.picBus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBus.Location = new System.Drawing.Point(465, 29);
            this.picBus.Name = "picBus";
            this.picBus.Size = new System.Drawing.Size(300, 200);
            this.picBus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBus.TabIndex = 1;
            this.picBus.TabStop = false;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(465, 245);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(300, 36);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "Загрузить фото";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Список автобусов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(462, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Фото автобуса";
            // 
            // BusManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.picBus);
            this.Controls.Add(this.dgvBuses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BusManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление автопарком";
            this.Load += new System.EventHandler(this.BusManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBuses;
        private System.Windows.Forms.PictureBox picBus;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}