namespace GameEngine
{
    partial class Editor
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.decorRadioButton = new System.Windows.Forms.RadioButton();
            this.objectsRadioButton = new System.Windows.Forms.RadioButton();
            this.wallsRadioButton = new System.Windows.Forms.RadioButton();
            this.listBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.xPosition = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.yPosition = new System.Windows.Forms.NumericUpDown();
            this.wLabel = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.hLabel = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.objectTypes = new System.Windows.Forms.ListBox();
            this.imagePathLabel = new System.Windows.Forms.Label();
            this.imagePath = new System.Windows.Forms.TextBox();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.layerDepthLabel = new System.Windows.Forms.Label();
            this.layerDepth = new System.Windows.Forms.NumericUpDown();
            this.gameGroupBox = new System.Windows.Forms.GroupBox();
            this.resetNPC = new System.Windows.Forms.Button();
            this.paused = new System.Windows.Forms.CheckBox();
            this.mapSizeGroup = new System.Windows.Forms.GroupBox();
            this.drawGridCheckBox = new System.Windows.Forms.CheckBox();
            this.mapHeight = new System.Windows.Forms.NumericUpDown();
            this.mapWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.decorSourceHeightLabel = new System.Windows.Forms.Label();
            this.decorSourceHeight = new System.Windows.Forms.NumericUpDown();
            this.decorSourceWidthLabel = new System.Windows.Forms.Label();
            this.decorSourceWidth = new System.Windows.Forms.NumericUpDown();
            this.decorSourceYLabel = new System.Windows.Forms.Label();
            this.decorSourceY = new System.Windows.Forms.NumericUpDown();
            this.decorSourceXLabel = new System.Windows.Forms.Label();
            this.decorSourceX = new System.Windows.Forms.NumericUpDown();
            this.sourceRectangleLabel = new System.Windows.Forms.Label();
            this.drawSelected = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerDepth)).BeginInit();
            this.gameGroupBox.SuspendLayout();
            this.mapSizeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceX)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(284, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.MouseEnter += new System.EventHandler(this.menuStrip_MouseEnter);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.noneRadioButton);
            this.groupBox1.Controls.Add(this.decorRadioButton);
            this.groupBox1.Controls.Add(this.objectsRadioButton);
            this.groupBox1.Controls.Add(this.wallsRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create";
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Location = new System.Drawing.Point(6, 21);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.noneRadioButton.TabIndex = 5;
            this.noneRadioButton.Text = "None";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            this.noneRadioButton.CheckedChanged += new System.EventHandler(this.noneRadioButton_CheckedChanged);
            // 
            // decorRadioButton
            // 
            this.decorRadioButton.AutoSize = true;
            this.decorRadioButton.Location = new System.Drawing.Point(6, 90);
            this.decorRadioButton.Name = "decorRadioButton";
            this.decorRadioButton.Size = new System.Drawing.Size(54, 17);
            this.decorRadioButton.TabIndex = 4;
            this.decorRadioButton.Text = "Decor";
            this.decorRadioButton.UseVisualStyleBackColor = true;
            this.decorRadioButton.CheckedChanged += new System.EventHandler(this.decorRadioButton_CheckedChanged);
            // 
            // objectsRadioButton
            // 
            this.objectsRadioButton.AutoSize = true;
            this.objectsRadioButton.Location = new System.Drawing.Point(6, 67);
            this.objectsRadioButton.Name = "objectsRadioButton";
            this.objectsRadioButton.Size = new System.Drawing.Size(61, 17);
            this.objectsRadioButton.TabIndex = 3;
            this.objectsRadioButton.Text = "Objects";
            this.objectsRadioButton.UseVisualStyleBackColor = true;
            this.objectsRadioButton.CheckedChanged += new System.EventHandler(this.objectsRadioButton_CheckedChanged);
            // 
            // wallsRadioButton
            // 
            this.wallsRadioButton.AutoSize = true;
            this.wallsRadioButton.Location = new System.Drawing.Point(6, 43);
            this.wallsRadioButton.Name = "wallsRadioButton";
            this.wallsRadioButton.Size = new System.Drawing.Size(51, 17);
            this.wallsRadioButton.TabIndex = 2;
            this.wallsRadioButton.Text = "Walls";
            this.wallsRadioButton.UseVisualStyleBackColor = true;
            this.wallsRadioButton.CheckedChanged += new System.EventHandler(this.wallsRadioButton_CheckedChanged);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(6, 151);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(272, 147);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(34, 304);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(162, 304);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // xPosition
            // 
            this.xPosition.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.xPosition.Location = new System.Drawing.Point(17, 333);
            this.xPosition.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.xPosition.Minimum = new decimal(new int[] {
            90000,
            0,
            0,
            -2147483648});
            this.xPosition.Name = "xPosition";
            this.xPosition.Size = new System.Drawing.Size(50, 20);
            this.xPosition.TabIndex = 7;
            this.xPosition.ValueChanged += new System.EventHandler(this.xPosition_ValueChanged);
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(0, 335);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 13);
            this.xLabel.TabIndex = 8;
            this.xLabel.Text = "X:";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(68, 335);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 13);
            this.yLabel.TabIndex = 10;
            this.yLabel.Text = "Y:";
            // 
            // yPosition
            // 
            this.yPosition.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.yPosition.Location = new System.Drawing.Point(85, 333);
            this.yPosition.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.yPosition.Minimum = new decimal(new int[] {
            90000,
            0,
            0,
            -2147483648});
            this.yPosition.Name = "yPosition";
            this.yPosition.Size = new System.Drawing.Size(50, 20);
            this.yPosition.TabIndex = 9;
            this.yPosition.ValueChanged += new System.EventHandler(this.yPosition_ValueChanged);
            // 
            // wLabel
            // 
            this.wLabel.AutoSize = true;
            this.wLabel.Location = new System.Drawing.Point(137, 335);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(21, 13);
            this.wLabel.TabIndex = 12;
            this.wLabel.Text = "W:";
            // 
            // width
            // 
            this.width.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.width.Location = new System.Drawing.Point(158, 333);
            this.width.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(50, 20);
            this.width.TabIndex = 11;
            this.width.ValueChanged += new System.EventHandler(this.width_ValueChanged);
            // 
            // hLabel
            // 
            this.hLabel.AutoSize = true;
            this.hLabel.Location = new System.Drawing.Point(209, 335);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(18, 13);
            this.hLabel.TabIndex = 14;
            this.hLabel.Text = "H:";
            // 
            // height
            // 
            this.height.Increment = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.height.Location = new System.Drawing.Point(228, 333);
            this.height.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(50, 20);
            this.height.TabIndex = 13;
            this.height.ValueChanged += new System.EventHandler(this.height_ValueChanged);
            // 
            // objectTypes
            // 
            this.objectTypes.FormattingEnabled = true;
            this.objectTypes.Location = new System.Drawing.Point(3, 359);
            this.objectTypes.Name = "objectTypes";
            this.objectTypes.Size = new System.Drawing.Size(275, 56);
            this.objectTypes.TabIndex = 15;
            this.objectTypes.Visible = false;
            // 
            // imagePathLabel
            // 
            this.imagePathLabel.AutoSize = true;
            this.imagePathLabel.Location = new System.Drawing.Point(27, 364);
            this.imagePathLabel.Name = "imagePathLabel";
            this.imagePathLabel.Size = new System.Drawing.Size(39, 13);
            this.imagePathLabel.TabIndex = 5;
            this.imagePathLabel.Text = "Image:";
            this.imagePathLabel.Visible = false;
            // 
            // imagePath
            // 
            this.imagePath.Enabled = false;
            this.imagePath.Location = new System.Drawing.Point(66, 361);
            this.imagePath.Name = "imagePath";
            this.imagePath.Size = new System.Drawing.Size(100, 20);
            this.imagePath.TabIndex = 16;
            this.imagePath.Visible = false;
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(170, 359);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(75, 23);
            this.loadImageButton.TabIndex = 17;
            this.loadImageButton.Text = "Load";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Visible = false;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // layerDepthLabel
            // 
            this.layerDepthLabel.AutoSize = true;
            this.layerDepthLabel.Location = new System.Drawing.Point(27, 389);
            this.layerDepthLabel.Name = "layerDepthLabel";
            this.layerDepthLabel.Size = new System.Drawing.Size(39, 13);
            this.layerDepthLabel.TabIndex = 18;
            this.layerDepthLabel.Text = "Depth:";
            this.layerDepthLabel.Visible = false;
            // 
            // layerDepth
            // 
            this.layerDepth.DecimalPlaces = 3;
            this.layerDepth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.layerDepth.Location = new System.Drawing.Point(66, 387);
            this.layerDepth.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.layerDepth.Name = "layerDepth";
            this.layerDepth.Size = new System.Drawing.Size(53, 20);
            this.layerDepth.TabIndex = 19;
            this.layerDepth.Visible = false;
            this.layerDepth.ValueChanged += new System.EventHandler(this.layerDepth_ValueChanged);
            // 
            // gameGroupBox
            // 
            this.gameGroupBox.Controls.Add(this.drawSelected);
            this.gameGroupBox.Controls.Add(this.resetNPC);
            this.gameGroupBox.Controls.Add(this.paused);
            this.gameGroupBox.Location = new System.Drawing.Point(12, 510);
            this.gameGroupBox.Name = "gameGroupBox";
            this.gameGroupBox.Size = new System.Drawing.Size(127, 101);
            this.gameGroupBox.TabIndex = 21;
            this.gameGroupBox.TabStop = false;
            this.gameGroupBox.Text = "Game";
            // 
            // resetNPC
            // 
            this.resetNPC.Location = new System.Drawing.Point(7, 17);
            this.resetNPC.Name = "resetNPC";
            this.resetNPC.Size = new System.Drawing.Size(75, 23);
            this.resetNPC.TabIndex = 10;
            this.resetNPC.Text = "Reset";
            this.resetNPC.UseVisualStyleBackColor = true;
            this.resetNPC.Click += new System.EventHandler(this.resetNPC_Click);
            // 
            // paused
            // 
            this.paused.AutoSize = true;
            this.paused.Location = new System.Drawing.Point(7, 73);
            this.paused.Name = "paused";
            this.paused.Size = new System.Drawing.Size(62, 17);
            this.paused.TabIndex = 9;
            this.paused.Text = "Paused";
            this.paused.UseVisualStyleBackColor = true;
            this.paused.CheckedChanged += new System.EventHandler(this.paused_CheckedChanged);
            // 
            // mapSizeGroup
            // 
            this.mapSizeGroup.Controls.Add(this.drawGridCheckBox);
            this.mapSizeGroup.Controls.Add(this.mapHeight);
            this.mapSizeGroup.Controls.Add(this.mapWidth);
            this.mapSizeGroup.Controls.Add(this.label2);
            this.mapSizeGroup.Controls.Add(this.label1);
            this.mapSizeGroup.Location = new System.Drawing.Point(145, 510);
            this.mapSizeGroup.Name = "mapSizeGroup";
            this.mapSizeGroup.Size = new System.Drawing.Size(127, 101);
            this.mapSizeGroup.TabIndex = 20;
            this.mapSizeGroup.TabStop = false;
            this.mapSizeGroup.Text = "Map Size";
            // 
            // drawGridCheckBox
            // 
            this.drawGridCheckBox.AutoSize = true;
            this.drawGridCheckBox.Location = new System.Drawing.Point(6, 73);
            this.drawGridCheckBox.Name = "drawGridCheckBox";
            this.drawGridCheckBox.Size = new System.Drawing.Size(73, 17);
            this.drawGridCheckBox.TabIndex = 5;
            this.drawGridCheckBox.Text = "Draw Grid";
            this.drawGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // mapHeight
            // 
            this.mapHeight.Location = new System.Drawing.Point(50, 44);
            this.mapHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.mapHeight.Name = "mapHeight";
            this.mapHeight.Size = new System.Drawing.Size(46, 20);
            this.mapHeight.TabIndex = 4;
            this.mapHeight.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.mapHeight.ValueChanged += new System.EventHandler(this.mapHeight_ValueChanged);
            // 
            // mapWidth
            // 
            this.mapWidth.Location = new System.Drawing.Point(50, 17);
            this.mapWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.mapWidth.Name = "mapWidth";
            this.mapWidth.Size = new System.Drawing.Size(46, 20);
            this.mapWidth.TabIndex = 3;
            this.mapWidth.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.mapWidth.ValueChanged += new System.EventHandler(this.mapWidth_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Height:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width:";
            // 
            // decorSourceHeightLabel
            // 
            this.decorSourceHeightLabel.AutoSize = true;
            this.decorSourceHeightLabel.Location = new System.Drawing.Point(209, 442);
            this.decorSourceHeightLabel.Name = "decorSourceHeightLabel";
            this.decorSourceHeightLabel.Size = new System.Drawing.Size(18, 13);
            this.decorSourceHeightLabel.TabIndex = 29;
            this.decorSourceHeightLabel.Text = "H:";
            this.decorSourceHeightLabel.Visible = false;
            // 
            // decorSourceHeight
            // 
            this.decorSourceHeight.Location = new System.Drawing.Point(228, 440);
            this.decorSourceHeight.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.decorSourceHeight.Name = "decorSourceHeight";
            this.decorSourceHeight.Size = new System.Drawing.Size(50, 20);
            this.decorSourceHeight.TabIndex = 28;
            this.decorSourceHeight.Visible = false;
            this.decorSourceHeight.ValueChanged += new System.EventHandler(this.decorSourceHeight_ValueChanged);
            // 
            // decorSourceWidthLabel
            // 
            this.decorSourceWidthLabel.AutoSize = true;
            this.decorSourceWidthLabel.Location = new System.Drawing.Point(137, 442);
            this.decorSourceWidthLabel.Name = "decorSourceWidthLabel";
            this.decorSourceWidthLabel.Size = new System.Drawing.Size(21, 13);
            this.decorSourceWidthLabel.TabIndex = 27;
            this.decorSourceWidthLabel.Text = "W:";
            this.decorSourceWidthLabel.Visible = false;
            // 
            // decorSourceWidth
            // 
            this.decorSourceWidth.Location = new System.Drawing.Point(158, 440);
            this.decorSourceWidth.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.decorSourceWidth.Name = "decorSourceWidth";
            this.decorSourceWidth.Size = new System.Drawing.Size(50, 20);
            this.decorSourceWidth.TabIndex = 26;
            this.decorSourceWidth.Visible = false;
            this.decorSourceWidth.ValueChanged += new System.EventHandler(this.decorSourceWidth_ValueChanged);
            // 
            // decorSourceYLabel
            // 
            this.decorSourceYLabel.AutoSize = true;
            this.decorSourceYLabel.Location = new System.Drawing.Point(68, 442);
            this.decorSourceYLabel.Name = "decorSourceYLabel";
            this.decorSourceYLabel.Size = new System.Drawing.Size(17, 13);
            this.decorSourceYLabel.TabIndex = 25;
            this.decorSourceYLabel.Text = "Y:";
            this.decorSourceYLabel.Visible = false;
            // 
            // decorSourceY
            // 
            this.decorSourceY.Location = new System.Drawing.Point(85, 440);
            this.decorSourceY.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.decorSourceY.Minimum = new decimal(new int[] {
            90000,
            0,
            0,
            -2147483648});
            this.decorSourceY.Name = "decorSourceY";
            this.decorSourceY.Size = new System.Drawing.Size(50, 20);
            this.decorSourceY.TabIndex = 24;
            this.decorSourceY.Visible = false;
            this.decorSourceY.ValueChanged += new System.EventHandler(this.decorSourceY_ValueChanged);
            // 
            // decorSourceXLabel
            // 
            this.decorSourceXLabel.AutoSize = true;
            this.decorSourceXLabel.Location = new System.Drawing.Point(0, 442);
            this.decorSourceXLabel.Name = "decorSourceXLabel";
            this.decorSourceXLabel.Size = new System.Drawing.Size(17, 13);
            this.decorSourceXLabel.TabIndex = 23;
            this.decorSourceXLabel.Text = "X:";
            this.decorSourceXLabel.Visible = false;
            // 
            // decorSourceX
            // 
            this.decorSourceX.Location = new System.Drawing.Point(17, 440);
            this.decorSourceX.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.decorSourceX.Minimum = new decimal(new int[] {
            90000,
            0,
            0,
            -2147483648});
            this.decorSourceX.Name = "decorSourceX";
            this.decorSourceX.Size = new System.Drawing.Size(50, 20);
            this.decorSourceX.TabIndex = 22;
            this.decorSourceX.Visible = false;
            this.decorSourceX.ValueChanged += new System.EventHandler(this.decorSourceX_ValueChanged);
            // 
            // sourceRectangleLabel
            // 
            this.sourceRectangleLabel.AutoSize = true;
            this.sourceRectangleLabel.Location = new System.Drawing.Point(0, 420);
            this.sourceRectangleLabel.Name = "sourceRectangleLabel";
            this.sourceRectangleLabel.Size = new System.Drawing.Size(96, 13);
            this.sourceRectangleLabel.TabIndex = 30;
            this.sourceRectangleLabel.Text = "Source Rectangle:";
            this.sourceRectangleLabel.Visible = false;
            // 
            // drawSelected
            // 
            this.drawSelected.AutoSize = true;
            this.drawSelected.Checked = true;
            this.drawSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawSelected.Location = new System.Drawing.Point(7, 49);
            this.drawSelected.Name = "drawSelected";
            this.drawSelected.Size = new System.Drawing.Size(112, 17);
            this.drawSelected.TabIndex = 6;
            this.drawSelected.Text = "Highlight Selected";
            this.drawSelected.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 623);
            this.Controls.Add(this.sourceRectangleLabel);
            this.Controls.Add(this.decorSourceHeightLabel);
            this.Controls.Add(this.decorSourceHeight);
            this.Controls.Add(this.decorSourceWidthLabel);
            this.Controls.Add(this.decorSourceWidth);
            this.Controls.Add(this.decorSourceYLabel);
            this.Controls.Add(this.decorSourceY);
            this.Controls.Add(this.decorSourceXLabel);
            this.Controls.Add(this.decorSourceX);
            this.Controls.Add(this.gameGroupBox);
            this.Controls.Add(this.mapSizeGroup);
            this.Controls.Add(this.layerDepth);
            this.Controls.Add(this.layerDepthLabel);
            this.Controls.Add(this.loadImageButton);
            this.Controls.Add(this.imagePath);
            this.Controls.Add(this.imagePathLabel);
            this.Controls.Add(this.objectTypes);
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.height);
            this.Controls.Add(this.wLabel);
            this.Controls.Add(this.width);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.yPosition);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.xPosition);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerDepth)).EndInit();
            this.gameGroupBox.ResumeLayout(false);
            this.gameGroupBox.PerformLayout();
            this.mapSizeGroup.ResumeLayout(false);
            this.mapSizeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decorSourceX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton decorRadioButton;
        private System.Windows.Forms.RadioButton objectsRadioButton;
        private System.Windows.Forms.RadioButton wallsRadioButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.NumericUpDown xPosition;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.NumericUpDown yPosition;
        private System.Windows.Forms.Label wLabel;
        private System.Windows.Forms.NumericUpDown width;
        private System.Windows.Forms.Label hLabel;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.ListBox objectTypes;
        private System.Windows.Forms.Label imagePathLabel;
        private System.Windows.Forms.TextBox imagePath;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.Label layerDepthLabel;
        private System.Windows.Forms.NumericUpDown layerDepth;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gameGroupBox;
        private System.Windows.Forms.Button resetNPC;
        private System.Windows.Forms.GroupBox mapSizeGroup;
        private System.Windows.Forms.CheckBox drawGridCheckBox;
        public System.Windows.Forms.NumericUpDown mapHeight;
        public System.Windows.Forms.NumericUpDown mapWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox paused;
        private System.Windows.Forms.Label decorSourceHeightLabel;
        private System.Windows.Forms.NumericUpDown decorSourceHeight;
        private System.Windows.Forms.Label decorSourceWidthLabel;
        private System.Windows.Forms.NumericUpDown decorSourceWidth;
        private System.Windows.Forms.Label decorSourceYLabel;
        private System.Windows.Forms.NumericUpDown decorSourceY;
        private System.Windows.Forms.Label decorSourceXLabel;
        private System.Windows.Forms.NumericUpDown decorSourceX;
        private System.Windows.Forms.Label sourceRectangleLabel;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.CheckBox drawSelected;
    }
}