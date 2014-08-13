<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Screen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Screen))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.startAsyncButton1 = New System.Windows.Forms.Button
        Me.startAsyncButton2 = New System.Windows.Forms.Button
        Me.startAsyncButton3 = New System.Windows.Forms.Button
        Me.startAsyncButton4 = New System.Windows.Forms.Button
        Me.startAsyncButton5 = New System.Windows.Forms.Button
        Me.startAsyncButton6 = New System.Windows.Forms.Button
        Me.startAsyncButton7 = New System.Windows.Forms.Button
        Me.startAsyncButton8 = New System.Windows.Forms.Button
        Me.startAsyncButton9 = New System.Windows.Forms.Button
        Me.startAsyncButton10 = New System.Windows.Forms.Button
        Me.startAsyncButton11 = New System.Windows.Forms.Button
        Me.startAsyncButton12 = New System.Windows.Forms.Button
        Me.startAsyncButton13 = New System.Windows.Forms.Button
        Me.startAsyncButton14 = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.cancelAsyncButton = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Version = New System.Windows.Forms.Label
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.affectedcount_Label = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.operationlabel = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select the input folder to process:"
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "exe"
        Me.SaveFileDialog1.FileName = "Output File"
        Me.SaveFileDialog1.Filter = "All files|*.*"
        Me.SaveFileDialog1.InitialDirectory = "%Desktop%"
        Me.SaveFileDialog1.Title = "Save the result file to:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label4.Location = New System.Drawing.Point(414, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 12)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "0/0"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 26)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(512, 45)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoEllipsis = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label3.Location = New System.Drawing.Point(7, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(499, 16)
        Me.Label3.TabIndex = 45
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoEllipsis = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label2.Location = New System.Drawing.Point(7, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(499, 14)
        Me.Label2.TabIndex = 44
        '
        'startAsyncButton1
        '
        Me.startAsyncButton1.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.startAsyncButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton1.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton1.Location = New System.Drawing.Point(12, 94)
        Me.startAsyncButton1.Name = "startAsyncButton1"
        Me.startAsyncButton1.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton1.TabIndex = 49
        Me.startAsyncButton1.Tag = "Search and Replace Filename allows you to search for a case-sensitive string with" & _
            "in a file name and replace it with your specified string."
        Me.startAsyncButton1.Text = "Search and Replace File Rename"
        Me.startAsyncButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton1, "Search and Replace File Rename")
        Me.startAsyncButton1.UseVisualStyleBackColor = False
        '
        'startAsyncButton2
        '
        Me.startAsyncButton2.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.startAsyncButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton2.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton2.Location = New System.Drawing.Point(142, 94)
        Me.startAsyncButton2.Name = "startAsyncButton2"
        Me.startAsyncButton2.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton2.TabIndex = 65
        Me.startAsyncButton2.Tag = "Search and Replace Filename allows you to search for a case-sensitive string with" & _
            "in a folder name and replace it with your specified string."
        Me.startAsyncButton2.Text = "Search and Replace Folder Rename"
        Me.startAsyncButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton2, "Search and Replace Folder Rename")
        Me.startAsyncButton2.UseVisualStyleBackColor = False
        '
        'startAsyncButton3
        '
        Me.startAsyncButton3.BackColor = System.Drawing.Color.SeaGreen
        Me.startAsyncButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton3.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton3.Location = New System.Drawing.Point(272, 94)
        Me.startAsyncButton3.Name = "startAsyncButton3"
        Me.startAsyncButton3.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton3.TabIndex = 67
        Me.startAsyncButton3.Tag = "Number Series File Rename renames all files to a number series using the series s" & _
            "tart, step and padding values entered by you."
        Me.startAsyncButton3.Text = "Number Series File Rename"
        Me.startAsyncButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton3, "Number Series File Rename")
        Me.startAsyncButton3.UseVisualStyleBackColor = False
        '
        'startAsyncButton4
        '
        Me.startAsyncButton4.BackColor = System.Drawing.Color.YellowGreen
        Me.startAsyncButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton4.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton4.Location = New System.Drawing.Point(13, 140)
        Me.startAsyncButton4.Name = "startAsyncButton4"
        Me.startAsyncButton4.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton4.TabIndex = 68
        Me.startAsyncButton4.Tag = "File Name Prefixer allows you to add a substring to the start of a file name."
        Me.startAsyncButton4.Text = "File Name Prefixer"
        Me.startAsyncButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton4, "File Name Prefixer")
        Me.startAsyncButton4.UseVisualStyleBackColor = False
        '
        'startAsyncButton5
        '
        Me.startAsyncButton5.BackColor = System.Drawing.Color.Olive
        Me.startAsyncButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton5.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton5.Location = New System.Drawing.Point(143, 140)
        Me.startAsyncButton5.Name = "startAsyncButton5"
        Me.startAsyncButton5.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton5.TabIndex = 69
        Me.startAsyncButton5.Tag = "File Name Suffixer allows you to add a substring to the end of a file name."
        Me.startAsyncButton5.Text = "File Name Suffixer"
        Me.startAsyncButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton5, "File Name Suffixer")
        Me.startAsyncButton5.UseVisualStyleBackColor = False
        '
        'startAsyncButton6
        '
        Me.startAsyncButton6.BackColor = System.Drawing.Color.OliveDrab
        Me.startAsyncButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton6.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton6.Location = New System.Drawing.Point(272, 140)
        Me.startAsyncButton6.Name = "startAsyncButton6"
        Me.startAsyncButton6.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton6.TabIndex = 70
        Me.startAsyncButton6.Tag = "File Name to Lower Case forces all characters in the file name to lower case."
        Me.startAsyncButton6.Text = "File Name to Lower Case"
        Me.startAsyncButton6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton6, "File Name to Lower Case")
        Me.startAsyncButton6.UseVisualStyleBackColor = False
        '
        'startAsyncButton7
        '
        Me.startAsyncButton7.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.startAsyncButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton7.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton7.Location = New System.Drawing.Point(402, 140)
        Me.startAsyncButton7.Name = "startAsyncButton7"
        Me.startAsyncButton7.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton7.TabIndex = 71
        Me.startAsyncButton7.Tag = "File Name to Upper Case forces all characters in the file name to upper case."
        Me.startAsyncButton7.Text = "File Name to Upper Case"
        Me.startAsyncButton7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton7, "File Name to Upper Case")
        Me.startAsyncButton7.UseVisualStyleBackColor = False
        '
        'startAsyncButton8
        '
        Me.startAsyncButton8.BackColor = System.Drawing.Color.Green
        Me.startAsyncButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton8.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton8.Location = New System.Drawing.Point(13, 186)
        Me.startAsyncButton8.Name = "startAsyncButton8"
        Me.startAsyncButton8.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton8.TabIndex = 74
        Me.startAsyncButton8.Tag = resources.GetString("startAsyncButton8.Tag")
        Me.startAsyncButton8.Text = "File Name to Title Case"
        Me.startAsyncButton8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton8, "File Name to Title Case")
        Me.startAsyncButton8.UseVisualStyleBackColor = False
        '
        'startAsyncButton9
        '
        Me.startAsyncButton9.BackColor = System.Drawing.Color.Green
        Me.startAsyncButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton9.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton9.Location = New System.Drawing.Point(402, 94)
        Me.startAsyncButton9.Name = "startAsyncButton9"
        Me.startAsyncButton9.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton9.TabIndex = 75
        Me.startAsyncButton9.Tag = "File Name String Inserter allows you to insert a substring into a file name at a " & _
            "specified character position."
        Me.startAsyncButton9.Text = "File Name String Inserter"
        Me.startAsyncButton9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton9, "File Name String Inserter")
        Me.startAsyncButton9.UseVisualStyleBackColor = False
        '
        'startAsyncButton10
        '
        Me.startAsyncButton10.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.startAsyncButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton10.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton10.Location = New System.Drawing.Point(143, 186)
        Me.startAsyncButton10.Name = "startAsyncButton10"
        Me.startAsyncButton10.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton10.TabIndex = 78
        Me.startAsyncButton10.Tag = "Truncate File Name removes a specified number of characters from the end of the f" & _
            "ile name."
        Me.startAsyncButton10.Text = "Truncate File Name"
        Me.startAsyncButton10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton10, "Truncate File Name")
        Me.startAsyncButton10.UseVisualStyleBackColor = False
        '
        'startAsyncButton11
        '
        Me.startAsyncButton11.BackColor = System.Drawing.Color.YellowGreen
        Me.startAsyncButton11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton11.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton11.Location = New System.Drawing.Point(272, 186)
        Me.startAsyncButton11.Name = "startAsyncButton11"
        Me.startAsyncButton11.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton11.TabIndex = 79
        Me.startAsyncButton11.Tag = "Shorten any part of File Name allows you to remove a specified number of characte" & _
            "rs from a file name, starting at a specified character position."
        Me.startAsyncButton11.Text = "Shorten any part of File Name"
        Me.startAsyncButton11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton11, "Shorten any part of File Name")
        Me.startAsyncButton11.UseVisualStyleBackColor = False
        '
        'startAsyncButton12
        '
        Me.startAsyncButton12.BackColor = System.Drawing.Color.Olive
        Me.startAsyncButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton12.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton12.Location = New System.Drawing.Point(402, 186)
        Me.startAsyncButton12.Name = "startAsyncButton12"
        Me.startAsyncButton12.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton12.TabIndex = 81
        Me.startAsyncButton12.Tag = "Keep File Name Substring allows you to keep a substring of specified length from " & _
            "a file name and remove the rest."
        Me.startAsyncButton12.Text = "Keep File Name Substring"
        Me.startAsyncButton12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton12, "Keep File Name Substring")
        Me.startAsyncButton12.UseVisualStyleBackColor = False
        '
        'startAsyncButton13
        '
        Me.startAsyncButton13.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.startAsyncButton13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton13.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton13.Location = New System.Drawing.Point(13, 232)
        Me.startAsyncButton13.Name = "startAsyncButton13"
        Me.startAsyncButton13.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton13.TabIndex = 82
        Me.startAsyncButton13.Tag = "Folder Name Prefixer allows you to add a substring to the start of a folder name." & _
            ""
        Me.startAsyncButton13.Text = "Folder Name Prefixer"
        Me.startAsyncButton13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton13, "Folder Name Prefixer")
        Me.startAsyncButton13.UseVisualStyleBackColor = False
        '
        'startAsyncButton14
        '
        Me.startAsyncButton14.BackColor = System.Drawing.Color.SeaGreen
        Me.startAsyncButton14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.startAsyncButton14.ForeColor = System.Drawing.Color.White
        Me.startAsyncButton14.Location = New System.Drawing.Point(143, 232)
        Me.startAsyncButton14.Name = "startAsyncButton14"
        Me.startAsyncButton14.Size = New System.Drawing.Size(124, 40)
        Me.startAsyncButton14.TabIndex = 83
        Me.startAsyncButton14.Tag = "Folder Name Suffixer allows you to add a substring to the end of a folder name."
        Me.startAsyncButton14.Text = "Folder Name Suffixer"
        Me.startAsyncButton14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.startAsyncButton14, "Folder Name Suffixer")
        Me.startAsyncButton14.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(12, 12)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox1.TabIndex = 61
        Me.CheckBox1.Text = "Recursive"
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "Operations on folders will be run recursively")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(92, 12)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(122, 17)
        Me.CheckBox2.TabIndex = 66
        Me.CheckBox2.Text = "Affect File Extension"
        Me.ToolTip1.SetToolTip(Me.CheckBox2, "Filename extensions will be affected by name changing operations")
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'cancelAsyncButton
        '
        Me.cancelAsyncButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cancelAsyncButton.BackColor = System.Drawing.Color.YellowGreen
        Me.cancelAsyncButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelAsyncButton.Enabled = False
        Me.cancelAsyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cancelAsyncButton.ForeColor = System.Drawing.Color.White
        Me.cancelAsyncButton.Location = New System.Drawing.Point(428, 298)
        Me.cancelAsyncButton.Name = "cancelAsyncButton"
        Me.cancelAsyncButton.Size = New System.Drawing.Size(98, 30)
        Me.cancelAsyncButton.TabIndex = 50
        Me.cancelAsyncButton.Text = "Cancel"
        Me.cancelAsyncButton.UseVisualStyleBackColor = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(11, 77)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(512, 23)
        Me.ProgressBar1.TabIndex = 51
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Version
        '
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Dock = System.Windows.Forms.DockStyle.Top
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.ForeColor = System.Drawing.Color.Gray
        Me.Version.Location = New System.Drawing.Point(0, 0)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(534, 16)
        Me.Version.TabIndex = 58
        Me.Version.Text = "BUILD {0}{1:00}{2:00}.{3}"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(14, 49)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(73, 17)
        Me.RadioButton1.TabIndex = 59
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Single File"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(93, 49)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(106, 17)
        Me.RadioButton2.TabIndex = 60
        Me.RadioButton2.Text = "Folder's Contents"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "exe"
        Me.OpenFileDialog1.FileName = "Input File"
        Me.OpenFileDialog1.Filter = "All files|*.*"
        Me.OpenFileDialog1.InitialDirectory = "%Desktop%"
        Me.OpenFileDialog1.Title = "Select the file to operate on:"
        '
        'affectedcount_Label
        '
        Me.affectedcount_Label.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.affectedcount_Label.AutoSize = True
        Me.affectedcount_Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affectedcount_Label.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.affectedcount_Label.Location = New System.Drawing.Point(71, 14)
        Me.affectedcount_Label.Name = "affectedcount_Label"
        Me.affectedcount_Label.Size = New System.Drawing.Size(0, 12)
        Me.affectedcount_Label.TabIndex = 63
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label1.Location = New System.Drawing.Point(12, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 12)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Altered Files:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label5.Location = New System.Drawing.Point(12, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 12)
        Me.Label5.TabIndex = 72
        Me.Label5.Text = "Operation:"
        '
        'operationlabel
        '
        Me.operationlabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.operationlabel.AutoSize = True
        Me.operationlabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.operationlabel.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.operationlabel.Location = New System.Drawing.Point(72, 2)
        Me.operationlabel.Name = "operationlabel"
        Me.operationlabel.Size = New System.Drawing.Size(0, 12)
        Me.operationlabel.TabIndex = 73
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.Version)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 76)
        Me.Panel1.TabIndex = 76
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Location = New System.Drawing.Point(416, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 13)
        Me.Label8.TabIndex = 70
        Me.Label8.Text = "Additional Parameters"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(309, 34)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 32)
        Me.GroupBox1.TabIndex = 69
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(272, 26)
        Me.Label7.TabIndex = 67
        Me.Label7.Text = "Please select whether the chosen operation is to act on " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "either a single file or" & _
            " the contents of a selected folder:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.operationlabel)
        Me.Panel2.Controls.Add(Me.affectedcount_Label)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 339)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(536, 110)
        Me.Panel2.TabIndex = 77
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Location = New System.Drawing.Point(12, 278)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(410, 50)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "..."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Main_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(536, 449)
        Me.Controls.Add(Me.startAsyncButton14)
        Me.Controls.Add(Me.startAsyncButton13)
        Me.Controls.Add(Me.startAsyncButton12)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.startAsyncButton11)
        Me.Controls.Add(Me.startAsyncButton10)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.startAsyncButton9)
        Me.Controls.Add(Me.startAsyncButton8)
        Me.Controls.Add(Me.startAsyncButton7)
        Me.Controls.Add(Me.startAsyncButton6)
        Me.Controls.Add(Me.startAsyncButton5)
        Me.Controls.Add(Me.startAsyncButton4)
        Me.Controls.Add(Me.startAsyncButton3)
        Me.Controls.Add(Me.startAsyncButton2)
        Me.Controls.Add(Me.cancelAsyncButton)
        Me.Controls.Add(Me.startAsyncButton1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(544, 483)
        Me.Name = "Main_Screen"
        Me.Text = "Fox FileSystem Ops"
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents cancelAsyncButton As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents startAsyncButton1 As System.Windows.Forms.Button
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents affectedcount_Label As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents startAsyncButton2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents startAsyncButton3 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton4 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton5 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton6 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton7 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents operationlabel As System.Windows.Forms.Label
    Friend WithEvents startAsyncButton8 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton9 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents startAsyncButton10 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton11 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents startAsyncButton12 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton13 As System.Windows.Forms.Button
    Friend WithEvents startAsyncButton14 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
