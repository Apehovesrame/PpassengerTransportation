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
            this.btnRouteManager = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSellTicket = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnBuses = new System.Windows.Forms.Button();
            this.btnManageDrivers = new System.Windows.Forms.Button();
            this.btnAddTrip = new System.Windows.Forms.Button();
            this.btnEditTrip = new System.Windows.Forms.Button();
            this.btnDeleteTrip = new System.Windows.Forms.Button();
            this.btnRestoreTrip = new System.Windows.Forms.Button();
            this.btnShowPassengers = new System.Windows.Forms.Button();
            this.chkShowDeleted = new System.Windows.Forms.CheckBox();
            this.btnHardDelete = new System.Windows.Forms.Button();
            this.btnClearArchive = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvTrips = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.btnRouteManager);
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
            this.panelTop.Controls.Add(this.btnHardDelete);
            this.panelTop.Controls.Add(this.btnClearArchive);
            this.panelTop.Controls.Add(this.btnAddUser);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(6);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(2568, 192);
            this.panelTop.TabIndex = 0;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint_1);
            // 
            // btnRouteManager
            // 
            this.btnRouteManager.Location = new System.Drawing.Point(1445, 23);
            this.btnRouteManager.Name = "btnRouteManager";
            this.btnRouteManager.Size = new System.Drawing.Size(187, 96);
            this.btnRouteManager.TabIndex = 12;
            this.btnRouteManager.Text = "Маршруты";
            this.btnRouteManager.UseVisualStyleBackColor = true;
            this.btnRouteManager.Click += new System.EventHandler(this.btnRouteManager_Click_1);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(2344, 23);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 96);
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
            this.btnSellTicket.Location = new System.Drawing.Point(2024, 23);
            this.btnSellTicket.Margin = new System.Windows.Forms.Padding(6);
            this.btnSellTicket.Name = "btnSellTicket";
            this.btnSellTicket.Size = new System.Drawing.Size(308, 96);
            this.btnSellTicket.TabIndex = 0;
            this.btnSellTicket.Text = "ПРОДАТЬ БИЛЕТ";
            this.btnSellTicket.UseVisualStyleBackColor = false;
            this.btnSellTicket.Click += new System.EventHandler(this.btnSellTicket_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(800, 23);
            this.btnReports.Margin = new System.Windows.Forms.Padding(6);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(180, 96);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "Отчеты";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnBuses
            // 
            this.btnBuses.Location = new System.Drawing.Point(608, 23);
            this.btnBuses.Margin = new System.Windows.Forms.Padding(6);
            this.btnBuses.Name = "btnBuses";
            this.btnBuses.Size = new System.Drawing.Size(180, 96);
            this.btnBuses.TabIndex = 4;
            this.btnBuses.Text = "Автопарк";
            this.btnBuses.UseVisualStyleBackColor = true;
            this.btnBuses.Click += new System.EventHandler(this.btnBuses_Click);
            // 
            // btnManageDrivers
            // 
            this.btnManageDrivers.Location = new System.Drawing.Point(416, 23);
            this.btnManageDrivers.Margin = new System.Windows.Forms.Padding(6);
            this.btnManageDrivers.Name = "btnManageDrivers";
            this.btnManageDrivers.Size = new System.Drawing.Size(180, 96);
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
            this.btnAddTrip.Location = new System.Drawing.Point(24, 23);
            this.btnAddTrip.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddTrip.Name = "btnAddTrip";
            this.btnAddTrip.Size = new System.Drawing.Size(380, 96);
            this.btnAddTrip.TabIndex = 6;
            this.btnAddTrip.Text = "+ Создать Рейс";
            this.btnAddTrip.UseVisualStyleBackColor = false;
            this.btnAddTrip.Click += new System.EventHandler(this.btnAddTrip_Click);
            // 
            // btnEditTrip
            // 
            this.btnEditTrip.Location = new System.Drawing.Point(24, 131);
            this.btnEditTrip.Margin = new System.Windows.Forms.Padding(6);
            this.btnEditTrip.Name = "btnEditTrip";
            this.btnEditTrip.Size = new System.Drawing.Size(180, 48);
            this.btnEditTrip.TabIndex = 7;
            this.btnEditTrip.Text = "Изменить";
            this.btnEditTrip.UseVisualStyleBackColor = true;
            this.btnEditTrip.Click += new System.EventHandler(this.btnEditTrip_Click);
            // 
            // btnDeleteTrip
            // 
            this.btnDeleteTrip.Location = new System.Drawing.Point(216, 131);
            this.btnDeleteTrip.Margin = new System.Windows.Forms.Padding(6);
            this.btnDeleteTrip.Name = "btnDeleteTrip";
            this.btnDeleteTrip.Size = new System.Drawing.Size(188, 48);
            this.btnDeleteTrip.TabIndex = 8;
            this.btnDeleteTrip.Text = "Удалить";
            this.btnDeleteTrip.UseVisualStyleBackColor = true;
            this.btnDeleteTrip.Click += new System.EventHandler(this.btnDeleteTrip_Click);
            // 
            // btnRestoreTrip
            // 
            this.btnRestoreTrip.Location = new System.Drawing.Point(216, 131);
            this.btnRestoreTrip.Margin = new System.Windows.Forms.Padding(6);
            this.btnRestoreTrip.Name = "btnRestoreTrip";
            this.btnRestoreTrip.Size = new System.Drawing.Size(188, 48);
            this.btnRestoreTrip.TabIndex = 10;
            this.btnRestoreTrip.Text = "Восстановить";
            this.btnRestoreTrip.UseVisualStyleBackColor = true;
            this.btnRestoreTrip.Click += new System.EventHandler(this.btnRestoreTrip_Click);
            // 
            // btnShowPassengers
            // 
            this.btnShowPassengers.Location = new System.Drawing.Point(992, 23);
            this.btnShowPassengers.Margin = new System.Windows.Forms.Padding(6);
            this.btnShowPassengers.Name = "btnShowPassengers";
            this.btnShowPassengers.Size = new System.Drawing.Size(240, 96);
            this.btnShowPassengers.TabIndex = 11;
            this.btnShowPassengers.Text = "Список пассажиров";
            this.btnShowPassengers.UseVisualStyleBackColor = true;
            this.btnShowPassengers.Click += new System.EventHandler(this.btnShowPassengers_Click);
            // 
            // chkShowDeleted
            // 
            this.chkShowDeleted.AutoSize = true;
            this.chkShowDeleted.Location = new System.Drawing.Point(430, 140);
            this.chkShowDeleted.Margin = new System.Windows.Forms.Padding(6);
            this.chkShowDeleted.Name = "chkShowDeleted";
            this.chkShowDeleted.Size = new System.Drawing.Size(254, 29);
            this.chkShowDeleted.TabIndex = 9;
            this.chkShowDeleted.Text = "Показать удаленные";
            this.chkShowDeleted.UseVisualStyleBackColor = true;
            this.chkShowDeleted.CheckedChanged += new System.EventHandler(this.chkShowDeleted_CheckedChanged);
            // 
            // btnHardDelete
            // 
            this.btnHardDelete.ForeColor = System.Drawing.Color.Red;
            this.btnHardDelete.Location = new System.Drawing.Point(24, 131);
            this.btnHardDelete.Name = "btnHardDelete";
            this.btnHardDelete.Size = new System.Drawing.Size(180, 48);
            this.btnHardDelete.TabIndex = 13;
            this.btnHardDelete.Text = "Удалить навсегда";
            this.btnHardDelete.UseVisualStyleBackColor = true;
            this.btnHardDelete.Click += new System.EventHandler(this.btnHardDelete_Click);
            // 
            // btnClearArchive
            // 
            this.btnClearArchive.ForeColor = System.Drawing.Color.DarkRed;
            this.btnClearArchive.Location = new System.Drawing.Point(440, 131);
            this.btnClearArchive.Name = "btnClearArchive";
            this.btnClearArchive.Size = new System.Drawing.Size(120, 48);
            this.btnClearArchive.TabIndex = 14;
            this.btnClearArchive.Text = "Очистить архив";
            this.btnClearArchive.UseVisualStyleBackColor = true;
            this.btnClearArchive.Click += new System.EventHandler(this.btnClearArchive_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(1241, 23);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(187, 96);
            this.btnAddUser.TabIndex = 15;
            this.btnAddUser.Text = "+ Сотрудник";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1229);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(2568, 42);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // statusLabelUser
            // 
            this.statusLabelUser.Name = "statusLabelUser";
            this.statusLabelUser.Size = new System.Drawing.Size(195, 32);
            this.statusLabelUser.Text = "Пользователь: ...";
            this.statusLabelUser.Click += new System.EventHandler(this.statusLabelUser_Click);
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
            this.dgvTrips.Location = new System.Drawing.Point(0, 192);
            this.dgvTrips.Margin = new System.Windows.Forms.Padding(6);
            this.dgvTrips.MultiSelect = false;
            this.dgvTrips.Name = "dgvTrips";
            this.dgvTrips.ReadOnly = true;
            this.dgvTrips.RowHeadersWidth = 82;
            this.dgvTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrips.Size = new System.Drawing.Size(2568, 1037);
            this.dgvTrips.TabIndex = 1;
            this.dgvTrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrips_CellContentClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2568, 1271);
            this.Controls.Add(this.dgvTrips);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private System.Windows.Forms.Button btnHardDelete;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnClearArchive;
        private System.Windows.Forms.Button btnBuses;
        private System.Windows.Forms.Button btnEditTrip;
        private System.Windows.Forms.Button btnDeleteTrip;
        private System.Windows.Forms.Button btnRestoreTrip;
        private System.Windows.Forms.Button btnShowPassengers;
        private System.Windows.Forms.CheckBox chkShowDeleted;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelUser;
        private System.Windows.Forms.Button btnRouteManager;
    }
}