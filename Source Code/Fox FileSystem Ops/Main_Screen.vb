Imports System.ComponentModel
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Globalization

Public Class Main_Screen

    Dim precount_max As Integer
    Dim cancel_operation As Boolean
    Dim currentcount As Integer
    Dim affectedcount As Integer
    Dim percentComplete As Integer
    Private highestPercentageReached As Integer = 0

    Dim lastfile As String = ""
    Dim lastfolder As String = ""
    Dim lastprefix As String = ""
    Dim lastsuffix As String = ""
    Dim lastsearchterm As String = ""
    Dim lastreplaceterm As String = ""
    Dim lastfileoperatedon As String = ""
    Dim lastfolderoperatedon As String = ""
    Dim lastseriesstart As Integer = 0
    Dim lastseriesstringlength As String = 1
    Dim lastseriesstep As Integer = 1
    Dim laststringinsert As String = ""
    Dim laststringinsertposition As Integer = 1
    Dim lastcharacterremovecount As Integer = 1

    Dim tempcounter As Integer = 0

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\Sounds\UHOH.WAV").Replace("\\", "\")) = True Then
                My.Computer.Audio.Play((Application.StartupPath & "\Sounds\UHOH.WAV").Replace("\\", "\"), AudioPlayMode.Background)
            End If
            Dim Display_Message1 As New Display_Message()
            Display_Message1.Message_Textbox.Text = "The Application encountered the following problem: " & vbCrLf & identifier_msg & ": " & ex.Message.ToString
            Display_Message1.Timer1.Interval = 1000
            Display_Message1.ShowDialog()
            Display_Message1.Dispose()
            Display_Message1 = Nothing
            If My.Computer.FileSystem.DirectoryExists((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs") = False Then
                My.Computer.FileSystem.CreateDirectory((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
            End If
            Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
            filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
            filewriter.Flush()
            filewriter.Close()
            filewriter = Nothing
            ex = Nothing
            identifier_msg = Nothing
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub Control_Enabler(ByVal disable As Boolean)
        Try
            If disable = True Then
                CheckBox1.Enabled = False
                CheckBox2.Enabled = False
                RadioButton1.Enabled = False
                RadioButton2.Enabled = False
                cancelAsyncButton.Enabled = True
                startAsyncButton1.Enabled = False
                startAsyncButton2.Enabled = False
                startAsyncButton3.Enabled = False
                startAsyncButton4.Enabled = False
                startAsyncButton5.Enabled = False
                startAsyncButton6.Enabled = False
                startAsyncButton7.Enabled = False
                startAsyncButton8.Enabled = False
                startAsyncButton9.Enabled = False
                startAsyncButton10.Enabled = False
                startAsyncButton11.Enabled = False
                startAsyncButton12.Enabled = False
                startAsyncButton13.Enabled = False
                startAsyncButton14.Enabled = False
            Else
                CheckBox1.Enabled = True
                CheckBox2.Enabled = True
                RadioButton1.Enabled = True
                RadioButton2.Enabled = True
                cancelAsyncButton.Enabled = False
                startAsyncButton1.Enabled = True
                startAsyncButton2.Enabled = True
                startAsyncButton3.Enabled = True
                startAsyncButton4.Enabled = True
                startAsyncButton5.Enabled = True
                startAsyncButton6.Enabled = True
                startAsyncButton7.Enabled = True
                startAsyncButton8.Enabled = True
                startAsyncButton9.Enabled = True
                startAsyncButton10.Enabled = True
                startAsyncButton11.Enabled = True
                startAsyncButton12.Enabled = True
                startAsyncButton13.Enabled = True
                startAsyncButton14.Enabled = True
            End If
        Catch ex As Exception
            Error_Handler(ex, "Control_Enabler")
        End Try
    End Sub

    Private Sub ClearLabels()
        Try
            affectedcount_Label.Text = ""
            Label2.Text = ""
            Label3.Text = ""
            Label4.Text = ""
        Catch ex As Exception
            Error_Handler(ex, "ClearLabels")
        End Try
    End Sub

    Private Function NewFileName(ByVal input As String, ByVal tempnum As Integer) As String
        NewFileName = ""
        Dim inf As FileInfo = New FileInfo(input)
        If (input.Length - inf.Extension.Length) > 0 Then
            NewFileName = input.Insert(input.Length - inf.Extension.Length, "(" & tempnum & ")")
        Else
            NewFileName = input & "(" & tempnum & ")"
        End If
        inf = Nothing
    End Function

    Private Function NewFolderName(ByVal input As String, ByVal tempnum As Integer) As String
        NewFolderName = ""
        Dim inf As DirectoryInfo = New DirectoryInfo(input)
        NewFolderName = input & "(" & tempnum & ")"
        inf = Nothing
    End Function

    Private Sub searchandreplacefilerename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton1.Click
        Try
            operationlabel.Text = "Search and Replace File Rename"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName

                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Search Term"
                    diag.TextBox1.Text = lastsearchterm
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastsearchterm = diag.TextBox1.Text
                        diag.Label1.Text = "Replace Term"
                        diag.TextBox1.Text = lastreplaceterm
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            lastreplaceterm = diag.TextBox1.Text
                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("searchandreplacefilerename")

                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Search Term"
                    diag.TextBox1.Text = lastsearchterm
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastsearchterm = diag.TextBox1.Text
                        diag.Label1.Text = "Replace Term"
                        diag.TextBox1.Text = lastreplaceterm
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            lastreplaceterm = diag.TextBox1.Text
                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("searchandreplacefilerename")

                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "SearchandReplaceFilenameRename")
        End Try
    End Sub


    Private Sub cancelAsyncButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelAsyncButton.Click
        Try
            cancel_operation = True
            Me.BackgroundWorker1.CancelAsync()
            Control_Enabler(False)
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "cancelAsyncButton_Click")
        End Try
    End Sub

    Private Sub Precount(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal includefoldercount As Boolean = False)
        precount_max = 0
        If RadioButton1.Checked = True Then
            precount_max = 1
            worker.ReportProgress(0)
        Else
            If CheckBox1.Checked = True Then
                Precount_Recursive_Runner(FolderBrowserDialog1.SelectedPath, worker, e, includefoldercount)
            Else
                Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                For Each finfo As FileInfo In dinfo.GetFiles()
                    If worker.CancellationPending Then
                        cancel_operation = True
                        finfo = Nothing
                        Exit For
                    End If
                    precount_max = precount_max + 1
                    'worker.ReportProgress(0)
                    finfo = Nothing
                Next
                worker.ReportProgress(0)
                If includefoldercount = True Then
                    For Each dinfo3 As DirectoryInfo In dinfo.GetDirectories
                        If worker.CancellationPending Then
                            cancel_operation = True
                            dinfo3 = Nothing
                            Exit For
                        End If
                        precount_max = precount_max + 1
                        'worker.ReportProgress(0)
                        dinfo3 = Nothing
                    Next
                    precount_max = precount_max + 1
                End If
                dinfo = Nothing
            End If
        End If
        '    worker.Dispose()
        '   worker = Nothing
    End Sub

    Private Sub Precount_Recursive_Runner(ByVal dir As String, ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal includefoldercount As Boolean = False)

        Try
            Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
            For Each finfo As FileInfo In dinfo.GetFiles()
                If worker.CancellationPending Then
                    cancel_operation = True
                    finfo = Nothing
                    Exit For
                End If
                precount_max = precount_max + 1
                'worker.ReportProgress(0)
                finfo = Nothing
            Next
            worker.ReportProgress(0)
            For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                If worker.CancellationPending Then
                    cancel_operation = True
                    dinfo2 = Nothing
                    Exit For
                End If
                Precount_Recursive_Runner(dinfo2.FullName, worker, e, includefoldercount)
                dinfo2 = Nothing
            Next
            If includefoldercount = True Then
                precount_max = precount_max + 1
            End If
            dinfo = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Precount_Recursive_Runner")
        End Try
        ' worker.Dispose()
        '  worker = Nothing
    End Sub


    Private Sub SearchAndReplaceFileRename_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        ' Dim result As String = ""
        Try
            If RadioButton1.Checked = True Then
                Try

                
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If


                    If lastsearchterm.Length > 0 Then
                        replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                    Else
                        replacename = searchname
                    End If

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "SearchAndReplaceFileRename")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If lastsearchterm.Length > 0 Then
                                    replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "SearchAndReplaceFileRename")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                SearchAndReplaceFileRename_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "SearchAndReplaceFileRename")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "SearchAndReplaceFileRename")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If lastsearchterm.Length > 0 Then
                                    replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "SearchAndReplaceFileRename")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "SearchAndReplaceFileRename")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "SearchAndReplaceFileRename")
        End Try
        ' worker.Dispose()
        'worker = Nothing

        'Return result
    End Sub


    Private Sub SearchAndReplaceFolderRename_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        ' Dim result As String = ""
        Try

            If CheckBox1.Checked = True Then
                Try


                    Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                    lastfolderoperatedon = dinfo.FullName

                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    searchname = dinfo.Name
                    If lastsearchterm.Length > 0 Then
                        replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                    Else
                        replacename = searchname
                    End If

                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.DirectoryExists(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        dinfo.MoveTo(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    currentcount = currentcount + dinfo.GetFiles.Length
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If

                    For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                        Try
                            If worker.CancellationPending Then
                                cancel_operation = True
                                dinfo2 = Nothing
                                Exit For
                            End If
                            SearchAndReplaceFolderRename_Recursive(worker, e, dinfo2.FullName)
                            dinfo2 = Nothing
                        Catch ex As Exception
                            Error_Handler(ex, "SearchAndReplaceFolderRename")
                        End Try
                    Next
                    lastfolder = dinfo.FullName
                    dinfo = Nothing
                Catch ex As Exception
                    Error_Handler(ex, "SearchAndReplaceFolderRename")
                End Try
            Else
                Try
                    Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                    lastfolderoperatedon = dinfo.FullName


                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    searchname = dinfo.Name
                    If lastsearchterm.Length > 0 Then
                        replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                    Else
                        replacename = searchname
                    End If

                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.DirectoryExists(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        dinfo.MoveTo(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If

                    currentcount = currentcount + dinfo.GetFiles.Length
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If


                    For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories
                        Try
                            lastfolderoperatedon = dinfo2.FullName
                            searchname = ""
                            replacename = ""
                            origreplacename = ""
                            searchname = dinfo2.Name
                            If lastsearchterm.Length > 0 Then
                                replacename = searchname.Replace(lastsearchterm, lastreplaceterm)
                            Else
                                replacename = searchname
                            End If

                            origreplacename = replacename

                            If Not replacename = searchname Then
                                If Not replacename.ToLower = searchname.ToLower Then
                                    While My.Computer.FileSystem.DirectoryExists(((dinfo2.Parent.FullName & "\" & replacename).Replace("\\", "\"))) = True
                                        replacename = NewFileName(origreplacename, tempcounter)
                                        tempcounter = tempcounter + 1
                                    End While
                                End If
                                dinfo2.MoveTo(((dinfo2.Parent.FullName & "\" & replacename).Replace("\\", "\")))
                                affectedcount = affectedcount + 1
                            End If
                            dinfo2 = Nothing
                        Catch ex As Exception
                            Error_Handler(ex, "SearchAndReplaceFolderRename")
                        End Try
                        currentcount = currentcount + 1
                        If precount_max > 0 Then
                            percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                        Else
                            percentComplete = 100
                        End If
                        If percentComplete > highestPercentageReached Then
                            highestPercentageReached = percentComplete
                            worker.ReportProgress(percentComplete)
                        End If
                    Next

                    'worker.ReportProgress(percentComplete)
                    lastfolder = dinfo.FullName
                    dinfo = Nothing
                Catch ex As Exception
                    Error_Handler(ex, "SearchAndReplaceFolderRename")
                End Try
            End If




        Catch ex As Exception
            Error_Handler(ex, "SearchAndReplaceFolderRename")
        End Try
        ' worker.Dispose()
        'worker = Nothing

        'Return result
    End Sub


    Private Sub NumberSeriesFilenameRename_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        ' Dim result As String = ""
        Try
            If RadioButton1.Checked = True Then
                Try

                    Dim seriescounter As Integer
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    seriescounter = 0
                    seriescounter = lastseriesstart

                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    replacename = seriescounter
                    seriescounter = seriescounter + lastseriesstep
                    While replacename.Length < lastseriesstringlength
                        replacename = "0" & replacename
                    End While

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        Dim seriescounter As Integer
                        seriescounter = 0
                        seriescounter = lastseriesstart
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If



                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""




                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = seriescounter
                                seriescounter = seriescounter + lastseriesstep

                                While replacename.Length < lastseriesstringlength
                                    replacename = "0" & replacename
                                End While

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                NumberSeriesFilenameRename_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        Dim seriescounter As Integer
                        seriescounter = 0
                        seriescounter = lastseriesstart

                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If



                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""


                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = seriescounter
                                seriescounter = seriescounter + lastseriesstep

                                While replacename.Length < lastseriesstringlength
                                    replacename = "0" & replacename
                                End While

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "NumberSeriesFilenameRename_Recursive")
        End Try
        ' worker.Dispose()
        'worker = Nothing

        'Return result
    End Sub


    Private Sub FilePrefixer_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try


                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If



                    replacename = lastprefix & searchname


                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FilePrefixer_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = lastprefix & searchname

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FilePrefixer_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FilePrefixer_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FilePrefixer_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FilePrefixer_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = lastprefix & searchname

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FilePrefixer_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FilePrefixer_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "FilePrefixer_Recursive")
        End Try

    End Sub


    Private Sub FileSuffixer_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    replacename = searchname & lastsuffix

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FileSuffixer_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname & lastsuffix

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileSuffixer_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FileSuffixer_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileSuffixer_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileSuffixer_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname & lastsuffix

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileSuffixer_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileSuffixer_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "FileSuffixer_Recursive")
        End Try

    End Sub

    Private Sub FileNameStringInserter_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    If searchname.Length >= (laststringinsertposition) Then
                        replacename = searchname.Insert(laststringinsertposition - 1, laststringinsert)
                    Else
                        replacename = searchname & laststringinsert
                    End If


                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FileNameStringInserter_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If searchname.Length >= (laststringinsertposition) Then
                                    replacename = searchname.Insert(laststringinsertposition - 1, laststringinsert)
                                Else
                                    replacename = searchname & laststringinsert
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameStringInserter_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FileNameStringInserter_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameStringInserter_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameStringInserter_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If searchname.Length >= (laststringinsertposition) Then
                                    replacename = searchname.Insert(laststringinsertposition - 1, laststringinsert)
                                Else
                                    replacename = searchname & laststringinsert
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameStringInserter_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameStringInserter_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "FileNameStringInserter_Recursive")
        End Try

    End Sub

    Private Sub ShortenAnyPartOfFilename_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    If (searchname.Length - lastcharacterremovecount) > 0 Then
                        If searchname.Length >= (laststringinsertposition) Then
                            replacename = searchname.Remove(laststringinsertposition - 1, lastcharacterremovecount)
                        Else
                            replacename = searchname
                        End If
                    Else
                        replacename = searchname
                    End If

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If

                                If (searchname.Length - lastcharacterremovecount) > 0 Then
                                    If searchname.Length >= (laststringinsertposition) Then
                                        replacename = searchname.Remove(laststringinsertposition - 1, lastcharacterremovecount)
                                    Else
                                        replacename = searchname
                                    End If
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                ShortenAnyPartOfFilename_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If (searchname.Length - lastcharacterremovecount) > 0 Then
                                    If searchname.Length >= (laststringinsertposition) Then
                                        replacename = searchname.Remove(laststringinsertposition - 1, lastcharacterremovecount)
                                    Else
                                        replacename = searchname
                                    End If
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "ShortenAnyPartOfFilename_Recursive")
        End Try

    End Sub



    Private Sub KeepFileNameSubstring_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    If (searchname.Length - lastcharacterremovecount) > 0 Then
                        If searchname.Length >= (laststringinsertposition) Then
                            replacename = searchname.Substring(laststringinsertposition - 1, lastcharacterremovecount)
                        Else
                            replacename = searchname
                        End If
                    Else
                        replacename = searchname
                    End If

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If

                                If (searchname.Length - lastcharacterremovecount) > 0 Then
                                    If searchname.Length >= (laststringinsertposition) Then
                                        replacename = searchname.Substring(laststringinsertposition - 1, lastcharacterremovecount)
                                    Else
                                        replacename = searchname
                                    End If
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                KeepFileNameSubstring_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If (searchname.Length - lastcharacterremovecount) > 0 Then
                                    If searchname.Length >= (laststringinsertposition) Then
                                        replacename = searchname.Substring(laststringinsertposition - 1, lastcharacterremovecount)
                                    Else
                                        replacename = searchname
                                    End If
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "KeepFileNameSubstring_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "KeepFileNameSubstring_Recursive")
        End Try

    End Sub



    Private Sub TruncateFilename_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    If searchname.Length > (lastcharacterremovecount) Then
                        replacename = searchname.Substring(0, searchname.Length - lastcharacterremovecount)
                    Else
                        replacename = searchname
                    End If


                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "TruncateFilename_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If searchname.Length > (lastcharacterremovecount) Then
                                    replacename = searchname.Substring(0, searchname.Length - lastcharacterremovecount)
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "TruncateFilename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                TruncateFilename_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "TruncateFilename_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "TruncateFilename_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                If searchname.Length > (lastcharacterremovecount) Then
                                    replacename = searchname.Substring(0, searchname.Length - lastcharacterremovecount)
                                Else
                                    replacename = searchname
                                End If

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "TruncateFilename_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "TruncateFilename_Recursive")
                    End Try
                End If
            End If



        Catch ex As Exception
            Error_Handler(ex, "TruncateFilename_Recursive")
        End Try

    End Sub

    Private Sub FileNameToLowerCase_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    replacename = searchname.ToLower

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FileNameToLowerCase_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname.ToLower

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToLowerCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FileNameToLowerCase_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToLowerCase_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToLowerCase_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname.ToLower

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToLowerCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToLowerCase_Recursive")
                    End Try
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "FileNameToLowerCase_Recursive")
        End Try
    End Sub


    Private Sub FileNameToUpperCase_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    replacename = searchname.ToUpper

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FileNameToUpperCase_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname.ToUpper

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToUpperCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FileNameToUpperCase_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToUpperCase_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToUpperCase_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                replacename = searchname.ToUpper

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToUpperCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToUpperCase_Recursive")
                    End Try
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "FileNameToUpperCase_Recursive")
        End Try
    End Sub


    Private Sub FileNameToTitleCase_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton1.Checked = True Then
                Try
                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""
                    Dim finfo As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                    lastfileoperatedon = finfo.FullName
                    lastfolderoperatedon = finfo.DirectoryName
                    If CheckBox2.Checked = True Then
                        searchname = finfo.Name
                    Else
                        searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                    End If

                    Dim cinfo As CultureInfo = New CultureInfo("en-US")
                    replacename = cinfo.TextInfo.ToTitleCase(searchname.ToLower())
                    cinfo = Nothing

                    If CheckBox2.Checked = False Then
                        searchname = searchname & finfo.Extension
                        replacename = replacename & finfo.Extension
                    End If



                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFileName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    lastfile = ((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))

                    finfo = Nothing
                    currentcount = currentcount + 1
                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "FileNameToTitleCase_Recursive")
                End Try
            Else
                If CheckBox1.Checked = True Then
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(dir)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""



                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If


                                Dim cinfo As CultureInfo = New CultureInfo("en-US")
                                replacename = cinfo.TextInfo.ToTitleCase(searchname.ToLower())
                                cinfo = Nothing

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToTitleCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If
                        Next
                        ' worker.ReportProgress(percentComplete)
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FileNameToTitleCase_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToTitleCase_Recursive")
                            End Try
                        Next
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToTitleCase_Recursive")
                    End Try
                Else
                    Try
                        Dim dinfo As DirectoryInfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                        For Each finfo As FileInfo In dinfo.GetFiles()
                            Try
                                lastfileoperatedon = finfo.FullName
                                lastfolderoperatedon = finfo.DirectoryName
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    finfo = Nothing
                                    Exit For
                                End If


                                Dim searchname, replacename, origreplacename As String
                                searchname = ""
                                replacename = ""
                                origreplacename = ""
                                If CheckBox2.Checked = True Then
                                    searchname = finfo.Name
                                Else
                                    searchname = finfo.Name.Substring(0, (finfo.Name.Length - finfo.Extension.Length))
                                End If

                                Dim cinfo As CultureInfo = New CultureInfo("en-US")
                                replacename = cinfo.TextInfo.ToTitleCase(searchname.ToLower())
                                cinfo = Nothing

                                If CheckBox2.Checked = False Then
                                    searchname = searchname & finfo.Extension
                                    replacename = replacename & finfo.Extension
                                End If

                                origreplacename = replacename

                                If Not replacename = searchname Then
                                    If Not replacename.ToLower = searchname.ToLower Then
                                        While My.Computer.FileSystem.FileExists(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\"))) = True
                                            replacename = NewFileName(origreplacename, tempcounter)
                                            tempcounter = tempcounter + 1
                                        End While
                                    End If
                                    finfo.MoveTo(((finfo.DirectoryName & "\" & replacename).Replace("\\", "\")))
                                    affectedcount = affectedcount + 1
                                End If

                                finfo = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FileNameToTitleCase_Recursive")
                            End Try
                            currentcount = currentcount + 1
                            If precount_max > 0 Then
                                percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                            Else
                                percentComplete = 100
                            End If
                            If percentComplete > highestPercentageReached Then
                                highestPercentageReached = percentComplete
                                worker.ReportProgress(percentComplete)
                            End If


                        Next
                        'worker.ReportProgress(percentComplete)
                        dinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FileNameToTitleCase_Recursive")
                    End Try
                End If
            End If
        Catch ex As Exception
            Error_Handler(ex, "FileNameToTitleCase_Recursive")
        End Try
    End Sub

    Private Sub FolderPrefixer_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton2.Checked = True Then

                Try
                    Dim dinfo As DirectoryInfo
                    If dir = "" Then
                        dinfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                    Else
                        dinfo = New DirectoryInfo(dir)
                    End If

                    If CheckBox1.Checked = True Then
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FolderPrefixer_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FolderPrefixer_Recursive")
                            End Try
                        Next
                    End If

                    lastfileoperatedon = ""
                    lastfolderoperatedon = dinfo.FullName
                    If worker.CancellationPending Then
                        cancel_operation = True
                        dinfo = Nothing
                        Exit Try
                    End If


                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""


                    searchname = dinfo.Name
                  
                    replacename = lastprefix & searchname

                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.DirectoryExists(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFolderName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        dinfo.MoveTo(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    currentcount = currentcount + dinfo.GetFiles.Length
                    If CheckBox1.Checked = False Then
                        currentcount = currentcount + dinfo.GetDirectories.Length
                    End If
                    currentcount = currentcount + 1

                    dinfo = Nothing
            

                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If


                Catch ex As Exception
                    Error_Handler(ex, "FolderPrefixer_Recursive")
                End Try

            End If


        Catch ex As Exception
            Error_Handler(ex, "FolderPrefixer_Recursive")
        End Try

    End Sub

    Private Sub FolderSuffixer_Recursive(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs, Optional ByVal dir As String = "")
        Try
            If RadioButton2.Checked = True Then

                Try
                    Dim dinfo As DirectoryInfo
                    If dir = "" Then
                        dinfo = New DirectoryInfo(FolderBrowserDialog1.SelectedPath)
                    Else
                        dinfo = New DirectoryInfo(dir)
                    End If

                    If CheckBox1.Checked = True Then
                        For Each dinfo2 As DirectoryInfo In dinfo.GetDirectories()
                            Try
                                If worker.CancellationPending Then
                                    cancel_operation = True
                                    dinfo2 = Nothing
                                    Exit For
                                End If
                                FolderSuffixer_Recursive(worker, e, dinfo2.FullName)
                                dinfo2 = Nothing
                            Catch ex As Exception
                                Error_Handler(ex, "FolderSuffixer_Recursive")
                            End Try
                        Next
                    End If

                    lastfileoperatedon = ""
                    lastfolderoperatedon = dinfo.FullName
                    If worker.CancellationPending Then
                        cancel_operation = True
                        dinfo = Nothing
                        Exit Try
                    End If


                    Dim searchname, replacename, origreplacename As String
                    searchname = ""
                    replacename = ""
                    origreplacename = ""


                    searchname = dinfo.Name

                    replacename = searchname & lastsuffix

                    origreplacename = replacename

                    If Not replacename = searchname Then
                        If Not replacename.ToLower = searchname.ToLower Then
                            While My.Computer.FileSystem.DirectoryExists(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\"))) = True
                                replacename = NewFolderName(origreplacename, tempcounter)
                                tempcounter = tempcounter + 1
                            End While
                        End If
                        dinfo.MoveTo(((dinfo.Parent.FullName & "\" & replacename).Replace("\\", "\")))
                        affectedcount = affectedcount + 1
                    End If
                    currentcount = currentcount + dinfo.GetFiles.Length
                    If CheckBox1.Checked = False Then
                        currentcount = currentcount + dinfo.GetDirectories.Length
                    End If
                    currentcount = currentcount + 1

                    dinfo = Nothing


                    If precount_max > 0 Then
                        percentComplete = CSng(currentcount) / CSng(precount_max) * 100
                    Else
                        percentComplete = 100
                    End If
                    If percentComplete > highestPercentageReached Then
                        highestPercentageReached = percentComplete
                        worker.ReportProgress(percentComplete)
                    End If


                Catch ex As Exception
                    Error_Handler(ex, "FolderSuffixer_Recursive")
                End Try

            End If


        Catch ex As Exception
            Error_Handler(ex, "FolderSuffixer_Recursive")
        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
            Try
                precount_max = 0
                currentcount = 0
                affectedcount = 0
                percentComplete = 0
                highestPercentageReached = 0

                tempcounter = 0
                lastfileoperatedon = ""


                cancel_operation = False

                worker.ReportProgress(percentComplete)

                If worker.CancellationPending Then
                    e.Cancel = True
                    cancel_operation = True
                End If



                If e.Argument.ToString = "searchandreplacefilerename" Then
                    Precount(worker, e)
                    If CheckBox1.Checked = False Then
                        SearchAndReplaceFileRename_Recursive(worker, e)
                    Else
                        SearchAndReplaceFileRename_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "searchandreplacefolderrename" Then
                    Precount(worker, e, True)
                    If CheckBox1.Checked = False Then
                        SearchAndReplaceFolderRename_Recursive(worker, e)
                    Else
                        SearchAndReplaceFolderRename_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "numberseriesfilenamerename" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        NumberSeriesFilenameRename_Recursive(worker, e)
                    Else
                        NumberSeriesFilenameRename_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "fileprefixer" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FilePrefixer_Recursive(worker, e)
                    Else
                        FilePrefixer_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "filesuffixer" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FileSuffixer_Recursive(worker, e)
                    Else
                        FileSuffixer_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "filenametolowercase" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FileNameToLowerCase_Recursive(worker, e)
                    Else
                        FileNameToLowerCase_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "filenametouppercase" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FileNameToUpperCase_Recursive(worker, e)
                    Else
                        FileNameToUpperCase_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "filenametotitlecase" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FileNameToTitleCase_Recursive(worker, e)
                    Else
                        FileNameToTitleCase_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "filenamestringinserter" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        FileNameStringInserter_Recursive(worker, e)
                    Else
                        FileNameStringInserter_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "truncatefilename" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        TruncateFilename_Recursive(worker, e)
                    Else
                        TruncateFilename_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "shortenanypartoffilename" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        ShortenAnyPartOfFilename_Recursive(worker, e)
                    Else
                        ShortenAnyPartOfFilename_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "keepfilenamesubstring" Then
                    Precount(worker, e, False)
                    If CheckBox1.Checked = False Then
                        KeepFileNameSubstring_Recursive(worker, e)
                    Else
                        KeepFileNameSubstring_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "folderprefixer" Then
                    Precount(worker, e, True)
                    If CheckBox1.Checked = False Then
                        FolderPrefixer_Recursive(worker, e)
                    Else
                        FolderPrefixer_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If
                If e.Argument.ToString = "foldersuffixer" Then
                    Precount(worker, e, True)
                    If CheckBox1.Checked = False Then
                        FolderSuffixer_Recursive(worker, e)
                    Else
                        FolderSuffixer_Recursive(worker, e, FolderBrowserDialog1.SelectedPath)
                    End If
                End If

                e.Result = ""
            Catch ex As Exception
                Error_Handler(ex, "backgroundWorker1_DoWork")
            End Try
            Try
                If Not worker Is Nothing Then
                    worker.Dispose()
                End If
                sender = Nothing
                e = Nothing
                ' percentComplete = 100
                'worker.ReportProgress(percentComplete)
            Catch ex As Exception
                Error_Handler(ex, "backgroundWorker1_DoWork")
            End Try
        Catch ex As Exception
            Error_Handler(ex, "backgroundWorker1_DoWork")
        End Try
    End Sub

    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If Not (e.Error Is Nothing) Then
                Error_Handler(e.Error, "backgroundWorker1_RunWorkerCompleted")
            ElseIf e.Cancelled Then
                Me.ProgressBar1.Value = 0
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\Sounds\HEEY.WAV").Replace("\\", "\")) = True Then
                    My.Computer.Audio.Play((Application.StartupPath & "\Sounds\HEEY.WAV").Replace("\\", "\"), AudioPlayMode.Background)
                End If
            Else
                Me.ProgressBar1.Value = 100
                If My.Computer.FileSystem.FileExists((Application.StartupPath & "\Sounds\VICTORY.WAV").Replace("\\", "\")) = True Then
                    My.Computer.Audio.Play((Application.StartupPath & "\Sounds\VICTORY.WAV").Replace("\\", "\"), AudioPlayMode.Background)
                End If
            End If
   Control_Enabler(False)


        Catch ex As Exception
            Error_Handler(ex, "backgroundWorker1_RunWorkerCompleted")
        End Try
    End Sub

    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try
            Me.affectedcount_Label.Text = affectedcount
            Me.Label4.Text = currentcount & "/" & precount_max
            Me.Label2.Text = lastfileoperatedon
            Me.Label3.Text = lastfolderoperatedon



            If e.ProgressPercentage < 100 Then
                Me.ProgressBar1.Value = e.ProgressPercentage
            Else
                Me.ProgressBar1.Value = 100
            End If

        Catch ex As Exception
            Error_Handler(ex, "backgroundWorker1_ProgressChanged")
        End Try
    End Sub




    Private Sub Main_Screen_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Save_Settings()
        Catch ex As Exception
            Error_Handler(ex, "Closed")
        End Try
    End Sub

    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
            Load_Settings()
        Catch ex As Exception
            Error_Handler(ex, "Load")
        End Try
    End Sub

    Private Sub Load_Settings()
        Try
            If Not My.Settings.lastfile = "" And Not My.Settings.lastfile Is Nothing Then
                lastfile = My.Settings.lastfile
            Else
                lastfile = ""
            End If

            If Not My.Settings.lastfolder = "" And Not My.Settings.lastfolder Is Nothing Then
                lastfolder = My.Settings.lastfolder
            Else
                lastfolder = ""
            End If

            If Not My.Settings.lastprefix = "" And Not My.Settings.lastprefix Is Nothing Then
                lastprefix = My.Settings.lastprefix
            Else
                lastprefix = ""
            End If

            If Not My.Settings.lastsuffix = "" And Not My.Settings.lastsuffix Is Nothing Then
                lastsuffix = My.Settings.lastsuffix
            Else
                lastsuffix = ""
            End If

            If Not My.Settings.lastsearchterm = "" And Not My.Settings.lastsearchterm Is Nothing Then
                lastsearchterm = My.Settings.lastsearchterm
            Else
                lastsearchterm = ""
            End If

            If Not My.Settings.lastreplaceterm = "" And Not My.Settings.lastreplaceterm Is Nothing Then
                lastreplaceterm = My.Settings.lastreplaceterm
            Else
                lastreplaceterm = ""
            End If

            If Not My.Settings.lastfileoperatedon = "" And Not My.Settings.lastfileoperatedon Is Nothing Then
                lastfileoperatedon = My.Settings.lastfileoperatedon
            Else
                lastfileoperatedon = ""
            End If

            If Not My.Settings.RadioButton1 = Nothing Then
                RadioButton1.Checked = My.Settings.RadioButton1
            Else
                RadioButton1.Checked = True
            End If

            If Not My.Settings.RadioButton2 = Nothing Then
                RadioButton2.Checked = My.Settings.RadioButton2
            Else
                RadioButton2.Checked = False
            End If

            If Not My.Settings.Checkbox1 = Nothing Then
                CheckBox1.Checked = My.Settings.Checkbox1
            Else
                CheckBox1.Checked = False
            End If

            If Not My.Settings.Checkbox2 = Nothing Then
                CheckBox2.Checked = My.Settings.Checkbox2
            Else
                CheckBox2.Checked = False
            End If

            If Not My.Settings.lastfolderoperatedon = "" And Not My.Settings.lastfolderoperatedon Is Nothing Then
                lastfolderoperatedon = My.Settings.lastfolderoperatedon
            Else
                lastfolderoperatedon = ""
            End If

            If Not My.Settings.lastseriesstringlength = Nothing Then
                lastseriesstringlength = My.Settings.lastseriesstringlength
            Else
                lastseriesstringlength = 1
            End If

            If Not My.Settings.lastseriesstart = Nothing Then
                lastseriesstart = My.Settings.lastseriesstart
            Else
                lastseriesstart = 0
            End If

            If Not My.Settings.lastseriesstep = Nothing Then
                lastseriesstep = My.Settings.lastseriesstep
            Else
                lastseriesstep = 1
            End If

            If Not My.Settings.laststringinsertposition = Nothing Then
                laststringinsertposition = My.Settings.laststringinsertposition
            Else
                laststringinsertposition = 1
            End If

            If Not My.Settings.lastcharacterremovecount = Nothing Then
                lastcharacterremovecount = My.Settings.lastcharacterremovecount
            Else
                lastcharacterremovecount = 1
            End If

            If Not My.Settings.laststringinsert = "" And Not My.Settings.laststringinsert Is Nothing Then
                laststringinsert = My.Settings.laststringinsert
            Else
                laststringinsert = ""
            End If
        Catch ex As Exception
            Error_Handler(ex, "Load Settings")
        End Try
    End Sub

    Private Sub Save_Settings()
        Try
            My.Settings.Reset()

            My.Settings.lastfile = lastfile
            My.Settings.lastfolder = lastfolder
            My.Settings.lastprefix = lastprefix
            My.Settings.lastsuffix = lastsuffix
            My.Settings.lastsearchterm = lastsearchterm
            My.Settings.lastreplaceterm = lastreplaceterm
            My.Settings.lastfileoperatedon = lastfileoperatedon
            My.Settings.lastfolderoperatedon = lastfolderoperatedon
            My.Settings.RadioButton1 = RadioButton1.Checked
            My.Settings.RadioButton2 = RadioButton2.Checked
            My.Settings.Checkbox1 = CheckBox1.Checked
            My.Settings.Checkbox2 = CheckBox2.Checked
            My.Settings.lastseriesstart = lastseriesstart
            My.Settings.lastseriesstringlength = lastseriesstringlength
            My.Settings.lastseriesstep = lastseriesstep
            My.Settings.laststringinsert = laststringinsert
            My.Settings.laststringinsertposition = laststringinsertposition
            My.Settings.lastcharacterremovecount = lastcharacterremovecount

            My.Settings.Save()

        Catch ex As Exception
            Error_Handler(ex, "Save Settings")
        End Try
    End Sub

    Private Sub searchandreplacefolderrename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton2.Click
        Try
            operationlabel.Text = "Search and Replace Folder Rename"
            RadioButton2.Checked = True

            If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                FolderBrowserDialog1.SelectedPath = lastfolder
            End If
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                lastfolder = FolderBrowserDialog1.SelectedPath
                Dim diag As Dialog1 = New Dialog1()
                diag.Label1.Text = "Search Term"
                diag.TextBox1.Text = lastsearchterm
                If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                    lastsearchterm = diag.TextBox1.Text
                    diag.Label1.Text = "Replace Term"
                    diag.TextBox1.Text = lastreplaceterm
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastreplaceterm = diag.TextBox1.Text
                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("searchandreplacefolderrename")

                    End If
                End If
                diag.Close()
                diag = Nothing
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "SearchandReplaceFilenameRename")
        End Try
    End Sub

    Private Sub NumberSeriesFilenameRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton3.Click
        Try
            operationlabel.Text = "Number Series File Rename"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName

                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number series start value:"
                    diag.NumericUpDown1.Minimum = 0
                    diag.NumericUpDown1.Value = lastseriesstart
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastseriesstart = diag.NumericUpDown1.Value
                        diag.Label1.Text = "Series step:"
                        diag.NumericUpDown1.Minimum = 1
                        diag.NumericUpDown1.Value = lastseriesstep
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            lastseriesstep = diag.NumericUpDown1.Value

                            diag.Label1.Text = "New number string length (Padded with zeroes)"
                            diag.NumericUpDown1.Value = lastseriesstringlength
                            If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                                lastseriesstringlength = diag.NumericUpDown1.Value

                                ProgressBar1.Value = 0
                                diag.WindowState = FormWindowState.Minimized
                                diag.Visible = False
                                Control_Enabler(True)
                                ClearLabels()
                                BackgroundWorker1.RunWorkerAsync("numberseriesfilenamerename")
                            End If
                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number series start value:"
                    diag.NumericUpDown1.Minimum = 0
                    diag.NumericUpDown1.Value = lastseriesstart
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastseriesstart = diag.NumericUpDown1.Value
                        diag.Label1.Text = "Series step:"
                        diag.NumericUpDown1.Minimum = 1
                        diag.NumericUpDown1.Value = lastseriesstep
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            lastseriesstep = diag.NumericUpDown1.Value

                            diag.Label1.Text = "New number string length (Padded with zeroes)"
                            diag.NumericUpDown1.Value = lastseriesstringlength
                            If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                                lastseriesstringlength = diag.NumericUpDown1.Value

                                ProgressBar1.Value = 0
                                diag.WindowState = FormWindowState.Minimized
                                diag.Visible = False
                                Control_Enabler(True)
                                ClearLabels()
                                BackgroundWorker1.RunWorkerAsync("numberseriesfilenamerename")
                            End If
                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "NumberSeriesFilenameRename")
        End Try
    End Sub

    Private Sub FilePrefixer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton4.Click
        Try
            operationlabel.Text = "File Name Prefixer"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName

                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Prefix to insert"
                    diag.TextBox1.Text = lastprefix
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastprefix = diag.TextBox1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("fileprefixer")
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Prefix to insert"
                    diag.TextBox1.Text = lastprefix
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastprefix = diag.TextBox1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("fileprefixer")


                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "fileprefixer")
        End Try
    End Sub

    Private Sub FileSuffixer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton5.Click
        Try
            operationlabel.Text = "File Name Suffixer"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName

                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Suffix to append:"
                    diag.TextBox1.Text = lastsuffix
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastsuffix = diag.TextBox1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("filesuffixer")
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "Suffix to append:"
                    diag.TextBox1.Text = lastsuffix
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastsuffix = diag.TextBox1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("filesuffixer")


                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "filesuffixer")
        End Try
    End Sub

    Private Sub FileNameToLowerCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton6.Click
        Try
            operationlabel.Text = "File Name to Lower Case"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName


                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametolowercase")
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath

                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametolowercase")
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "FileNameToLowerCase")
        End Try
    End Sub

    Private Sub FileNameToUpperCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton7.Click
        Try
            operationlabel.Text = "File Name to Upper Case"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName


                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametouppercase")
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath

                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametouppercase")
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "FileNameToUpperCase")
        End Try
    End Sub

    Private Sub FileNameToTitleCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton8.Click
        Try
            operationlabel.Text = "File Name to Title Case"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName


                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametotitlecase")
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath

                    ProgressBar1.Value = 0
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("filenametotitlecase")
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "FileNameToTitleCase")
        End Try
    End Sub

    Private Sub FileNameStringInserter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton9.Click
        Try
            operationlabel.Text = "File Name String Inserter"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName
                    Dim diag2 As Dialog2 = New Dialog2()
                    Dim diag As Dialog1 = New Dialog1()
                    diag.Label1.Text = "String to insert:"
                    diag.TextBox1.Text = laststringinsert
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        laststringinsert = diag.TextBox1.Text

                        diag2.Label1.Text = "Character index used insert the string at: (Not zero-based: i.e. 1 means the string will be inserted as the first character)"
                        diag2.NumericUpDown1.Text = laststringinsertposition
                        If diag2.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag2.NumericUpDown1.Text
                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            diag2.WindowState = FormWindowState.Minimized
                            diag2.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("filenamestringinserter")
                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                    diag2.Close()
                    diag2 = Nothing
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog1 = New Dialog1()
                    Dim diag2 As Dialog2 = New Dialog2()
                    diag.Label1.Text = "String to insert:"
                    diag.TextBox1.Text = laststringinsert
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        laststringinsert = diag.TextBox1.Text

                        diag2.Label1.Text = "Character index used insert the string at: (Not zero-based: i.e. 1 means the string will be inserted as the first character)"
                        diag2.NumericUpDown1.Minimum = 1
                        diag2.NumericUpDown1.Text = laststringinsertposition
                        If diag2.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag2.NumericUpDown1.Text
                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            diag2.WindowState = FormWindowState.Minimized
                            diag2.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("filenamestringinserter")
                        End If

                    End If
                    diag.Close()
                    diag = Nothing
                    diag2.Close()
                    diag2 = Nothing
                End If
                End If
                sender = Nothing
                e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "FileNameStringInserter")
        End Try
    End Sub

    Private Sub TruncateFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton10.Click
        Try
            operationlabel.Text = "Truncate File Name"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to remove from the end of the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("truncatefilename")
                    End If
                    diag.Close()
                    diag = Nothing
                  
                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to remove from the end of the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text

                        ProgressBar1.Value = 0
                        diag.WindowState = FormWindowState.Minimized
                        diag.Visible = False
                        Control_Enabler(True)
                        ClearLabels()
                        BackgroundWorker1.RunWorkerAsync("truncatefilename")
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "TruncateFileName")
        End Try
    End Sub

    Private Sub ShortenAnyPartOfFileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton11.Click
        Try
            operationlabel.Text = "Shorten any part of File Name"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to remove from the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text
                        diag.Label1.Text = "Character index used remove characters from: (Not zero-based: i.e. 1 means the characters will be removed starting from the first character)"
                        diag.NumericUpDown1.Text = laststringinsertposition
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag.NumericUpDown1.Text

                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("shortenanypartoffilename")
                        End If
                        end if
                        diag.Close()
                        diag = Nothing

                    End If
                Else
                    If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                        FolderBrowserDialog1.SelectedPath = lastfolder
                    End If
                    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to remove from the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text
                        diag.Label1.Text = "Character index used remove characters from: (Not zero-based: i.e. 1 means the characters will be removed starting from the first character)"
                        diag.NumericUpDown1.Text = laststringinsertposition
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag.NumericUpDown1.Text

                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("shortenanypartoffilename")
                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "ShortenAnyPartOfFileName")
        End Try
    End Sub

    Private Sub KeepFileNameSubstring_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton12.Click
        Try
            operationlabel.Text = "Keep Length-Defined File Name Substring"
            If RadioButton1.Checked = True Then
                If My.Computer.FileSystem.FileExists(lastfile) Then
                    OpenFileDialog1.FileName = lastfile
                End If
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfile = OpenFileDialog1.FileName
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to keep from the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text
                        diag.Label1.Text = "Character index used keep characters from: (Not zero-based: i.e. 1 means the characters will be removed starting from the first character)"
                        diag.NumericUpDown1.Text = laststringinsertposition
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag.NumericUpDown1.Text

                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("keepfilenamesubstring")
                        End If
                    End If
                    diag.Close()
                    diag = Nothing

                End If
            Else
                If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                    FolderBrowserDialog1.SelectedPath = lastfolder
                End If
                If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    lastfolder = FolderBrowserDialog1.SelectedPath
                    Dim diag As Dialog2 = New Dialog2()
                    diag.Label1.Text = "Number of characters to keep from the filename:"
                    diag.NumericUpDown1.Text = lastcharacterremovecount
                    If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                        lastcharacterremovecount = diag.NumericUpDown1.Text
                        diag.Label1.Text = "Character index used keep characters from: (Not zero-based: i.e. 1 means the characters will be removed starting from the first character)"
                        diag.NumericUpDown1.Text = laststringinsertposition
                        If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                            laststringinsertposition = diag.NumericUpDown1.Text

                            ProgressBar1.Value = 0
                            diag.WindowState = FormWindowState.Minimized
                            diag.Visible = False
                            Control_Enabler(True)
                            ClearLabels()
                            BackgroundWorker1.RunWorkerAsync("keepfilenamesubstring")
                        End If
                    End If
                    diag.Close()
                    diag = Nothing
                End If
            End If
            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "KeepFileNameSubstring")
        End Try
    End Sub

    Private Sub FolderPrefixer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton13.Click
        Try
            operationlabel.Text = "Folder Name Prefixer"
            RadioButton2.Checked = True

            
            If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                FolderBrowserDialog1.SelectedPath = lastfolder
            End If
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                lastfolder = FolderBrowserDialog1.SelectedPath
                Dim diag As Dialog1 = New Dialog1()
                diag.Label1.Text = "Prefix to insert"
                diag.TextBox1.Text = lastprefix
                If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                    lastprefix = diag.TextBox1.Text

                    ProgressBar1.Value = 0
                    diag.WindowState = FormWindowState.Minimized
                    diag.Visible = False
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("folderprefixer")
                End If
                diag.Close()
                diag = Nothing
            End If

            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "folderprefixer")
        End Try
    End Sub

    Private Sub FolderSuffixer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startAsyncButton14.Click
        Try
            operationlabel.Text = "Folder Name Suffixer"
            RadioButton2.Checked = True


            If My.Computer.FileSystem.DirectoryExists(lastfolder) Then
                FolderBrowserDialog1.SelectedPath = lastfolder
            End If
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                lastfolder = FolderBrowserDialog1.SelectedPath
                Dim diag As Dialog1 = New Dialog1()
                diag.Label1.Text = "Suffix to append"
                diag.TextBox1.Text = lastsuffix
                If diag.ShowDialog = Windows.Forms.DialogResult.OK Then
                    lastsuffix = diag.TextBox1.Text

                    ProgressBar1.Value = 0
                    diag.WindowState = FormWindowState.Minimized
                    diag.Visible = False
                    Control_Enabler(True)
                    ClearLabels()
                    BackgroundWorker1.RunWorkerAsync("foldersuffixer")
                End If
                diag.Close()
                diag = Nothing
            End If

            sender = Nothing
            e = Nothing
        Catch ex As Exception
            Error_Handler(ex, "foldersuffixer")
        End Try
    End Sub

    Private Sub buttonhover(ByVal sender As Object, ByVal e As System.EventArgs) Handles startAsyncButton1.MouseEnter, startAsyncButton2.MouseEnter, startAsyncButton3.MouseEnter, startAsyncButton4.MouseEnter, startAsyncButton5.MouseEnter, startAsyncButton6.MouseEnter, startAsyncButton7.MouseEnter, startAsyncButton8.MouseEnter, startAsyncButton9.MouseEnter, startAsyncButton10.MouseEnter, startAsyncButton11.MouseEnter, startAsyncButton12.MouseEnter, startAsyncButton13.MouseEnter, startAsyncButton14.MouseEnter
        Try
            Label6.Text = sender.Tag
        Catch ex As Exception
            Error_Handler(ex, "Display Button Tag")
        End Try
    End Sub
    Private Sub buttonleave(ByVal sender As Object, ByVal e As System.EventArgs) Handles startAsyncButton1.MouseLeave, startAsyncButton2.MouseLeave, startAsyncButton3.MouseLeave, startAsyncButton4.MouseLeave, startAsyncButton5.MouseLeave, startAsyncButton6.MouseLeave, startAsyncButton7.MouseLeave, startAsyncButton8.MouseLeave, startAsyncButton9.MouseLeave, startAsyncButton10.MouseLeave, startAsyncButton11.MouseLeave, startAsyncButton12.MouseLeave, startAsyncButton13.MouseEnter, startAsyncButton14.MouseEnter
        Try
            Label6.Text = "..."
        Catch ex As Exception
            Error_Handler(ex, "Remove Button Tag")
        End Try
    End Sub


End Class
