Option Explicit On

Imports System.IO
Imports Microsoft.Reporting.WinForms

Public Class MainForm

#Region "Properties"

    Private DBConnection As SqlClient.SqlConnection
    Private DT_NotSubmitted As ApplicationDataSet.NotSubmittedDataTable
    Private DT_ToEndorse As ApplicationDataSet.TimesheetDetailDataTable
    Private DT_ToApprove As ApplicationDataSet.TimesheetDetailDataTable
    Private DT_ManHours As ApplicationDataSet.TimesheetHeaderDataTable
    Private DT_Rejected As ApplicationDataSet.TimesheetDetailDataTable
    Private DT_Approved As ApplicationDataSet.TimesheetDetailDataTable
    Private DT_SpecialRigts As ApplicationDataSet.SpecialRightsDataTable

    Private DA_ToEndorse As SqlClient.SqlDataAdapter
    Private ToEndorseSelectCommand As SqlClient.SqlCommand

    Private DA_ToApprove As SqlClient.SqlDataAdapter
    Private ToApproveSelectCommand As SqlClient.SqlCommand

    Private DA_ManHours As SqlClient.SqlDataAdapter
    Private ManHoursSelectCommand As SqlClient.SqlCommand

    Private DA_Rejected As SqlClient.SqlDataAdapter
    Private RejectedSelectCommand As SqlClient.SqlCommand

    Private DA_Approved As SqlClient.SqlDataAdapter
    Private ApprovedSelectCommand As SqlClient.SqlCommand

    Private EndorseTSSQLCommand As SqlClient.SqlCommand
    Private ApproveTSSQLCommand As SqlClient.SqlCommand
    Private RejectTSSQLCommand As SqlClient.SqlCommand
    Private ManHoursSQLCommand As SqlClient.SqlCommand
    Private SpecialRightsSQLCommand As SqlClient.SqlCommand


    Private DefaultFont As Font ' default font used in the grids
    Private DT_Discipline As ApplicationDataSet.GenericListDataTable
    Private DT_FromYearWeek As ApplicationDataSet.GenericListDataTable
    Private DT_ToYearWeek As ApplicationDataSet.GenericListDataTable
    Private DT_CostCode As ApplicationDataSet.GenericListDataTable
    Private DT_CTRCode As ApplicationDataSet.GenericListDataTable
    Private DT_Department As ApplicationDataSet.GenericListDataTable
    Private DT_Company As ApplicationDataSet.GenericListDataTable
    Private DT_UserLogins As ApplicationDataSet.GenericListDataTable
    Private DT_AssignedLocation As ApplicationDataSet.GenericListDataTable
    Private DT_PAFNo As ApplicationDataSet.GenericListDataTable
    Private DT_RatePayCurrencyID As ApplicationDataSet.GenericListDataTable
    Private DT_StartDate As ApplicationDataSet.GenericListDataTable
    Private DT_DemobilisationDate As ApplicationDataSet.GenericListDataTable

    Private Const TSReportDefault As String = "Default timesheet report"
    Private Const TSReportGroupByCostCTRCode As String = "Report group by Cost and CTR codes"
    Private Const TSPendingManHour As String = "Pending Approval"
    Private Const TSAutomation As String = "Report history TS "
    Private UserConnected As String
    Private NotSubmittedFilter As String
    Private ToApprove_ToEndorseFilter As String
    Private Approved_RejectedFilter As String
    Private ReportFilter As String

    Public w As StreamWriter
    Public pathFileLog As String

    ' Droits utilisateur pour l'accès à l'onglet Timesheet adjustment
    Dim TSAdjustmentRead As Boolean
    Dim TSAdjustmentWrite As Boolean

    'Variables "de session" utilisé pour l'ajustment timesheet
    Private TSAdjEmpId As Integer
    Private TSAdjYear As String
    Private TSAdjWeek As String

    Private TSAdjTSDetails As TimesheetApproval.TSAdjustmentDataSet.TimesheetDetailDataTable
    Private TSAdjHours As TimesheetApproval.TSAdjustmentDataSet.HoursDataTable
    Private TSAdjTSError As TimesheetApproval.TSAdjustmentDataSet.TimesheetErrorDataTable
    Private TSAdjTSHeader As TimesheetApproval.TSAdjustmentDataSet.TimesheetHeaderDataTable

    Private currentInvoicingDate As Date

#End Region

#Region "Events"

