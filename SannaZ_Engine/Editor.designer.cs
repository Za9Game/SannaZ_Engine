﻿using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SannaZ_Engine
{
#if DEBUG
    partial class Editor
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

        private System.Windows.Forms.Label SannaZEngineText;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton objectsRadioButton;
        private System.Windows.Forms.RadioButton lightRadioButton;
        private System.Windows.Forms.RadioButton hudRadioButton;
        private System.Windows.Forms.RadioButton boxesColliderRadioButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.NumericUpDown xPosition;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.NumericUpDown yPosition;
        private System.Windows.Forms.Label wLabel;
        private System.Windows.Forms.NumericUpDown width;
        private System.Windows.Forms.Label hLabel;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.NumericUpDown scale;
        private System.Windows.Forms.Label intensityLabel;
        private System.Windows.Forms.NumericUpDown intensity;
        private System.Windows.Forms.ListBox objectTypes;
        private System.Windows.Forms.ListBox hudTypes;
        private System.Windows.Forms.Label propertyText;
        private System.Windows.Forms.ListBox propertyTypes;
        private System.Windows.Forms.Label animationPathLabel;
        private System.Windows.Forms.TextBox animationPath;
        private System.Windows.Forms.Button loadAnimationButton;
        private System.Windows.Forms.Button removeAnimationButton;
        private System.Windows.Forms.Label spritePathLabel;
        private System.Windows.Forms.TextBox spritePath;
        private System.Windows.Forms.Button loadSpriteButton;
        private System.Windows.Forms.Button removeSpriteButton;
        private System.Windows.Forms.Label layerDepthLabel;
        private System.Windows.Forms.NumericUpDown layerDepth;
        private System.Windows.Forms.Label tagLabel;
        private System.Windows.Forms.ComboBox tagDropDown;
        private System.Windows.Forms.TextBox addTagText;
        private System.Windows.Forms.Button addTagButton;
        private System.Windows.Forms.Button removeTagButton;
        private System.Windows.Forms.CheckBox collidableCheckBox;
        private System.Windows.Forms.CheckBox enemyStopCheckBox;
        private System.Windows.Forms.CheckBox gravityCheckBox;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.GroupBox gameGroupBox;
        private System.Windows.Forms.GroupBox mapSizeGroup;
        private System.Windows.Forms.CheckBox drawGridCheckBox;
        public System.Windows.Forms.NumericUpDown mapHeight;
        public System.Windows.Forms.NumericUpDown mapWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox paused;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.CheckBox drawSelected;
        private System.Windows.Forms.TextBox changeHudTextBox;
        private System.Windows.Forms.Label changeHudTextLabel;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.NumericUpDown layerValue;

#region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SannaZEngineText = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.objectsRadioButton = new System.Windows.Forms.RadioButton();
            this.lightRadioButton = new System.Windows.Forms.RadioButton();
            this.hudRadioButton = new System.Windows.Forms.RadioButton();
            this.boxesColliderRadioButton = new System.Windows.Forms.RadioButton();
            this.listBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.xPosition = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.yPosition = new System.Windows.Forms.NumericUpDown();
            this.wLabel = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.hLabel = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.scale = new System.Windows.Forms.NumericUpDown();
            this.intensityLabel = new System.Windows.Forms.Label();
            this.intensity = new System.Windows.Forms.NumericUpDown();
            this.objectTypes = new System.Windows.Forms.ListBox();
            this.hudTypes = new System.Windows.Forms.ListBox();
            this.propertyText = new System.Windows.Forms.Label();
            this.propertyTypes = new System.Windows.Forms.ListBox();
            this.animationPathLabel = new System.Windows.Forms.Label();
            this.animationPath = new System.Windows.Forms.TextBox();
            this.loadAnimationButton = new System.Windows.Forms.Button();
            this.removeAnimationButton = new System.Windows.Forms.Button();
            this.spritePathLabel = new System.Windows.Forms.Label();
            this.spritePath = new System.Windows.Forms.TextBox();
            this.loadSpriteButton = new System.Windows.Forms.Button();
            this.removeSpriteButton = new System.Windows.Forms.Button();
            this.layerDepthLabel = new System.Windows.Forms.Label();
            this.layerDepth = new System.Windows.Forms.NumericUpDown();
            this.tagLabel = new System.Windows.Forms.Label();
            this.tagDropDown = new System.Windows.Forms.ComboBox();
            this.addTagText = new System.Windows.Forms.TextBox();
            this.addTagButton = new System.Windows.Forms.Button();
            this.removeTagButton = new System.Windows.Forms.Button();
            this.collidableCheckBox = new System.Windows.Forms.CheckBox();
            this.enemyStopCheckBox = new System.Windows.Forms.CheckBox();
            this.gravityCheckBox = new System.Windows.Forms.CheckBox();
            this.gameGroupBox = new System.Windows.Forms.GroupBox();
            this.drawSelected = new System.Windows.Forms.CheckBox();
            this.paused = new System.Windows.Forms.CheckBox();
            this.mapSizeGroup = new System.Windows.Forms.GroupBox();
            this.drawGridCheckBox = new System.Windows.Forms.CheckBox();
            this.mapHeight = new System.Windows.Forms.NumericUpDown();
            this.mapWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.changeHudTextBox = new System.Windows.Forms.TextBox();
            this.changeHudTextLabel = new System.Windows.Forms.Label();
            this.layerLabel = new System.Windows.Forms.Label();
            this.layerValue = new System.Windows.Forms.NumericUpDown();
            this.game11 = new SannaZ_Engine.Game1();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerDepth)).BeginInit();
            this.gameGroupBox.SuspendLayout();
            this.mapSizeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SannaZEngineText
            // 
            this.SannaZEngineText.AutoSize = true;
            this.SannaZEngineText.BackColor = System.Drawing.Color.Transparent;
            this.SannaZEngineText.Location = new System.Drawing.Point(1384, 7);
            this.SannaZEngineText.Name = "SannaZEngineText";
            this.SannaZEngineText.Size = new System.Drawing.Size(87, 13);
            this.SannaZEngineText.TabIndex = 0;
            this.SannaZEngineText.Text = "SannaZEngine!!!";
            this.SannaZEngineText.Click += new System.EventHandler(this.SannaZEngineText_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1474, 24);
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
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.noneRadioButton);
            this.groupBox1.Controls.Add(this.objectsRadioButton);
            this.groupBox1.Controls.Add(this.lightRadioButton);
            this.groupBox1.Controls.Add(this.hudRadioButton);
            this.groupBox1.Controls.Add(this.boxesColliderRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(955, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
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
            // lightRadioButton
            // 
            this.lightRadioButton.AutoSize = true;
            this.lightRadioButton.Location = new System.Drawing.Point(6, 91);
            this.lightRadioButton.Name = "lightRadioButton";
            this.lightRadioButton.Size = new System.Drawing.Size(53, 17);
            this.lightRadioButton.TabIndex = 3;
            this.lightRadioButton.Text = "Lights";
            this.lightRadioButton.UseVisualStyleBackColor = true;
            this.lightRadioButton.CheckedChanged += new System.EventHandler(this.lightRadioButton_CheckedChanged);
            // 
            // hudRadioButton
            // 
            this.hudRadioButton.AutoSize = true;
            this.hudRadioButton.Location = new System.Drawing.Point(200, 21);
            this.hudRadioButton.Name = "hudRadioButton";
            this.hudRadioButton.Size = new System.Drawing.Size(45, 17);
            this.hudRadioButton.TabIndex = 3;
            this.hudRadioButton.Text = "Hud";
            this.hudRadioButton.UseVisualStyleBackColor = true;
            this.hudRadioButton.CheckedChanged += new System.EventHandler(this.hudRadioButton_CheckedChanged);
            // 
            // boxesColliderRadioButton
            // 
            this.boxesColliderRadioButton.AutoSize = true;
            this.boxesColliderRadioButton.Location = new System.Drawing.Point(6, 43);
            this.boxesColliderRadioButton.Name = "boxesColliderRadioButton";
            this.boxesColliderRadioButton.Size = new System.Drawing.Size(88, 17);
            this.boxesColliderRadioButton.TabIndex = 2;
            this.boxesColliderRadioButton.Text = "BoxesCollider";
            this.boxesColliderRadioButton.UseVisualStyleBackColor = true;
            this.boxesColliderRadioButton.CheckedChanged += new System.EventHandler(this.boxesColliderRadioButtonRadioButton_CheckedChanged);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(955, 153);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(520, 147);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(983, 306);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(1111, 306);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // removeAllButton
            // 
            this.removeAllButton.Location = new System.Drawing.Point(1189, 306);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(40, 23);
            this.removeAllButton.TabIndex = 6;
            this.removeAllButton.Text = "R All";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Visible = false;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // xPosition
            // 
            this.xPosition.Location = new System.Drawing.Point(966, 335);
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
            this.xLabel.Location = new System.Drawing.Point(949, 337);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 13);
            this.xLabel.TabIndex = 8;
            this.xLabel.Text = "X:";
            this.xLabel.Click += new System.EventHandler(this.xLabel_Click);
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(1017, 337);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 13);
            this.yLabel.TabIndex = 10;
            this.yLabel.Text = "Y:";
            this.yLabel.Click += new System.EventHandler(this.yLabel_Click);
            // 
            // yPosition
            // 
            this.yPosition.Location = new System.Drawing.Point(1034, 335);
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
            this.wLabel.Location = new System.Drawing.Point(1086, 337);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(21, 13);
            this.wLabel.TabIndex = 12;
            this.wLabel.Text = "W:";
            this.wLabel.Click += new System.EventHandler(this.wLabel_Click);
            // 
            // width
            // 
            this.width.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.width.Location = new System.Drawing.Point(1107, 335);
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
            this.hLabel.Location = new System.Drawing.Point(1158, 337);
            this.hLabel.Name = "hLabel";
            this.hLabel.Size = new System.Drawing.Size(18, 13);
            this.hLabel.TabIndex = 14;
            this.hLabel.Text = "H:";
            this.hLabel.Click += new System.EventHandler(this.hLabel_Click);
            // 
            // height
            // 
            this.height.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.height.Location = new System.Drawing.Point(1177, 335);
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
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Location = new System.Drawing.Point(1089, 339);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(37, 13);
            this.scaleLabel.TabIndex = 12;
            this.scaleLabel.Text = "Scale:";
            this.scaleLabel.Visible = false;
            // 
            // scale
            // 
            this.scale.DecimalPlaces = 2;
            this.scale.Increment = new decimal(new int[] {
            10,
            0,
            0,
            196608});
            this.scale.Location = new System.Drawing.Point(1119, 337);
            this.scale.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(50, 20);
            this.scale.TabIndex = 11;
            this.scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.scale.Visible = false;
            this.scale.ValueChanged += new System.EventHandler(this.scale_ValueChanged);
            // 
            // intensityLabel
            // 
            this.intensityLabel.AutoSize = true;
            this.intensityLabel.Location = new System.Drawing.Point(950, 374);
            this.intensityLabel.Name = "intensityLabel";
            this.intensityLabel.Size = new System.Drawing.Size(49, 13);
            this.intensityLabel.TabIndex = 12;
            this.intensityLabel.Text = "Intensity:";
            this.intensityLabel.Visible = false;
            // 
            // intensity
            // 
            this.intensity.DecimalPlaces = 2;
            this.intensity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            196608});
            this.intensity.Location = new System.Drawing.Point(999, 372);
            this.intensity.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.intensity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.intensity.Name = "intensity";
            this.intensity.Size = new System.Drawing.Size(50, 20);
            this.intensity.TabIndex = 11;
            this.intensity.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.intensity.Visible = false;
            this.intensity.ValueChanged += new System.EventHandler(this.intensity_ValueChanged);
            // 
            // objectTypes
            // 
            this.objectTypes.FormattingEnabled = true;
            this.objectTypes.Location = new System.Drawing.Point(952, 363);
            this.objectTypes.Name = "objectTypes";
            this.objectTypes.Size = new System.Drawing.Size(269, 43);
            this.objectTypes.TabIndex = 15;
            this.objectTypes.Visible = false;
            this.objectTypes.SelectedIndexChanged += new System.EventHandler(this.objectTypes_SelectedIndexChanged);
            // 
            // hudTypes
            // 
            this.hudTypes.FormattingEnabled = true;
            this.hudTypes.Location = new System.Drawing.Point(952, 363);
            this.hudTypes.Name = "hudTypes";
            this.hudTypes.Size = new System.Drawing.Size(269, 43);
            this.hudTypes.TabIndex = 15;
            this.hudTypes.Visible = false;
            // 
            // propertyText
            // 
            this.propertyText.AutoSize = true;
            this.propertyText.BackColor = System.Drawing.Color.Transparent;
            this.propertyText.Location = new System.Drawing.Point(1249, 342);
            this.propertyText.Name = "propertyText";
            this.propertyText.Size = new System.Drawing.Size(46, 13);
            this.propertyText.TabIndex = 22;
            this.propertyText.Text = "Property";
            this.propertyText.Visible = false;
            this.propertyText.Click += new System.EventHandler(this.propertyText_Click);
            // 
            // propertyTypes
            // 
            this.propertyTypes.FormattingEnabled = true;
            this.propertyTypes.Location = new System.Drawing.Point(1249, 361);
            this.propertyTypes.Name = "propertyTypes";
            this.propertyTypes.Size = new System.Drawing.Size(225, 56);
            this.propertyTypes.TabIndex = 15;
            this.propertyTypes.Visible = false;
            this.propertyTypes.SelectedIndexChanged += new System.EventHandler(this.propertyTypes_SelectedIndexChanged);
            // 
            // animationPathLabel
            // 
            this.animationPathLabel.AutoSize = true;
            this.animationPathLabel.Location = new System.Drawing.Point(1241, 432);
            this.animationPathLabel.Name = "animationPathLabel";
            this.animationPathLabel.Size = new System.Drawing.Size(56, 13);
            this.animationPathLabel.TabIndex = 5;
            this.animationPathLabel.Text = "Animation:";
            this.animationPathLabel.Visible = false;
            this.animationPathLabel.Click += new System.EventHandler(this.animationPathLabel_Click);
            // 
            // animationPath
            // 
            this.animationPath.Enabled = false;
            this.animationPath.Location = new System.Drawing.Point(1299, 435);
            this.animationPath.Name = "animationPath";
            this.animationPath.Size = new System.Drawing.Size(100, 20);
            this.animationPath.TabIndex = 16;
            this.animationPath.Visible = false;
            this.animationPath.TextChanged += new System.EventHandler(this.animationPath_TextChanged);
            // 
            // loadAnimationButton
            // 
            this.loadAnimationButton.Location = new System.Drawing.Point(1399, 432);
            this.loadAnimationButton.Name = "loadAnimationButton";
            this.loadAnimationButton.Size = new System.Drawing.Size(75, 23);
            this.loadAnimationButton.TabIndex = 17;
            this.loadAnimationButton.Text = "Load";
            this.loadAnimationButton.UseVisualStyleBackColor = true;
            this.loadAnimationButton.Visible = false;
            this.loadAnimationButton.Click += new System.EventHandler(this.loadAnimationButton_Click);
            // 
            // removeAnimationButton
            // 
            this.removeAnimationButton.Location = new System.Drawing.Point(1399, 457);
            this.removeAnimationButton.Name = "removeAnimationButton";
            this.removeAnimationButton.Size = new System.Drawing.Size(75, 23);
            this.removeAnimationButton.TabIndex = 17;
            this.removeAnimationButton.Text = "Remove";
            this.removeAnimationButton.UseVisualStyleBackColor = true;
            this.removeAnimationButton.Visible = false;
            this.removeAnimationButton.Click += new System.EventHandler(this.removeAnimationButton_Click);
            // 
            // spritePathLabel
            // 
            this.spritePathLabel.AutoSize = true;
            this.spritePathLabel.Location = new System.Drawing.Point(1241, 434);
            this.spritePathLabel.Name = "spritePathLabel";
            this.spritePathLabel.Size = new System.Drawing.Size(37, 13);
            this.spritePathLabel.TabIndex = 5;
            this.spritePathLabel.Text = "Sprite:";
            this.spritePathLabel.Visible = false;
            // 
            // spritePath
            // 
            this.spritePath.Enabled = false;
            this.spritePath.Location = new System.Drawing.Point(1299, 434);
            this.spritePath.Name = "spritePath";
            this.spritePath.Size = new System.Drawing.Size(100, 20);
            this.spritePath.TabIndex = 16;
            this.spritePath.Visible = false;
            // 
            // loadSpriteButton
            // 
            this.loadSpriteButton.Location = new System.Drawing.Point(1400, 432);
            this.loadSpriteButton.Name = "loadSpriteButton";
            this.loadSpriteButton.Size = new System.Drawing.Size(75, 23);
            this.loadSpriteButton.TabIndex = 17;
            this.loadSpriteButton.Text = "Load";
            this.loadSpriteButton.UseVisualStyleBackColor = true;
            this.loadSpriteButton.Visible = false;
            this.loadSpriteButton.Click += new System.EventHandler(this.loadSpriteButton_Click);
            // 
            // removeSpriteButton
            // 
            this.removeSpriteButton.Location = new System.Drawing.Point(1400, 457);
            this.removeSpriteButton.Name = "removeSpriteButton";
            this.removeSpriteButton.Size = new System.Drawing.Size(75, 23);
            this.removeSpriteButton.TabIndex = 17;
            this.removeSpriteButton.Text = "Remove";
            this.removeSpriteButton.UseVisualStyleBackColor = true;
            this.removeSpriteButton.Visible = false;
            this.removeSpriteButton.Click += new System.EventHandler(this.removeSpriteButton_Click);
            // 
            // layerDepthLabel
            // 
            this.layerDepthLabel.AutoSize = true;
            this.layerDepthLabel.Location = new System.Drawing.Point(951, 409);
            this.layerDepthLabel.Name = "layerDepthLabel";
            this.layerDepthLabel.Size = new System.Drawing.Size(65, 13);
            this.layerDepthLabel.TabIndex = 5;
            this.layerDepthLabel.Text = "LayerDepth:";
            this.layerDepthLabel.Visible = false;
            this.layerDepthLabel.Click += new System.EventHandler(this.layerDepthLabel_Click);
            // 
            // layerDepth
            // 
            this.layerDepth.DecimalPlaces = 3;
            this.layerDepth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.layerDepth.Location = new System.Drawing.Point(1022, 407);
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
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Location = new System.Drawing.Point(951, 434);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(29, 13);
            this.tagLabel.TabIndex = 5;
            this.tagLabel.Text = "Tag:";
            this.tagLabel.Visible = false;
            this.tagLabel.Click += new System.EventHandler(this.tagLabel_Click);
            // 
            // tagDropDown
            // 
            this.tagDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tagDropDown.FormattingEnabled = true;
            this.tagDropDown.Location = new System.Drawing.Point(979, 432);
            this.tagDropDown.Name = "tagDropDown";
            this.tagDropDown.Size = new System.Drawing.Size(100, 21);
            this.tagDropDown.TabIndex = 19;
            this.tagDropDown.Visible = false;
            this.tagDropDown.DropDown += new System.EventHandler(this.tagDropDown_Active);
            this.tagDropDown.SelectedIndexChanged += new System.EventHandler(this.tagDropDown_IndexChanged);
            this.tagDropDown.DropDownClosed += new System.EventHandler(this.tagDropDown_Disactive);
            // 
            // addTagText
            // 
            this.addTagText.Location = new System.Drawing.Point(979, 460);
            this.addTagText.Name = "addTagText";
            this.addTagText.Size = new System.Drawing.Size(83, 20);
            this.addTagText.TabIndex = 16;
            this.addTagText.Visible = false;
            this.addTagText.TextChanged += new System.EventHandler(this.addTagText_TextChanged);
            // 
            // addTagButton
            // 
            this.addTagButton.Location = new System.Drawing.Point(1066, 459);
            this.addTagButton.Name = "addTagButton";
            this.addTagButton.Size = new System.Drawing.Size(32, 20);
            this.addTagButton.TabIndex = 17;
            this.addTagButton.Text = "Add";
            this.addTagButton.UseVisualStyleBackColor = true;
            this.addTagButton.Visible = false;
            this.addTagButton.Click += new System.EventHandler(this.addTagButton_Click);
            // 
            // removeTagButton
            // 
            this.removeTagButton.Location = new System.Drawing.Point(1082, 432);
            this.removeTagButton.Name = "removeTagButton";
            this.removeTagButton.Size = new System.Drawing.Size(54, 20);
            this.removeTagButton.TabIndex = 17;
            this.removeTagButton.Text = "Remove";
            this.removeTagButton.UseVisualStyleBackColor = true;
            this.removeTagButton.Visible = false;
            this.removeTagButton.Click += new System.EventHandler(this.removeTagButton_Click);
            // 
            // collidableCheckBox
            // 
            this.collidableCheckBox.AutoSize = true;
            this.collidableCheckBox.Location = new System.Drawing.Point(1084, 409);
            this.collidableCheckBox.Name = "collidableCheckBox";
            this.collidableCheckBox.Size = new System.Drawing.Size(64, 17);
            this.collidableCheckBox.TabIndex = 9;
            this.collidableCheckBox.Text = "Collision";
            this.collidableCheckBox.UseVisualStyleBackColor = true;
            this.collidableCheckBox.Visible = false;
            this.collidableCheckBox.CheckedChanged += new System.EventHandler(this.collidable_CheckedChanged);
            // 
            // enemyStopCheckBox
            // 
            this.enemyStopCheckBox.AutoSize = true;
            this.enemyStopCheckBox.Location = new System.Drawing.Point(954, 434);
            this.enemyStopCheckBox.Name = "enemyStopCheckBox";
            this.enemyStopCheckBox.Size = new System.Drawing.Size(48, 17);
            this.enemyStopCheckBox.TabIndex = 9;
            this.enemyStopCheckBox.Text = "Stop";
            this.enemyStopCheckBox.UseVisualStyleBackColor = true;
            this.enemyStopCheckBox.Visible = false;
            this.enemyStopCheckBox.CheckedChanged += new System.EventHandler(this.enemyStop_CheckedChanged);
            // 
            // gravityCheckBox
            // 
            this.gravityCheckBox.AutoSize = true;
            this.gravityCheckBox.Location = new System.Drawing.Point(1154, 409);
            this.gravityCheckBox.Name = "gravityCheckBox";
            this.gravityCheckBox.Size = new System.Drawing.Size(59, 17);
            this.gravityCheckBox.TabIndex = 9;
            this.gravityCheckBox.Text = "Gravity";
            this.gravityCheckBox.UseVisualStyleBackColor = true;
            this.gravityCheckBox.Visible = false;
            this.gravityCheckBox.CheckedChanged += new System.EventHandler(this.gravity_CheckedChanged);
            // 
            // gameGroupBox
            // 
            this.gameGroupBox.Controls.Add(this.drawSelected);
            this.gameGroupBox.Controls.Add(this.paused);
            this.gameGroupBox.Location = new System.Drawing.Point(961, 512);
            this.gameGroupBox.Name = "gameGroupBox";
            this.gameGroupBox.Size = new System.Drawing.Size(127, 101);
            this.gameGroupBox.TabIndex = 21;
            this.gameGroupBox.TabStop = false;
            this.gameGroupBox.Text = "Game";
            this.gameGroupBox.Enter += new System.EventHandler(this.gameGroupBox_Enter);
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
            this.drawSelected.CheckedChanged += new System.EventHandler(this.drawSelected_CheckedChanged);
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
            this.mapSizeGroup.Location = new System.Drawing.Point(1094, 512);
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
            this.drawGridCheckBox.CheckedChanged += new System.EventHandler(this.drawGridCheckBox_CheckedChanged);
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
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // changeHudTextBox
            // 
            this.changeHudTextBox.Location = new System.Drawing.Point(1129, 337);
            this.changeHudTextBox.Name = "changeHudTextBox";
            this.changeHudTextBox.Size = new System.Drawing.Size(100, 20);
            this.changeHudTextBox.TabIndex = 16;
            this.changeHudTextBox.Visible = false;
            this.changeHudTextBox.TextChanged += new System.EventHandler(this.textBox_ValueChanged);
            // 
            // changeHudTextLabel
            // 
            this.changeHudTextLabel.AutoSize = true;
            this.changeHudTextLabel.Location = new System.Drawing.Point(1094, 337);
            this.changeHudTextLabel.Name = "changeHudTextLabel";
            this.changeHudTextLabel.Size = new System.Drawing.Size(31, 13);
            this.changeHudTextLabel.TabIndex = 0;
            this.changeHudTextLabel.Text = "Text:";
            this.changeHudTextLabel.Visible = false;
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Location = new System.Drawing.Point(952, 488);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(36, 13);
            this.layerLabel.TabIndex = 0;
            this.layerLabel.Text = "Layer:";
            this.layerLabel.Visible = false;
            this.layerLabel.Click += new System.EventHandler(this.layerLabel_Click);
            // 
            // layerValue
            // 
            this.layerValue.Location = new System.Drawing.Point(992, 486);
            this.layerValue.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.layerValue.Minimum = new decimal(new int[] {
            90000,
            0,
            0,
            -2147483648});
            this.layerValue.Name = "layerValue";
            this.layerValue.Size = new System.Drawing.Size(50, 20);
            this.layerValue.TabIndex = 23;
            this.layerValue.Visible = false;
            this.layerValue.ValueChanged += new System.EventHandler(this.layer_ValueChanged);
            // 
            // game11
            // 
            this.game11.Location = new System.Drawing.Point(13, 27);
            this.game11.MouseHoverUpdatesOnly = false;
            this.game11.Name = "game11";
            this.game11.Size = new System.Drawing.Size(930, 580);
            this.game11.TabIndex = 24;
            this.game11.Text = "game11";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 623);
            this.Controls.Add(this.game11);
            this.Controls.Add(this.SannaZEngineText);
            this.Controls.Add(this.gameGroupBox);
            this.Controls.Add(this.mapSizeGroup);
            this.Controls.Add(this.layerDepthLabel);
            this.Controls.Add(this.layerDepth);
            this.Controls.Add(this.tagLabel);
            this.Controls.Add(this.tagDropDown);
            this.Controls.Add(this.addTagButton);
            this.Controls.Add(this.addTagText);
            this.Controls.Add(this.removeTagButton);
            this.Controls.Add(this.collidableCheckBox);
            this.Controls.Add(this.enemyStopCheckBox);
            this.Controls.Add(this.gravityCheckBox);
            this.Controls.Add(this.objectTypes);
            this.Controls.Add(this.hudTypes);
            this.Controls.Add(this.propertyText);
            this.Controls.Add(this.propertyTypes);
            this.Controls.Add(this.animationPathLabel);
            this.Controls.Add(this.animationPath);
            this.Controls.Add(this.loadAnimationButton);
            this.Controls.Add(this.removeAnimationButton);
            this.Controls.Add(this.spritePathLabel);
            this.Controls.Add(this.spritePath);
            this.Controls.Add(this.loadSpriteButton);
            this.Controls.Add(this.removeSpriteButton);
            this.Controls.Add(this.hLabel);
            this.Controls.Add(this.height);
            this.Controls.Add(this.wLabel);
            this.Controls.Add(this.width);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.scale);
            this.Controls.Add(this.intensityLabel);
            this.Controls.Add(this.intensity);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.yPosition);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.xPosition);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.removeAllButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.changeHudTextBox);
            this.Controls.Add(this.changeHudTextLabel);
            this.Controls.Add(this.layerLabel);
            this.Controls.Add(this.layerValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Editor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SannaZEditor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerDepth)).EndInit();
            this.gameGroupBox.ResumeLayout(false);
            this.gameGroupBox.PerformLayout();
            this.mapSizeGroup.ResumeLayout(false);
            this.mapSizeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#endregion

        private Game1 game11;
    }
#endif
}