namespace ImageProcessingControl
{
    partial class ImageProcessingDialog
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
                markers.Dispose();
                classifyMut.Dispose();
                GPSMut.Dispose();
                opencv.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageProcessingDialog));
            this.StopwatchGroupBox = new System.Windows.Forms.GroupBox();
            this.StopwatchTextBox = new System.Windows.Forms.TextBox();
            this.ResetStopwatchLinkLabel = new System.Windows.Forms.LinkLabel();
            this.StartStopwatchButton = new System.Windows.Forms.Button();
            this.stopStopwatchButton = new System.Windows.Forms.Button();
            this.ControlPanelGroupBox = new System.Windows.Forms.GroupBox();
            this.InitGpsHomeButton = new System.Windows.Forms.Button();
            this.RunMserButton = new System.Windows.Forms.Button();
            this.SpeedTestGroupBox = new System.Windows.Forms.GroupBox();
            this.PingTextBox = new System.Windows.Forms.TextBox();
            this.PingLabel = new System.Windows.Forms.Label();
            this.PingButton = new System.Windows.Forms.Button();
            this.LiveFeedButton = new System.Windows.Forms.Button();
            this.GMapButton = new System.Windows.Forms.Button();
            this.ImagesControlGroupBox = new System.Windows.Forms.GroupBox();
            this.zoomTextBox = new System.Windows.Forms.TextBox();
            this.StopShootButton = new System.Windows.Forms.Button();
            this.StartShootButton = new System.Windows.Forms.Button();
            this.cleanClassificationDirectory = new System.Windows.Forms.Button();
            this.CopyDirectory = new System.Windows.Forms.Button();
            this.ResetImages = new System.Windows.Forms.Button();
            this.AddImagesButtom = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MaxANumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MinANumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxAreaLabel = new System.Windows.Forms.Label();
            this.MinAreaLabel = new System.Windows.Forms.Label();
            this.SetButton = new System.Windows.Forms.Button();
            this.MDNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MVNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DeltaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MinDiversityLabel = new System.Windows.Forms.Label();
            this.MaxVariationLabel = new System.Windows.Forms.Label();
            this.DeltaMserLabel = new System.Windows.Forms.Label();
            this.ImageListView = new System.Windows.Forms.ListView();
            this.Image_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateTime_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Imagelatitude_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageLongitude_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sourceFile_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ImageViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageLargeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageSmallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SmallImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TargetImagePictureBox = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.ImagesTabPage = new System.Windows.Forms.TabPage();
            this.CropsTabPage = new System.Windows.Forms.TabPage();
            this.CropListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CropContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CropViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropLargeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropSmallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeImageList2 = new System.Windows.Forms.ImageList(this.components);
            this.SmallImageList2 = new System.Windows.Forms.ImageList(this.components);
            this.TrueTargetsTabPage = new System.Windows.Forms.TabPage();
            this.TrueTargetsListView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TrueTargetsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TrueTargetsViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrueTargetsLargeIconsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.TrueTargetsSmallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrueTargetsDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrueTaregtsDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeTrueImageList = new System.Windows.Forms.ImageList(this.components);
            this.SmallTrueImageList = new System.Windows.Forms.ImageList(this.components);
            this.TargetInfoGroupBox = new System.Windows.Forms.GroupBox();
            this._OrientationLabel = new System.Windows.Forms.Label();
            this.OrientationLabel = new System.Windows.Forms.Label();
            this._YawLabel = new System.Windows.Forms.Label();
            this.YawLabel = new System.Windows.Forms.Label();
            this._LetterColorLabel = new System.Windows.Forms.Label();
            this._LetterLabel = new System.Windows.Forms.Label();
            this._ShapeColorLabel = new System.Windows.Forms.Label();
            this._ShapeLabel = new System.Windows.Forms.Label();
            this._TargetLongitudeLabel = new System.Windows.Forms.Label();
            this._TargetLatitudeLabel = new System.Windows.Forms.Label();
            this._ImageLongitudeLabel = new System.Windows.Forms.Label();
            this._ImageLatitudeLabel = new System.Windows.Forms.Label();
            this._ImageNameLabel = new System.Windows.Forms.Label();
            this.ImageNameLabel = new System.Windows.Forms.Label();
            this.LetterColorLabel = new System.Windows.Forms.Label();
            this.ImageLatitudeLabel = new System.Windows.Forms.Label();
            this.LetterLabel = new System.Windows.Forms.Label();
            this.ImageLongitudeLabel = new System.Windows.Forms.Label();
            this.ShapeColorLabel = new System.Windows.Forms.Label();
            this.TargetLatitudeLabel = new System.Windows.Forms.Label();
            this.ShapeLabel = new System.Windows.Forms.Label();
            this.TargetLongitudeLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.upperMenuStrip = new System.Windows.Forms.MenuStrip();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDBAUVSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopwatchGroupBox.SuspendLayout();
            this.ControlPanelGroupBox.SuspendLayout();
            this.SpeedTestGroupBox.SuspendLayout();
            this.ImagesControlGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxANumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinANumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeltaNumericUpDown)).BeginInit();
            this.ImageContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.ImagesTabPage.SuspendLayout();
            this.CropsTabPage.SuspendLayout();
            this.CropContextMenuStrip.SuspendLayout();
            this.TrueTargetsTabPage.SuspendLayout();
            this.TrueTargetsContextMenuStrip.SuspendLayout();
            this.TargetInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.upperMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StopwatchGroupBox
            // 
            this.StopwatchGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StopwatchGroupBox.BackColor = System.Drawing.Color.Black;
            this.StopwatchGroupBox.Controls.Add(this.StopwatchTextBox);
            this.StopwatchGroupBox.Controls.Add(this.ResetStopwatchLinkLabel);
            this.StopwatchGroupBox.Controls.Add(this.StartStopwatchButton);
            this.StopwatchGroupBox.Controls.Add(this.stopStopwatchButton);
            this.StopwatchGroupBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.StopwatchGroupBox.Location = new System.Drawing.Point(4, 862);
            this.StopwatchGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopwatchGroupBox.Name = "StopwatchGroupBox";
            this.StopwatchGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopwatchGroupBox.Size = new System.Drawing.Size(229, 139);
            this.StopwatchGroupBox.TabIndex = 7;
            this.StopwatchGroupBox.TabStop = false;
            this.StopwatchGroupBox.Text = "Stopwatch";
            // 
            // StopwatchTextBox
            // 
            this.StopwatchTextBox.BackColor = System.Drawing.Color.Black;
            this.StopwatchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.StopwatchTextBox.ForeColor = System.Drawing.Color.Yellow;
            this.StopwatchTextBox.Location = new System.Drawing.Point(7, 23);
            this.StopwatchTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopwatchTextBox.Name = "StopwatchTextBox";
            this.StopwatchTextBox.ReadOnly = true;
            this.StopwatchTextBox.Size = new System.Drawing.Size(207, 46);
            this.StopwatchTextBox.TabIndex = 0;
            this.StopwatchTextBox.TabStop = false;
            this.StopwatchTextBox.Text = "00:00:00";
            this.StopwatchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResetStopwatchLinkLabel
            // 
            this.ResetStopwatchLinkLabel.AutoSize = true;
            this.ResetStopwatchLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ResetStopwatchLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ResetStopwatchLinkLabel.Location = new System.Drawing.Point(25, 112);
            this.ResetStopwatchLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ResetStopwatchLinkLabel.Name = "ResetStopwatchLinkLabel";
            this.ResetStopwatchLinkLabel.Size = new System.Drawing.Size(52, 18);
            this.ResetStopwatchLinkLabel.TabIndex = 6;
            this.ResetStopwatchLinkLabel.TabStop = true;
            this.ResetStopwatchLinkLabel.Text = "Reset";
            this.ResetStopwatchLinkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.ResetStopwatchLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ResetStopwatchLinkLabel_LinkClicked);
            // 
            // StartStopwatchButton
            // 
            this.StartStopwatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.StartStopwatchButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartStopwatchButton.Location = new System.Drawing.Point(8, 78);
            this.StartStopwatchButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartStopwatchButton.Name = "StartStopwatchButton";
            this.StartStopwatchButton.Size = new System.Drawing.Size(100, 28);
            this.StartStopwatchButton.TabIndex = 4;
            this.StartStopwatchButton.Text = "START";
            this.StartStopwatchButton.UseVisualStyleBackColor = true;
            this.StartStopwatchButton.Click += new System.EventHandler(this.StartStopwatchButton_Click);
            // 
            // stopStopwatchButton
            // 
            this.stopStopwatchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.stopStopwatchButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stopStopwatchButton.Location = new System.Drawing.Point(115, 78);
            this.stopStopwatchButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stopStopwatchButton.Name = "stopStopwatchButton";
            this.stopStopwatchButton.Size = new System.Drawing.Size(100, 28);
            this.stopStopwatchButton.TabIndex = 5;
            this.stopStopwatchButton.Text = "STOP";
            this.stopStopwatchButton.UseVisualStyleBackColor = true;
            this.stopStopwatchButton.Click += new System.EventHandler(this.stopStopwatchButton_Click);
            // 
            // ControlPanelGroupBox
            // 
            this.ControlPanelGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ControlPanelGroupBox.BackColor = System.Drawing.Color.Black;
            this.ControlPanelGroupBox.Controls.Add(this.InitGpsHomeButton);
            this.ControlPanelGroupBox.Controls.Add(this.RunMserButton);
            this.ControlPanelGroupBox.Controls.Add(this.SpeedTestGroupBox);
            this.ControlPanelGroupBox.Controls.Add(this.LiveFeedButton);
            this.ControlPanelGroupBox.Controls.Add(this.GMapButton);
            this.ControlPanelGroupBox.Controls.Add(this.ImagesControlGroupBox);
            this.ControlPanelGroupBox.ForeColor = System.Drawing.Color.White;
            this.ControlPanelGroupBox.Location = new System.Drawing.Point(5, 160);
            this.ControlPanelGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ControlPanelGroupBox.Name = "ControlPanelGroupBox";
            this.ControlPanelGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ControlPanelGroupBox.Size = new System.Drawing.Size(228, 700);
            this.ControlPanelGroupBox.TabIndex = 8;
            this.ControlPanelGroupBox.TabStop = false;
            this.ControlPanelGroupBox.Text = "Control Panel";
            // 
            // InitGpsHomeButton
            // 
            this.InitGpsHomeButton.ForeColor = System.Drawing.Color.Black;
            this.InitGpsHomeButton.Location = new System.Drawing.Point(44, 602);
            this.InitGpsHomeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.InitGpsHomeButton.Name = "InitGpsHomeButton";
            this.InitGpsHomeButton.Size = new System.Drawing.Size(137, 34);
            this.InitGpsHomeButton.TabIndex = 5;
            this.InitGpsHomeButton.Text = "Init GPS Home";
            this.InitGpsHomeButton.UseVisualStyleBackColor = true;
            this.InitGpsHomeButton.Click += new System.EventHandler(this.InitHomeGpsButton_Click);
            // 
            // RunMserButton
            // 
            this.RunMserButton.ForeColor = System.Drawing.Color.Black;
            this.RunMserButton.Location = new System.Drawing.Point(45, 560);
            this.RunMserButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RunMserButton.Name = "RunMserButton";
            this.RunMserButton.Size = new System.Drawing.Size(137, 34);
            this.RunMserButton.TabIndex = 4;
            this.RunMserButton.Text = "Run MSER";
            this.RunMserButton.UseVisualStyleBackColor = true;
            this.RunMserButton.Click += new System.EventHandler(this.RunMserButton_Click);
            // 
            // SpeedTestGroupBox
            // 
            this.SpeedTestGroupBox.BackColor = System.Drawing.Color.Black;
            this.SpeedTestGroupBox.Controls.Add(this.PingTextBox);
            this.SpeedTestGroupBox.Controls.Add(this.PingLabel);
            this.SpeedTestGroupBox.Controls.Add(this.PingButton);
            this.SpeedTestGroupBox.ForeColor = System.Drawing.Color.White;
            this.SpeedTestGroupBox.Location = new System.Drawing.Point(19, 295);
            this.SpeedTestGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SpeedTestGroupBox.Name = "SpeedTestGroupBox";
            this.SpeedTestGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SpeedTestGroupBox.Size = new System.Drawing.Size(192, 170);
            this.SpeedTestGroupBox.TabIndex = 3;
            this.SpeedTestGroupBox.TabStop = false;
            this.SpeedTestGroupBox.Text = "Ping";
            // 
            // PingTextBox
            // 
            this.PingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PingTextBox.Location = new System.Drawing.Point(28, 25);
            this.PingTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PingTextBox.Name = "PingTextBox";
            this.PingTextBox.Size = new System.Drawing.Size(132, 30);
            this.PingTextBox.TabIndex = 2;
            this.PingTextBox.Text = "192.168.1.12";
            this.PingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PingLabel
            // 
            this.PingLabel.AutoSize = true;
            this.PingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PingLabel.ForeColor = System.Drawing.Color.Red;
            this.PingLabel.Location = new System.Drawing.Point(15, 122);
            this.PingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PingLabel.Name = "PingLabel";
            this.PingLabel.Size = new System.Drawing.Size(0, 25);
            this.PingLabel.TabIndex = 1;
            // 
            // PingButton
            // 
            this.PingButton.ForeColor = System.Drawing.Color.Black;
            this.PingButton.Location = new System.Drawing.Point(25, 73);
            this.PingButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PingButton.Name = "PingButton";
            this.PingButton.Size = new System.Drawing.Size(137, 34);
            this.PingButton.TabIndex = 0;
            this.PingButton.Text = "Ping";
            this.PingButton.UseVisualStyleBackColor = true;
            this.PingButton.Click += new System.EventHandler(this.PingButton_Click);
            // 
            // LiveFeedButton
            // 
            this.LiveFeedButton.ForeColor = System.Drawing.Color.Black;
            this.LiveFeedButton.Location = new System.Drawing.Point(44, 518);
            this.LiveFeedButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LiveFeedButton.Name = "LiveFeedButton";
            this.LiveFeedButton.Size = new System.Drawing.Size(137, 34);
            this.LiveFeedButton.TabIndex = 3;
            this.LiveFeedButton.Text = "Live Feed";
            this.LiveFeedButton.UseVisualStyleBackColor = true;
            // 
            // GMapButton
            // 
            this.GMapButton.ForeColor = System.Drawing.Color.Black;
            this.GMapButton.Location = new System.Drawing.Point(45, 475);
            this.GMapButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GMapButton.Name = "GMapButton";
            this.GMapButton.Size = new System.Drawing.Size(137, 34);
            this.GMapButton.TabIndex = 2;
            this.GMapButton.Text = "Google Map";
            this.GMapButton.UseVisualStyleBackColor = true;
            this.GMapButton.Click += new System.EventHandler(this.GMapButton_Click);
            // 
            // ImagesControlGroupBox
            // 
            this.ImagesControlGroupBox.BackColor = System.Drawing.Color.Black;
            this.ImagesControlGroupBox.Controls.Add(this.zoomTextBox);
            this.ImagesControlGroupBox.Controls.Add(this.StopShootButton);
            this.ImagesControlGroupBox.Controls.Add(this.StartShootButton);
            this.ImagesControlGroupBox.Controls.Add(this.cleanClassificationDirectory);
            this.ImagesControlGroupBox.Controls.Add(this.CopyDirectory);
            this.ImagesControlGroupBox.Controls.Add(this.ResetImages);
            this.ImagesControlGroupBox.Controls.Add(this.AddImagesButtom);
            this.ImagesControlGroupBox.ForeColor = System.Drawing.Color.White;
            this.ImagesControlGroupBox.Location = new System.Drawing.Point(19, 23);
            this.ImagesControlGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImagesControlGroupBox.Name = "ImagesControlGroupBox";
            this.ImagesControlGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImagesControlGroupBox.Size = new System.Drawing.Size(192, 270);
            this.ImagesControlGroupBox.TabIndex = 2;
            this.ImagesControlGroupBox.TabStop = false;
            this.ImagesControlGroupBox.Text = "Images control";
            // 
            // zoomTextBox
            // 
            this.zoomTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.zoomTextBox.Location = new System.Drawing.Point(125, 27);
            this.zoomTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zoomTextBox.Name = "zoomTextBox";
            this.zoomTextBox.Size = new System.Drawing.Size(45, 26);
            this.zoomTextBox.TabIndex = 6;
            this.zoomTextBox.Text = "0";
            this.zoomTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StopShootButton
            // 
            this.StopShootButton.ForeColor = System.Drawing.Color.Black;
            this.StopShootButton.Location = new System.Drawing.Point(25, 63);
            this.StopShootButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopShootButton.Name = "StopShootButton";
            this.StopShootButton.Size = new System.Drawing.Size(137, 34);
            this.StopShootButton.TabIndex = 5;
            this.StopShootButton.Text = "Stop Shoot";
            this.StopShootButton.UseVisualStyleBackColor = true;
            this.StopShootButton.Click += new System.EventHandler(this.StopShootButton_Click);
            // 
            // StartShootButton
            // 
            this.StartShootButton.ForeColor = System.Drawing.Color.Black;
            this.StartShootButton.Location = new System.Drawing.Point(11, 23);
            this.StartShootButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartShootButton.Name = "StartShootButton";
            this.StartShootButton.Size = new System.Drawing.Size(99, 34);
            this.StartShootButton.TabIndex = 4;
            this.StartShootButton.Text = "Start Shoot";
            this.StartShootButton.UseVisualStyleBackColor = true;
            this.StartShootButton.Click += new System.EventHandler(this.StartShootButton_Click);
            // 
            // cleanClassificationDirectory
            // 
            this.cleanClassificationDirectory.ForeColor = System.Drawing.Color.Black;
            this.cleanClassificationDirectory.Location = new System.Drawing.Point(27, 223);
            this.cleanClassificationDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cleanClassificationDirectory.Name = "cleanClassificationDirectory";
            this.cleanClassificationDirectory.Size = new System.Drawing.Size(137, 34);
            this.cleanClassificationDirectory.TabIndex = 3;
            this.cleanClassificationDirectory.Text = "Clean SVM";
            this.cleanClassificationDirectory.UseVisualStyleBackColor = true;
            this.cleanClassificationDirectory.Click += new System.EventHandler(this.cleanClassificationDirectory_Click);
            // 
            // CopyDirectory
            // 
            this.CopyDirectory.ForeColor = System.Drawing.Color.Black;
            this.CopyDirectory.Location = new System.Drawing.Point(27, 181);
            this.CopyDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CopyDirectory.Name = "CopyDirectory";
            this.CopyDirectory.Size = new System.Drawing.Size(137, 34);
            this.CopyDirectory.TabIndex = 2;
            this.CopyDirectory.Text = "Copy";
            this.CopyDirectory.UseVisualStyleBackColor = true;
            this.CopyDirectory.Click += new System.EventHandler(this.CopyDirectory_Click);
            // 
            // ResetImages
            // 
            this.ResetImages.Cursor = System.Windows.Forms.Cursors.Default;
            this.ResetImages.ForeColor = System.Drawing.Color.Black;
            this.ResetImages.Location = new System.Drawing.Point(27, 142);
            this.ResetImages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ResetImages.Name = "ResetImages";
            this.ResetImages.Size = new System.Drawing.Size(137, 34);
            this.ResetImages.TabIndex = 1;
            this.ResetImages.Text = "Reset";
            this.ResetImages.UseVisualStyleBackColor = true;
            this.ResetImages.Click += new System.EventHandler(this.ResetImages_Click);
            // 
            // AddImagesButtom
            // 
            this.AddImagesButtom.ForeColor = System.Drawing.Color.Black;
            this.AddImagesButtom.Location = new System.Drawing.Point(27, 102);
            this.AddImagesButtom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddImagesButtom.Name = "AddImagesButtom";
            this.AddImagesButtom.Size = new System.Drawing.Size(137, 34);
            this.AddImagesButtom.TabIndex = 0;
            this.AddImagesButtom.Text = "Add Images";
            this.AddImagesButtom.UseVisualStyleBackColor = true;
            this.AddImagesButtom.Click += new System.EventHandler(this.AddImagesButtom_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.MaxANumericUpDown);
            this.groupBox1.Controls.Add(this.MinANumericUpDown);
            this.groupBox1.Controls.Add(this.MaxAreaLabel);
            this.groupBox1.Controls.Add(this.MinAreaLabel);
            this.groupBox1.Controls.Add(this.SetButton);
            this.groupBox1.Controls.Add(this.MDNumericUpDown);
            this.groupBox1.Controls.Add(this.MVNumericUpDown);
            this.groupBox1.Controls.Add(this.DeltaNumericUpDown);
            this.groupBox1.Controls.Add(this.MinDiversityLabel);
            this.groupBox1.Controls.Add(this.MaxVariationLabel);
            this.groupBox1.Controls.Add(this.DeltaMserLabel);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(435, 562);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(185, 274);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mser properties";
            // 
            // MaxANumericUpDown
            // 
            this.MaxANumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MaxANumericUpDown.Location = new System.Drawing.Point(89, 177);
            this.MaxANumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaxANumericUpDown.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.MaxANumericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxANumericUpDown.Name = "MaxANumericUpDown";
            this.MaxANumericUpDown.Size = new System.Drawing.Size(85, 26);
            this.MaxANumericUpDown.TabIndex = 29;
            this.MaxANumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxANumericUpDown.Value = new decimal(new int[] {
            14400,
            0,
            0,
            0});
            // 
            // MinANumericUpDown
            // 
            this.MinANumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MinANumericUpDown.Location = new System.Drawing.Point(89, 140);
            this.MinANumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinANumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MinANumericUpDown.Name = "MinANumericUpDown";
            this.MinANumericUpDown.Size = new System.Drawing.Size(85, 26);
            this.MinANumericUpDown.TabIndex = 28;
            this.MinANumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MinANumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // MaxAreaLabel
            // 
            this.MaxAreaLabel.Location = new System.Drawing.Point(9, 180);
            this.MaxAreaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MaxAreaLabel.Name = "MaxAreaLabel";
            this.MaxAreaLabel.Size = new System.Drawing.Size(69, 28);
            this.MaxAreaLabel.TabIndex = 27;
            this.MaxAreaLabel.Text = "Max Area";
            // 
            // MinAreaLabel
            // 
            this.MinAreaLabel.Location = new System.Drawing.Point(9, 150);
            this.MinAreaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MinAreaLabel.Name = "MinAreaLabel";
            this.MinAreaLabel.Size = new System.Drawing.Size(65, 21);
            this.MinAreaLabel.TabIndex = 26;
            this.MinAreaLabel.Text = "Min Area";
            // 
            // SetButton
            // 
            this.SetButton.ForeColor = System.Drawing.Color.Black;
            this.SetButton.Location = new System.Drawing.Point(89, 234);
            this.SetButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(85, 28);
            this.SetButton.TabIndex = 25;
            this.SetButton.Text = "Set";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // MDNumericUpDown
            // 
            this.MDNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MDNumericUpDown.Location = new System.Drawing.Point(89, 102);
            this.MDNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MDNumericUpDown.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.MDNumericUpDown.Name = "MDNumericUpDown";
            this.MDNumericUpDown.Size = new System.Drawing.Size(85, 26);
            this.MDNumericUpDown.TabIndex = 24;
            this.MDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MDNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // MVNumericUpDown
            // 
            this.MVNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MVNumericUpDown.Location = new System.Drawing.Point(89, 64);
            this.MVNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MVNumericUpDown.Name = "MVNumericUpDown";
            this.MVNumericUpDown.Size = new System.Drawing.Size(85, 26);
            this.MVNumericUpDown.TabIndex = 23;
            this.MVNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MVNumericUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // DeltaNumericUpDown
            // 
            this.DeltaNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DeltaNumericUpDown.Location = new System.Drawing.Point(89, 25);
            this.DeltaNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeltaNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.DeltaNumericUpDown.Name = "DeltaNumericUpDown";
            this.DeltaNumericUpDown.Size = new System.Drawing.Size(85, 26);
            this.DeltaNumericUpDown.TabIndex = 22;
            this.DeltaNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DeltaNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // MinDiversityLabel
            // 
            this.MinDiversityLabel.Location = new System.Drawing.Point(9, 103);
            this.MinDiversityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MinDiversityLabel.Name = "MinDiversityLabel";
            this.MinDiversityLabel.Size = new System.Drawing.Size(65, 39);
            this.MinDiversityLabel.TabIndex = 4;
            this.MinDiversityLabel.Text = "Min Diversity";
            // 
            // MaxVariationLabel
            // 
            this.MaxVariationLabel.Location = new System.Drawing.Point(8, 62);
            this.MaxVariationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MaxVariationLabel.Name = "MaxVariationLabel";
            this.MaxVariationLabel.Size = new System.Drawing.Size(65, 39);
            this.MaxVariationLabel.TabIndex = 2;
            this.MaxVariationLabel.Text = "Max Variation";
            // 
            // DeltaMserLabel
            // 
            this.DeltaMserLabel.AutoSize = true;
            this.DeltaMserLabel.Location = new System.Drawing.Point(9, 30);
            this.DeltaMserLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeltaMserLabel.Name = "DeltaMserLabel";
            this.DeltaMserLabel.Size = new System.Drawing.Size(41, 17);
            this.DeltaMserLabel.TabIndex = 0;
            this.DeltaMserLabel.Text = "Delta";
            // 
            // ImageListView
            // 
            this.ImageListView.BackColor = System.Drawing.Color.Black;
            this.ImageListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Image_columnHeader,
            this.DateTime_columnHeader,
            this.Imagelatitude_columnHeader,
            this.ImageLongitude_columnHeader,
            this.sourceFile_columnHeader});
            this.ImageListView.ContextMenuStrip = this.ImageContextMenuStrip;
            this.ImageListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageListView.ForeColor = System.Drawing.Color.White;
            this.ImageListView.FullRowSelect = true;
            this.ImageListView.HideSelection = false;
            this.ImageListView.LargeImageList = this.LargeImageList1;
            this.ImageListView.Location = new System.Drawing.Point(4, 4);
            this.ImageListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImageListView.Name = "ImageListView";
            this.ImageListView.Size = new System.Drawing.Size(893, 514);
            this.ImageListView.SmallImageList = this.SmallImageList1;
            this.ImageListView.TabIndex = 10;
            this.ImageListView.UseCompatibleStateImageBehavior = false;
            this.ImageListView.View = System.Windows.Forms.View.Details;
            this.ImageListView.SelectedIndexChanged += new System.EventHandler(this.ImageListView_SelectedIndexChange);
            this.ImageListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageListView_KeyDown);
            this.ImageListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ImageListView_MouseDoubleClick);
            // 
            // Image_columnHeader
            // 
            this.Image_columnHeader.Text = "Image";
            this.Image_columnHeader.Width = 88;
            // 
            // DateTime_columnHeader
            // 
            this.DateTime_columnHeader.Text = "Date Time";
            this.DateTime_columnHeader.Width = 93;
            // 
            // Imagelatitude_columnHeader
            // 
            this.Imagelatitude_columnHeader.Text = "Image Latitude";
            this.Imagelatitude_columnHeader.Width = 89;
            // 
            // ImageLongitude_columnHeader
            // 
            this.ImageLongitude_columnHeader.Text = "Image Longitude";
            this.ImageLongitude_columnHeader.Width = 97;
            // 
            // sourceFile_columnHeader
            // 
            this.sourceFile_columnHeader.Text = "source file";
            this.sourceFile_columnHeader.Width = 73;
            // 
            // ImageContextMenuStrip
            // 
            this.ImageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImageViewToolStripMenuItem,
            this.ImageDeleteToolStripMenuItem});
            this.ImageContextMenuStrip.Name = "contextMenuStrip";
            this.ImageContextMenuStrip.Size = new System.Drawing.Size(123, 52);
            this.ImageContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ImageContextMenuStrip_Opening);
            // 
            // ImageViewToolStripMenuItem
            // 
            this.ImageViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImageLargeIconsToolStripMenuItem,
            this.ImageSmallIconsToolStripMenuItem,
            this.ImageDetailesToolStripMenuItem});
            this.ImageViewToolStripMenuItem.Name = "ImageViewToolStripMenuItem";
            this.ImageViewToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.ImageViewToolStripMenuItem.Text = "View";
            // 
            // ImageLargeIconsToolStripMenuItem
            // 
            this.ImageLargeIconsToolStripMenuItem.Name = "ImageLargeIconsToolStripMenuItem";
            this.ImageLargeIconsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.ImageLargeIconsToolStripMenuItem.Text = "Large Icons";
            this.ImageLargeIconsToolStripMenuItem.Click += new System.EventHandler(this.ImageLargeIconsToolStripMenuItem_Click);
            // 
            // ImageSmallIconsToolStripMenuItem
            // 
            this.ImageSmallIconsToolStripMenuItem.Name = "ImageSmallIconsToolStripMenuItem";
            this.ImageSmallIconsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.ImageSmallIconsToolStripMenuItem.Text = "Small Icons";
            this.ImageSmallIconsToolStripMenuItem.Click += new System.EventHandler(this.ImageSmallIconsToolStripMenuItem_Click);
            // 
            // ImageDetailesToolStripMenuItem
            // 
            this.ImageDetailesToolStripMenuItem.Name = "ImageDetailesToolStripMenuItem";
            this.ImageDetailesToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.ImageDetailesToolStripMenuItem.Text = "Detailes";
            this.ImageDetailesToolStripMenuItem.Click += new System.EventHandler(this.ImageDetailesToolStripMenuItem_Click);
            // 
            // ImageDeleteToolStripMenuItem
            // 
            this.ImageDeleteToolStripMenuItem.Name = "ImageDeleteToolStripMenuItem";
            this.ImageDeleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.ImageDeleteToolStripMenuItem.Text = "Delete";
            this.ImageDeleteToolStripMenuItem.Click += new System.EventHandler(this.ImageDeleteToolStripMenuItem_Click);
            // 
            // LargeImageList1
            // 
            this.LargeImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.LargeImageList1.ImageSize = new System.Drawing.Size(100, 100);
            this.LargeImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SmallImageList1
            // 
            this.SmallImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SmallImageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TargetImagePictureBox
            // 
            this.TargetImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetImagePictureBox.BackColor = System.Drawing.Color.Black;
            this.TargetImagePictureBox.Location = new System.Drawing.Point(4, 0);
            this.TargetImagePictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TargetImagePictureBox.Name = "TargetImagePictureBox";
            this.TargetImagePictureBox.Size = new System.Drawing.Size(515, 494);
            this.TargetImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TargetImagePictureBox.TabIndex = 11;
            this.TargetImagePictureBox.TabStop = false;
            this.TargetImagePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.TargetImagePictureBox_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Location = new System.Drawing.Point(241, 164);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.TabControl);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.TargetInfoGroupBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.TargetImagePictureBox);
            this.splitContainer1.Size = new System.Drawing.Size(1441, 843);
            this.splitContainer1.SplitterDistance = 917;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 12;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.ImagesTabPage);
            this.TabControl.Controls.Add(this.CropsTabPage);
            this.TabControl.Controls.Add(this.TrueTargetsTabPage);
            this.TabControl.Location = new System.Drawing.Point(4, 4);
            this.TabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(909, 551);
            this.TabControl.TabIndex = 21;
            this.TabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // ImagesTabPage
            // 
            this.ImagesTabPage.BackColor = System.Drawing.Color.Black;
            this.ImagesTabPage.Controls.Add(this.ImageListView);
            this.ImagesTabPage.ForeColor = System.Drawing.Color.White;
            this.ImagesTabPage.Location = new System.Drawing.Point(4, 25);
            this.ImagesTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImagesTabPage.Name = "ImagesTabPage";
            this.ImagesTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImagesTabPage.Size = new System.Drawing.Size(901, 522);
            this.ImagesTabPage.TabIndex = 0;
            this.ImagesTabPage.Text = "Images";
            // 
            // CropsTabPage
            // 
            this.CropsTabPage.BackColor = System.Drawing.Color.Black;
            this.CropsTabPage.Controls.Add(this.CropListView);
            this.CropsTabPage.Location = new System.Drawing.Point(4, 25);
            this.CropsTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CropsTabPage.Name = "CropsTabPage";
            this.CropsTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CropsTabPage.Size = new System.Drawing.Size(901, 522);
            this.CropsTabPage.TabIndex = 1;
            this.CropsTabPage.Text = "Crops";
            // 
            // CropListView
            // 
            this.CropListView.BackColor = System.Drawing.Color.Black;
            this.CropListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.CropListView.ContextMenuStrip = this.CropContextMenuStrip;
            this.CropListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CropListView.ForeColor = System.Drawing.Color.White;
            this.CropListView.FullRowSelect = true;
            this.CropListView.HideSelection = false;
            this.CropListView.LargeImageList = this.LargeImageList2;
            this.CropListView.Location = new System.Drawing.Point(4, 4);
            this.CropListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CropListView.Name = "CropListView";
            this.CropListView.Size = new System.Drawing.Size(893, 514);
            this.CropListView.SmallImageList = this.SmallImageList2;
            this.CropListView.TabIndex = 11;
            this.CropListView.UseCompatibleStateImageBehavior = false;
            this.CropListView.View = System.Windows.Forms.View.Details;
            this.CropListView.SelectedIndexChanged += new System.EventHandler(this.CropListView_SelectedIndexChanged);
            this.CropListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CropListView_KeyDown);
            this.CropListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CropListView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Image";
            this.columnHeader1.Width = 88;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date Time";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Image Latitude";
            this.columnHeader3.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Image Longitude";
            this.columnHeader4.Width = 97;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "source file";
            this.columnHeader5.Width = 73;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "isReatTarget";
            this.columnHeader6.Width = 89;
            // 
            // CropContextMenuStrip
            // 
            this.CropContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CropViewToolStripMenuItem,
            this.CropDeleteToolStripMenuItem});
            this.CropContextMenuStrip.Name = "CropContextMenuStrip";
            this.CropContextMenuStrip.Size = new System.Drawing.Size(123, 52);
            this.CropContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.CropContextMenuStrip_Opening);
            // 
            // CropViewToolStripMenuItem
            // 
            this.CropViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CropLargeIconsToolStripMenuItem,
            this.CropSmallIconsToolStripMenuItem,
            this.CropDetailesToolStripMenuItem});
            this.CropViewToolStripMenuItem.Name = "CropViewToolStripMenuItem";
            this.CropViewToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.CropViewToolStripMenuItem.Text = "View";
            // 
            // CropLargeIconsToolStripMenuItem
            // 
            this.CropLargeIconsToolStripMenuItem.Name = "CropLargeIconsToolStripMenuItem";
            this.CropLargeIconsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.CropLargeIconsToolStripMenuItem.Text = "Large Icons";
            this.CropLargeIconsToolStripMenuItem.Click += new System.EventHandler(this.CropLargeIconsToolStripMenuItem_Click);
            // 
            // CropSmallIconsToolStripMenuItem
            // 
            this.CropSmallIconsToolStripMenuItem.Name = "CropSmallIconsToolStripMenuItem";
            this.CropSmallIconsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.CropSmallIconsToolStripMenuItem.Text = "Small Icons";
            this.CropSmallIconsToolStripMenuItem.Click += new System.EventHandler(this.CropSmallIconsToolStripMenuItem_Click);
            // 
            // CropDetailesToolStripMenuItem
            // 
            this.CropDetailesToolStripMenuItem.Name = "CropDetailesToolStripMenuItem";
            this.CropDetailesToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.CropDetailesToolStripMenuItem.Text = "Detailes";
            this.CropDetailesToolStripMenuItem.Click += new System.EventHandler(this.CropDetailesToolStripMenuItem_Click);
            // 
            // CropDeleteToolStripMenuItem
            // 
            this.CropDeleteToolStripMenuItem.Name = "CropDeleteToolStripMenuItem";
            this.CropDeleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.CropDeleteToolStripMenuItem.Text = "Delete";
            this.CropDeleteToolStripMenuItem.Click += new System.EventHandler(this.CropDeleteToolStripMenuItem_Click);
            // 
            // LargeImageList2
            // 
            this.LargeImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.LargeImageList2.ImageSize = new System.Drawing.Size(100, 100);
            this.LargeImageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SmallImageList2
            // 
            this.SmallImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SmallImageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallImageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TrueTargetsTabPage
            // 
            this.TrueTargetsTabPage.BackColor = System.Drawing.Color.Black;
            this.TrueTargetsTabPage.Controls.Add(this.TrueTargetsListView);
            this.TrueTargetsTabPage.ForeColor = System.Drawing.Color.Black;
            this.TrueTargetsTabPage.Location = new System.Drawing.Point(4, 25);
            this.TrueTargetsTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TrueTargetsTabPage.Name = "TrueTargetsTabPage";
            this.TrueTargetsTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TrueTargetsTabPage.Size = new System.Drawing.Size(901, 522);
            this.TrueTargetsTabPage.TabIndex = 2;
            this.TrueTargetsTabPage.Text = "True Targets";
            // 
            // TrueTargetsListView
            // 
            this.TrueTargetsListView.BackColor = System.Drawing.Color.Black;
            this.TrueTargetsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.TrueTargetsListView.ContextMenuStrip = this.TrueTargetsContextMenuStrip;
            this.TrueTargetsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrueTargetsListView.ForeColor = System.Drawing.Color.White;
            this.TrueTargetsListView.FullRowSelect = true;
            this.TrueTargetsListView.HideSelection = false;
            this.TrueTargetsListView.LargeImageList = this.LargeTrueImageList;
            this.TrueTargetsListView.Location = new System.Drawing.Point(4, 4);
            this.TrueTargetsListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TrueTargetsListView.Name = "TrueTargetsListView";
            this.TrueTargetsListView.Size = new System.Drawing.Size(893, 514);
            this.TrueTargetsListView.SmallImageList = this.SmallTrueImageList;
            this.TrueTargetsListView.TabIndex = 12;
            this.TrueTargetsListView.UseCompatibleStateImageBehavior = false;
            this.TrueTargetsListView.View = System.Windows.Forms.View.Details;
            this.TrueTargetsListView.SelectedIndexChanged += new System.EventHandler(this.TrueTargetsListView_SelectedIndexChanged);
            this.TrueTargetsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TrueTargetsListView_KeyDown);
            this.TrueTargetsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrueTargetsListView_MouseDoubleClick);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Image";
            this.columnHeader7.Width = 88;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Date Time";
            this.columnHeader8.Width = 93;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Image Latitude";
            this.columnHeader9.Width = 89;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Image Longitude";
            this.columnHeader10.Width = 97;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "source file";
            this.columnHeader11.Width = 73;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "isReatTarget";
            this.columnHeader12.Width = 89;
            // 
            // TrueTargetsContextMenuStrip
            // 
            this.TrueTargetsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrueTargetsViewToolStripMenuItem,
            this.TrueTaregtsDeleteToolStripMenuItem});
            this.TrueTargetsContextMenuStrip.Name = "contextMenuStrip";
            this.TrueTargetsContextMenuStrip.Size = new System.Drawing.Size(123, 52);
            this.TrueTargetsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.TrueTargetsContextMenuStrip_Opening);
            // 
            // TrueTargetsViewToolStripMenuItem
            // 
            this.TrueTargetsViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TrueTargetsLargeIconsToolStripMenuItem2,
            this.TrueTargetsSmallIconsToolStripMenuItem,
            this.TrueTargetsDetailesToolStripMenuItem});
            this.TrueTargetsViewToolStripMenuItem.Name = "TrueTargetsViewToolStripMenuItem";
            this.TrueTargetsViewToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.TrueTargetsViewToolStripMenuItem.Text = "View";
            // 
            // TrueTargetsLargeIconsToolStripMenuItem2
            // 
            this.TrueTargetsLargeIconsToolStripMenuItem2.Name = "TrueTargetsLargeIconsToolStripMenuItem2";
            this.TrueTargetsLargeIconsToolStripMenuItem2.Size = new System.Drawing.Size(153, 24);
            this.TrueTargetsLargeIconsToolStripMenuItem2.Text = "Large Icons";
            this.TrueTargetsLargeIconsToolStripMenuItem2.Click += new System.EventHandler(this.TrueTargetsLargeIconsToolStripMenuItem_Click);
            // 
            // TrueTargetsSmallIconsToolStripMenuItem
            // 
            this.TrueTargetsSmallIconsToolStripMenuItem.Name = "TrueTargetsSmallIconsToolStripMenuItem";
            this.TrueTargetsSmallIconsToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.TrueTargetsSmallIconsToolStripMenuItem.Text = "Small Icons";
            this.TrueTargetsSmallIconsToolStripMenuItem.Click += new System.EventHandler(this.TrueTargetsSmallIconsToolStripMenuItem_Click);
            // 
            // TrueTargetsDetailesToolStripMenuItem
            // 
            this.TrueTargetsDetailesToolStripMenuItem.Name = "TrueTargetsDetailesToolStripMenuItem";
            this.TrueTargetsDetailesToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.TrueTargetsDetailesToolStripMenuItem.Text = "Detailes";
            this.TrueTargetsDetailesToolStripMenuItem.Click += new System.EventHandler(this.TrueTargetsDetailesToolStripMenuItem_Click);
            // 
            // TrueTaregtsDeleteToolStripMenuItem
            // 
            this.TrueTaregtsDeleteToolStripMenuItem.Name = "TrueTaregtsDeleteToolStripMenuItem";
            this.TrueTaregtsDeleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.TrueTaregtsDeleteToolStripMenuItem.Text = "Delete";
            this.TrueTaregtsDeleteToolStripMenuItem.Click += new System.EventHandler(this.TrueTargetsDeleteToolStripMenuItem_Click);
            // 
            // LargeTrueImageList
            // 
            this.LargeTrueImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.LargeTrueImageList.ImageSize = new System.Drawing.Size(100, 100);
            this.LargeTrueImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SmallTrueImageList
            // 
            this.SmallTrueImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SmallTrueImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SmallTrueImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TargetInfoGroupBox
            // 
            this.TargetInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TargetInfoGroupBox.BackColor = System.Drawing.Color.Black;
            this.TargetInfoGroupBox.Controls.Add(this._OrientationLabel);
            this.TargetInfoGroupBox.Controls.Add(this.OrientationLabel);
            this.TargetInfoGroupBox.Controls.Add(this._YawLabel);
            this.TargetInfoGroupBox.Controls.Add(this.YawLabel);
            this.TargetInfoGroupBox.Controls.Add(this._LetterColorLabel);
            this.TargetInfoGroupBox.Controls.Add(this._LetterLabel);
            this.TargetInfoGroupBox.Controls.Add(this._ShapeColorLabel);
            this.TargetInfoGroupBox.Controls.Add(this._ShapeLabel);
            this.TargetInfoGroupBox.Controls.Add(this._TargetLongitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this._TargetLatitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this._ImageLongitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this._ImageLatitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this._ImageNameLabel);
            this.TargetInfoGroupBox.Controls.Add(this.ImageNameLabel);
            this.TargetInfoGroupBox.Controls.Add(this.LetterColorLabel);
            this.TargetInfoGroupBox.Controls.Add(this.ImageLatitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this.LetterLabel);
            this.TargetInfoGroupBox.Controls.Add(this.ImageLongitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this.ShapeColorLabel);
            this.TargetInfoGroupBox.Controls.Add(this.TargetLatitudeLabel);
            this.TargetInfoGroupBox.Controls.Add(this.ShapeLabel);
            this.TargetInfoGroupBox.Controls.Add(this.TargetLongitudeLabel);
            this.TargetInfoGroupBox.ForeColor = System.Drawing.Color.White;
            this.TargetInfoGroupBox.Location = new System.Drawing.Point(1, 560);
            this.TargetInfoGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TargetInfoGroupBox.Name = "TargetInfoGroupBox";
            this.TargetInfoGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TargetInfoGroupBox.Size = new System.Drawing.Size(424, 277);
            this.TargetInfoGroupBox.TabIndex = 20;
            this.TargetInfoGroupBox.TabStop = false;
            this.TargetInfoGroupBox.Text = "Target Info";
            // 
            // _OrientationLabel
            // 
            this._OrientationLabel.AutoSize = true;
            this._OrientationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._OrientationLabel.ForeColor = System.Drawing.Color.Red;
            this._OrientationLabel.Location = new System.Drawing.Point(137, 223);
            this._OrientationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._OrientationLabel.Name = "_OrientationLabel";
            this._OrientationLabel.Size = new System.Drawing.Size(0, 24);
            this._OrientationLabel.TabIndex = 32;
            // 
            // OrientationLabel
            // 
            this.OrientationLabel.AutoSize = true;
            this.OrientationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OrientationLabel.Location = new System.Drawing.Point(20, 223);
            this.OrientationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OrientationLabel.Name = "OrientationLabel";
            this.OrientationLabel.Size = new System.Drawing.Size(106, 24);
            this.OrientationLabel.TabIndex = 31;
            this.OrientationLabel.Text = "Orientation:";
            // 
            // _YawLabel
            // 
            this._YawLabel.AutoSize = true;
            this._YawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._YawLabel.ForeColor = System.Drawing.Color.Red;
            this._YawLabel.Location = new System.Drawing.Point(81, 245);
            this._YawLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._YawLabel.Name = "_YawLabel";
            this._YawLabel.Size = new System.Drawing.Size(0, 24);
            this._YawLabel.TabIndex = 30;
            // 
            // YawLabel
            // 
            this.YawLabel.AutoSize = true;
            this.YawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.YawLabel.Location = new System.Drawing.Point(20, 245);
            this.YawLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.YawLabel.Name = "YawLabel";
            this.YawLabel.Size = new System.Drawing.Size(51, 24);
            this.YawLabel.TabIndex = 29;
            this.YawLabel.Text = "Yaw:";
            // 
            // _LetterColorLabel
            // 
            this._LetterColorLabel.AutoSize = true;
            this._LetterColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._LetterColorLabel.ForeColor = System.Drawing.Color.Red;
            this._LetterColorLabel.Location = new System.Drawing.Point(148, 198);
            this._LetterColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._LetterColorLabel.Name = "_LetterColorLabel";
            this._LetterColorLabel.Size = new System.Drawing.Size(0, 24);
            this._LetterColorLabel.TabIndex = 28;
            // 
            // _LetterLabel
            // 
            this._LetterLabel.AutoSize = true;
            this._LetterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._LetterLabel.ForeColor = System.Drawing.Color.Red;
            this._LetterLabel.Location = new System.Drawing.Point(89, 176);
            this._LetterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._LetterLabel.Name = "_LetterLabel";
            this._LetterLabel.Size = new System.Drawing.Size(0, 24);
            this._LetterLabel.TabIndex = 27;
            // 
            // _ShapeColorLabel
            // 
            this._ShapeColorLabel.AutoSize = true;
            this._ShapeColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ShapeColorLabel.ForeColor = System.Drawing.Color.Red;
            this._ShapeColorLabel.Location = new System.Drawing.Point(148, 154);
            this._ShapeColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._ShapeColorLabel.Name = "_ShapeColorLabel";
            this._ShapeColorLabel.Size = new System.Drawing.Size(0, 24);
            this._ShapeColorLabel.TabIndex = 26;
            // 
            // _ShapeLabel
            // 
            this._ShapeLabel.AutoSize = true;
            this._ShapeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ShapeLabel.ForeColor = System.Drawing.Color.Red;
            this._ShapeLabel.Location = new System.Drawing.Point(95, 132);
            this._ShapeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._ShapeLabel.Name = "_ShapeLabel";
            this._ShapeLabel.Size = new System.Drawing.Size(0, 24);
            this._ShapeLabel.TabIndex = 25;
            // 
            // _TargetLongitudeLabel
            // 
            this._TargetLongitudeLabel.AutoSize = true;
            this._TargetLongitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._TargetLongitudeLabel.ForeColor = System.Drawing.Color.Red;
            this._TargetLongitudeLabel.Location = new System.Drawing.Point(183, 110);
            this._TargetLongitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._TargetLongitudeLabel.Name = "_TargetLongitudeLabel";
            this._TargetLongitudeLabel.Size = new System.Drawing.Size(0, 24);
            this._TargetLongitudeLabel.TabIndex = 24;
            // 
            // _TargetLatitudeLabel
            // 
            this._TargetLatitudeLabel.AutoSize = true;
            this._TargetLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._TargetLatitudeLabel.ForeColor = System.Drawing.Color.Red;
            this._TargetLatitudeLabel.Location = new System.Drawing.Point(168, 87);
            this._TargetLatitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._TargetLatitudeLabel.Name = "_TargetLatitudeLabel";
            this._TargetLatitudeLabel.Size = new System.Drawing.Size(0, 24);
            this._TargetLatitudeLabel.TabIndex = 23;
            // 
            // _ImageLongitudeLabel
            // 
            this._ImageLongitudeLabel.AutoSize = true;
            this._ImageLongitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ImageLongitudeLabel.ForeColor = System.Drawing.Color.Red;
            this._ImageLongitudeLabel.Location = new System.Drawing.Point(185, 65);
            this._ImageLongitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._ImageLongitudeLabel.Name = "_ImageLongitudeLabel";
            this._ImageLongitudeLabel.Size = new System.Drawing.Size(0, 24);
            this._ImageLongitudeLabel.TabIndex = 22;
            // 
            // _ImageLatitudeLabel
            // 
            this._ImageLatitudeLabel.AutoSize = true;
            this._ImageLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ImageLatitudeLabel.ForeColor = System.Drawing.Color.Red;
            this._ImageLatitudeLabel.Location = new System.Drawing.Point(167, 43);
            this._ImageLatitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._ImageLatitudeLabel.Name = "_ImageLatitudeLabel";
            this._ImageLatitudeLabel.Size = new System.Drawing.Size(0, 24);
            this._ImageLatitudeLabel.TabIndex = 21;
            // 
            // _ImageNameLabel
            // 
            this._ImageNameLabel.AutoSize = true;
            this._ImageNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ImageNameLabel.ForeColor = System.Drawing.Color.Red;
            this._ImageNameLabel.Location = new System.Drawing.Point(153, 21);
            this._ImageNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._ImageNameLabel.Name = "_ImageNameLabel";
            this._ImageNameLabel.Size = new System.Drawing.Size(0, 24);
            this._ImageNameLabel.TabIndex = 20;
            // 
            // ImageNameLabel
            // 
            this.ImageNameLabel.AutoSize = true;
            this.ImageNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ImageNameLabel.Location = new System.Drawing.Point(20, 21);
            this.ImageNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ImageNameLabel.Name = "ImageNameLabel";
            this.ImageNameLabel.Size = new System.Drawing.Size(123, 24);
            this.ImageNameLabel.TabIndex = 11;
            this.ImageNameLabel.Text = "Image Name:";
            // 
            // LetterColorLabel
            // 
            this.LetterColorLabel.AutoSize = true;
            this.LetterColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LetterColorLabel.Location = new System.Drawing.Point(20, 198);
            this.LetterColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LetterColorLabel.Name = "LetterColorLabel";
            this.LetterColorLabel.Size = new System.Drawing.Size(111, 24);
            this.LetterColorLabel.TabIndex = 19;
            this.LetterColorLabel.Text = "Letter Color:";
            // 
            // ImageLatitudeLabel
            // 
            this.ImageLatitudeLabel.AutoSize = true;
            this.ImageLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ImageLatitudeLabel.Location = new System.Drawing.Point(20, 43);
            this.ImageLatitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ImageLatitudeLabel.Name = "ImageLatitudeLabel";
            this.ImageLatitudeLabel.Size = new System.Drawing.Size(137, 24);
            this.ImageLatitudeLabel.TabIndex = 12;
            this.ImageLatitudeLabel.Text = "Image Latitude:";
            // 
            // LetterLabel
            // 
            this.LetterLabel.AutoSize = true;
            this.LetterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LetterLabel.Location = new System.Drawing.Point(20, 176);
            this.LetterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LetterLabel.Name = "LetterLabel";
            this.LetterLabel.Size = new System.Drawing.Size(61, 24);
            this.LetterLabel.TabIndex = 18;
            this.LetterLabel.Text = "Letter:";
            // 
            // ImageLongitudeLabel
            // 
            this.ImageLongitudeLabel.AutoSize = true;
            this.ImageLongitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ImageLongitudeLabel.Location = new System.Drawing.Point(20, 65);
            this.ImageLongitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ImageLongitudeLabel.Name = "ImageLongitudeLabel";
            this.ImageLongitudeLabel.Size = new System.Drawing.Size(156, 24);
            this.ImageLongitudeLabel.TabIndex = 13;
            this.ImageLongitudeLabel.Text = "Image Longitude:";
            // 
            // ShapeColorLabel
            // 
            this.ShapeColorLabel.AutoSize = true;
            this.ShapeColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ShapeColorLabel.Location = new System.Drawing.Point(20, 154);
            this.ShapeColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ShapeColorLabel.Name = "ShapeColorLabel";
            this.ShapeColorLabel.Size = new System.Drawing.Size(120, 24);
            this.ShapeColorLabel.TabIndex = 17;
            this.ShapeColorLabel.Text = "Shape Color:";
            // 
            // TargetLatitudeLabel
            // 
            this.TargetLatitudeLabel.AutoSize = true;
            this.TargetLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.TargetLatitudeLabel.Location = new System.Drawing.Point(20, 87);
            this.TargetLatitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TargetLatitudeLabel.Name = "TargetLatitudeLabel";
            this.TargetLatitudeLabel.Size = new System.Drawing.Size(139, 24);
            this.TargetLatitudeLabel.TabIndex = 14;
            this.TargetLatitudeLabel.Text = "Target Latitude:";
            // 
            // ShapeLabel
            // 
            this.ShapeLabel.AutoSize = true;
            this.ShapeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ShapeLabel.Location = new System.Drawing.Point(20, 132);
            this.ShapeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ShapeLabel.Name = "ShapeLabel";
            this.ShapeLabel.Size = new System.Drawing.Size(70, 24);
            this.ShapeLabel.TabIndex = 16;
            this.ShapeLabel.Text = "Shape:";
            // 
            // TargetLongitudeLabel
            // 
            this.TargetLongitudeLabel.AutoSize = true;
            this.TargetLongitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.TargetLongitudeLabel.Location = new System.Drawing.Point(20, 110);
            this.TargetLongitudeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TargetLongitudeLabel.Name = "TargetLongitudeLabel";
            this.TargetLongitudeLabel.Size = new System.Drawing.Size(158, 24);
            this.TargetLongitudeLabel.TabIndex = 15;
            this.TargetLongitudeLabel.Text = "Target Longitude:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::ImageProcessingControl.Properties.Resources.TASblack;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(103, 603);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(385, 187);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // upperMenuStrip
            // 
            this.upperMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.upperMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.upperMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.upperMenuStrip.Name = "upperMenuStrip";
            this.upperMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.upperMenuStrip.Size = new System.Drawing.Size(1685, 28);
            this.upperMenuStrip.TabIndex = 13;
            this.upperMenuStrip.Text = "menuStrip1";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteDBToolStripMenuItem,
            this.deleteDBAUVSToolStripMenuItem});
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // deleteDBToolStripMenuItem
            // 
            this.deleteDBToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.deleteDBToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.deleteDBToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.DimGray;
            this.deleteDBToolStripMenuItem.Name = "deleteDBToolStripMenuItem";
            this.deleteDBToolStripMenuItem.Size = new System.Drawing.Size(205, 24);
            this.deleteDBToolStripMenuItem.Text = "Delete DB";
            this.deleteDBToolStripMenuItem.Click += new System.EventHandler(this.deleteDBToolStripMenuItem_Click);
            // 
            // deleteDBAUVSToolStripMenuItem
            // 
            this.deleteDBAUVSToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.deleteDBAUVSToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.deleteDBAUVSToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.deleteDBAUVSToolStripMenuItem.Name = "deleteDBAUVSToolStripMenuItem";
            this.deleteDBAUVSToolStripMenuItem.Size = new System.Drawing.Size(205, 24);
            this.deleteDBAUVSToolStripMenuItem.Text = "Delete DB + AUVSI";
            this.deleteDBAUVSToolStripMenuItem.Click += new System.EventHandler(this.deleteDBAUVSToolStripMenuItem_Click);
            // 
            // ImageProcessingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::ImageProcessingControl.Properties.Resources.BackgroundGUI;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1685, 1004);
            this.Controls.Add(this.upperMenuStrip);
            this.Controls.Add(this.StopwatchGroupBox);
            this.Controls.Add(this.ControlPanelGroupBox);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.upperMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImageProcessingDialog";
            this.Text = "GreyOwl ImageProcessing";
            this.TransparencyKey = System.Drawing.Color.FloralWhite;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageProcessingDialog_MouseMove);
            this.StopwatchGroupBox.ResumeLayout(false);
            this.StopwatchGroupBox.PerformLayout();
            this.ControlPanelGroupBox.ResumeLayout(false);
            this.SpeedTestGroupBox.ResumeLayout(false);
            this.SpeedTestGroupBox.PerformLayout();
            this.ImagesControlGroupBox.ResumeLayout(false);
            this.ImagesControlGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxANumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinANumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MDNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MVNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeltaNumericUpDown)).EndInit();
            this.ImageContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TargetImagePictureBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.ImagesTabPage.ResumeLayout(false);
            this.CropsTabPage.ResumeLayout(false);
            this.CropContextMenuStrip.ResumeLayout(false);
            this.TrueTargetsTabPage.ResumeLayout(false);
            this.TrueTargetsContextMenuStrip.ResumeLayout(false);
            this.TargetInfoGroupBox.ResumeLayout(false);
            this.TargetInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.upperMenuStrip.ResumeLayout(false);
            this.upperMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox StopwatchGroupBox;
        private System.Windows.Forms.TextBox StopwatchTextBox;
        private System.Windows.Forms.LinkLabel ResetStopwatchLinkLabel;
        private System.Windows.Forms.Button StartStopwatchButton;
        private System.Windows.Forms.Button stopStopwatchButton;
        private System.Windows.Forms.GroupBox ControlPanelGroupBox;
        private System.Windows.Forms.GroupBox ImagesControlGroupBox;
        private System.Windows.Forms.Button AddImagesButtom;
        private System.Windows.Forms.ListView ImageListView;
        private System.Windows.Forms.ColumnHeader Image_columnHeader;
        private System.Windows.Forms.ColumnHeader DateTime_columnHeader;
        private System.Windows.Forms.ColumnHeader Imagelatitude_columnHeader;
        private System.Windows.Forms.ColumnHeader ImageLongitude_columnHeader;
        private System.Windows.Forms.ColumnHeader sourceFile_columnHeader;
        private System.Windows.Forms.PictureBox TargetImagePictureBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label ImageNameLabel;
        private System.Windows.Forms.Label LetterLabel;
        private System.Windows.Forms.Label ShapeColorLabel;
        private System.Windows.Forms.Label ShapeLabel;
        private System.Windows.Forms.Label TargetLongitudeLabel;
        private System.Windows.Forms.Label TargetLatitudeLabel;
        private System.Windows.Forms.Label ImageLongitudeLabel;
        private System.Windows.Forms.Label ImageLatitudeLabel;
        private System.Windows.Forms.Label LetterColorLabel;
        private System.Windows.Forms.GroupBox TargetInfoGroupBox;
        private System.Windows.Forms.Button LiveFeedButton;
        private System.Windows.Forms.Button GMapButton;
        private System.Windows.Forms.ImageList LargeImageList1;
        private System.Windows.Forms.ImageList SmallImageList1;
        private System.Windows.Forms.ContextMenuStrip ImageContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ImageViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImageLargeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImageSmallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImageDetailesToolStripMenuItem;
        private System.Windows.Forms.Label _ImageLatitudeLabel;
        private System.Windows.Forms.Label _ImageNameLabel;
        private System.Windows.Forms.Label _LetterColorLabel;
        private System.Windows.Forms.Label _LetterLabel;
        private System.Windows.Forms.Label _ShapeColorLabel;
        private System.Windows.Forms.Label _ShapeLabel;
        private System.Windows.Forms.Label _TargetLongitudeLabel;
        private System.Windows.Forms.Label _TargetLatitudeLabel;
        private System.Windows.Forms.Label _ImageLongitudeLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox SpeedTestGroupBox;
        private System.Windows.Forms.Label PingLabel;
        private System.Windows.Forms.Button PingButton;
        private System.Windows.Forms.TextBox PingTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label DeltaMserLabel;
        private System.Windows.Forms.Label MaxVariationLabel;
        private System.Windows.Forms.Label MinDiversityLabel;
        private System.Windows.Forms.ToolStripMenuItem ImageDeleteToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage ImagesTabPage;
        private System.Windows.Forms.TabPage CropsTabPage;
        private System.Windows.Forms.ListView CropListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList LargeImageList2;
        private System.Windows.Forms.ImageList SmallImageList2;
        private System.Windows.Forms.ContextMenuStrip CropContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CropViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropLargeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropSmallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropDetailesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropDeleteToolStripMenuItem;
        private System.Windows.Forms.Button ResetImages;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button cleanClassificationDirectory;
        private System.Windows.Forms.Button CopyDirectory;
        private System.Windows.Forms.Button RunMserButton;
        private System.Windows.Forms.NumericUpDown MDNumericUpDown;
        private System.Windows.Forms.NumericUpDown MVNumericUpDown;
        private System.Windows.Forms.NumericUpDown DeltaNumericUpDown;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Button StartShootButton;
        private System.Windows.Forms.Button StopShootButton;
        private System.Windows.Forms.MenuStrip upperMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDBAUVSToolStripMenuItem;
        private System.Windows.Forms.TextBox zoomTextBox;
        private System.Windows.Forms.NumericUpDown MaxANumericUpDown;
        private System.Windows.Forms.NumericUpDown MinANumericUpDown;
        private System.Windows.Forms.Label MaxAreaLabel;
        private System.Windows.Forms.Label MinAreaLabel;
        private System.Windows.Forms.Label _YawLabel;
        private System.Windows.Forms.Label YawLabel;
        private System.Windows.Forms.TabPage TrueTargetsTabPage;
        private System.Windows.Forms.ListView TrueTargetsListView;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ImageList LargeTrueImageList;
        private System.Windows.Forms.ImageList SmallTrueImageList;
        private System.Windows.Forms.ContextMenuStrip TrueTargetsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TrueTargetsViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TrueTargetsLargeIconsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem TrueTargetsSmallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TrueTargetsDetailesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TrueTaregtsDeleteToolStripMenuItem;
        private System.Windows.Forms.Label OrientationLabel;
        private System.Windows.Forms.Label _OrientationLabel;
        private System.Windows.Forms.Button InitGpsHomeButton;
    }
}