#Region "IndexChanged"

    Private Sub TabChangehandler(ByVal sender As Object, ByVal e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            If TabControl1.SelectedTab.Name = ToEndorseTabPage.Name Or TabControl1.SelectedTab.Name = ToApproveTabPage.Name Or TabControl1.SelectedTab.Name = ManHoursTabPage.Name Or TabControl1.SelectedTab.Name = AccessRightsTabPage.Name Then
                Me.SaveButton.Enabled = True
                Me.SaveButton.Show()
            Else
                Me.SaveButton.Enabled = False
                Me.SaveButton.Hide()
            End If
            If TabControl1.SelectedTab.Name = ReportTabPage.Name Then ' report
                Me.RefreshButton_Click(sender, e)
            End If
            If TabControl1.SelectedTab.Name = AccessRightsTabPage.Name Then ' l'utilisateur clic qur l'onglet "manage filters"
                InitializeTabAccessRights()
            End If
            If TabControl1.SelectedTab.Name = TabPageTSAdjustment.Name Then ' l'utilisateur clic qur l'onglet "TimeSheet Adjustment"
                InitializeTabTSAdjustment()
                getCurrentInvoicingDate()
            End If
            ResizeHandler(sender, e)
        Catch ex As Exception
        Finally
            Log("INFO Tab Change : " & TabControl1.SelectedTab.Name, w)
            If TabControl1.SelectedTab.Name = ToEndorseTabPage.Name Then
                Log("INFO Request on tab : " & ToEndorseSelectCommand.CommandText, w)
            ElseIf TabControl1.SelectedTab.Name = ToApproveTabPage.Name Then
                Log("INFO Request on tab : " & ToApproveSelectCommand.CommandText, w)
            ElseIf TabControl1.SelectedTab.Name = ApprovedTabPage.Name Then
                Log("INFO Request on tab : " & ApprovedSelectCommand.CommandText, w)
            ElseIf TabControl1.SelectedTab.Name = RejectedTabPage.Name Then
                Log("INFO Request on tab : " & RejectedSelectCommand.CommandText, w)
            End If
        End Try
    End Sub

    Private Sub CostCodeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostCodeComboBox.SelectedIndexChanged
        Try
            Dim CostCode As String = CStr(CostCodeComboBox.SelectedValue)
            DT_CTRCode = GetCTRCodeList(CostCode)
            CTRCodeComboBox.SelectedValue = "%"
        Catch ex As Exception
            Log("ERROR CostCodeComboBox_SelectedIndexChanged :" + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub ChoiceUserComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sqlcmd_SpecialRights As SqlClient.SqlCommand

        Dim UserLogin As String
        Dim Department As String

        UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
        Department = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(1).ToString

        Dim reader As SqlClient.SqlDataReader

        'Affichage du bouton ADDRow pour les datagrid
        Me.ButtonAddRowTA.Visible = True
        Me.ButtonAddRowNS.Visible = True
        Me.ButtonAddRowOB.Visible = True

        'Chargement des données dans les TableAdapters (qui sont liés aux datagridsview)
        Me.FiltersTATableAdapter.Fill(Me.ApplicationDataSet.FiltersTA, UserLogin, Department)
        Me.FiltersNSTableAdapter.Fill(Me.ApplicationDataSet.FiltersNS, UserLogin, Department)
        Me.FiltersOBTableAdapter.Fill(Me.ApplicationDataSet.FiltersOB, UserLogin, Department)

        'Affichage des datagrids
        Me.ToApproveRightsDataGrid.Visible = True
        Me.NotSubmitRightsDataGridLeft.Visible = True
        Me.OnBehalfRightsDataGridLeft.Visible = True

        Me.SortDataGridsRightsTab()

        'Mapping department
        Me.TextBoxDept.Text = Department

        sqlcmd_SpecialRights =
            New SqlClient.SqlCommand("SELECT EntryOnBehalf,  " & _
                                            "EndorseTimesheets,  " & _
                                            "ApproveTimesheets,  " & _
                                            "ManHoursZero,  " & _
                                            "AccessRights,  " & _
                                            "ReportFullRights,  " & _
                                            "TSAdjustmentRead,  " & _
                                            "TSAdjustmentWrite  " & _
                                            "FROM SpecialRights " & _
                                            "WHERE [SpecialRights].[Userlogin] = '" & UserLogin & "'", DBConnection)


        If DBConnection.State <> ConnectionState.Open Then
            DBConnection.Open()
        End If
        Try
            reader = sqlcmd_SpecialRights.ExecuteReader
            reader.Read()
            Me.OnbehalfCheckBox.Checked = reader.GetBoolean(0)
            Me.EndorserCheckBox.Checked = reader.GetBoolean(1)
            Me.ApproverCheckBox.Checked = reader.GetBoolean(2)
            Me.ZeroManhourCheckBox.Checked = reader.GetBoolean(3)
            Me.AccessRightsCheckBox.Checked = reader.GetBoolean(4)
            Me.FullAccessReportCheckBox.Checked = reader.GetBoolean(5)
            Me.CheckBoxTSadjustRead.Checked = reader.GetBoolean(6)
            Me.CheckBoxTSadjustWrite.Checked = reader.GetBoolean(7)
            reader.Close()

        Catch ex As Exception
            Log("ERROR ChoiceUserComboBox_SelectedIndexChanged: " + ex.Message, w)
        End Try
    End Sub 'ChoiceUserComboBox_SelectedIndexChanged

    'Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
    '   If TabControl1.SelectedTab.Name = AccessRightsTabPage.Name Then ' l'utilisateur clic qur l'onglet "manage filters"
    '       InitializeTabAccessRights()
    '   End If
    '
    'End Sub

#End Region

#Region "Click"

    Private Sub ButtonMajDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMajDept.Click

        Dim SQLCmd As SqlClient.SqlCommand
        Dim Query As String
        Dim CurrentDept As String
        Dim NewDept As String
        Dim UserLogin As String

        Try

            If ChoiceUserComboBox.SelectedIndex = -1 Then
                MsgBox("You must select a user!", CType(vbExclamation, MsgBoxStyle), "")
                Return
            End If

            UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
            CurrentDept = Me.TextBoxDept.Text
            NewDept = Me.TextBoxDeptNew.Text

            DBConnection.Close()
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If

            'Cas où le current Dep = "" 
            If CurrentDept = "" Then
                Query = "INSERT INTO Filters (userLogin,Department,ToApproveFilter,OnBehalfFilter,NotSubmitFilter) VALUES ('" + UserLogin + "', '" + NewDept + "',0,0,0)"
                SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
                SQLCmd.ExecuteNonQuery()
            End If
            Query = "UPDATE Filters SET Department = '" + NewDept + "' WHERE UserLogin = '" + UserLogin + "' AND Department = '" + CurrentDept + "'"
            SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
            SQLCmd.ExecuteNonQuery()

            MsgBox("Departement successfully changed!", CType(vbInformation, MsgBoxStyle), "")
            'RAZ bouttons
            Me.TextBoxDept.Text = ""
            Me.TextBoxDeptNew.Text = ""
            Me.RefreshUserList()
        Catch ex As Exception
            MsgBox("Problem during the change of the department: " + ex.Message, CType(vbInformation, MsgBoxStyle), "")
            Log("ERROR ButtonMajDept_Click: " + ex.Message, w)
        Finally
            SQLCmd.Dispose()
            DBConnection.Close()
        End Try

    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Me.SaveButton.Enabled = False
        ' determine the current page/datagrid/datatable
        If TabControl1.SelectedTab.Name = ToEndorseTabPage.Name Then ' endorse tab
            If ValidData(ToEndorseRightDataGrid) Then
                SaveData(DT_ToEndorse)
                UpdateDatasets(DBConnection, NotSubmittedFilter, ToApprove_ToEndorseFilter, ReportFilter)
            End If
        End If
        If TabControl1.SelectedTab.Name = ToApproveTabPage.Name Then ' approve tab
            If ValidData(ToApproveRightDataGrid) Then
                SaveData(DT_ToApprove)
                UpdateDatasets(DBConnection, NotSubmittedFilter, ToApprove_ToEndorseFilter, ReportFilter)
            End If
        End If
        If TabControl1.SelectedTab.Name = ManHoursTabPage.Name Then ' man hours zero tab
            If ValidData(ManHoursRightDataGrid) Then
                SaveData(DT_ManHours)
                UpdateDatasets(DBConnection, NotSubmittedFilter, ToApprove_ToEndorseFilter, ReportFilter)
            End If
        End If
        If TabControl1.SelectedTab.Name = AccessRightsTabPage.Name Then ' access rights tab
            Try
                Me.Validate()
                'Sauvegarde dse combox box
                SaveSpecialRight()
                ' sauvegarde des données présentes dans les datagrid
                Me.FiltersNSBindingSource.EndEdit()
                Me.FiltersTABindingSource.EndEdit()
                Me.FiltersOBBindingSource.EndEdit()
                Me.FiltersNSTableAdapter.Update(Me.ApplicationDataSet.FiltersNS)
                Me.FiltersTATableAdapter.Update(Me.ApplicationDataSet.FiltersTA)
                Me.FiltersOBTableAdapter.Update(Me.ApplicationDataSet.FiltersOB)
                MsgBox("Save successful")
            Catch ex As Exception
                Log("ERROR During SaveButton_Click pour AccessRightsTabPage: " + ex.Message, w)
                MsgBox("ERROR During SaveButton_Click: " + ex.Message, CType(vbExclamation, MsgBoxStyle), "")
            End Try
        End If
        Me.SaveButton.Enabled = True
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click, Refresh2Button.Click
        If TabControl1.SelectedTab.Name <> ReportTabPage.Name Then
            UpdateDatasets(DBConnection, NotSubmittedFilter, ToApprove_ToEndorseFilter, Approved_RejectedFilter)
        Else ' report tab is selected

            Select Case CType(Me.ReportFormatComboBox.SelectedItem, String)
                Case TSReportDefault
                    ShowOrHideFilterForReport(TSReportDefault)
                    Me.ReportViewer.Reset()
                    Me.ReportViewer.LocalReport.LoadReportDefinition(New IO.StringReader(My.Resources.TimesheetDefault))
                    ReportViewer.Tag = TSReportDefault
                    Me.ReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("TimesheetDetail"))

                    Me.ReportViewer.LocalReport.DataSources.Item("TimesheetDetail").Value _
                                = GetReportingDT(CType(DisciplineComboBox.SelectedValue, String), _
                                                 GetFilterCostcodeAll(CType(CostCodeComboBox.SelectedValue, String)), _
                                                 GetFilterCtrcodeAll(CType(CTRCodeComboBox.SelectedValue, String)), _
                                                 CType(FromYearWeekComboBox.SelectedValue, String), _
                                                 CType(ToYearWeekComboBox.SelectedValue, String), _
                                                 ReportFilter)
                    ' Creation parameters for report Header 
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CTRCode", Me.CTRCodeComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("StartDate", Me.FromYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("EndDate", Me.ToYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("Discipline", Me.DisciplineComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CostCode", Me.CostCodeComboBox.Text))
                    Me.ReportViewer.RefreshReport()

                Case TSReportGroupByCostCTRCode
                    ShowOrHideFilterForReport(TSReportGroupByCostCTRCode)
                    Me.ReportViewer.Reset()
                    Me.ReportViewer.LocalReport.LoadReportDefinition(New IO.StringReader(My.Resources.TimesheetDefaultCostCTRCodeGrouping))
                    ReportViewer.Tag = TSReportGroupByCostCTRCode
                    Me.ReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("TimesheetDetail"))

                    Me.ReportViewer.LocalReport.DataSources.Item("TimesheetDetail").Value _
                                    = GetReportingDT(CType(DisciplineComboBox.SelectedValue, String), _
                                                     GetFilterCostcodeAll(CType(CostCodeComboBox.SelectedValue, String)), _
                                                     GetFilterCtrcodeAll(CType(CTRCodeComboBox.SelectedValue, String)), _
                                                     CType(FromYearWeekComboBox.SelectedValue, String), _
                                                     CType(ToYearWeekComboBox.SelectedValue, String), _
                                                     ReportFilter)
                    ' Creation parameters for report Header 
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CTRCode", Me.CTRCodeComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("StartDate", Me.FromYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("EndDate", Me.ToYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("Discipline", Me.DisciplineComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CostCode", Me.CostCodeComboBox.Text))
                    Me.ReportViewer.RefreshReport()

                Case TSPendingManHour
                    ShowOrHideFilterForReport(TSPendingManHour)
                    Me.ReportViewer.Reset()
                    Me.ReportViewer.LocalReport.LoadReportDefinition(New IO.StringReader(My.Resources.PendingManHour))
                    ReportViewer.Tag = TSPendingManHour
                    Me.ReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("PendingManHour"))

                    Me.ReportViewer.LocalReport.DataSources.Item("PendingManHour").Value _
                                    = GetReportingPending(CType(DisciplineComboBox.SelectedValue, String), _
                                                     GetFilterCostcodeAll(CType(CostCodeComboBox.SelectedValue, String)), _
                                                     GetFilterCtrcodeAll(CType(CTRCodeComboBox.SelectedValue, String)), _
                                                     CType(FromYearWeekComboBox.SelectedValue, String), _
                                                     CType(ToYearWeekComboBox.SelectedValue, String), _
                                                     ReportFilter)

                    ' Creation parameters for report Header                               
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CTRCode", Me.CTRCodeComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("StartDate", Me.FromYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("EndDate", Me.ToYearWeekComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("Discipline", Me.DisciplineComboBox.Text))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CostCode", Me.CostCodeComboBox.Text))
                    Me.ReportViewer.RefreshReport()

                Case TSAutomation
                    ShowOrHideFilterForReport(TSAutomation)
                    Me.ReportViewer.Reset()
                    Me.ReportViewer.LocalReport.LoadReportDefinition(New IO.StringReader(My.Resources.Automation))
                    ReportViewer.Tag = TSPendingManHour
                    Me.ReportViewer.LocalReport.DataSources.Clear()
                    Me.ReportViewer.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("Automation"))

                    Me.ReportViewer.LocalReport.DataSources.Item("Automation").Value _
                                    = GetReportingAutomation(GetFilterDepartmentAll(CType(DepartmentComboBox.SelectedValue, String)), _
                                                     GetFilterCtrcodeAll(CType(CTRCodeComboBox.SelectedValue, String)), _
                                                     CType(FromYearWeekComboBox.SelectedValue, String), _
                                                     CType(ToYearWeekComboBox.SelectedValue, String), _
                                                     CType(CompanyComboBox.SelectedValue, String), _
                                                     CType(AssignedLocationComboBox.SelectedValue, String), _
                                                     CType(PAFNoComboBox.SelectedValue, String), _
                                                     CType(RatePayCurrencyIDComboBox.SelectedValue, String), _
                                                     StartDateTimePicker, _
                                                     DemobilisationDateTimePicker, _
                                                     ReportFilter)

                    ' Creation parameters for report Header                    
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("DepartmentNo", Me.DepartmentComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("CTRCode", Me.CTRCodeComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("StartDate", Me.FromYearWeekComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("EndDate", Me.ToYearWeekComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("Company", Me.CompanyComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("AssignedLocation", Me.AssignedLocationComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("PAFNo", Me.PAFNoComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("RatePayCurrencyID", Me.RatePayCurrencyIDComboBox.SelectedValue.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("StartDatePAF", Me.StartDateTimePicker.Value.ToString))
                    Me.ReportViewer.LocalReport.SetParameters(New ReportParameter("DemobilisationDate", Me.DemobilisationDateTimePicker.Value.ToString))
                    Me.ReportViewer.RefreshReport()

                Case Else
                    MsgBox("Select a report format from the list", MsgBoxStyle.Exclamation, "Error")
                    Return
            End Select
        End If
    End Sub

    Private Sub CreateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateButton.Click
        Try
            'affichage dialog box
            If NewUserTextBox.Text = "" Then
                MsgBox("You must enter a user login", CType(vbExclamation, MsgBoxStyle), "")
            Else
                Dim Count As Integer
                Dim cmd As New System.Data.SqlClient.SqlCommand
                cmd.CommandType = System.Data.CommandType.Text
                cmd.Connection = DBConnection
                If DBConnection.State <> ConnectionState.Open Then
                    DBConnection.Open()
                End If

                ' Si le dept saisi = "" On vérifie si le user n'existe pas déjà dans spécial Right
                If TextBoxDeptCreate.Text = "" Then
                    cmd.CommandText = "SELECT COUNT(*) FROM SpecialRights WHERE UserLogin = '" + NewUserTextBox.Text + "'"
                    Count = CInt(cmd.ExecuteScalar())

                    If Count <> 0 Then
                        MsgBox("The user/dep already exists", CType(vbCritical, MsgBoxStyle), "")
                    Else
                        cmd.CommandText = "INSERT INTO SpecialRights (userLogin,EntryOnBehalf,EndorseTimesheets,ApproveTimesheets," & _
                                  " ManHoursZero,AccessRights,ReportFullRights) VALUES ('" + NewUserTextBox.Text + "',0,0,0,0,0,0)"
                        cmd.ExecuteNonQuery()
                        MsgBox("User successfully created!", CType(vbInformation, MsgBoxStyle), "")
                    End If
                    'Si le dep saisi <> "" on vérifie s'il n'existe pas d'entrée dans spécial Right et Filters
                Else
                    cmd.CommandText = "SELECT COUNT(*) FROM SpecialRights WHERE UserLogin = '" + NewUserTextBox.Text + "'"
                    Count = CInt(cmd.ExecuteScalar())

                    ' le user n'existe pas dans special right , il faut le créer
                    If Count = 0 Then
                        cmd.CommandText = "INSERT INTO SpecialRights (userLogin,EntryOnBehalf,EndorseTimesheets,ApproveTimesheets," & _
                                  " ManHoursZero,AccessRights,ReportFullRights) VALUES ('" + NewUserTextBox.Text + "',0,0,0,0,0,0)"
                        cmd.ExecuteNonQuery()
                    End If

                    cmd.CommandText = "SELECT COUNT(*) FROM Filters WHERE UserLogin = '" + NewUserTextBox.Text + "' AND Department = '" + TextBoxDeptCreate.Text + "'"
                    Count = CInt(cmd.ExecuteScalar())

                    If Count <> 0 Then
                        MsgBox("The user/dep already exists", CType(vbCritical, MsgBoxStyle), "")
                    Else
                        cmd.CommandText = "INSERT INTO Filters (userLogin,Department,ToApproveFilter,OnBehalfFilter,NotSubmitFilter) VALUES ('" + NewUserTextBox.Text + "', '" + TextBoxDeptCreate.Text + "',0,0,0)"
                        cmd.ExecuteNonQuery()
                        MsgBox("User successfully created!", CType(vbInformation, MsgBoxStyle), "")
                    End If

                End If
                'Remise à zéro du champ
                NewUserTextBox.Text = ""
                TextBoxDeptCreate.Text = ""

                Me.RefreshUserList()

            End If
        Catch ex As Exception
            MsgBox("Problem during the creation: " + ex.Message, CType(vbInformation, MsgBoxStyle), "")
            Log("ERROR Create user button click: " + ex.Message, w)
        Finally
            DBConnection.Close()
        End Try
    End Sub

    Private Sub ButtonAddRowTA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRowTA.Click
        Dim UserLogin As String
        Dim Department As String

        UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
        Department = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(1).ToString

        'ajout de la nouvelle ligne
        FiltersTATableAdapter.Insert(UserLogin, Department)
        Me.Validate()
        Me.FiltersTABindingSource.EndEdit()
        Me.FiltersTATableAdapter.Update(Me.ApplicationDataSet.FiltersTA)
        'maj du tableadapter
        FiltersTATableAdapter.Fill(Me.ApplicationDataSet.FiltersTA, UserLogin, Department)

    End Sub

    Private Sub ButtonAddRowNS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRowNS.Click
        Dim UserLogin As String
        Dim Department As String

        UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
        Department = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(1).ToString

        'ajout de la nouvelle ligne
        FiltersNSTableAdapter.Insert(UserLogin, Department)
        Me.Validate()
        Me.FiltersNSBindingSource.EndEdit()
        Me.FiltersNSTableAdapter.Update(Me.ApplicationDataSet.FiltersNS)
        'maj du tableadapter
        FiltersNSTableAdapter.Fill(Me.ApplicationDataSet.FiltersNS, UserLogin, Department)

    End Sub

    Private Sub ButtonAddRowOB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRowOB.Click
        Dim UserLogin As String
        Dim Department As String

        UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
        Department = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(1).ToString

        'ajout de la nouvelle ligne
        FiltersOBTableAdapter.Insert(UserLogin, Department)
        Me.Validate()
        Me.FiltersOBBindingSource.EndEdit()
        Me.FiltersOBTableAdapter.Update(Me.ApplicationDataSet.FiltersOB)
        'maj du tableadapter
        FiltersOBTableAdapter.Fill(Me.ApplicationDataSet.FiltersOB, UserLogin, Department)

    End Sub

    Private Sub ButtonTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTest.Click

        Dim SQLCmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim GeneratedFilter As String
        Dim Query As String
        Dim selectedUser As String
        Dim MsgboxTitle As String
        Try


            If ChoiceUserComboBox.SelectedIndex = -1 Then
                MsgBox("You must select a user!", CType(vbExclamation, MsgBoxStyle), "")
                Return
            End If

            selectedUser = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString

            Query = ""
            MsgboxTitle = ""

            DBConnection.Close()
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If

            'We generate the filter according to the tab where the user is
            If TabControl2.SelectedTab.Name = TabPage1.Name Then
                Query = "SELECT [Timesheet].[dbo].[GetToApproveFilter] ('" & selectedUser & "')"
                MsgboxTitle = "To approve filter for "
            End If
            If TabControl2.SelectedTab.Name = TabPage2.Name Then
                Query = "SELECT [Timesheet].[dbo].[GetNotSubmittedFilter]  ('" & selectedUser & "')"
                MsgboxTitle = "Not submited filter for "
            End If
            If TabControl2.SelectedTab.Name = TabPage3.Name Then
                Query = "SELECT [Timesheet].[dbo].[GetOnBehalfFilter]  ('" & selectedUser & "')"
                MsgboxTitle = "on Behalf filter for "
            End If

            MsgboxTitle = MsgboxTitle + selectedUser

            'Execute the query
            SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
            reader = SQLCmd.ExecuteReader
            reader.Read()
            GeneratedFilter = reader.GetString(0)

            MsgBox(GeneratedFilter, CType(vbInformation, MsgBoxStyle), MsgboxTitle)

            reader.Close()

        Catch ex As Exception
            Log("ERROR During ButtonTest_Click: " + ex.Message, w)
            MsgBox("ERROR During ButtonTest_Click: " + ex.Message, CType(vbExclamation, MsgBoxStyle), "")
        Finally
            DBConnection.Close()
        End Try
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        Try
            'affichage dialog box
            Dim answer As Integer
            answer = MsgBox("Do you really want to delete this user?", CType(vbQuestion + vbYesNo, MsgBoxStyle), "Confirm delete")
            If answer = vbYes Then
                'Suppression de l'utilisateur sélectionné de la table special rights
                Dim UserLogin As String
                Dim Department As String
                Dim Count As Integer

                UserLogin = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString
                Department = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(1).ToString
                Dim cmd As New System.Data.SqlClient.SqlCommand
                cmd.CommandType = System.Data.CommandType.Text


                'Suppression du user/dep de la table filter 
                cmd.CommandText = "DELETE FROM Filters WHERE userLogin = '" + UserLogin + "' AND Department = '" + Department + "'"
                cmd.Connection = DBConnection
                If DBConnection.State <> ConnectionState.Open Then
                    DBConnection.Open()
                End If
                cmd.ExecuteNonQuery()

                'S'il ne reste plus de rows dans la table Filters alors on supprime le user de SpecialRigths
                cmd.CommandText = "SELECT COUNT(*) FROM Filters WHERE userLogin = '" + UserLogin + "'"
                Count = CInt(cmd.ExecuteScalar())

                If Count = 0 Then
                    cmd.CommandText = "DELETE FROM SpecialRights WHERE userLogin = '" + UserLogin + "'"
                    cmd.ExecuteNonQuery()
                End If

                DBConnection.Close()
                MsgBox("User successfully deleted!", CType(vbInformation, MsgBoxStyle), "")
                'Remise à zéro de la liste déroulante
                Me.RefreshUserList()

            End If
        Catch ex As Exception
            MsgBox("Problem during the suppression: " + ex.Message, CType(vbInformation, MsgBoxStyle), "")
            Log("ERROR delete user button click: " + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub ButtonViewLocations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewLocations.Click
        Locations.Show()
    End Sub

    Private Sub ButtonViewDiscipline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewDiscipline.Click
        Discipline.Show()
    End Sub

    Private Sub ButtonViewDisciplines2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewDisciplines2.Click
        Discipline.Show()
    End Sub

    Private Sub ButtonViewlocations2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonViewlocations2.Click
        Locations.Show()
    End Sub

#End Region

#Region "Others"

    Private Sub HeaderPaintHandler(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles ToEndorseDataGrid.CellPainting, ToApproveDataGrid.CellPainting
        Dim ColumnsTohandle As String() = {"NTSa", "NTSu", "NTMo", "NTTu", "NTWe", "NTTh", "NTFr", "OTSa", "OTSu", "OTMo", "OTTu", "OTWe", "OTTh", "OTFr", "Total"}
        If e.RowIndex = -1 Then ' this is the header row
            If ColumnsTohandle.Contains(CType(e.Value, String)) Then
                Dim DrawingFormat As New Drawing.StringFormat
                DrawingFormat.FormatFlags = StringFormatFlags.DirectionVertical
                DrawingFormat.Alignment = StringAlignment.Center
                ' DrawingFormat.LineAlignment = StringAlignment.Center
                e.PaintBackground(e.CellBounds, True)
                e.Graphics.DrawString(CType(e.Value, String), DefaultFont, Brushes.Black, e.CellBounds, DrawingFormat)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub ResizeHandler(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        Const RightPanelWidth As Integer = 220
        Try
            Me.TabControl1.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - Me.StatusStrip1.Height - Me.SaveButton.Height - 5)
            Me.SaveButton.Location = New Point(Me.ClientSize.Width - Me.SaveButton.Width, Me.ClientSize.Height - Me.StatusStrip1.Height - Me.SaveButton.Height - 2)
            Me.RefreshButton.Location = New Point(0, Me.ClientSize.Height - Me.StatusStrip1.Height - Me.RefreshButton.Height - 2)

            Me.NotSubmittedDataGrid.Size = Me.NotSubmittedTabPage.ClientSize

            Me.ToApproveDataGrid.Size = New Size(Me.ToApproveTabPage.ClientSize.Width - RightPanelWidth, Me.ToApproveTabPage.ClientSize.Height)
            Me.ToApproveRightDataGrid.Location = New Point(Me.ToApproveTabPage.ClientSize.Width - RightPanelWidth, ToApproveDataGrid.Location.Y)
            Me.ToApproveRightDataGrid.Size = New Size(RightPanelWidth, Me.ToApproveDataGrid.Height)
            Me.ToApproveRightDataGrid.ColumnHeadersHeight = Me.ToApproveDataGrid.ColumnHeadersHeight

            Me.ToEndorseDataGrid.Size = New Size(Me.ToEndorseTabPage.ClientSize.Width - RightPanelWidth, Me.ToEndorseTabPage.ClientSize.Height)
            Me.ToEndorseRightDataGrid.Location = New Point(Me.ToEndorseTabPage.ClientSize.Width - RightPanelWidth, ToEndorseDataGrid.Location.Y)
            Me.ToEndorseRightDataGrid.Size = New Size(RightPanelWidth, Me.ToEndorseDataGrid.Height)
            Me.ToEndorseRightDataGrid.ColumnHeadersHeight = Me.ToEndorseDataGrid.ColumnHeadersHeight


            Me.ManHoursDataGrid.Size = New Size(650, Me.ManHoursTabPage.ClientSize.Height)
            Me.ManHoursRightDataGrid.Size = New Size(180, Me.ManHoursTabPage.ClientSize.Height)
            Me.ManHoursDataGrid.AutoResizeColumnHeadersHeight()
            Me.ManHoursRightDataGrid.AutoResizeColumnHeadersHeight()
            Me.ManHoursRightDataGrid.ColumnHeadersHeight = Me.ManHoursDataGrid.ColumnHeadersHeight

            Me.ApprovedDataGrid.Size = Me.ApprovedTabPage.ClientSize
            Me.RejectedDataGrid.Size = Me.RejectedTabPage.ClientSize

            Me.ReportingSplitContainer.Size = Me.ReportTabPage.ClientSize
            Me.ReportingSplitContainer.SplitterDistance = 100
            Me.ReportViewer.Location = New Point(0, 0)
            Me.ReportViewer.Size = Me.ReportingSplitContainer.Panel2.ClientSize
        Catch ex As Exception
            Log("ERROR ResizeHandler : " + ex.Message, w)
        End Try
    End Sub

    Private Sub ApproveRejectChangeHandler(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles ToApproveRightDataGrid.CellClick, ToEndorseRightDataGrid.CellClick
        ' manage change between the two checkboxes Approve and Reject so that only one is selected at any one time
        ' note that this Sub handles the ToApprove and ToEndorse
        ' col 0 is approve or endorse
        ' col 1 is reject
        Try
            If e.ColumnIndex = 2 Then ' col 2 is reason - not handled
                Return
            End If

            Dim ApproveCheckbox As DataGridViewCheckBoxCell
            Dim RejectCheckbox As DataGridViewCheckBoxCell
            ApproveCheckbox = CType(CType(sender, DataGridView).Item(0, e.RowIndex), DataGridViewCheckBoxCell)
            RejectCheckbox = CType(CType(sender, DataGridView).Item(1, e.RowIndex), DataGridViewCheckBoxCell)
            If e.ColumnIndex = 0 Then ' approve column
                If CType(ApproveCheckbox.Value, Boolean) Then
                    ApproveCheckbox.Value = False
                Else
                    ApproveCheckbox.Value = True
                    RejectCheckbox.Value = False
                End If
            End If
            If e.ColumnIndex = 1 Then ' reject column
                If CType(RejectCheckbox.Value, Boolean) Then
                    RejectCheckbox.Value = False
                Else
                    RejectCheckbox.Value = True
                    ApproveCheckbox.Value = False
                End If
            End If
        Catch ex As Exception
            Log("ERROR ApproveRejectChangeHandler: " + ex.Message, w)
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

#Region "Init"

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialiseComponents()

        Dim UserRole As String
        GetUserRights(DBConnection, UserRole, UserConnected, NotSubmittedFilter, ToApprove_ToEndorseFilter, Approved_RejectedFilter, ReportFilter)
        Me.UserName.Text = UserConnected + " (" + UserRole + ")"
        UpdateDatasets(DBConnection, NotSubmittedFilter, ToApprove_ToEndorseFilter, Approved_RejectedFilter)
        ResizeHandler(Nothing, Nothing)

        Me.ReportViewer.RefreshReport()
        Me.ReportViewerTSAdj.RefreshReport()
    End Sub

    Private Sub InitialiseComponents()
        Try
            Const HoursColumnWidth As Integer = 30

            DefaultFont = New Font("Arial", 7, FontStyle.Regular)

            DBConnection = New SqlClient.SqlConnection(My.Settings.DB_CONNECTION)

            Me.NotSubmittedDataGrid.ReadOnly = True

            EndorseTSSQLCommand = New SqlClient.SqlCommand
            EndorseTSSQLCommand.Connection = DBConnection
            EndorseTSSQLCommand.CommandType = CommandType.StoredProcedure
            EndorseTSSQLCommand.CommandText = "EndorseTimeSheetRow"
            EndorseTSSQLCommand.Parameters.Add("@EmployeeID", SqlDbType.Int)
            EndorseTSSQLCommand.Parameters.Add("@Year", SqlDbType.Int)
            EndorseTSSQLCommand.Parameters.Add("@Week", SqlDbType.Int)
            EndorseTSSQLCommand.Parameters.Add("@CostCode", SqlDbType.VarChar, 50)
            EndorseTSSQLCommand.Parameters.Add("@CTRCode", SqlDbType.VarChar, 50)
            EndorseTSSQLCommand.Parameters.Add("@UserLogin", SqlDbType.VarChar, 50)

            ApproveTSSQLCommand = New SqlClient.SqlCommand
            ApproveTSSQLCommand.Connection = DBConnection
            ApproveTSSQLCommand.CommandType = CommandType.StoredProcedure
            ApproveTSSQLCommand.CommandText = "ApproveTimeSheetRow"
            ApproveTSSQLCommand.Parameters.Add("@EmployeeID", SqlDbType.Int)
            ApproveTSSQLCommand.Parameters.Add("@Year", SqlDbType.Int)
            ApproveTSSQLCommand.Parameters.Add("@Week", SqlDbType.Int)
            ApproveTSSQLCommand.Parameters.Add("@CostCode", SqlDbType.VarChar, 50)
            ApproveTSSQLCommand.Parameters.Add("@CTRCode", SqlDbType.VarChar, 50)
            ApproveTSSQLCommand.Parameters.Add("@UserLogin", SqlDbType.VarChar, 50)

            ManHoursSQLCommand = New SqlClient.SqlCommand
            ManHoursSQLCommand.Connection = DBConnection
            ManHoursSQLCommand.CommandType = CommandType.StoredProcedure
            ManHoursSQLCommand.CommandText = "RejectedZeroHoursRow"
            ManHoursSQLCommand.Parameters.Add("@EmployeeID", SqlDbType.Int)
            ManHoursSQLCommand.Parameters.Add("@Year", SqlDbType.Int)
            ManHoursSQLCommand.Parameters.Add("@Week", SqlDbType.Int)
            ManHoursSQLCommand.Parameters.Add("@RejectReason", SqlDbType.VarChar, 100)

            RejectTSSQLCommand = New SqlClient.SqlCommand
            RejectTSSQLCommand.Connection = DBConnection
            RejectTSSQLCommand.CommandType = CommandType.StoredProcedure
            RejectTSSQLCommand.CommandText = "RejectTimeSheetRow"
            RejectTSSQLCommand.Parameters.Add("@EmployeeID", SqlDbType.Int)
            RejectTSSQLCommand.Parameters.Add("@Year", SqlDbType.Int)
            RejectTSSQLCommand.Parameters.Add("@Week", SqlDbType.Int)
            RejectTSSQLCommand.Parameters.Add("@CostCode", SqlDbType.VarChar, 50)
            RejectTSSQLCommand.Parameters.Add("@CTRCode", SqlDbType.VarChar, 50)
            RejectTSSQLCommand.Parameters.Add("@RejectReason", SqlDbType.VarChar, 100)
            RejectTSSQLCommand.Parameters.Add("@UserLogin", SqlDbType.VarChar, 50)

            DA_ToEndorse = New SqlClient.SqlDataAdapter()
            ToEndorseSelectCommand = New SqlClient.SqlCommand
            ToEndorseSelectCommand.Connection = DBConnection
            ToEndorseSelectCommand.CommandType = CommandType.Text
            ToEndorseSelectCommand.CommandText = "SELECT	TS.[Year], TS.[Week], " & _
                                                "		[EmployeeInfo].[EmployeeNumber]," & _
                                                "		[EmployeeInfo].[EmployeeID]," & _
                                                "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                                                "		[EmployeeInfo].[LocationID]," & _
                                                "		[Discipline], [DisciplineDescription]," & _
                                                "		TS.[CostCode]," & _
                                                "		TS.[CTRCode]," & _
                                                "		[CTRCode].[Description]," & _
                                                "		TS.[NTSaturday], TS.[NTSunday], TS.[NTMonday], TS.[NTTuesday], TS.[NTWednesday], TS.[NTThursday], TS.[NTFriday], TS.[TotalNormalTime], " & _
                                                "		TS.[OTSaturday], TS.[OTSunday], TS.[OTMonday], TS.[OTTuesday], TS.[OTWednesday], TS.[OTThursday], TS.[OTFriday], TS.[TotalOverTime], " & _
                                                "       Replace(convert(varchar(11),TS.[SubmittedDate],13),' ','-') As SubmittedDate," & _
                                                "       Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As StartDate " & _
                                                "FROM [EmployeeInfo] " & _
                                                "INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                                "INNER JOIN [YearWeek] ON TS.[Week] = [YearWeek].[Week] AND TS.[Year] = [YearWeek].[Year] " & _
                                                "INNER JOIN [CTRCode] ON TS.[CTRCode] = [CTRCode].[CTRCode] AND TS.[CostCode] = [CTRCode].[CostCode] " & _
                                                "WHERE TS.[Submitted] = 1 " & _
                                                "AND ISNULL(EmployeeInfo.EmployeeLogin, '') <> system_user"

            ' Note that the update command cannot be built this was as it calls either endorse or reject
            DA_ToEndorse.SelectCommand = ToEndorseSelectCommand
            DA_ToEndorse.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                                                {New System.Data.Common.DataTableMapping("Table", "TimesheetDetail", New System.Data.Common.DataColumnMapping() { _
                                                 New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                                                 New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNo"), _
                                                 New System.Data.Common.DataColumnMapping("Name", "EmployeeName"), _
                                                 New System.Data.Common.DataColumnMapping("LocationID", "Location"), _
                                                 New System.Data.Common.DataColumnMapping("Year", "Year"), _
                                                 New System.Data.Common.DataColumnMapping("Week", "Week"), _
                                                 New System.Data.Common.DataColumnMapping("StartWeek", "StartDate"), _
                                                 New System.Data.Common.DataColumnMapping("CostCode", "CostCode"), _
                                                 New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode"), _
                                                 New System.Data.Common.DataColumnMapping("Description", "Description"), _
                                                 New System.Data.Common.DataColumnMapping("NTSaturday", "NTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("NTSunday", "NTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("NTMonday", "NTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("NTTuesday", "NTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTWednesday", "NTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTThursday", "NTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("NTFriday", "NTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalNormalTime", "NTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("OTSaturday", "OTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("OTSunday", "OTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("OTMonday", "OTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("OTTuesday", "OTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTWednesday", "OTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTThursday", "OTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("OTFriday", "OTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalOverTime", "OTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("SubmittedDate", "SubmittedDate")})})

            DT_ToEndorse = New ApplicationDataSet.TimesheetDetailDataTable
            Me.ToEndorseDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ToEndorseDataGrid.DataSource = DT_ToEndorse

            ' ---
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.LocationColumn.ColumnName, "Location", 50))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.EmployeeNoColumn.ColumnName, "Employee No.", 60))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.EmployeeNameColumn.ColumnName, "Name", 100))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.CostCodeColumn.ColumnName, "Cost Code", 40))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.CTRCodeColumn.ColumnName, "CTRCode", 65))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.DescriptionColumn.ColumnName, "Description", 120))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.YearColumn.ColumnName, "Year", 40))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.WeekColumn.ColumnName, "Wk", 25))
            Me.ToEndorseDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.StartDateColumn.ColumnName, "StartWeek", 70))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTTotalColumn.ColumnName, "NTTotal", 50))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTTotalColumn.ColumnName, "OTTotal", 50))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTSaturdayColumn.ColumnName, "NTSa", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTSundayColumn.ColumnName, "NTSu", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTMondayColumn.ColumnName, "NTMo", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTTuesdayColumn.ColumnName, "NTTu", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTWednesdayColumn.ColumnName, "NTWe", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTThursdayColumn.ColumnName, "NTTh", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.NTFridayColumn.ColumnName, "NTFr", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTSaturdayColumn.ColumnName, "OTSa", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTSundayColumn.ColumnName, "OTSu", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTMondayColumn.ColumnName, "OTMo", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTTuesdayColumn.ColumnName, "OTTu", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTWednesdayColumn.ColumnName, "OTWe", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTThursdayColumn.ColumnName, "OTTh", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewHourColumn(DT_ToEndorse.OTFridayColumn.ColumnName, "OTFr", HoursColumnWidth))
            Me.ToEndorseDataGrid.Columns.Add(NewDateColumn(DT_ToEndorse.SubmittedDateColumn.ColumnName, "Submitted", 70))
            Me.ToEndorseDataGrid.ScrollBars = ScrollBars.Horizontal

            ToEndorseRightDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ToEndorseRightDataGrid.DataSource = DT_ToEndorse
            ToEndorseRightDataGrid.RowHeadersVisible = False
            ToEndorseRightDataGrid.AllowUserToAddRows = False
            ToEndorseRightDataGrid.AllowUserToDeleteRows = False
            ToEndorseRightDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            ToEndorseRightDataGrid.ScrollBars = ScrollBars.Vertical

            ToEndorseRightDataGrid.Columns.Add(NewCheckboxColumn(DT_ToEndorse.EndorsedColumn.ColumnName, "Endorse", 50))
            ToEndorseRightDataGrid.Columns.Add(NewCheckboxColumn(DT_ToEndorse.RejectedColumn.ColumnName, "Reject", 50))
            ToEndorseRightDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.RejectReasonColumn.ColumnName, "Reason", 120, False))

            ' ---

            DA_ToApprove = New SqlClient.SqlDataAdapter()
            ToApproveSelectCommand = New SqlClient.SqlCommand
            ToApproveSelectCommand.Connection = DBConnection
            ToApproveSelectCommand.CommandType = CommandType.Text
            ToApproveSelectCommand.CommandText = "SELECT	TS.[Year], TS.[Week], " & _
                                                "		[EmployeeInfo].[EmployeeNumber]," & _
                                                "		[EmployeeInfo].[EmployeeID]," & _
                                                "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                                                "		[EmployeeInfo].[LocationID]," & _
                                                "		[Discipline], [DisciplineDescription]," & _
                                                "		TS.[CostCode]," & _
                                                "		TS.[CTRCode]," & _
                                                "		[CTRCode].[Description]," & _
                                                "		TS.[NTSaturday], TS.[NTSunday], TS.[NTMonday], TS.[NTTuesday], TS.[NTWednesday], TS.[NTThursday], TS.[NTFriday], TS.[TotalNormalTime], " & _
                                                "		TS.[OTSaturday], TS.[OTSunday], TS.[OTMonday], TS.[OTTuesday], TS.[OTWednesday], TS.[OTThursday], TS.[OTFriday], TS.[TotalOverTime], " & _
                                                "       Replace(convert(varchar(11),TS.[SubmittedDate],13),' ','-') As SubmittedDate," & _
                                                "       Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As StartDate " & _
                                                "FROM [EmployeeInfo] " & _
                                                "INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                                "INNER JOIN [YearWeek] ON TS.[Week] = [YearWeek].[Week] AND TS.[Year] = [YearWeek].[Year] " & _
                                                "INNER JOIN [CTRCode] ON TS.[CTRCode] = [CTRCode].[CTRCode] AND TS.[CostCode] = [CTRCode].[CostCode] " & _
                                                "WHERE (TS.[Submitted] = 1 OR TS.[Endorsed] = 1) AND TS.[Approved] =  0" & _
                                                "AND ISNULL(EmployeeInfo.EmployeeLogin, '') <> system_user"

            ' Note that the update command cannot be built this was as it calls either endorse or reject
            DA_ToApprove.SelectCommand = ToApproveSelectCommand
            DA_ToApprove.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                                        {New System.Data.Common.DataTableMapping("Table", "TimesheetDetail", New System.Data.Common.DataColumnMapping() { _
                                         New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                                         New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNo"), _
                                         New System.Data.Common.DataColumnMapping("Name", "EmployeeName"), _
                                         New System.Data.Common.DataColumnMapping("LocationID", "Location"), _
                                         New System.Data.Common.DataColumnMapping("Year", "Year"), _
                                         New System.Data.Common.DataColumnMapping("Week", "Week"), _
                                         New System.Data.Common.DataColumnMapping("StartWeek", "StartDate"), _
                                         New System.Data.Common.DataColumnMapping("CostCode", "CostCode"), _
                                         New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode"), _
                                         New System.Data.Common.DataColumnMapping("Description", "Description"), _
                                         New System.Data.Common.DataColumnMapping("NTSaturday", "NTSaturday"), _
                                         New System.Data.Common.DataColumnMapping("NTSunday", "NTSunday"), _
                                         New System.Data.Common.DataColumnMapping("NTMonday", "NTMonday"), _
                                         New System.Data.Common.DataColumnMapping("NTTuesday", "NTTuesday"), _
                                         New System.Data.Common.DataColumnMapping("NTWednesday", "NTWednesday"), _
                                         New System.Data.Common.DataColumnMapping("NTThursday", "NTThursday"), _
                                         New System.Data.Common.DataColumnMapping("NTFriday", "NTFriday"), _
                                         New System.Data.Common.DataColumnMapping("TotalNormalTime", "NTTotal"), _
                                         New System.Data.Common.DataColumnMapping("OTSaturday", "OTSaturday"), _
                                         New System.Data.Common.DataColumnMapping("OTSunday", "OTSunday"), _
                                         New System.Data.Common.DataColumnMapping("OTMonday", "OTMonday"), _
                                         New System.Data.Common.DataColumnMapping("OTTuesday", "OTTuesday"), _
                                         New System.Data.Common.DataColumnMapping("OTWednesday", "OTWednesday"), _
                                         New System.Data.Common.DataColumnMapping("OTThursday", "OTThursday"), _
                                         New System.Data.Common.DataColumnMapping("OTFriday", "OTFriday"), _
                                         New System.Data.Common.DataColumnMapping("TotalOverTime", "OTTotal"), _
                                         New System.Data.Common.DataColumnMapping("SubmittedDate", "SubmittedDate")})})

            DT_ToApprove = New ApplicationDataSet.TimesheetDetailDataTable
            Me.ToApproveDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ToApproveDataGrid.DataSource = DT_ToApprove

            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.LocationColumn.ColumnName, "Location", 50))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.EmployeeNoColumn.ColumnName, "Employee No.", 60))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.EmployeeNameColumn.ColumnName, "Name", 100))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.CostCodeColumn.ColumnName, "Cost Code", 40))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.CTRCodeColumn.ColumnName, "CTRCode", 65))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.DescriptionColumn.ColumnName, "Description", 120))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.YearColumn.ColumnName, "Year", 40))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.WeekColumn.ColumnName, "Wk", 25))
            Me.ToApproveDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.StartDateColumn.ColumnName, "StartWeek", 70))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTTotalColumn.ColumnName, "NTTotal", 50))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTTotalColumn.ColumnName, "OTTotal", 50))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTSaturdayColumn.ColumnName, "NTSa", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTSundayColumn.ColumnName, "NTSu", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTMondayColumn.ColumnName, "NTMo", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTTuesdayColumn.ColumnName, "NTTu", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTWednesdayColumn.ColumnName, "NTWe", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTThursdayColumn.ColumnName, "NTTh", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.NTFridayColumn.ColumnName, "NTFr", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTSaturdayColumn.ColumnName, "OTSa", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTSundayColumn.ColumnName, "OTSu", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTMondayColumn.ColumnName, "OTMo", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTTuesdayColumn.ColumnName, "OTTu", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTWednesdayColumn.ColumnName, "OTWe", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTThursdayColumn.ColumnName, "OTTh", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewHourColumn(DT_ToApprove.OTFridayColumn.ColumnName, "OTFr", HoursColumnWidth))
            Me.ToApproveDataGrid.Columns.Add(NewDateColumn(DT_ToApprove.SubmittedDateColumn.ColumnName, "Submitted", 70))
            Me.ToApproveDataGrid.ScrollBars = ScrollBars.Horizontal

            ToApproveRightDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ToApproveRightDataGrid.DataSource = DT_ToApprove
            ToApproveRightDataGrid.RowHeadersVisible = False
            ToApproveRightDataGrid.AllowUserToAddRows = False
            ToApproveRightDataGrid.AllowUserToDeleteRows = False
            ToApproveRightDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            ToApproveRightDataGrid.ScrollBars = ScrollBars.Vertical

            Me.ToApproveRightDataGrid.Columns.Add(NewCheckboxColumn(DT_ToApprove.ApprovedColumn.ColumnName, "Approve", 50))
            Me.ToApproveRightDataGrid.Columns.Add(NewCheckboxColumn(DT_ToApprove.RejectedColumn.ColumnName, "Reject", 50))
            Me.ToApproveRightDataGrid.Columns.Add(NewTextColumn(DT_ToApprove.RejectReasonColumn.ColumnName, "Reason", 120, False))

            ' -- man hours zero
            DA_ManHours = New SqlClient.SqlDataAdapter()
            ManHoursSelectCommand = New SqlClient.SqlCommand
            ManHoursSelectCommand.Connection = DBConnection
            ManHoursSelectCommand.CommandType = CommandType.Text
            ManHoursSelectCommand.CommandText = "SELECT	[TimesheetHeader].[Year], [TimesheetHeader].[Week], " & _
                                                "       Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As StartDate, " & _
                                                "		[EmployeeInfo].[EmployeeNumber]," & _
                                                "		[EmployeeInfo].[EmployeeID]," & _
                                                "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                                                "		[EmployeeInfo].[LocationID]," & _
                                                "		[Discipline], [DisciplineDescription]," & _
                                                "       Replace(convert(varchar(11),[TimesheetHeader].[LastUpdate],13),' ','-') As LastUpdate " & _
                                                "FROM [EmployeeInfo] " & _
                                                "INNER JOIN [TimesheetHeader] ON [TimesheetHeader].[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                                "INNER JOIN [YearWeek] ON [TimesheetHeader].[Week] = [YearWeek].[Week] AND [TimesheetHeader].[Year] = [YearWeek].[Year] " & _
                                                "WHERE [TimesheetHeader].[ZeroHours] = 1 ORDER BY [TimesheetHeader].[LastUpdate] desc"

            ' Note that the update command cannot be built this was as it calls either endorse or reject
            DA_ManHours.SelectCommand = ManHoursSelectCommand
            DA_ManHours.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                                        {New System.Data.Common.DataTableMapping("Table", "TimesheetHeader", New System.Data.Common.DataColumnMapping() { _
                                         New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                                         New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNo"), _
                                         New System.Data.Common.DataColumnMapping("Name", "EmployeeName"), _
                                         New System.Data.Common.DataColumnMapping("Discipline", "DisciplineNo"), _
                                         New System.Data.Common.DataColumnMapping("DisciplineDescription", "DisciplineDescription"), _
                                         New System.Data.Common.DataColumnMapping("Year", "Year"), _
                                         New System.Data.Common.DataColumnMapping("Week", "Week"), _
                                         New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), _
                                         New System.Data.Common.DataColumnMapping("LastUpdate", "LastUpdate")})})
            DT_ManHours = New ApplicationDataSet.TimesheetHeaderDataTable
            Me.ManHoursDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ManHoursDataGrid.DataSource = DT_ManHours

            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.EmployeeNoColumn.ColumnName, "Employee No.", 60))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.EmployeeNameColumn.ColumnName, "Name", 100))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.DisciplineNoColumn.ColumnName, "Discipline", 70))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.DisciplineDescriptionColumn.ColumnName, "Description", 160))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.YearColumn.ColumnName, "Year", 40))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ManHours.WeekColumn.ColumnName, "Wk", 25))
            Me.ManHoursDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.StartDateColumn.ColumnName, "StartWeek", 76))
            Me.ManHoursDataGrid.Columns.Add(NewDateColumn(DT_ManHours.LastUpdateColumn.ColumnName, "LastUpdate", 76))
            Me.ManHoursDataGrid.ScrollBars = ScrollBars.Horizontal

            ManHoursRightDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ManHoursRightDataGrid.DataSource = DT_ManHours
            ManHoursRightDataGrid.RowHeadersVisible = False
            ManHoursRightDataGrid.AllowUserToAddRows = False
            ManHoursRightDataGrid.AllowUserToDeleteRows = False
            ManHoursRightDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            ManHoursRightDataGrid.ScrollBars = ScrollBars.Vertical

            Me.ManHoursRightDataGrid.Columns.Add(NewCheckboxColumn(DT_ManHours.RejectedColumn.ColumnName, "Reject", 50))
            Me.ManHoursRightDataGrid.Columns.Add(NewTextColumn(DT_ManHours.RejectReasonColumn.ColumnName, "Reason", 120, False))

            ' -- rejected
            DA_Rejected = New SqlClient.SqlDataAdapter()
            RejectedSelectCommand = New SqlClient.SqlCommand
            RejectedSelectCommand.Connection = DBConnection
            RejectedSelectCommand.CommandType = CommandType.Text
            RejectedSelectCommand.CommandText = "SELECT	TS.[Year], TS.[Week], " & _
                                        "		[EmployeeInfo].[EmployeeNumber]," & _
                                        "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                                        "		[EmployeeInfo].[LocationID]," & _
                                        "		[Discipline], [DisciplineDescription]," & _
                                        "		TS.[CostCode]," & _
                                        "		TS.[CTRCode]," & _
                                        "		[CTRCode].[Description]," & _
                                        "		TS.[NTSaturday], TS.[NTSunday], TS.[NTMonday], TS.[NTTuesday], TS.[NTWednesday], TS.[NTThursday], TS.[NTFriday],TS.[TotalNormalTime], " & _
                                        "		TS.[OTSaturday], TS.[OTSunday], TS.[OTMonday], TS.[OTTuesday], TS.[OTWednesday], TS.[OTThursday], TS.[OTFriday], TS.[TotalOverTime], " & _
                                        "       Replace(convert(varchar(11),TS.[SubmittedDate],13),' ','-') As SubmittedDate, Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As StartDate, TS.[RejectedBy], Replace(convert(varchar(11),TS.[RejectedDate],13),' ','-') As RejectedDate, TS.[RejectReason] " & _
                                        "FROM [EmployeeInfo] " & _
                                        "INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                        "INNER JOIN [YearWeek] ON TS.[Week] = [YearWeek].[Week] AND TS.[Year] = [YearWeek].[Year] " & _
                                        "INNER JOIN [CTRCode] ON TS.[CTRCode] = [CTRCode].[CTRCode] AND TS.[CostCode] = [CTRCode].[CostCode] " & _
                                        "WHERE TS.[Rejected] = 1 "


            ' Note that the update command cannot be built this was as it calls either endorse or reject
            DA_Rejected.SelectCommand = RejectedSelectCommand
            DA_Rejected.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                                                {New System.Data.Common.DataTableMapping("Table", "TimesheetDetail", New System.Data.Common.DataColumnMapping() { _
                                                 New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                                                 New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNo"), _
                                                 New System.Data.Common.DataColumnMapping("Name", "EmployeeName"), _
                                                 New System.Data.Common.DataColumnMapping("Discipline", "DisciplineNo"), _
                                                 New System.Data.Common.DataColumnMapping("DisciplineDescription", "DisciplineDescription"), _
                                                 New System.Data.Common.DataColumnMapping("LocationID", "Location"), _
                                                 New System.Data.Common.DataColumnMapping("Year", "Year"), _
                                                 New System.Data.Common.DataColumnMapping("Week", "Week"), _
                                                 New System.Data.Common.DataColumnMapping("StartWeek", "StartDate"), _
                                                 New System.Data.Common.DataColumnMapping("CostCode", "CostCode"), _
                                                 New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode"), _
                                                 New System.Data.Common.DataColumnMapping("Description", "Description"), _
                                                 New System.Data.Common.DataColumnMapping("NTSaturday", "NTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("NTSunday", "NTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("NTMonday", "NTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("NTTuesday", "NTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTWednesday", "NTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTThursday", "NTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("NTFriday", "NTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalNormalTime", "NTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("OTSaturday", "OTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("OTSunday", "OTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("OTMonday", "OTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("OTTuesday", "OTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTWednesday", "OTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTThursday", "OTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("OTFriday", "OTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalOverTime", "OTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("SubmittedDate", "SubmittedDate"), _
                                                 New System.Data.Common.DataColumnMapping("RejectedDate", "RejectedDate"), _
                                                 New System.Data.Common.DataColumnMapping("RejectReason", "RejectReason"), _
                                                 New System.Data.Common.DataColumnMapping("RejectedBy", "RejectedBy")})})

            DT_Rejected = New ApplicationDataSet.TimesheetDetailDataTable
            RejectedDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            RejectedDataGrid.DataSource = DT_Rejected

            ' ---
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.LocationColumn.ColumnName, "Location", 50))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DisciplineNoColumn.ColumnName, "Discipline", 60))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DisciplineDescriptionColumn.ColumnName, "Description", 120))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.EmployeeNoColumn.ColumnName, "Employee No.", 60))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.EmployeeNameColumn.ColumnName, "Name", 100))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.CostCodeColumn.ColumnName, "Cost Code", 40))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.CTRCodeColumn.ColumnName, "CTRCode", 65))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DescriptionColumn.ColumnName, "Description", 120))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.YearColumn.ColumnName, "Year", 40))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.WeekColumn.ColumnName, "Wk", 25))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.StartDateColumn.ColumnName, "StartWeek", 70))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTTotalColumn.ColumnName, "NTTotal", 50))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTTotalColumn.ColumnName, "OTTotal", 50))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTSaturdayColumn.ColumnName, "NTSa", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTSundayColumn.ColumnName, "NTSu", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTMondayColumn.ColumnName, "NTMo", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTTuesdayColumn.ColumnName, "NTTu", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTWednesdayColumn.ColumnName, "NTWe", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTThursdayColumn.ColumnName, "NTTh", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTFridayColumn.ColumnName, "NTFr", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTSaturdayColumn.ColumnName, "OTSa", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTSundayColumn.ColumnName, "OTSu", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTMondayColumn.ColumnName, "OTMo", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTTuesdayColumn.ColumnName, "OTTu", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTWednesdayColumn.ColumnName, "OTWe", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTThursdayColumn.ColumnName, "OTTh", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTFridayColumn.ColumnName, "OTFr", HoursColumnWidth))
            Me.RejectedDataGrid.Columns.Add(NewDateColumn(DT_Rejected.SubmittedDateColumn.ColumnName, "Submitted", 70))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.RejectedByColumn.ColumnName, "Rejected By", 70))
            Me.RejectedDataGrid.Columns.Add(NewDateColumn(DT_Rejected.RejectedDateColumn.ColumnName, "Rejected Date", 70))
            Me.RejectedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.RejectReasonColumn.ColumnName, "Reason", 150))

            ' Approved

            DA_Approved = New SqlClient.SqlDataAdapter()
            ApprovedSelectCommand = New SqlClient.SqlCommand
            ApprovedSelectCommand.Connection = DBConnection
            ApprovedSelectCommand.CommandType = CommandType.Text
            ApprovedSelectCommand.CommandText = "SELECT	TS.[Year], TS.[Week], " & _
                                        "		[EmployeeInfo].[EmployeeNumber]," & _
                                        "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                                        "		[EmployeeInfo].[LocationID]," & _
                                        "		[Discipline], [DisciplineDescription]," & _
                                        "		TS.[CostCode]," & _
                                        "		TS.[CTRCode]," & _
                                        "		[CTRCode].[Description]," & _
                                        "		TS.[NTSaturday], TS.[NTSunday], TS.[NTMonday], TS.[NTTuesday], TS.[NTWednesday], TS.[NTThursday], TS.[NTFriday], TS.[TotalNormalTime], " & _
                                        "		TS.[OTSaturday], TS.[OTSunday], TS.[OTMonday], TS.[OTTuesday], TS.[OTWednesday], TS.[OTThursday], TS.[OTFriday], TS.[TotalOverTime], " & _
                                        "       Replace(convert(varchar(11),TS.[SubmittedDate],13),' ','-') As SubmittedDate, Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As StartDate, TS.[ApprovedBy], Replace(convert(varchar(11),TS.[ApprovedDate],13),' ','-') As ApprovedDate " & _
                                        "FROM [EmployeeInfo] " & _
                                        "INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                        "INNER JOIN [YearWeek] ON TS.[Week] = [YearWeek].[Week] AND TS.[Year] = [YearWeek].[Year] " & _
                                        "INNER JOIN [CTRCode] ON TS.[CTRCode] = [CTRCode].[CTRCode] AND TS.[CostCode] = [CTRCode].[CostCode] " & _
                                        "WHERE TS.[Approved] = 1 " & _
                                        "AND [ApprovedDate] > GetDate() - 7 "


            ' Note that the update command cannot be built this was as it calls either endorse or reject
            DA_Approved.SelectCommand = ApprovedSelectCommand
            DA_Approved.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                                                {New System.Data.Common.DataTableMapping("Table", "TimesheetDetail", New System.Data.Common.DataColumnMapping() { _
                                                 New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                                                 New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNo"), _
                                                 New System.Data.Common.DataColumnMapping("Name", "EmployeeName"), _
                                                 New System.Data.Common.DataColumnMapping("Discipline", "DisciplineNo"), _
                                                 New System.Data.Common.DataColumnMapping("DisciplineDescription", "DisciplineDescription"), _
                                                 New System.Data.Common.DataColumnMapping("LocationID", "Location"), _
                                                 New System.Data.Common.DataColumnMapping("Year", "Year"), _
                                                 New System.Data.Common.DataColumnMapping("Week", "Week"), _
                                                 New System.Data.Common.DataColumnMapping("StartWeek", "StartDate"), _
                                                 New System.Data.Common.DataColumnMapping("CostCode", "CostCode"), _
                                                 New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode"), _
                                                 New System.Data.Common.DataColumnMapping("Description", "Description"), _
                                                 New System.Data.Common.DataColumnMapping("NTSaturday", "NTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("NTSunday", "NTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("NTMonday", "NTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("NTTuesday", "NTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTWednesday", "NTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("NTThursday", "NTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("NTFriday", "NTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalNormalTime", "NTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("OTSaturday", "OTSaturday"), _
                                                 New System.Data.Common.DataColumnMapping("OTSunday", "OTSunday"), _
                                                 New System.Data.Common.DataColumnMapping("OTMonday", "OTMonday"), _
                                                 New System.Data.Common.DataColumnMapping("OTTuesday", "OTTuesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTWednesday", "OTWednesday"), _
                                                 New System.Data.Common.DataColumnMapping("OTThursday", "OTThursday"), _
                                                 New System.Data.Common.DataColumnMapping("OTFriday", "OTFriday"), _
                                                 New System.Data.Common.DataColumnMapping("TotalOverTime", "OTTotal"), _
                                                 New System.Data.Common.DataColumnMapping("SubmittedDate", "SubmittedDate"), _
                                                 New System.Data.Common.DataColumnMapping("ApprovedDate", "ApprovedDate"), _
                                                 New System.Data.Common.DataColumnMapping("ApprovedBy", "ApprovedBy")})})

            DT_Approved = New ApplicationDataSet.TimesheetDetailDataTable
            ApprovedDataGrid.AutoGenerateColumns = False ' before thay are generated by the binding
            ApprovedDataGrid.DataSource = DT_Approved

            ' ---
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.LocationColumn.ColumnName, "Location", 50))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DisciplineNoColumn.ColumnName, "Discipline", 60))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DisciplineDescriptionColumn.ColumnName, "Description", 120))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.EmployeeNoColumn.ColumnName, "Employee No.", 60))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.EmployeeNameColumn.ColumnName, "Name", 100))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.CostCodeColumn.ColumnName, "Cost Code", 40))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.CTRCodeColumn.ColumnName, "CTRCode", 65))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.DescriptionColumn.ColumnName, "Description", 120))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.YearColumn.ColumnName, "Year", 40))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.WeekColumn.ColumnName, "Wk", 25))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_ToEndorse.StartDateColumn.ColumnName, "StartWeek", 70))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTTotalColumn.ColumnName, "NTTotal", 50))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTTotalColumn.ColumnName, "OTTotal", 50))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTSaturdayColumn.ColumnName, "NTSa", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTSundayColumn.ColumnName, "NTSu", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTMondayColumn.ColumnName, "NTMo", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTTuesdayColumn.ColumnName, "NTTu", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTWednesdayColumn.ColumnName, "NTWe", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTThursdayColumn.ColumnName, "NTTh", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.NTFridayColumn.ColumnName, "NTFr", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTSaturdayColumn.ColumnName, "OTSa", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTSundayColumn.ColumnName, "OTSu", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTMondayColumn.ColumnName, "OTMo", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTTuesdayColumn.ColumnName, "OTTu", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTWednesdayColumn.ColumnName, "OTWe", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTThursdayColumn.ColumnName, "OTTh", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewHourColumn(DT_Rejected.OTFridayColumn.ColumnName, "OTFr", HoursColumnWidth))
            Me.ApprovedDataGrid.Columns.Add(NewDateColumn(DT_Rejected.SubmittedDateColumn.ColumnName, "Submitted", 70))
            Me.ApprovedDataGrid.Columns.Add(NewTextColumn(DT_Rejected.ApprovedByColumn.ColumnName, "Approved By", 70))
            Me.ApprovedDataGrid.Columns.Add(NewDateColumn(DT_Rejected.ApprovedDateColumn.ColumnName, "Approved Date", 70))

            ' reporting
            Me.ReportFormatComboBox.Items.Add(TSReportDefault)
            Me.ReportFormatComboBox.Items.Add(TSReportGroupByCostCTRCode)
            Me.ReportFormatComboBox.Items.Add(TSPendingManHour)
            Me.ReportFormatComboBox.Items.Add(TSAutomation)
            Me.ReportFormatComboBox.SelectedItem = TSReportDefault
            Me.ReportViewer.Tag = TSReportDefault

            ' Log               
            pathFileLog = ".\Logs\" & Replace(Replace(CType(Now, String), ":", ".", 1), "/", "", 1) & ".log"
            w = File.AppendText(pathFileLog)

            ' Initialisation of Mobilisation dates
            InitMobisationDate()

        Catch ex As EvaluateException
            Log("ERROR InitialiseComponents: " + ex.Message(), w)
        End Try

    End Sub

    Private Sub InitMobisationDate()
        Try
            Dim sqlcmd As SqlClient.SqlCommand
            sqlcmd = New SqlClient.SqlCommand("SELECT TOP 1 StartDate FROM EmployeeInfo WHERE StartDate is not null ORDER BY StartDate asc", DBConnection)
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            Me.StartDateTimePicker.MinDate = CType(sqlcmd.ExecuteScalar(), DateTime)
            Me.StartDateTimePicker.Value = Me.StartDateTimePicker.MinDate
            sqlcmd.Dispose()
            sqlcmd = New SqlClient.SqlCommand("SELECT TOP 1 StartDate FROM EmployeeInfo WHERE StartDate is not null ORDER BY StartDate desc", DBConnection)
            Me.StartDateTimePicker.MaxDate = CType(sqlcmd.ExecuteScalar(), DateTime)
            sqlcmd.Dispose()
            sqlcmd = New SqlClient.SqlCommand("SELECT TOP 1 ActualDemobilisationDate FROM EmployeeInfo WHERE ActualDemobilisationDate is not null ORDER BY ActualDemobilisationDate asc", DBConnection)
            Me.DemobilisationDateTimePicker.MinDate = CType(sqlcmd.ExecuteScalar(), DateTime)
            sqlcmd.Dispose()
            sqlcmd = New SqlClient.SqlCommand("SELECT TOP 1 ActualDemobilisationDate FROM EmployeeInfo WHERE ActualDemobilisationDate is not null ORDER BY ActualDemobilisationDate desc", DBConnection)
            Me.DemobilisationDateTimePicker.MaxDate = CType(sqlcmd.ExecuteScalar(), DateTime)
            Me.DemobilisationDateTimePicker.Value = Me.DemobilisationDateTimePicker.MaxDate
            sqlcmd.Dispose()
        Catch ex As Exception
            Log("ERROR InitMobisationDate: " + ex.Message, w)
            ' First openning connexion 
            MsgBox("ERROR rights in Database", MsgBoxStyle.Critical)
            Me.Close()
        End Try
    End Sub

    Private Sub InitializeTabAccessRights()
        Try
            If Me.ChoiceUserComboBox.SelectedIndex = -1 Then
                Me.RefreshUserList()
            End If

        Catch ex As Exception
            Log("ERROR InitializeTabAccessRights: " + ex.Message, w)
        End Try
    End Sub ' InitializeTabAccessRights

