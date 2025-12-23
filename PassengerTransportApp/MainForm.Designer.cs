namespace PassengerTransportApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSellTicket = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnBuses = new System.Windows.Forms.Button();
            this.btnManageDrivers = new System.Windows.Forms.Button();
            this.btnAddTrip = new System.Windows.Forms.Button();
            this.btnEditTrip = new System.Windows.Forms.Button();
            this.btnDeleteTrip = new System.Windows.Forms.Button();
            this.btnRestoreTrip = new System.Windows.Forms.Button();
            this.btnShowPassengers = new System.Windows.Forms.Button(); // <-- НОВАЯ КНОПКА
            this.chkShowDeleted = new System.Windows.Forms.CheckBox();
            this.dgvTrips = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnSellTicket);
            this.panelTop.Controls.Add(this.btnReports);
            this.panelTop.Controls.Add(this.btnBuses);
            this.panelTop.Controls.Add(this.btnManageDrivers);
            this.panelTop.Controls.Add(this.btnAddTrip);
            this.panelTop.Controls.Add(this.btnEditTrip);
            this.panelTop.Controls.Add(this.btnDeleteTrip);
            this.panelTop.Controls.Add(this.btnRestoreTrip);
            this.panelTop.Controls.Add(this.btnShowPassengers);
            this.panelTop.Controls.Add(this.chkShowDeleted);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1284, 100);
            this.panelTop.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip1.TabIndex = 2;
            // 
            // statusLabelUser
            // 
            this.statusLabelUser.Name = "statusLabelUser";
            this.statusLabelUser.Size = new System.Drawing.Size(112, 17);
            this.statusLabelUser.Text = "Пользователь: ...";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1172, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 50);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSellTicket
            // 
            this.btnSellTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSellTicket.BackColor = System.Drawing.Color.LightGreen;
            this.btnSellTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSellTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSellTicket.Location = new System.Drawing.Point(1012, 12);
            this.btnSellTicket.Name = "btnSellTicket";
            this.btnSellTicket.Size = new System.Drawing.Size(154, 50);
            this.btnSellTicket.TabIndex = 0;
            this.btnSellTicket.Text = "ПРОДАТЬ БИЛЕТ";
            this.btnSellTicket.UseVisualStyleBackColor = false;
            this.btnSellTicket.Click += new System.EventHandler(this.btnSellTicket_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(400, 12);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(90, 50);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "Отчеты";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnBuses
            // 
            this.btnBuses.Location = new System.Drawing.Point(304, 12);
            this.btnBuses.Name = "btnBuses";
            this.btnBuses.Size = new System.Drawing.Size(90, 50);
            this.btnBuses.TabIndex = 4;
            this.btnBuses.Text = "Автопарк";
            this.btnBuses.UseVisualStyleBackColor = true;
            this.btnBuses.Click += new System.EventHandler(this.btnBuses_Click);
            // 
            // btnManageDrivers
            // 
            this.btnManageDrivers.Location = new System.Drawing.Point(208, 12);
            this.btnManageDrivers.Name = "btnManageDrivers";
            this.btnManageDrivers.Size = new System.Drawing.Size(90, 50);
            this.btnManageDrivers.TabIndex = 5;
            this.btnManageDrivers.Text = "Водители";
            this.btnManageDrivers.UseVisualStyleBackColor = true;
            this.btnManageDrivers.Click += new System.EventHandler(this.btnManageDrivers_Click);
            // 
            // btnAddTrip
            // 
            this.btnAddTrip.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAddTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTrip.ForeColor = System.Drawing.Color.White;
            this.btnAddTrip.Location = new System.Drawing.Point(12, 12);
            this.btnAddTrip.Name = "btnAddTrip";
            this.btnAddTrip.Size = new System.Drawing.Size(190, 50);
            this.btnAddTrip.TabIndex = 6;
            this.btnAddTrip.Text = "+ Создать Рейс";
            this.btnAddTrip.UseVisualStyleBackColor = false;
            this.btnAddTrip.Click += new System.EventHandler(this.btnAddTrip_Click);
            // 
            // btnEditTrip
            // 
            this.btnEditTrip.Location = new System.Drawing.Point(12, 68);
            this.btnEditTrip.Name = "btnEditTrip";
            this.btnEditTrip.Size = new System.Drawing.Size(90, 25);
            this.btnEditTrip.TabIndex = 7;
            this.btnEditTrip.Text = "Изменить";
            this.btnEditTrip.UseVisualStyleBackColor = true;
            this.btnEditTrip.Click += new System.EventHandler(this.btnEditTrip_Click);
            // 
            // btnDeleteTrip
            // 
            this.btnDeleteTrip.Location = new System.Drawing.Point(108, 68);
            this.btnDeleteTrip.Name = "btnDeleteTrip";
            this.btnDeleteTrip.Size = new System.Drawing.Size(94, 25);
            this.btnDeleteTrip.TabIndex = 8;
            this.btnDeleteTrip.Text = "Удалить";
            this.btnDeleteTrip.UseVisualStyleBackColor = true;
            this.btnDeleteTrip.Click += new System.EventHandler(this.btnDeleteTrip_Click);
            // 
            // btnRestoreTrip
            // 
            this.btnRestoreTrip.Location = new System.Drawing.Point(108, 68);
            this.btnRestoreTrip.Name = "btnRestoreTrip";
            this.btnRestoreTrip.Size = new System.Drawing.Size(94, 25);
            this.btnRestoreTrip.TabIndex = 10;
            this.btnRestoreTrip.Text = "Восстановить";
            this.btnRestoreTrip.UseVisualStyleBackColor = true;
            this.btnRestoreTrip.Click += new System.EventHandler(this.btnRestoreTrip_Click);
            // 
            // btnShowPassengers
            // 
            this.btnShowPassengers.Location = new System.Drawing.Point(496, 12);
            this.btnShowPassengers.Name = "btnShowPassengers";
            this.btnShowPassengers.Size = new System.Drawing.Size(120, 50);
            this.btnShowPassengers.TabIndex = 11;
            this.btnShowPassengers.Text = "Список пассажиров";
            this.btnShowPassengers.UseVisualStyleBackColor = true;
            this.btnShowPassengers.Click += new System.EventHandler(this.btnShowPassengers_Click);
            // 
            // chkShowDeleted
            // 
            this.chkShowDeleted.AutoSize = true;
            this.chkShowDeleted.Location = new System.Drawing.Point(215, 73);
            this.chkShowDeleted.Name = "chkShowDeleted";
            this.chkShowDeleted.Size = new System.Drawing.Size(133, 17);
            this.chkShowDeleted.TabIndex = 9;
            this.chkShowDeleted.Text = "Показать удаленные";
            this.chkShowDeleted.UseVisualStyleBackColor = true;
            this.chkShowDeleted.CheckedChanged += new System.EventHandler(this.chkShowDeleted_CheckedChanged);
            // 
            // dgvTrips
            // 
            this.dgvTrips.AllowUserToAddRows = false;
            this.dgvTrips.AllowUserToDeleteRows = false;
            this.dgvTrips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrips.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTrips.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrips.Location = new System.Drawing.Point(0, 100);
            this.dgvTrips.MultiSelect = false;
            this.dgvTrips.Name = "dgvTrips";
            this.dgvTrips.ReadOnly = true;
            this.dgvTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrips.Size = new System.Drawing.Size(1284, 539);
            this.dgvTrips.TabIndex = 1;
            this.dgvTrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrips_CellContentClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.dgvTrips);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Система управления пассажироперевозками";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSellTicket;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.DataGridView dgvTrips;
        private System.Windows.Forms.Button btnManageDrivers;
        private System.Windows.Forms.Button btnAddTrip;
        private System.Windows.Forms.Button btnBuses;
        private System.Windows.Forms.Button btnEditTrip;
        private System.Windows.Forms.Button btnDeleteTrip;
        private System.Windows.Forms.Button btnRestoreTrip;
        private System.Windows.Forms.Button btnShowPassengers;
        private System.Windows.Forms.CheckBox chkShowDeleted;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelUser;
    }
}