#End Region

#Region "Reports"

    Public Sub ShowOrHideFilterForReport(ByVal ReportName As String)
        Try
            If ReportName = TSAutomation Then
                Me.CompanyComboBox.Show()
                Me.DepartmentComboBox.Show()
                Me.AssignedLocationComboBox.Show()
                Me.PAFNoComboBox.Show()
                Me.RatePayCurrencyIDComboBox.Show()
                Me.StartDateTimePicker.Show()
                Me.DemobilisationDateTimePicker.Show()
                Me.CostCodeComboBox.Hide()
                Me.DisciplineComboBox.Hide()
                Me.Label10.Show()
                Me.Label12.Show()
                Me.Label11.Show()
                Me.Label13.Show()
                Me.Label14.Show()
                Me.Label8.Show()
                Me.Label9.Show()
                Me.Label3.Hide()
                Me.Label2.Hide()
            Else
                Me.CTRCodeComboBox.Show()
                Me.CostCodeComboBox.Show()
                Me.DisciplineComboBox.Show()
                Me.FromYearWeekComboBox.Show()
                Me.ToYearWeekComboBox.Show()
                Me.Label4.Show()
                Me.Label3.Show()
                Me.Label2.Show()
                Me.Label6.Show()
                Me.Label5.Show()
                Me.CompanyComboBox.Hide()
                Me.AssignedLocationComboBox.Hide()
                Me.PAFNoComboBox.Hide()
                Me.RatePayCurrencyIDComboBox.Hide()
                Me.StartDateTimePicker.Hide()
                Me.DemobilisationDateTimePicker.Hide()
                Me.DepartmentComboBox.Hide()
                Me.Label9.Hide()
                Me.Label10.Hide()
                Me.Label12.Hide()
                Me.Label11.Hide()
                Me.Label13.Hide()
                Me.Label14.Hide()
                Me.Label8.Hide()
            End If
        Catch ex As Exception
            Log("ERROR ShowOrHideFilterForReport: " + ex.Message, w)
        End Try
    End Sub

    Private Function GetReportingDT(ByVal DisciplineFilter As String, ByVal CostCodeFilter As String, ByVal CTRCodeFilter As String, _
                                    ByVal FromYearWeekFilter As String, ByVal ToYearWeekFilter As String, ByVal QueryString As String) As ApplicationDataSet.TimesheetDetailDataTable
        GetReportingDT = New ApplicationDataSet.TimesheetDetailDataTable

        Dim StartDate As String
        Dim EndDate As String
        If FromYearWeekFilter = "%" Then
            StartDate = CStr(Format(New Date(2007, 10, 1), "yyyy-MM-dd"))
        Else
            StartDate = GetStartDate(FromYearWeekFilter)
        End If
        If ToYearWeekFilter = "%" Then
            EndDate = CStr(Format(Date.Today, "yyyy-MM-dd"))
        Else
            EndDate = GetStartDate(ToYearWeekFilter)
        End If

        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.TimesheetDetailRow
        Dim sqlcmdtext As String

        sqlcmdtext = "SELECT	TS.[Year], TS.[Week], " & _
                        "		[EmployeeInfo].[EmployeeNumber]," & _
                        "		[EmployeeInfo].[EmployeeID]," & _
                        "		RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name, " & _
                        "		[EmployeeInfo].[LocationID]," & _
                        "		[DisciplineNo], [DisciplineDescription]," & _
                        "		TS.[CostCode]," & _
                        "		TS.[CTRCode]," & _
                        "		[CTRCode].[Description]," & _
                        "		TS.[NTSaturday], TS.[NTSunday], TS.[NTMonday], TS.[NTTuesday], TS.[NTWednesday], TS.[NTThursday], TS.[NTFriday], TS.[TotalNormalTime], " & _
                        "		TS.[OTSaturday], TS.[OTSunday], TS.[OTMonday], TS.[OTTuesday], TS.[OTWednesday], TS.[OTThursday], TS.[OTFriday], TS.[TotalOverTime], " & _
                        "       TS.[SubmittedDate], Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-'),TS.[Approved] " & _
                        "FROM [EmployeeInfo] " & _
                        "INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                        "INNER JOIN [CTRCode] ON TS.[CTRCode] = [CTRCode].[CTRCode] AND TS.[CostCode] = [CTRCode].[CostCode] " & _
                        "INNER JOIN YearWeek ON TS.[Year] = [YearWeek].[Year] AND TS.[Week] = [YearWeek].[Week] " & _
                        "WHERE (TS.[Submitted] = 1 or TS.[Endorsed] = 1 or TS.[Approved] = 1)" &
                        "AND [EmployeeInfo].[DisciplineNo] LIKE '" + DisciplineFilter + "' " & _
                        "AND TS.[CostCode] IN (" + CostCodeFilter + ") " & _
                        "AND TS.[CTRCode] IN (" + CTRCodeFilter + ") " & _
                        "AND [YearWeek].[StartDate] BETWEEN '" & StartDate & "'  AND '" & EndDate & "' " & _
                        "AND " + QueryString + ""

        sqlcmd = New SqlClient.SqlCommand(sqlcmdtext, DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetReportingDT.NewTimesheetDetailRow
                row.Year = reader.GetInt32(0)
                row.Week = reader.GetInt32(1)
                row.StartDate = reader.GetString(28)
                row.EmployeeNo = reader.GetString(2)
                row.EmployeeID = reader.GetInt32(3)
                row.EmployeeName = reader.GetString(4)
                row.Location = reader.GetString(5)
                row.DisciplineNo = reader.GetString(6)
                row.DisciplineDescription = reader.GetString(7)
                row.CostCode = reader.GetString(8)
                row.CTRCode = reader.GetString(9)
                row.Description = reader.GetString(10)
                row.NTTotal = CType(reader.GetSqlMoney(18), Single)
                row.OTTotal = CType(reader.GetSqlMoney(26), Single)
                row.Approved = reader.GetBoolean(29)
                GetReportingDT.AddTimesheetDetailRow(row)
            End While
        Catch ex As Exception
            Log("ERROR Report Default Timesheet: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
            Log("INFO Report Default Timesheet: " + sqlcmd.CommandText, w)
        End Try
    End Function

    Private Function GetReportingPending(ByVal DisciplineFilter As String, ByVal CostCodeFilter As String, ByVal CTRCodeFilter As String, _
                                   ByVal FromYearWeekFilter As String, ByVal ToYearWeekFilter As String, ByVal QueryString As String) As ApplicationDataSet.PendingManHourDataTable

        GetReportingPending = New ApplicationDataSet.PendingManHourDataTable

        Dim StartDate As String
        Dim EndDate As String
        If FromYearWeekFilter = "%" Then
            StartDate = CStr(Format(New Date(2007, 10, 1), "yyyy-MM-dd"))
        Else
            StartDate = GetStartDate(FromYearWeekFilter)
        End If
        If ToYearWeekFilter = "%" Then
            EndDate = CStr(Format(Date.Today, "yyyy-MM-dd"))
        Else
            EndDate = GetStartDate(ToYearWeekFilter)
        End If

        Dim sqlcmd1 As SqlClient.SqlCommand
        Dim SQLCmd As SqlClient.SqlCommand
        Dim sqlcmd2 As SqlClient.SqlCommand
        Dim reader1 As SqlClient.SqlDataReader
        Dim reader2 As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.PendingManHourRow
        Dim sqlcmdtext1 As String
        Dim Approver As String
        Dim Login As String
        Dim Filter As String
        Dim sqlcmdtext2 As String
        Dim sqlcmdtemp As String

        sqlcmdtext1 = "SELECT RTRIM([EmployeeInfo].[LastName]) As Approver, " & _
                      "[SpecialRights].userlogin " & _
                      "FROM [EmployeeInfo], [SpecialRights] " & _
                      "WHERE  [SpecialRights].userlogin = [EmployeeInfo].Employeelogin " & _
                      "AND ([SpecialRights].ApproveTimeSheets = 1 " & _
                      "OR   [SpecialRights].EndorseTimeSheets = 1) "


        If QueryString <> "1=1" Then
            sqlcmdtext1 = sqlcmdtext1 & "AND [SpecialRights].userlogin = '" & UserConnected & "'"
        Else
            sqlcmdtext1 = sqlcmdtext1 & "AND  [SpecialRights].userlogin <> 'KONINET\approvaladmin'"
        End If

        sqlcmdtext2 = ""
        Try
            sqlcmd1 = New SqlClient.SqlCommand(sqlcmdtext1, DBConnection)
            If sqlcmd1.Connection.State <> ConnectionState.Open Then
                sqlcmd1.Connection.Open()
            End If
            reader1 = sqlcmd1.ExecuteReader
            While reader1.Read
                Approver = reader1.GetString(0)
                Login = reader1.GetString(1)

                SQLCmd = New SqlClient.SqlCommand("SELECT [Timesheet].[dbo].[GetToApproveFilter] ('" & Login & "')", DBConnection)
                If DBConnection.State <> ConnectionState.Open Then
                    DBConnection.Open()
                End If
                Filter = CStr(SQLCmd.ExecuteScalar())

                sqlcmdtemp = "SELECT	'" + Approver + "' As Approver," & _
                        "       convert(varchar(4),TsApproved.[Year]) + convert(varchar(2),TsApproved.[Week]) As TSYrWk, " & _
                        "		Approved.[EmployeeNumber] As Emp_NO," & _
                        "		RTRIM(Approved.[LastName]) + ', ' + RTRIM(Approved.[FirstName]) As Emp_Name, " & _
                        "		TsApproved.[TotalNormalTime] As Tot_NT, " & _
                        "		TsApproved.[TotalOverTime] As Tot_OT, " & _
                        "       TsApproved.[TotalNormalTime] + TsApproved.[TotalOverTime] As Tot_Hour " & _
                        "FROM [EmployeeInfo] Approved " & _
                        "INNER JOIN [TimesheetDetail] TsApproved ON TsApproved.EmployeeID = Approved.EmployeeID " & _
                        "INNER JOIN [YearWeek] ON TsApproved.[Year] = [YearWeek].[Year] AND TsApproved.[Week] = [YearWeek].[Week] " & _
                        "WHERE (TsApproved.Submitted = 1 " & _
                        "OR TsApproved.Endorsed = 1) " & _
                        "AND Approved.[DisciplineNo] LIKE '" + DisciplineFilter + "' " & _
                        "AND TsApproved.[CostCode] IN (" + CostCodeFilter + ") " & _
                        "AND TsApproved.[CTRCode] IN (" + CTRCodeFilter + ") " & _
                        "AND [YearWeek].[StartDate] BETWEEN '" & StartDate & "' AND '" & EndDate & "'" & _
                        "AND " + Replace(Filter, "TS", "TsApproved") + ""


                If sqlcmdtext2 = "" Then
                    sqlcmdtext2 = sqlcmdtemp
                Else
                    sqlcmdtext2 = sqlcmdtext2 & " UNION " & sqlcmdtemp
                End If
            End While
            sqlcmdtext2 = sqlcmdtext2 + " ORDER BY Approver "
        Catch ex As Exception
            Log("ERROR Report Pending first request: " + ex.Message, w)
        Finally
            If Not IsNothing(reader1) Then
                reader1.Close()
            End If
            Log("INFO Report Pending first request: " + sqlcmd1.CommandText, w)
            Log("INFO Report Pending second request: " + sqlcmdtext2, w)
        End Try

        sqlcmd2 = New SqlClient.SqlCommand(sqlcmdtext2, DBConnection)
        If sqlcmd2.Connection.State <> ConnectionState.Open Then
            sqlcmd2.Connection.Open()
        End If
        Try
            reader2 = sqlcmd2.ExecuteReader
            While reader2.Read
                row = GetReportingPending.NewPendingManHourRow
                row.Approver = reader2.GetString(0)
                row.TSYrWk = reader2.GetString(1)
                row.Emp_NO = reader2.GetString(2)
                row.Emp_Name = reader2.GetString(3)
                row.Tot_NT = CType(reader2.GetSqlMoney(4), Single)
                row.Tot_OT = CType(reader2.GetSqlMoney(5), Single)
                row.Tot_Hour = CType(reader2.GetSqlMoney(6), Single)
                GetReportingPending.AddPendingManHourRow(row)
            End While
        Catch ex As Exception
            Log("ERROR Report Pending second request: " + ex.Message, w)
        Finally
            If Not IsNothing(reader2) Then
                reader2.Close()
            End If
            Log("INFO Report Pending second request: " + sqlcmd2.CommandText, w)
        End Try
    End Function

    Private Function GetReportingAutomation(ByVal DepartmentNo As String, ByVal CTRCodeFilter As String, _
                                   ByVal FromYearWeekFilter As String, ByVal ToYearWeekFilter As String, ByVal Company As String, ByVal AssignedLocation As String, ByVal PAFNo As String, ByVal RatePayCurrencyID As String, ByVal StartDatePAF As DateTimePicker, ByVal DemobilisationDate As DateTimePicker, ByVal QueryString As String) As ApplicationDataSet.AutomationDataTable

        GetReportingAutomation = New ApplicationDataSet.AutomationDataTable

        Dim StartDate As String
        Dim EndDate As String
        If FromYearWeekFilter = "%" Then
            StartDate = CStr(Format(New Date(2007, 10, 1), "yyyy-MM-dd"))
        Else
            StartDate = GetStartDate(FromYearWeekFilter)
        End If
        If ToYearWeekFilter = "%" Then
            EndDate = CStr(Format(Date.Today, "yyyy-MM-dd"))
        Else
            EndDate = GetStartDate(ToYearWeekFilter)
        End If


        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.AutomationRow
        Dim sqlcmdtext As String
        sqlcmdtext = "SELECT	 SUBSTRING(TS.[CTRCode],5,3) As Dept_code, " & _
                     "           TS.[CTRCode], " & _
                     "           RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]) As Name," & _
                     "           (select convert(varchar(2),Month(EndDate)) from YearWeek where Year = TS.[Year] and Week = TS.[Week]) + '\' + Convert(varchar(4),TS.[Year])  as Month," & _
                     "               'Week n°' + convert(varchar(10),TS.Week) " & _
                     "		     as Week, " & _
                     "           TS.[TotalNormalTime] + TS.[TotalOverTime] as TotHour,  " & _
                     "           [EmployeeInfo].[CompanyName] ," & _
                     "           [EmployeeInfo].[RateAssignLocation] ," & _
                     "           [EmployeeInfo].[PAFNo] ," & _
                     "           [EmployeeInfo].[RateAssignCurrencyID] ," & _
                     "           [EmployeeInfo].[StartDate] ," & _
                     "           [EmployeeInfo].[DemobilisationDate], " & _
                     "           Replace(convert(varchar(11),[YearWeek].[EndDate],13),' ','-') " & _
                     "           FROM [EmployeeInfo]" & _
                     "           INNER JOIN [TimesheetDetail] TS ON TS.[EmployeeID] = [EmployeeInfo].[EmployeeID]  " & _
                     "           INNER JOIN [YearWeek]        ON TS.[Year] = [YearWeek].[Year] AND TS.[Week] = [YearWeek].[Week] " & _
                     "           WHERE  TS.[CTRCode] IN (" + CTRCodeFilter + ") " & _
                     "              AND [YearWeek].[StartDate] BETWEEN '" & StartDate & "'  AND '" & EndDate & "'" & _
                     "              AND SUBSTRING(TS.[CTRCode],5,3) IN (" + DepartmentNo + ") " & _
                     "              AND [EmployeeInfo].[CompanyName] LIKE '" + Company + "' " & _
                     "              AND [EmployeeInfo].[RateAssignLocation] LIKE '" + AssignedLocation + "' " & _
                     "              AND [EmployeeInfo].[PAFNo] LIKE '" + PAFNo + "' " & _
                     "              AND [EmployeeInfo].[RateAssignCurrencyID] LIKE '" + RatePayCurrencyID + "' " & _
                     "              AND cast([EmployeeInfo].[StartDate] as Date) > '" + CStr(Format(StartDatePAF.Value, "yyyy-MM-dd")) + "' " & _
                     "              AND cast([EmployeeInfo].[DemobilisationDate] as Date) < '" + CStr(Format(DemobilisationDate.Value, "yyyy-MM-dd")) + "' " & _
                     "              AND " + QueryString + " " & _
                     "           ORDER BY  TS.Year ASC, TS.Week ASC, TS.[CTRCode] ASC"


        sqlcmd = New SqlClient.SqlCommand(sqlcmdtext, DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetReportingAutomation.NewAutomationRow
                row.Department = reader.GetString(0)
                row.CTRCode = reader.GetString(1)
                row.Name = reader.GetString(2)
                row.Month = reader.GetString(3)
                row.Week = reader.GetString(4)
                row.TotHour = CType(reader.GetSqlMoney(5), Single)
                row.EndDate = reader.GetString(12)
                GetReportingAutomation.AddAutomationRow(row)
            End While
        Catch ex As Exception
            Log("ERROR Report Automation: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
            Log("INFO Report Automation: " + sqlcmd.CommandText, w)
        End Try
    End Function

#End Region

#Region "Others"


    Private Function NewTextColumn(ByRef BindingName As String, ByVal HeaderName As String, ByVal Width As Integer, Optional ByRef IsReadOnly As Boolean = True) As DataGridViewColumn
        NewTextColumn = New DataGridViewColumn
        NewTextColumn.DataPropertyName = BindingName
        NewTextColumn.Name = BindingName
        NewTextColumn.CellTemplate = New DataGridViewTextBoxCell
        NewTextColumn.DefaultCellStyle = New DataGridViewCellStyle
        NewTextColumn.Width = Width
        NewTextColumn.ReadOnly = IsReadOnly
        NewTextColumn.HeaderText = HeaderName
        NewTextColumn.HeaderCell.Style.Font = DefaultFont
        NewTextColumn.DefaultCellStyle.Font = DefaultFont
    End Function

    Private Function NewHourColumn(ByRef BindingName As String, ByVal HeaderName As String, ByVal Width As Integer) As DataGridViewColumn
        NewHourColumn = New DataGridViewColumn
        NewHourColumn.DataPropertyName = BindingName
        NewHourColumn.Name = BindingName
        NewHourColumn.CellTemplate = New DataGridViewTextBoxCell
        NewHourColumn.DefaultCellStyle = New DataGridViewCellStyle
        NewHourColumn.DefaultCellStyle.Format = "#0.0#"
        NewHourColumn.Width = Width
        NewHourColumn.ReadOnly = True
        NewHourColumn.HeaderText = HeaderName
        NewHourColumn.HeaderCell.Style.Font = DefaultFont
        NewHourColumn.DefaultCellStyle.Font = DefaultFont
    End Function

    Private Function NewDateColumn(ByRef BindingName As String, ByVal HeaderName As String, ByVal Width As Integer) As DataGridViewColumn
        NewDateColumn = New DataGridViewColumn
        NewDateColumn.DataPropertyName = BindingName
        NewDateColumn.Name = BindingName
        NewDateColumn.CellTemplate = New DataGridViewTextBoxCell
        NewDateColumn.DefaultCellStyle = New DataGridViewCellStyle
        NewDateColumn.DefaultCellStyle.Format = "yyyy-MM-dd"
        NewDateColumn.Width = Width
        NewDateColumn.ReadOnly = True
        NewDateColumn.HeaderText = HeaderName
        NewDateColumn.HeaderCell.Style.Font = DefaultFont
        NewDateColumn.DefaultCellStyle.Font = DefaultFont
    End Function

    Private Function NewCheckboxColumn(ByRef BindingName As String, ByVal HeaderName As String, ByVal Width As Integer) As DataGridViewCheckBoxColumn
        NewCheckboxColumn = New DataGridViewCheckBoxColumn
        NewCheckboxColumn.DataPropertyName = BindingName
        NewCheckboxColumn.Name = BindingName
        NewCheckboxColumn.CellTemplate = New DataGridViewCheckBoxCell
        NewCheckboxColumn.DefaultCellStyle = New DataGridViewCellStyle
        NewCheckboxColumn.Width = Width
        NewCheckboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        NewCheckboxColumn.ReadOnly = False
        NewCheckboxColumn.HeaderText = HeaderName
        NewCheckboxColumn.HeaderCell.Style.Font = DefaultFont
    End Function

    Private Sub GetUserRights(ByVal DBConnection As SqlClient.SqlConnection, ByRef UserRole As String, ByRef UserConnected As String, ByRef NotSubmittedFilter As String, ByRef ToApprove_ToEndorseFilter As String, ByRef Approved_RejectedFilter As String, ByRef ReportFilter As String)
        Dim SQLCmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim EndorseTimesheets As Boolean
        Dim ApproveeTimesheets As Boolean
        Dim ManHoursZero As Boolean
        Dim AccessRights As Boolean
        Dim ReportFullRights As Boolean

        ' Show or hide tab according to user rights
        Try
            UserConnected = Environment.UserDomainName & "\" & Environment.UserName
            SQLCmd = New SqlClient.SqlCommand("SELECT EndorseTimesheets, ApproveTimesheets, ManHoursZero, AccessRights, " & _
                                              "ReportFullRights,TSAdjustmentRead, TSAdjustmentWrite FROM SpecialRights " & _
                                              "WHERE UserLogin = '" & UserConnected & "'", DBConnection)
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            reader = SQLCmd.ExecuteReader
            If Not reader.Read() Then
                MsgBox("ERROR GetUserRights: You are not authorised to use this application!", MsgBoxStyle.Critical)
                Me.Close()
            End If
            EndorseTimesheets = reader.GetBoolean(0)
            ApproveeTimesheets = reader.GetBoolean(1)
            ManHoursZero = reader.GetBoolean(2)
            AccessRights = reader.GetBoolean(3)
            ReportFullRights = reader.GetBoolean(4)
            TSAdjustmentRead = reader.GetBoolean(5)
            TSAdjustmentWrite = reader.GetBoolean(6)

            If ApproveeTimesheets Then
                UserRole = "Approver"
                TabControl1.TabPages.Remove(ToEndorseTabPage)
            ElseIf EndorseTimesheets Then
                UserRole = "Endorser"
                TabControl1.TabPages.Remove(ToApproveTabPage)
            Else
                UserRole = "None"
                TabControl1.TabPages.Remove(ToEndorseTabPage)
                TabControl1.TabPages.Remove(ToApproveTabPage)
            End If
            If Not ManHoursZero Then
                TabControl1.TabPages.Remove(ManHoursTabPage)
            End If
            If Not AccessRights Then
                TabControl1.TabPages.Remove(AccessRightsTabPage)
            Else
                'LGX:InitializeTabAccessRights()
            End If
            If Not TSAdjustmentRead And Not TSAdjustmentWrite Then
                TabControl1.TabPages.Remove(TabPageTSAdjustment)
            End If
            reader.Close()
        Catch ex As Exception When reader.IsDBNull(2)
            Log("ERROR GetUserRights: QueryString is NULL", w)
        Catch ex As Exception
            Log("ERROR GetUserRights: " + ex.Message(), w)
        Finally
            Log("INFO GetUserRights:" + UserRole, w)
        End Try
        ' Init filters for each tab
        Try
            SQLCmd = New SqlClient.SqlCommand("SELECT [Timesheet].[dbo].[GetNotSubmittedFilter] ('" & UserConnected & "')", DBConnection)
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            NotSubmittedFilter = CStr(SQLCmd.ExecuteScalar())

            SQLCmd = New SqlClient.SqlCommand("SELECT [Timesheet].[dbo].[GetToApproveFilter] ('" & UserConnected & "')", DBConnection)
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            ToApprove_ToEndorseFilter = CStr(SQLCmd.ExecuteScalar())

            SQLCmd = New SqlClient.SqlCommand("SELECT [Timesheet].[dbo].[GetApprovedRejectedFilter] ('" & UserConnected & "')", DBConnection)
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            Approved_RejectedFilter = CStr(SQLCmd.ExecuteScalar())

            If ReportFullRights Then
                ReportFilter = "1=1"
            Else
                SQLCmd = New SqlClient.SqlCommand("SELECT [Timesheet].[dbo].[GetReportFilter] ('" & UserConnected & "',null)", DBConnection)
                If DBConnection.State <> ConnectionState.Open Then
                    DBConnection.Open()
                End If
                ReportFilter = CStr(SQLCmd.ExecuteScalar())
            End If

        Catch ex As Exception
            Log("ERROR GetUserRights (filters): " + ex.Message(), w)
        End Try
    End Sub ' GetUserRights

    Private Function GetDisciplineList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetDisciplineList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct DisciplineNo, Discipline.Description FROM Discipline", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetDisciplineList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetDisciplineList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetDisciplineList.NewGenericListRow
                row.ID = reader.GetString(0)
                row.Description = reader.GetString(1)
                GetDisciplineList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetDisciplineList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetDisciplineList

    Private Function GetYearWeekList() As ApplicationDataSet.GenericListDataTable
        GetYearWeekList = New ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow
        Dim Year As Integer
        Dim Week As Integer
        sqlcmd = New SqlClient.SqlCommand("SELECT Year, Week FROM YearWeek WHERE StartDate BETWEEN GetDate() - 200 AND GetDate() ORDER BY Year DESC , Week DESC", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If

        row = GetYearWeekList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetYearWeekList.AddGenericListRow(row)

        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetYearWeekList.NewGenericListRow
                Year = reader.GetInt32(0)
                Week = reader.GetInt32(1)

                row.ID = Year.ToString("0000") + Week.ToString("00")
                row.Description = row.ID
                GetYearWeekList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetYearWeekList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetYearWeekList

    Private Function GetCostCodeList() As ApplicationDataSet.GenericListDataTable
        GetCostCodeList = New ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct CostCode.CostCode, Description FROM CostCode INNER JOIN TimesheetDetail TS ON CostCode.CostCode = TS.CostCode WHERE Active = 1  AND " & ReportFilter & " ORDER BY CostCode", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If

        row = GetCostCodeList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetCostCodeList.AddGenericListRow(row)

        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetCostCodeList.NewGenericListRow
                row.ID = reader.GetString(0)
                row.Description = RTrim(row.ID) & " " & RTrim(reader.GetString(1))
                GetCostCodeList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetCostCodeList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetCostCodeList

    Private Function GetCTRCodeList(ByVal CostCode As String) As ApplicationDataSet.GenericListDataTable
        GetCTRCodeList = New ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow
        sqlcmd = New SqlClient.SqlCommand("SELECT DISTINCT CTRCode.CTRCode, Description FROM CTRCode, TimesheetDetail TS WHERE TS.CTRCode = CTRCode.CTRCode AND CTRCode.CostCode LIKE '" & CostCode & "' AND Active = 1 AND " & ReportFilter & " ORDER BY CTRCode", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If

        row = GetCTRCodeList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetCTRCodeList.AddGenericListRow(row)

        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetCTRCodeList.NewGenericListRow
                row.ID = reader.GetString(0)
                row.Description = RTrim(row.ID) & " " & RTrim(reader.GetString(1))
                GetCTRCodeList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetCTRCodeList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
            Log("INFO GetCTRCodeList: " + sqlcmd.CommandText, w)
        End Try
    End Function ' GetCTRCodeList

    Private Function GetDepartmentList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetDepartmentList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct(SUBSTRING(TS.CTRCode,5,3)) FROM TimesheetDetail TS  WHERE " & ReportFilter & " ORDER BY SUBSTRING(TS.CTRCode,5,3)", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetDepartmentList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetDepartmentList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetDepartmentList.NewGenericListRow
                row.ID = reader.GetString(0)
                row.Description = reader.GetString(0)
                GetDepartmentList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetDepartmentList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetDepartmentList

    Private Function GetCompanyList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetCompanyList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct(CompanyName) FROM EmployeeInfo", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetCompanyList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetCompanyList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetCompanyList.NewGenericListRow
                row.ID = reader.GetString(0)
                row.Description = reader.GetString(0)
                GetCompanyList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetCompanyList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetCompanyList

    Private Function GetUserLoginList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetUserLoginList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct RTRIM([EmployeeInfo].[LastName]) + ', ' + RTRIM([EmployeeInfo].[FirstName]), " & _
                                          "ISNULL([Filters].[Department],''), [SpecialRights].[Userlogin] , [Filters].[Department]" & _
                                          "FROM SpecialRights INNER JOIN EmployeeInfo ON [EmployeeInfo].[EmployeeLogin] = [SpecialRights].[Userlogin] left outer join Filters ON [SpecialRights].[Userlogin] = [Filters].[Userlogin] order by department", DBConnection)


        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetUserLoginList.NewGenericListRow
                row.Description = reader.GetString(0) + " " + reader.GetString(1)
                row.ID = reader.GetString(2) + "-" + reader.GetString(1)
                GetUserLoginList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetUserLoginList: " + ex.Message + " : Request : " + sqlcmd.CommandText, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
                DBConnection.Close()
            End If
        End Try
    End Function ' GetUserLoginList

    Private Function GetAssignedLocationList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetAssignedLocationList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct(RateAssignLocation) FROM EmployeeInfo", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetAssignedLocationList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetAssignedLocationList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetAssignedLocationList.NewGenericListRow
                row.Description = reader.GetString(0)
                row.ID = reader.GetString(0)
                GetAssignedLocationList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetAssignedLocationList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetAssignedLocationList

    Private Function GetPAFNoList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetPAFNoList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct(ISNULL(PAFNO,'')) FROM EmployeeInfo", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetPAFNoList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetPAFNoList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetPAFNoList.NewGenericListRow
                row.Description = reader.GetString(0)
                row.ID = reader.GetString(0)
                GetPAFNoList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetPAFNoList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetPAFNoList

    Private Function GetRatePayCurrencyIDList() As ApplicationDataSet.GenericListDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.GenericListRow

        GetRatePayCurrencyIDList = New ApplicationDataSet.GenericListDataTable
        sqlcmd = New SqlClient.SqlCommand("SELECT distinct(RateAssignCurrencyID)  FROM EmployeeInfo", DBConnection)
        If sqlcmd.Connection.State <> ConnectionState.Open Then
            sqlcmd.Connection.Open()
        End If
        row = GetRatePayCurrencyIDList.NewGenericListRow
        row.ID = "%"
        row.Description = "All"
        GetRatePayCurrencyIDList.AddGenericListRow(row)
        Try
            reader = sqlcmd.ExecuteReader
            While reader.Read
                row = GetRatePayCurrencyIDList.NewGenericListRow
                row.Description = reader.GetString(0)
                row.ID = reader.GetString(0)
                GetRatePayCurrencyIDList.AddGenericListRow(row)
            End While
        Catch ex As Exception
            Log("ERROR GetRatePayCurrencyIDList: " + ex.Message, w)
        Finally
            If Not IsNothing(reader) Then
                reader.Close()
            End If
        End Try
    End Function ' GetRatePayCurrencyIDList 

    Private Function GetNotSubmitted(ByVal DBConnection As SqlClient.SqlConnection, ByVal QueryString As String) As ApplicationDataSet.NotSubmittedDataTable
        GetNotSubmitted = New ApplicationDataSet.NotSubmittedDataTable
        Dim sqlcmd As SqlClient.SqlCommand
        Dim SQLText As String
        SQLText = "SELECT	[YearWeek].[Year], [YearWeek].[Week], " & _
                            "[EmployeeInfo].[EmployeeNumber], " & _
                            "RTRIM([LastName]) + ', ' + RTRIM([FirstName]) As Name, " & _
                            "[LocationID], " & _
                            "[Discipline], [DisciplineDescription], " & _
                            "Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') " & _
                    "FROM [EmployeeInfo],[YearWeek] " & _
                    "WHERE [EmployeeInfo].[DemobilisationDate] > GETDATE() " & _
                    "AND [EmployeeInfo].[StartDate] < [YearWeek].[EndDate] " & _
                    "AND [YearWeek].[EndDate] BETWEEN GETDATE() - 15 AND GETDATE() + 3 " & _
                    "AND CompanyID not like 'KNS' " & _
                    "AND " & QueryString & " " & _
                    "AND NOT EXISTS (SELECT 1 FROM [TimesheetHeader] " & _
                                    "WHERE [TimesheetHeader].[EmployeeID] = [EmployeeInfo].[EmployeeID] " & _
                                    "AND [YearWeek].[Year] = [TimesheetHeader].[Year] " & _
                                    "AND [YearWeek].[Week] = [TimesheetHeader].[Week] " & _
                                    "AND ([TimesheetHeader].[Submitted] = 1 " & _
                                    "OR [TimesheetHeader].[Endorsed] = 1 " & _
                                    "OR [TimesheetHeader].[Approved] = 1 " & _
                                    "OR [TimesheetHeader].[Rejected] = 1))"
        sqlcmd = New SqlClient.SqlCommand(SQLText, DBConnection)
        Dim Reader As SqlClient.SqlDataReader
        Dim row As ApplicationDataSet.NotSubmittedRow

        Try
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            Reader = sqlcmd.ExecuteReader
            While Reader.Read
                row = GetNotSubmitted.NewNotSubmittedRow
                row.Year = Reader.GetInt32(0)
                row.Week = Reader.GetInt32(1)
                row.EmployeeNo = Reader.GetString(2)
                row.EmployeeName = Reader.GetString(3)
                row.Location = Reader.GetString(4)
                row.Discipline = Reader.GetString(5)
                row.DisciplineDescription = Reader.GetString(6)
                row.StartWeek = Reader.GetString(7)
                GetNotSubmitted.AddNotSubmittedRow(row)
            End While

        Catch ex As Exception
            Log("ERROR GetNotSubmitted: " & ex.Message, w)
        Finally
            Log("INFO GetNotSubmitted: " & sqlcmd.CommandText, w)
            Reader.Close()
        End Try

    End Function

    Private Sub UpdateDatasets(ByVal DBConnection As SqlClient.SqlConnection, ByVal NotSubmittedFilter As String, ByVal ToApprove_ToEndorseFilter As String, ByVal Approved_RejectedFilter As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(DT_NotSubmitted) Then
                DT_NotSubmitted.Dispose()
            End If
            DT_NotSubmitted = GetNotSubmitted(DBConnection, NotSubmittedFilter)
            Me.NotSubmittedDataGrid.DataSource = DT_NotSubmitted

            DT_ToEndorse.Clear()
            If Not DA_ToEndorse.SelectCommand.CommandText.Contains(ToApprove_ToEndorseFilter) Then
                DA_ToEndorse.SelectCommand.CommandText += " AND " & ToApprove_ToEndorseFilter
            End If
            DA_ToEndorse.Fill(DT_ToEndorse)
            DT_ToEndorse.AcceptChanges()

            DT_ToApprove.Clear()
            If Not DA_ToApprove.SelectCommand.CommandText.Contains(ToApprove_ToEndorseFilter) Then
                DA_ToApprove.SelectCommand.CommandText += " AND " & ToApprove_ToEndorseFilter
            End If
            'DA_ToApprove.SelectCommand.CommandTimeout = 0
            DA_ToApprove.Fill(DT_ToApprove)
            DT_ToApprove.AcceptChanges()

            DT_ManHours.Clear()
            DA_ManHours.Fill(DT_ManHours)
            DT_ManHours.AcceptChanges()

            DT_Rejected.Clear()
            If Not DA_Rejected.SelectCommand.CommandText.Contains(NotSubmittedFilter) Then
                DA_Rejected.SelectCommand.CommandText += " AND " & Approved_RejectedFilter
            End If
            DA_Rejected.Fill(DT_Rejected)
            DT_Rejected.AcceptChanges()

            DT_Approved.Clear()
            If Not DA_Approved.SelectCommand.CommandText.Contains(NotSubmittedFilter) Then
                DA_Approved.SelectCommand.CommandText += " AND " & Approved_RejectedFilter
            End If
            DA_Approved.Fill(DT_Approved)
            DT_Approved.AcceptChanges()

            ' reporting datasets
            If IsNothing(DT_Discipline) Then
                DT_Discipline = GetDisciplineList()
                Me.DisciplineComboBox.DataSource = DT_Discipline
                Me.DisciplineComboBox.DisplayMember = DT_Discipline.DescriptionColumn.ColumnName
                Me.DisciplineComboBox.ValueMember = DT_Discipline.IDColumn.ColumnName
            End If
            If IsNothing(DT_FromYearWeek) Then
                DT_FromYearWeek = GetYearWeekList()
                Me.FromYearWeekComboBox.DataSource = DT_FromYearWeek
                Me.FromYearWeekComboBox.DisplayMember = DT_FromYearWeek.DescriptionColumn.ColumnName
                Me.FromYearWeekComboBox.ValueMember = DT_FromYearWeek.IDColumn.ColumnName

                ' to set the original selected value to the first week of the year (as it may start with week 02)
                Dim rows As DataRow()
                rows = DT_FromYearWeek.Select("ID LIKE '" & GetYearWeek() & "'", "ID")
                Me.FromYearWeekComboBox.SelectedValue = CType(rows(0), ApplicationDataSet.GenericListRow).ID

                DT_ToYearWeek = CType(DT_FromYearWeek.Copy, ApplicationDataSet.GenericListDataTable)
                Me.ToYearWeekComboBox.DataSource = DT_ToYearWeek
                Me.ToYearWeekComboBox.DisplayMember = DT_ToYearWeek.DescriptionColumn.ColumnName
                Me.ToYearWeekComboBox.ValueMember = DT_ToYearWeek.IDColumn.ColumnName
            End If
            If IsNothing(DT_CostCode) Then
                DT_CostCode = GetCostCodeList()
                Me.CostCodeComboBox.DataSource = DT_CostCode
                Me.CostCodeComboBox.DisplayMember = DT_CostCode.DescriptionColumn.ColumnName
                Me.CostCodeComboBox.ValueMember = DT_CostCode.IDColumn.ColumnName
            End If
            If IsNothing(DT_CTRCode) Then
                DT_CTRCode = GetCTRCodeList(CType(Me.CostCodeComboBox.SelectedValue, String))
                Me.CTRCodeComboBox.DataSource = DT_CTRCode
                Me.CTRCodeComboBox.DisplayMember = DT_CTRCode.DescriptionColumn.ColumnName
                Me.CTRCodeComboBox.ValueMember = DT_CTRCode.IDColumn.ColumnName
            End If
            If IsNothing(DT_Department) Then
                DT_Department = GetDepartmentList()
                Me.DepartmentComboBox.DataSource = DT_Department
                Me.DepartmentComboBox.DisplayMember = DT_Department.DescriptionColumn.ColumnName
                Me.DepartmentComboBox.ValueMember = DT_Department.IDColumn.ColumnName
            End If
            If IsNothing(DT_Company) Then
                DT_Company = GetCompanyList()
                Me.CompanyComboBox.DataSource = DT_Company
                Me.CompanyComboBox.DisplayMember = DT_Company.DescriptionColumn.ColumnName
                Me.CompanyComboBox.ValueMember = DT_Company.IDColumn.ColumnName
            End If
            If IsNothing(DT_AssignedLocation) Then
                DT_AssignedLocation = GetAssignedLocationList()
                Me.AssignedLocationComboBox.DataSource = DT_AssignedLocation
                Me.AssignedLocationComboBox.DisplayMember = DT_AssignedLocation.DescriptionColumn.ColumnName
                Me.AssignedLocationComboBox.ValueMember = DT_AssignedLocation.IDColumn.ColumnName
            End If
            If IsNothing(DT_PAFNo) Then
                DT_PAFNo = GetPAFNoList()
                Me.PAFNoComboBox.DataSource = DT_PAFNo
                Me.PAFNoComboBox.DisplayMember = DT_PAFNo.DescriptionColumn.ColumnName
                Me.PAFNoComboBox.ValueMember = DT_PAFNo.IDColumn.ColumnName
            End If
            If IsNothing(DT_RatePayCurrencyID) Then
                DT_RatePayCurrencyID = GetRatePayCurrencyIDList()
                Me.RatePayCurrencyIDComboBox.DataSource = DT_RatePayCurrencyID
                Me.RatePayCurrencyIDComboBox.DisplayMember = DT_RatePayCurrencyID.DescriptionColumn.ColumnName
                Me.RatePayCurrencyIDComboBox.ValueMember = DT_RatePayCurrencyID.IDColumn.ColumnName
            End If
            Me.Cursor = Cursors.Default
            SortDataGrids()
        Catch ex As Exception
            Log("ERROR UpdateDatasets: " + ex.Message() + "Requete :  " + DA_Approved.SelectCommand.CommandText, w)
        End Try
    End Sub

    Private Sub SynchDatagrids(ByVal sender As Object, ByVal e As EventArgs) Handles ToApproveDataGrid.Scroll, ToApproveRightDataGrid.Scroll, ToEndorseDataGrid.Scroll, ToEndorseRightDataGrid.Scroll, ManHoursDataGrid.Scroll, ManHoursRightDataGrid.Scroll
        Dim dg As DataGridView
        Try
            dg = CType(sender, DataGridView)
            Select Case dg.Name
                Case ToApproveDataGrid.Name
                    ToApproveRightDataGrid.FirstDisplayedScrollingRowIndex = ToApproveDataGrid.FirstDisplayedScrollingRowIndex
                Case ToApproveRightDataGrid.Name
                    ToApproveDataGrid.FirstDisplayedScrollingRowIndex = ToApproveRightDataGrid.FirstDisplayedScrollingRowIndex
                Case ToEndorseDataGrid.Name
                    ToEndorseRightDataGrid.FirstDisplayedScrollingRowIndex = ToEndorseDataGrid.FirstDisplayedScrollingRowIndex
                Case ToEndorseRightDataGrid.Name
                    ToEndorseDataGrid.FirstDisplayedScrollingRowIndex = ToEndorseRightDataGrid.FirstDisplayedScrollingRowIndex
                Case ManHoursDataGrid.Name
                    ManHoursRightDataGrid.FirstDisplayedScrollingRowIndex = ManHoursDataGrid.FirstDisplayedScrollingRowIndex
                Case ManHoursRightDataGrid.Name
                    ManHoursDataGrid.FirstDisplayedScrollingRowIndex = ManHoursRightDataGrid.FirstDisplayedScrollingRowIndex
            End Select
        Catch ex As Exception
            Log("ERROR SynchDatagrids: " + ex.Message, w)
        End Try
    End Sub

    Private Function GetFilterCtrcodeAll(ByVal selectvalue As String) As String
        If selectvalue = "%" Then
            For Each rowtemp As DataRow In GetCTRCodeList(CType(CostCodeComboBox.SelectedValue, String)).Rows
                If Not rowtemp Is DBNull.Value Then
                    If selectvalue = "%" Then
                        selectvalue = "'" & rowtemp.Item("ID").ToString & "'"
                    Else
                        selectvalue = selectvalue & ",'" & rowtemp.Item("ID").ToString & "'"
                    End If
                End If
            Next rowtemp
        Else
            selectvalue = "'" & selectvalue & "'"
        End If
        Return selectvalue
    End Function 'GetFilterCtrcodeAll

    Private Function GetFilterCostcodeAll(ByVal selectvalue As String) As String
        If selectvalue = "%" Then
            For Each rowtemp As DataRow In GetCostCodeList.Rows
                If Not rowtemp Is DBNull.Value Then
                    If selectvalue = "%" Then
                        selectvalue = "'" & rowtemp.Item("ID").ToString & "'"
                    Else
                        selectvalue = selectvalue & ",'" & rowtemp.Item("ID").ToString & "'"
                    End If
                End If
            Next rowtemp
        Else
            selectvalue = "'" & selectvalue & "'"
        End If
        Return selectvalue
    End Function 'GetFilterCostcodeAll    

    Private Function GetFilterDepartmentAll(ByVal selectvalue As String) As String
        If selectvalue = "%" Then
            For Each rowtemp As DataRow In GetDepartmentList.Rows
                If Not rowtemp Is DBNull.Value Then
                    If selectvalue = "%" Then
                        selectvalue = "'" & rowtemp.Item("ID").ToString & "'"
                    Else
                        selectvalue = selectvalue & ",'" & rowtemp.Item("ID").ToString & "'"
                    End If
                End If
            Next rowtemp
        Else
            selectvalue = "'" & selectvalue & "'"
        End If
        Return selectvalue
    End Function 'GetFilterDepartmentAll

    Private Sub DataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) _
    Handles ManHoursDataGrid.DataError, ManHoursRightDataGrid.DataError, ToApproveDataGrid.DataError, ToApproveRightDataGrid.DataError,
    ToEndorseDataGrid.DataError, ToEndorseRightDataGrid.DataError, ApprovedDataGrid.DataError, RejectedDataGrid.DataError, NotSubmittedDataGrid.DataError,
    ToApproveRightsDataGrid.DataError, NotSubmitRightsDataGridLeft.DataError, OnBehalfRightsDataGridLeft.DataError
        Try
            ' catch error data in dataGrid    
            MsgBox("ERROR CONTEXT" + e.Context.ToString)
        Catch ex As Exception
            Log("ERROR " + ex.Message, w)
        End Try
    End Sub ' DataGridView_DataError

    Public Shared Sub Log(ByVal logMessage As String, ByVal w As TextWriter)
        w.Write(vbCrLf + "Log Entry : ")
        w.Write(Environment.UserDomainName + "\" + Environment.UserName + " : ")
        w.Write("{0} {1}", DateTime.Now.ToLongTimeString, DateTime.Now.ToLongDateString())
        w.Write("  :{0}", logMessage)
        ' Update the underlying file.
        w.Flush()
    End Sub   ' Log 

    Private Function ValidData(ByRef DG As DataGridView) As Boolean
        Dim RejectCheckbox As DataGridViewCheckBoxCell
        Dim RejectReasonTextBox As DataGridViewTextBoxCell
        Try
            For i As Integer = 0 To DG.Rows.Count - 1
                If TabControl1.SelectedTab.Name = ManHoursTabPage.Name Then
                    RejectCheckbox = CType(DG.Item(0, i), DataGridViewCheckBoxCell)
                    RejectReasonTextBox = CType(DG.Item(1, i), DataGridViewTextBoxCell)
                Else
                    RejectCheckbox = CType(DG.Item(1, i), DataGridViewCheckBoxCell)
                    RejectReasonTextBox = CType(DG.Item(2, i), DataGridViewTextBoxCell)
                End If
                If CType(RejectCheckbox.Value, Boolean) Then
                    If CType(RejectReasonTextBox.FormattedValue, String) = "" Then
                        DG.CurrentCell = RejectReasonTextBox
                        MsgBox("Rejected rows need a reason", vbInformation)
                        Return False
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Log("ERROR Validation Data: " + ex.Message, w)
            Return False
        End Try
    End Function

    Private Sub EndorseRow(ByVal row As ApplicationDataSet.TimesheetDetailRow, ByVal UserLogin As String)
        Try
            EndorseTSSQLCommand.Parameters("@EmployeeID").Value = row.EmployeeID
            EndorseTSSQLCommand.Parameters("@Year").Value = row.Year
            EndorseTSSQLCommand.Parameters("@Week").Value = row.Week
            EndorseTSSQLCommand.Parameters("@CostCode").Value = row.CostCode
            EndorseTSSQLCommand.Parameters("@CTRCode").Value = row.CTRCode
            EndorseTSSQLCommand.Parameters("@UserLogin").Value = UserLogin
            EndorseTSSQLCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR TS to Endorse" + ex.Message, MsgBoxStyle.Information)
            Log("ERROR TS to Endorse: " + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub ApproveRow(ByVal row As ApplicationDataSet.TimesheetDetailRow, ByVal UserLogin As String)
        Try
            ApproveTSSQLCommand.Parameters("@EmployeeID").Value = row.EmployeeID
            ApproveTSSQLCommand.Parameters("@Year").Value = row.Year
            ApproveTSSQLCommand.Parameters("@Week").Value = row.Week
            ApproveTSSQLCommand.Parameters("@CostCode").Value = row.CostCode
            ApproveTSSQLCommand.Parameters("@CTRCode").Value = row.CTRCode
            ApproveTSSQLCommand.Parameters("@UserLogin").Value = UserLogin
            ApproveTSSQLCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR TS to Approve: " + ex.Message, MsgBoxStyle.Information)
            Log("ERROR TS to Approve: " + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub RejectRow(ByVal row As ApplicationDataSet.TimesheetDetailRow, ByVal UserLogin As String)
        Try
            RejectTSSQLCommand.Parameters("@EmployeeID").Value = row.EmployeeID
            RejectTSSQLCommand.Parameters("@Year").Value = row.Year
            RejectTSSQLCommand.Parameters("@Week").Value = row.Week
            RejectTSSQLCommand.Parameters("@CostCode").Value = row.CostCode
            RejectTSSQLCommand.Parameters("@CTRCode").Value = row.CTRCode
            RejectTSSQLCommand.Parameters("@RejectReason").Value = row.RejectReason
            RejectTSSQLCommand.Parameters("@UserLogin").Value = UserLogin
            RejectTSSQLCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR TS to Reject" + ex.Message, MsgBoxStyle.Information)
            Log("ERROR TS to Reject: " + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub ManHoursRow(ByVal row As ApplicationDataSet.TimesheetHeaderRow)
        Try
            ManHoursSQLCommand.Parameters("@EmployeeID").Value = row.EmployeeID
            ManHoursSQLCommand.Parameters("@Year").Value = row.Year
            ManHoursSQLCommand.Parameters("@Week").Value = row.Week
            ManHoursSQLCommand.Parameters("@RejectReason").Value = row.RejectReason

            ManHoursSQLCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("ERROR TS man hours : " + ex.Message, MsgBoxStyle.Information)
            Log("ERROR TS man hours: " + ex.Message, w)
        Finally
        End Try
    End Sub

    Private Sub SaveData(ByRef DT As ApplicationDataSet.TimesheetDetailDataTable)
        Dim UserLogin As String
        UserLogin = UserConnected
        For Each row As ApplicationDataSet.TimesheetDetailRow In DT.GetChanges(DataRowState.Modified).Rows
            ' the order is important: we do not want to endorse rows that were already endorsed
            If row.Approved Then
                ApproveRow(row, UserLogin)
            ElseIf row.Rejected Then
                RejectRow(row, UserLogin)
            ElseIf row.Endorsed Then
                EndorseRow(row, UserLogin)
            End If
        Next
    End Sub

    Private Sub SaveData(ByRef DT As ApplicationDataSet.TimesheetHeaderDataTable)
        For Each row As ApplicationDataSet.TimesheetHeaderRow In DT.GetChanges(DataRowState.Modified).Rows
            If row.Rejected Then
                ManHoursRow(row)
            End If
        Next
    End Sub

    Private Function GetStartDate(ByVal YearWeek As String) As String
        Dim Year As Integer
        Dim Week As Integer
        Try
            Year = Integer.Parse(YearWeek.Substring(0, 4))
            Week = Integer.Parse(YearWeek.Substring(4, 2))
            Dim sqlcmd As SqlClient.SqlCommand
            sqlcmd = New SqlClient.SqlCommand("SELECT cast(StartDate As Date) FROM YearWeek WHERE Year = @Year AND Week = @Week", DBConnection)
            sqlcmd.Parameters.AddWithValue("@Year", Year)
            sqlcmd.Parameters.AddWithValue("@Week", Week)
            GetStartDate = Format(CDate(sqlcmd.ExecuteScalar()), "yyyy-MM-dd")
        Catch ex As Exception
            Log("ERROR GetStartDate: " + ex.Message, w)
        End Try
    End Function

    Private Function GetYearWeek() As String
        Dim sqlcmd As SqlClient.SqlCommand
        Try
            sqlcmd = New SqlClient.SqlCommand("SELECT TOP 1 cast(Year as varchar(4)) + cast(Week as varchar(2)) FROM YearWeek WHERE cast(StartDate as Date) <=  cast(GetDate()-15 as Date) order by StartDate desc", DBConnection)
            GetYearWeek = CStr(sqlcmd.ExecuteScalar())
        Catch ex As Exception
            Log("ERROR GetYearWeek: " + ex.Message + " request : " + sqlcmd.CommandText, w)
        End Try
    End Function

    Private Sub RefreshUserList()
        'Bind de la dropdownlist
        DT_UserLogins = GetUserLoginList()
        ' mapping des colonnes dans le selected_index change car la ligne ci-dessous renvoie vers cette méthode  
        RemoveHandler Me.ChoiceUserComboBox.SelectedIndexChanged, AddressOf Me.ChoiceUserComboBox_SelectedIndexChanged
        Me.ChoiceUserComboBox.DataSource = DT_UserLogins
        Me.ChoiceUserComboBox.DisplayMember = DT_UserLogins.DescriptionColumn.ColumnName
        Me.ChoiceUserComboBox.ValueMember = DT_UserLogins.IDColumn.ColumnName
        Me.ChoiceUserComboBox.SelectedIndex = -1

        'On masque les datagrid 
        Me.ToApproveRightsDataGrid.Visible = False
        Me.NotSubmitRightsDataGridLeft.Visible = False
        Me.OnBehalfRightsDataGridLeft.Visible = False

        'RAZ des checkboxes
        Me.AccessRightsCheckBox.Checked = False
        Me.ApproverCheckBox.Checked = False
        Me.EndorserCheckBox.Checked = False
        Me.FullAccessReportCheckBox.Checked = False
        Me.OnbehalfCheckBox.Checked = False
        Me.ZeroManhourCheckBox.Checked = False
        Me.CheckBoxTSadjustRead.Checked = False
        Me.CheckBoxTSadjustWrite.Checked = False


        AddHandler Me.ChoiceUserComboBox.SelectedIndexChanged, AddressOf Me.ChoiceUserComboBox_SelectedIndexChanged
    End Sub

    Private Sub SaveSpecialRight()

        Dim selectedUser As String

        Try
            If ChoiceUserComboBox.SelectedIndex = -1 Then
                MsgBox("You must select a user!", CType(vbExclamation, MsgBoxStyle), "")
                Return
            End If

            selectedUser = Split(ChoiceUserComboBox.SelectedValue.ToString, "-").GetValue(0).ToString

            DBConnection.Close()
            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If

            SpecialRightsSQLCommand = New SqlClient.SqlCommand
            SpecialRightsSQLCommand.Connection = DBConnection
            SpecialRightsSQLCommand.CommandType = CommandType.Text
            SpecialRightsSQLCommand.CommandText = "UPDATE SpecialRights SET EntryOnBehalf  = @EntryOnBehalf, EndorseTimesheets = @EndorseTimesheets, " & _
                                                  "ApproveTimesheets = @ApproveTimesheets, ManHoursZero = @ManHoursZero, AccessRights = @AccessRights, " & _
                                                  "ReportFullRights  = @ReportFullRights, TSAdjustmentRead = @TSAdjustmentRead, " & _
                                                  "TSAdjustmentWrite = @TSAdjustmentWrite WHERE UserLogin = @userLogin"
            SpecialRightsSQLCommand.Parameters.Add("@EntryOnBehalf", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@EndorseTimesheets", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@ApproveTimesheets", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@ManHoursZero", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@AccessRights", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@ReportFullRights", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@TSAdjustmentRead", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@TSAdjustmentWrite", SqlDbType.Bit)
            SpecialRightsSQLCommand.Parameters.Add("@userLogin", SqlDbType.VarChar)

            SpecialRightsSQLCommand.Parameters("@EntryOnBehalf").Value = IIf(Me.OnbehalfCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@EndorseTimesheets").Value = IIf(Me.EndorserCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@ApproveTimesheets").Value = IIf(Me.ApproverCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@ManHoursZero").Value = IIf(Me.ZeroManhourCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@AccessRights").Value = IIf(Me.AccessRightsCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@ReportFullRights").Value = IIf(Me.FullAccessReportCheckBox.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@TSAdjustmentRead").Value = IIf(Me.CheckBoxTSadjustRead.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@TSAdjustmentWrite").Value = IIf(Me.CheckBoxTSadjustWrite.Checked, True, False)
            SpecialRightsSQLCommand.Parameters("@userLogin").Value = selectedUser

            SpecialRightsSQLCommand.ExecuteNonQuery()
            SpecialRightsSQLCommand.Dispose()
        Catch ex As Exception
            Log("ERROR During SaveSpecialRight: " + ex.Message, w)
            MsgBox("ERROR During SaveSpecialRight: " + ex.Message, CType(vbExclamation, MsgBoxStyle), "")
        End Try


    End Sub

    'LGX: Define the base sortable column and allows all datagridview's column to be sorted
    Private Sub SortDataGrids()
        Try
            'Not Submited : sort on employee last name
            NotSubmittedDataGrid.Sort(NotSubmittedDataGrid.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
            NotSubmittedDataGrid.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In NotSubmittedDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            ' Endorse : sort on CTR code
            ToEndorseDataGrid.Sort(ToEndorseDataGrid.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
            ToEndorseDataGrid.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In ToEndorseDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            ' Approve : sort on CTR code
            ToApproveDataGrid.Sort(ToApproveDataGrid.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
            ToApproveDataGrid.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In ToApproveDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            ' Man Hour : sort on CTR code
            ManHoursDataGrid.Sort(ManHoursDataGrid.Columns(6), System.ComponentModel.ListSortDirection.Ascending)
            ManHoursDataGrid.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In ManHoursDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            ' Rejected : sort on employee name
            RejectedDataGrid.Sort(RejectedDataGrid.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
            RejectedDataGrid.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In RejectedDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            ' Approved : sort on employee name
            ApprovedDataGrid.Sort(ApprovedDataGrid.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
            ApprovedDataGrid.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In ApprovedDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column
        Catch ex As Exception
            Log("ERROR During the sort of datagrids: " + ex.Message, w)
        Finally
        End Try

    End Sub

    'LGX: All datagrids of special rights tab to be sorted
    Private Sub SortDataGridsRightsTab()
        Try
            ToApproveDataGrid.Sort(ToApproveDataGrid.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
            ToApproveDataGrid.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In ToApproveDataGrid.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            NotSubmitRightsDataGridLeft.Sort(NotSubmitRightsDataGridLeft.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
            NotSubmitRightsDataGridLeft.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In NotSubmitRightsDataGridLeft.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column

            OnBehalfRightsDataGridLeft.Sort(OnBehalfRightsDataGridLeft.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
            OnBehalfRightsDataGridLeft.Columns(1).SortMode = DataGridViewColumnSortMode.Automatic
            For Each Column As DataGridViewColumn In OnBehalfRightsDataGridLeft.Columns
                Column.SortMode = DataGridViewColumnSortMode.Automatic
            Next Column
        Catch ex As Exception
            Log("ERROR During the sort of datagrids: " + ex.Message, w)
        Finally
        End Try

    End Sub

#End Region

#End Region

#Region "Timesheet Adjustment"

    'Clic sur le bouton "FIND" de l'onglet "Timesheet Adjustment"
    Private Sub ButtonFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFind.Click

        'Dim TSTable As TimesheetApproval.TSAdjustmentDataSet.TimesheetDetailDataTable
        'Dim TSHours As TimesheetApproval.TSAdjustmentDataSet.HoursDataTable

        Me.Cursor = Cursors.WaitCursor

        Me.ButtonAddRow.Enabled = False
        Me.PanelAdjustment.Visible = False
        Me.TSAdjEmptyFields()
        ' On met à zero le champ uniquement lorsque c'est l'utilisateur (et non le systeme) qui clic sur FIND
        If Not sender Is Nothing Then
            Me.RichTextBoxAdjComment.Text = ""
        End If


        'Si les paramètres de recherche sont bien renseignés
        If Me.CheckFindFields = True Then
            Me.TSAdjYear = Me.TextBoxYear.Text
            Me.TSAdjWeek = Me.TextBoxWeek.Text
            ' La recherche s'effectue suivant l'employeeNo
            If Me.TextBoxEmpID.Text = "" Then
                Me.TSAdjEmpId = Me.GetEmpIDByEmpNo(Me.TextBoxEmployeeNo.Text)
                TSAdjTSDetails = Me.TimesheetDetailTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjHours = Me.HoursTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjTSError = Me.TimesheetErrorTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjTSHeader = Me.TimesheetHeaderTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                Me.TextBoxName.Text = Me.GetEmpNameByEmpID(TSAdjEmpId)
            Else
                Me.TSAdjEmpId = CInt(Me.TextBoxEmpID.Text)
                TSAdjTSDetails = Me.TimesheetDetailTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjHours = Me.HoursTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjTSError = Me.TimesheetErrorTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                TSAdjTSHeader = Me.TimesheetHeaderTableAdapter.GetData(TSAdjEmpId, CInt(TSAdjYear), CInt(TSAdjWeek))
                Me.TextBoxName.Text = Me.GetEmpNameByEmpID(TSAdjEmpId)
            End If


            If TSAdjTSDetails.Rows.Count <> 0 Then
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSDetails").Value = TSAdjTSDetails
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetHours").Value = TSAdjHours
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSError").Value = TSAdjTSError
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSHeader").Value = TSAdjTSHeader
                Me.ActivateAddRowButton()
            Else
                TSAdjTSDetails.Clear()
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSDetails").Value = TSAdjTSDetails
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetHours").Value = TSAdjTSDetails
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSError").Value = TSAdjTSDetails
                Me.ReportViewerTSAdj.LocalReport.DataSources.Item("DataSetTSHeader").Value = TSAdjTSDetails
                MsgBox("No record found", MsgBoxStyle.Information, "")
            End If
        End If

        Me.ReportViewerTSAdj.RefreshReport()
        Me.Cursor = Cursors.Arrow
    End Sub

    ' Vide tous les champs de saisie "add row" de l'onglet "Timesheet Adjustment"
    Private Sub TSAdjEmptyFields()
        Me.ComboBoxCostCodeTSAdj.SelectedIndex = -1
        Me.ComboBoxCTRCodeTSAdj.SelectedIndex = -1
        Me.TextBoxNTSa.Text = "0"
        Me.TextBoxNTSu.Text = "0"
        Me.TextBoxNTM.Text = "0"
        Me.TextBoxNTTu.Text = "0"
        Me.TextBoxNTW.Text = "0"
        Me.TextBoxNTTh.Text = "0"
        Me.TextBoxNTF.Text = "0"
        Me.TextBoxNTTOt.Text = "0"
        Me.TextBoxOTSa.Text = "0"
        Me.TextBoxOTSu.Text = "0"
        Me.TextBoxOTM.Text = "0"
        Me.TextBoxOTTu.Text = "0"
        Me.TextBoxOTW.Text = "0"
        Me.TextBoxOTTh.Text = "0"
        Me.TextBoxOTF.Text = "0"
        Me.TextBoxOTTot.Text = "0"
    End Sub

    ' Vérifie la validité dse données saisies par l'utilisateur. Return TRUE si les données saisies sont valides, FALSE sinon.
    Private Function TSAdjCheckData() As Boolean
        Dim retour As Boolean
        retour = True
        If Me.ComboBoxCostCodeTSAdj.Text = "" Then
            MsgBox("Please provide a Cost Code!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.ComboBoxCTRCodeTSAdj.Text = "" Then
            MsgBox("Please provide a CTR Code!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTSa.Text = "" Then
            MsgBox("Please provide a NT Saturday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTSu.Text = "" Then
            MsgBox("Please provide a NT Sunday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTM.Text = "" Then
            MsgBox("Please provide a NT Monday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTTu.Text = "" Then
            MsgBox("Please provide a NT Tuesday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTW.Text = "" Then
            MsgBox("Please provide a NT Wednesday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTTh.Text = "" Then
            MsgBox("Please provide a NT Thursday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxNTF.Text = "" Then
            MsgBox("Please provide a NT Friday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False

        ElseIf Me.TextBoxOTSa.Text = "" Then
            MsgBox("Please provide a OT Saturday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTSu.Text = "" Then
            MsgBox("Please provide a OT Sunday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTM.Text = "" Then
            MsgBox("Please provide a OT Monday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTTu.Text = "" Then
            MsgBox("Please provide a OT Tuesday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTW.Text = "" Then
            MsgBox("Please provide a OT Wednesday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTTh.Text = "" Then
            MsgBox("Please provide a OT Thursday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxOTF.Text = "" Then
            MsgBox("Please provide a OT Friday value!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf CDec(Me.TextBoxNTTOt.Text) + CDec(Me.TextBoxOTTot.Text) = 0 Then
            MsgBox("You can not submit with zero man hour!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False

        End If

        Return retour
    End Function

    ' Clic sur  le bouton "add row" de l'onglet "Timesheet Adjustment"
    Private Sub ButtonAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRow.Click
        'Affichage du panel
        Me.PanelAdjustment.Visible = True
        'Bind combobox Cost Code
    End Sub

    ' Clic sur  le bouton "Validate Adjustment" de l'onglet "Timesheet Adjustment"
    Private Sub ButtonValidateAdj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonValidateAdj.Click
        If Me.TSAdjCheckData() Then
            Me.InsertDataInTSDetail()
            Me.ExportTSToPaf()
            'Rafraichissement du rapport
            Me.ButtonFind_Click(Nothing, Nothing)
            Me.TSAdjEmptyFields()
            Me.PanelAdjustment.Hide()
            MsgBox("Adjustment successfull", CType(vbInformation, MsgBoxStyle), "")
        End If
    End Sub

    Private Function CheckFindFields() As Boolean
        Dim retour As Boolean
        retour = True
        If Me.TextBoxEmpID.Text = "" And Me.TextBoxEmployeeNo.Text = "" Then
            MsgBox("Please provide an employee ID or an Employee Number!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxEmpID.Text <> "" And Me.TextBoxEmployeeNo.Text <> "" Then
            MsgBox("Please provide an employee ID OR an Employee Number!", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxYear.Text = "" Then
            MsgBox("Please provide a year !", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        ElseIf Me.TextBoxWeek.Text = "" Then
            MsgBox("Please provide a week !", MsgBoxStyle.Exclamation, "Missing field")
            retour = False
        End If
        Return retour
    End Function

    ' Récupère le nom d'un employé en fonction de son employeeID
    Private Function GetEmpNameByEmpID(ByVal EmpID As Int32) As String

        Dim retour As String
        Dim SQLCmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim Query As String

        If DBConnection.State <> ConnectionState.Open Then
            DBConnection.Open()
        End If

        Query = "Select LastName + ', ' + FirstName from Employeeinfo where EmployeeID = " + CStr(EmpID)
        'Execute the query
        SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
        reader = SQLCmd.ExecuteReader
        reader.Read()
        retour = reader.GetString(0)

        Return retour
    End Function

    ' Récupère l'employeeID d'un employé en fonction de son employeeNo
    Private Function GetEmpIDByEmpNo(ByVal EmpNo As String) As Int32

        Dim retour As Int32
        Dim SQLCmd As SqlClient.SqlCommand
        Dim reader As SqlClient.SqlDataReader
        Dim Query As String

        If DBConnection.State <> ConnectionState.Open Then
            DBConnection.Open()
        End If

        Query = "Select employeeID from Employeeinfo where EmployeeNumber = '" + EmpNo + "'"
        'Execute the query
        SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
        reader = SQLCmd.ExecuteReader
        reader.Read()
        retour = reader.GetInt32(0)

        Return retour
    End Function

    Private Sub FindByHitEnterKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxWeek.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.ButtonFind_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub InitializeTabTSAdjustment()
        RemoveHandler Me.ComboBoxCostCodeTSAdj.SelectedIndexChanged, AddressOf Me.ComboBoxCostCodeTSAdj_SelectedIndexChanged
        'Bind du combo box cost code
        Me.ComboBoxCostCodeTSAdj.DataSource = Me.CostCodeTableAdapter.GetData()
        Me.ComboBoxCostCodeTSAdj.DisplayMember = "CostCode"
        Me.ComboBoxCostCodeTSAdj.ValueMember = "CostCode"
        Me.ComboBoxCostCodeTSAdj.SelectedIndex = -1
        AddHandler Me.ComboBoxCostCodeTSAdj.SelectedIndexChanged, AddressOf Me.ComboBoxCostCodeTSAdj_SelectedIndexChanged
    End Sub

    Private Sub ComboBoxCostCodeTSAdj_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCostCodeTSAdj.SelectedIndexChanged
        If Me.ComboBoxCostCodeTSAdj.SelectedIndex <> -1 Then
            Me.ComboBoxCTRCodeTSAdj.DataSource = Me.CTRCodeTableAdapter.GetData(CStr(Me.ComboBoxCostCodeTSAdj.SelectedValue))
            Me.ComboBoxCTRCodeTSAdj.DisplayMember = "CTRCode"
            Me.ComboBoxCTRCodeTSAdj.ValueMember = "CTRCode"
            Me.ComboBoxCTRCodeTSAdj.SelectedIndex = -1
        End If
    End Sub

    ' Mets à jour le ch amp NT Total en fonction dse valeurs entrées par l'utilisateur
    Private Sub CalculateNTTotal()
        Dim ntsa As Decimal
        Dim ntsu As Decimal
        Dim ntm As Decimal
        Dim nttu As Decimal
        Dim ntw As Decimal
        Dim ntth As Decimal
        Dim ntf As Decimal
        Dim nttot As Decimal


        If Me.TextBoxNTSa.Text = "" Then
            ntsa = 0
        ElseIf IsNumeric(Me.TextBoxNTSa.Text) Then
            ntsa = CDec(Me.TextBoxNTSa.Text)
        Else
            MsgBox("NT Saturday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTSa.Text = ""
        End If

        If Me.TextBoxNTSu.Text = "" Then
            ntsu = 0
        ElseIf IsNumeric(Me.TextBoxNTSu.Text) Then
            ntsu = CDec(Me.TextBoxNTSu.Text)
        Else
            MsgBox("NT Sunday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTSu.Text = ""
        End If

        If Me.TextBoxNTM.Text = "" Then
            ntm = 0
        ElseIf IsNumeric(Me.TextBoxNTM.Text) Then
            ntm = CDec(Me.TextBoxNTM.Text)
        Else
            MsgBox("NT Monday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTM.Text = ""
        End If

        If Me.TextBoxNTTu.Text = "" Then
            nttu = 0
        ElseIf IsNumeric(Me.TextBoxNTTu.Text) Then
            nttu = CDec(Me.TextBoxNTTu.Text)
        Else
            MsgBox("NT Tuesday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTTu.Text = ""
        End If

        If Me.TextBoxNTW.Text = "" Then
            ntw = 0
        ElseIf IsNumeric(Me.TextBoxNTW.Text) Then
            ntw = CDec(Me.TextBoxNTW.Text)
        Else
            MsgBox("NT Wednseday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTW.Text = ""
        End If

        If Me.TextBoxNTTh.Text = "" Then
            ntth = 0
        ElseIf IsNumeric(Me.TextBoxNTTh.Text) Then
            ntth = CDec(Me.TextBoxNTTh.Text)
        Else
            MsgBox("NT Thursday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTTh.Text = ""
        End If

        If Me.TextBoxNTF.Text = "" Then
            ntf = 0
        ElseIf IsNumeric(Me.TextBoxNTF.Text) Then
            ntf = CDec(Me.TextBoxNTF.Text)
        Else
            MsgBox("NT Friday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxNTF.Text = ""
        End If

        nttot = ntsa + ntsu + ntm + nttu + ntw + ntth + ntf

        Me.TextBoxNTTOt.Text = CStr(nttot)

    End Sub

    ' Mets à jour le ch amp OT Total en fonction dse valeurs entrées par l'utilisateur
    Private Sub CalculateOTTotal()
        Dim otsa As Decimal
        Dim otsu As Decimal
        Dim otm As Decimal
        Dim ottu As Decimal
        Dim otw As Decimal
        Dim otth As Decimal
        Dim otf As Decimal
        Dim ottot As Decimal


        If Me.TextBoxOTSa.Text = "" Then
            otsa = 0
        ElseIf IsNumeric(Me.TextBoxOTSa.Text) Then
            otsa = CDec(Me.TextBoxOTSa.Text)
        Else
            MsgBox("OT Saturday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTSa.Text = ""
        End If

        If Me.TextBoxOTSu.Text = "" Then
            otsu = 0
        ElseIf IsNumeric(Me.TextBoxOTSu.Text) Then
            otsu = CDec(Me.TextBoxOTSu.Text)
        Else
            MsgBox("OT Sunday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTSu.Text = ""
        End If

        If Me.TextBoxOTM.Text = "" Then
            otm = 0
        ElseIf IsNumeric(Me.TextBoxOTM.Text) Then
            otm = CDec(Me.TextBoxOTM.Text)
        Else
            MsgBox("OT Monday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTM.Text = ""
        End If

        If Me.TextBoxOTTu.Text = "" Then
            ottu = 0
        ElseIf IsNumeric(Me.TextBoxOTTu.Text) Then
            ottu = CDec(Me.TextBoxOTTu.Text)
        Else
            MsgBox("OT Tuesday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTTu.Text = ""
        End If

        If Me.TextBoxOTW.Text = "" Then
            otw = 0
        ElseIf IsNumeric(Me.TextBoxOTW.Text) Then
            otw = CDec(Me.TextBoxOTW.Text)
        Else
            MsgBox("OT Wednseday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTW.Text = ""
        End If

        If Me.TextBoxOTTh.Text = "" Then
            otth = 0
        ElseIf IsNumeric(Me.TextBoxOTTh.Text) Then
            otth = CDec(Me.TextBoxOTTh.Text)
        Else
            MsgBox("OT Thursday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTTh.Text = ""
        End If

        If Me.TextBoxOTF.Text = "" Then
            otf = 0
        ElseIf IsNumeric(Me.TextBoxOTF.Text) Then
            otf = CDec(Me.TextBoxOTF.Text)
        Else
            MsgBox("OT Friday value must be a numeric value!", MsgBoxStyle.Exclamation, "Please enter a number")
            Me.TextBoxOTF.Text = ""
        End If

        ottot = otsa + otsu + otm + ottu + otw + otth + otf

        Me.TextBoxOTTot.Text = CStr(ottot)

    End Sub

    'Insère les données de l'ajustement de la timesheet dans la table timesheet detail
    ' Insère également la correction dans la table Timesheetcorrection
    Private Sub InsertDataInTSDetail()
        Dim SQLCmd As SqlClient.SqlCommand
        Dim Reader As SqlClient.SqlDataReader
        Dim Query As String
        Dim correctionID As Integer

        Try

            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If

            'Insertion dans Timesheetcorrection
            Query = "INSERT INTO [Timesheet].[dbo].[TimesheetCorrection] ([Reason],[UserLogin],[CorrectionDate]) VALUES (" & _
                    "'TIMESHEET ADJUSTMENT " + Replace(Me.RichTextBoxAdjComment.Text, "'", "''") + "', '" + Me.UserConnected + "', GETDATE() )"
            SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
            SQLCmd.ExecuteNonQuery()

            'On récupère le correctionID inséré
            Query = "SELECT TOP 1 [CorrectionID] FROM [Timesheet].[dbo].[TimesheetCorrection] order by CorrectionID desc"
            SQLCmd.CommandText = Query
            Reader = SQLCmd.ExecuteReader()
            Reader.Read()
            correctionID = Reader.GetInt32(0)
            Reader.Close()

            Query = "INSERT INTO [Timesheet].[dbo].[TimesheetDetail] ([EmployeeID],[Year],[Week],[CostCode],[CTRCode]," & _
                                 "[NTSaturday],[NTSunday],[NTMonday],[NTTuesday],[NTWednesday],[NTThursday],[NTFriday],[OTSaturday]," & _
                                 "[OTSunday],[OTMonday],[OTTuesday],[OTWednesday],[OTThursday],[OTFriday]," & _
                                 "[Approved],[ApprovedBy],[ApprovedDate],[UserLogin],[CorrectionID]) VALUES (" & _
                                 "'" + Me.TSAdjEmpId.ToString + "'," & _
                                 "'" + Me.TSAdjYear + "'," & _
                                 "'" + Me.TSAdjWeek + "'," & _
                                 "'" + Me.ComboBoxCostCodeTSAdj.Text + "'," & _
                                 "'" + Me.ComboBoxCTRCodeTSAdj.Text + "'," & _
                                 "'" + Replace(Me.TextBoxNTSa.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTSu.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTM.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTTu.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTW.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTTh.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxNTF.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTSa.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTSu.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTM.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTTu.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTW.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTTh.Text, ",", ".") + "'," & _
                                 "'" + Replace(Me.TextBoxOTF.Text, ",", ".") + "'," & _
                                 "'1'," & _
                                 "'" + Me.UserConnected + "'," & _
                                 "GETDATE()," & _
                                 "'" + Me.UserConnected + "'," & _
                                 "'" + correctionID.ToString() + "'" & _
                                 ")"


            SQLCmd.CommandText = Query
            'Execute the query
            SQLCmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Problem during insertion into Timesheetdetail table: " + ex.Message, CType(vbCritical, MsgBoxStyle), "")
            Log("ERROR InsertDataInTSDetail: " + ex.Message, w)
            Log("Query: " + Query, w)
        Finally
            SQLCmd.Dispose()
            DBConnection.Close()

        End Try
    End Sub

    'Insère les données de l'ajustement de la timesheet dans la table timesheet detail
    Private Sub ExportTSToPaf()
        Dim SQLCmd As SqlClient.SqlCommand
        Dim Query As String
        Try

            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If
            Query = "EXEC Timesheet.dbo.exportTS_to_paf " + Me.TSAdjEmpId.ToString + ", " + Me.TSAdjYear + "," + Me.TSAdjWeek + ""

            SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
            'Execute the query
            SQLCmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Problem during export to paf: " + ex.Message, CType(vbCritical, MsgBoxStyle), "")
            Log("ERROR ExportTSToPaf: " + ex.Message, w)
        Finally
            SQLCmd.Dispose()
            DBConnection.Close()

        End Try
    End Sub

    'Active ou désactive le bouton AddRow en fonction des règles fonctionnelles
    Private Sub ActivateAddRowButton()
        Dim activateButton As Boolean
        Dim TimesheetDate As DateTime

        activateButton = True

        'L'utilisateur doit avoir le droit en écriture
        If Me.TSAdjustmentWrite = False Then
            activateButton = False
        End If

        'Les TS doivent avoir le statut approved
        For Each row As DataRow In Me.TSAdjTSDetails.Rows
            If CBool(row.Item("Approved")) = False Then
                activateButton = False
            Else
                'Les TS sont editable seulement si elles appartiennent aux 4 derniers mois par rapport au mois de facturation en cours
                TimesheetDate = CDate(row.Item("EndDate"))
                If TimesheetDate.AddMonths(4) < Me.currentInvoicingDate Then
                    activateButton = False
                End If
            End If

        Next


        ' Si toutes les conditions sont requises, on active le bouton
        If activateButton = True Then
            Me.ButtonAddRow.Enabled = True
        End If

    End Sub

    'Récupère la date de facturation en cours
    Private Sub getCurrentInvoicingDate()
        Dim SQLCmd As SqlClient.SqlCommand
        Dim Reader As SqlClient.SqlDataReader
        Dim Query As String

        Try

            If DBConnection.State <> ConnectionState.Open Then
                DBConnection.Open()
            End If

            Query = "SELECT  TOP (1) StartMonth " & _
                    "FROM  YearWeek AS YearWeek_1 " & _
                    "WHERE  (NOT EXISTS (SELECT 1 AS Expr1 FROM PAF_DATA.dbo.Invoices AS Invoices_2 " & _
                    "WHERE (ProjectBillingMonth = YearWeek_1.StartMonth))) OR EXISTS (SELECT 1 AS Expr1 " & _
                    "FROM PAF_DATA.dbo.Invoices AS Invoices_1 WHERE  " & _
                    "(ProjectBillingMonth = YearWeek_1.StartMonth) AND (Closed = 0) AND (YearWeek_1.StartMonth > GETDATE() - 120 ))"
            SQLCmd = New SqlClient.SqlCommand(Query, DBConnection)
            Reader = SQLCmd.ExecuteReader()
            Reader.Read()
            currentInvoicingDate = Reader.GetDateTime(0)
            Reader.Close()

        Catch ex As Exception
            MsgBox("Problem during getCurrentInvoicingDate: " + ex.Message, CType(vbCritical, MsgBoxStyle), "")
            Log("ERROR getCurrentInvoicingDate: " + ex.Message, w)
            Log("Query: " + Query, w)
        Finally
            SQLCmd.Dispose()
            DBConnection.Close()

        End Try
    End Sub

#Region "events sur les champs de saisie des heures"

    Private Sub TextBoxNTSa_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTSa.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTSu_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTSu.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTM_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTM.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTTu_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTTu.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTW_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTW.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTTh_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTTh.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxNTF_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxNTF.Leave
        Me.CalculateNTTotal()
    End Sub

    Private Sub TextBoxOTSa_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTSa.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTSu_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTSu.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTM_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTM.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTTu_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTTu.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTW_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTW.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTTh_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTTh.Leave
        Me.CalculateOTTotal()
    End Sub

    Private Sub TextBoxOTF_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxOTF.Leave
        Me.CalculateOTTotal()
    End Sub
#End Region

#End Region

End Class
