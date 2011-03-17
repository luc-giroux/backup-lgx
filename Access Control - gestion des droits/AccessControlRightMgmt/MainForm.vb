Imports AccessControlRightMgmt.DataSet1

Public Class MainForm

    Friend Motif As String
    Dim RecTypeInit As Int32
    Dim CardsDTInit As CardsDataTable
    Dim CardsDTHasChanged As Boolean

    Private Sub ButtonFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFind.Click
        Me.Clear()
        FindCard()
    End Sub
    Private Sub FindCard()
        Dim z As DataSet1.CardListDataTable
        If TextBoxFindCardNo.TextLength > 0 Then
            z = MyDB.FindCards(CType(Me.TextBoxFindCardNo.Text, Integer), Me.TextBoxFindLastname.Text)
        Else
            z = MyDB.FindCards(0, Me.TextBoxFindLastname.Text)
        End If

        If IsNothing(z) OrElse z.Count = 0 Then
            MsgBox("Il n'y a aucune carte correspondant aux critères de recherches.", MsgBoxStyle.Information)
            Return
        End If
        If z.Count > 1 Then
            Dim CardListDialog As New CardList(z, Me)
            If CardListDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Return
            End If
        End If
        If z.Count = 1 Then
            Me.TextBoxCardNo.Text = CType(z.Rows(0), DataSet1.CardListRow).CardNo.ToString
        End If
        If Me.TextBoxCardNo.TextLength > 0 Then
            Dim row As DataSet1.CardListRow
            row = MyDB.GetCardDetails(Integer.Parse(Me.TextBoxCardNo.Text))
            Me.TextBoxFirstname.Text = row.FirstName.ToString
            Me.TextBoxLastname.Text = row.LastName.ToString
            Me.TextBoxFindLastname.Text = row.LastName.ToString
            Me.TextBoxEnterprise.Text = row.Enterprise.ToString
            Dim i As New ImageFormatConverter()
            Me.PictureBoxPhoto.Image = MyDB.GetPicture(Integer.Parse(Me.TextBoxCardNo.Text))
            Me.FillRights()
        End If

        'LGX On remplit le DS des évenements
        Me.EventsTableAdapter.Fill(Me.DataSet1.Events, CType(Me.TextBoxCardNo.Text, Short?))

        'LGX On remplit le DS de suivi des modification
        Me.IndivActivationChangesTableAdapter.Fill(Me.DataSet1.IndivActivationChanges, CShort(Me.TextBoxCardNo.Text))

        Me.DisplayOnSite(Me.TextBoxCardNo.Text)

        'LGX On remplit la liste des cartes de la personne
        Me.CardsTableAdapter.Fill(Me.DataSet1.Cards, CShort(TextBoxCardNo.Text))
        Me.CardsDTInit = CType(Me.DataSet1.Cards.Copy(), CardsDataTable)
        'Remplissage et activation de la combobox
        Me.InitComboBoxRecType(MyDB.getIndivRecordType(TextBoxCardNo.Text))
        Me.ComboBoxRecType.Enabled = True

        FixButton()
    End Sub

    Private AvailableGroups As DataSet1.CardGroupsDataTable
    Private AssignedGroups As DataSet1.CardGroupsDataTable
    Private Sub FillRights()
        Me.FillAvailableRights()
        Me.FillAssignedRights()
    End Sub
    Private Sub FillAvailableRights()
        If Not IsNothing(AvailableGroups) Then
            AvailableGroups.Dispose()
        End If
        Try
            AvailableGroups = MyDB.GetAvailableRights()
            Me.ListBoxAvailableRights.DataSource = Nothing
            Me.ListBoxAvailableRights.Items.Clear()
            Me.ListBoxAvailableRights.ValueMember = AvailableGroups.DoorGroupNdxColumn.ToString
            Me.ListBoxAvailableRights.DisplayMember = AvailableGroups.DoorGroupNameColumn.ToString
            Me.ListBoxAvailableRights.DataSource = AvailableGroups
        Catch ex As Exception
            MsgBox("FillAvailableRights: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
            MyDB.Log("Error", "In FillAvailableRights", ex.StackTrace)
        End Try
    End Sub ' FillAvailableRights
    Private Sub FillAssignedRights()
        If Not IsNothing(AssignedGroups) Then
            AssignedGroups.Dispose()
        End If
        Try
            AssignedGroups = MyDB.GetCardRightAssignments(Integer.Parse(Me.TextBoxCardNo.Text))
            Me.ListBoxAssignedRights.DataSource = Nothing
            Me.ListBoxAssignedRights.Items.Clear()
            Me.ListBoxAssignedRights.DataSource = AssignedGroups
            Me.ListBoxAssignedRights.DisplayMember = AssignedGroups.DoorGroupNameColumn.ToString
            Me.ListBoxAssignedRights.ValueMember = AssignedGroups.DoorGroupNdxColumn.ToString
            ' clear Available list of items already assigned to the card
            Dim rowToDelete As DataRow
            For Each row As DataSet1.CardGroupsRow In AssignedGroups
                rowToDelete = Me.AvailableGroups.Rows.Find(row.DoorGroupNdx)
                Me.AvailableGroups.Rows.Remove(rowToDelete)
            Next
            Me.AssignedGroups.AcceptChanges()
            Me.AvailableGroups.AcceptChanges()
        Catch ex As Exception
            MsgBox("FillAssignedRights: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
            MyDB.Log("Error", "in FillAssignedRights", ex.StackTrace)
        End Try
    End Sub ' FillAssignedRights
    Private Sub Clear()
        Me.TextBoxFirstname.Text = ""
        Me.TextBoxLastname.Text = ""
        Me.PictureBoxPhoto.Image = Nothing
        Me.TextBoxCardNo.Text = ""
        Me.TextBoxEnterprise.Text = ""
        Me.ListBoxAvailableRights.DataSource = Nothing
        Me.ListBoxAvailableRights.Items.Clear()
        Me.ListBoxAssignedRights.DataSource = Nothing
        Me.ListBoxAssignedRights.Items.Clear()
        Me.ButtonApply.Enabled = False
        Me.ButtonCancel.Enabled = False
    End Sub
    Private MyDB As DB

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            MyDB = New DB
            If Not MyDB.CheckIsAuthorised() Then
                MyDB.Log("Warning", "Not authorised")
                MsgBox("Vous n'avez pas les droits requis pour utiliser cette application.", MsgBoxStyle.Critical, "Erreur")
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Initialisation: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
            MyDB.Log("Error", "in MainForm_Load", ex.StackTrace)
            Me.Close()
        End Try
        FixButton()
    End Sub

    Private Sub MainForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.TextBoxFindCardNo.Focus()
    End Sub
    Private Sub TextBoxFindCardNo_KeyPressed(ByVal sender As System.Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles TextBoxFindCardNo.KeyPress
        If e.KeyChar = Chr(13) Then ' Find when <Enter> is pressed
            Me.ButtonFind_Click(Nothing, Nothing)
            Return
        End If
        If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.KeyChar = Nothing
        End If
    End Sub
    Private Sub TextBoxFindLastname_KeyPressed(ByVal sender As System.Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles TextBoxFindLastname.KeyPress
        If e.KeyChar = Chr(13) Then ' Find when <Enter> is pressed
            Me.ButtonFind_Click(Nothing, Nothing)
            Return
        End If
    End Sub

    Private Sub ButtonAddRemoveRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddRight.Click, ButtonRemoveRight.Click
        Dim TableToAdd As DataSet1.CardGroupsDataTable
        Dim TableToRemove As DataSet1.CardGroupsDataTable
        Dim SourceListBox As ListBox
        If Me.ButtonAddRight.Equals(sender) Then
            TableToAdd = Me.AssignedGroups
            TableToRemove = Me.AvailableGroups
            SourceListBox = Me.ListBoxAvailableRights
        Else
            TableToAdd = Me.AvailableGroups
            TableToRemove = Me.AssignedGroups
            SourceListBox = Me.ListBoxAssignedRights
        End If
        Try
            If Not IsNothing(SourceListBox.SelectedValue) Then
                Dim rowToDelete As DataRow
                Dim rowToAdd As DataSet1.CardGroupsRow
                rowToDelete = TableToRemove.Rows.Find(SourceListBox.SelectedValue)
                rowToAdd = TableToAdd.NewCardGroupsRow
                rowToAdd.SetField(0, rowToDelete(0)) ' TenantNdx
                rowToAdd.SetField(1, rowToDelete(1)) ' DoorGroupNdx
                rowToAdd.SetField(2, rowToDelete(2)) ' DoorGroupName
                rowToAdd.SetField(3, rowToDelete(3)) ' PriorityOrder
                rowToAdd.SetField(4, rowToDelete(4)) ' DoorSchedule
                rowToDelete.Delete()
                TableToAdd.Rows.Add(rowToAdd)
            End If
            MyDebug()
            FixButton()
        Catch ex As Exception
            MsgBox("ButtonAddRemoveRight_Click: " & ex.Message, MsgBoxStyle.Critical, "Erreur")
            MyDB.Log("Error", "in ButtonAddRemoveRight_Click", ex.StackTrace)
        End Try
    End Sub ' ButtonAddRemoveRight_Click
    Private Sub MyDebug()
        Debug.Print(vbNewLine)
        If Not IsNothing(Me.AssignedGroups.GetChanges()) Then
            For Each row As DataSet1.CardGroupsRow In Me.AssignedGroups.GetChanges().Rows
                If row.RowState = DataRowState.Deleted Then
                    Debug.Print(DataRowState.GetName(GetType(DataRowState), row.RowState) + " - " + row.Item("DoorGroupName", DataRowVersion.Original).ToString)
                Else
                    Debug.Print(DataRowState.GetName(GetType(DataRowState), row.RowState) + " - " + row.DoorGroupName)
                End If
            Next
        Else
            Debug.Print("No changes")
        End If
    End Sub
    Private Sub FixButton()
        If (Not IsNothing(AssignedGroups) AndAlso Not IsNothing(AssignedGroups.GetChanges)) Or
           (CardsDTHasChanged) Or
           (ComboBoxRecType.SelectedIndex <> -1 And ComboBoxRecType.SelectedIndex <> Me.RecTypeInit) Then
            Me.ButtonApply.Enabled = True
            Me.ButtonCancel.Enabled = True
            Me.ButtonFind.Enabled = False
            Me.TextBoxFindCardNo.Enabled = False
            Me.TextBoxFindLastname.Enabled = False
        Else
            Me.ButtonApply.Enabled = False
            Me.ButtonCancel.Enabled = False
            Me.ButtonFind.Enabled = True
            Me.TextBoxFindCardNo.Enabled = True
            Me.TextBoxFindLastname.Enabled = True
        End If
    End Sub
    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.FindCard()
        FixButton()
    End Sub

    Private Sub ButtonApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonApply.Click

        'Si le baddge a été désactivé, on demande le motif et on sauvegarde en base
        If IndivHasBeenDisabled() Then
            Dim MotifDialog As New Motif(Me)
            If MotifDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                MyDB.SaveIndivActivationChangesReason(Me.TextBoxCardNo.Text, Me.GetModificationType(), Me.Motif)
                Me.Motif = String.Empty
                'REfresh DS
                Me.IndivActivationChangesTableAdapter.Fill(Me.DataSet1.IndivActivationChanges, CShort(Me.TextBoxCardNo.Text))
            End If
        Else
            'On sauvegarde le type de modif en base
            MyDB.SaveIndivActivationChangesReason(Me.TextBoxCardNo.Text, Me.GetModificationType(), "")
            Me.IndivActivationChangesTableAdapter.Fill(Me.DataSet1.IndivActivationChanges, CShort(Me.TextBoxCardNo.Text))
        End If

        MyDB.ApplyGroupChanges(Me.AssignedGroups, Integer.Parse(Me.TextBoxCardNo.Text))
        Me.AssignedGroups.AcceptChanges()
        Me.FillRights()
        'LGX Maj de l'activation/desactivation des cartes de l'individu en base
        Me.CardsTableAdapter.Update(Me.DataSet1.Cards)
        'Maj du statut de l'individu
        MyDB.updateIndivRecordType(Me.TextBoxCardNo.Text, Me.ComboBoxRecType.SelectedIndex)
        ' maj du cache Inet pour impacter toutes portes
        MyDB.ApplyChangesToInetCache(Me.DataSet1.Cards, Me.TextBoxCardNo.Text)
        FindCard()
    End Sub

    'Return TRUE if the individual or one of his cards have changed from enabled to disabled
    Private Function IndivHasBeenDisabled() As Boolean

        Dim retour As Boolean
        retour = False

        'IF Rectype was permanent or temporaire and now disabled, the individual has been unactivated
        If Me.ComboBoxRecType.SelectedIndex = 2 And (Me.RecTypeInit = 0 Or Me.RecTypeInit = 1) Then
            Return True
        End If

        ' IF individual has not been unactivated, but only his cards 
        If Not IsNothing(Me.DataSet1.Cards.GetChanges()) Then
            For Each row As DataSet1.CardsRow In Me.DataSet1.Cards.GetChanges().Rows
                For Each rowInit As DataSet1.CardsRow In Me.CardsDTInit.Rows
                    'We compare the values changed for a same card
                    If rowInit.MiFareCardNumber = row.MiFareCardNumber Then
                        'The card was active and now unactive
                        If (Not rowInit.Disabled) And (row.Disabled) Then
                            retour = True
                        End If
                    End If
                Next
            Next
        End If

        Return retour

    End Function

    'Return TRUE if the individual or one of his cards have changed from disabled to enabled
    Private Function IndivHasBeenActivated() As Boolean

        Dim retour As Boolean
        retour = False

        'IF Rectype was disabled and now permanent or temporaire, the individual has been unactivated
        If Me.ComboBoxRecType.SelectedIndex <> 2 And (Me.RecTypeInit = 2) Then
            Return True
        End If

        ' IF individual has not been activated, but only his cards 
        If Not IsNothing(Me.DataSet1.Cards.GetChanges()) Then
            For Each row As DataSet1.CardsRow In Me.DataSet1.Cards.GetChanges().Rows
                For Each rowInit As DataSet1.CardsRow In Me.CardsDTInit.Rows
                    'We compare the values changed for a same card
                    If rowInit.MiFareCardNumber = row.MiFareCardNumber Then
                        'The card was active and not unactive
                        If (rowInit.Disabled) And (Not row.Disabled) Then
                            retour = True
                        End If
                    End If
                Next
            Next
        End If

        Return retour

    End Function

    'Returns the type of modification : IndivActivationChanges, activation, group assignment
    Private Function GetModificationType() As String
        If Me.IndivHasBeenDisabled() Then
            Return "DESACTIVATION"
        End If
        If Me.IndivHasBeenActivated() Then
            Return "ACTIVATION"
        End If
        If Not IsNothing(AssignedGroups) AndAlso Not IsNothing(AssignedGroups.GetChanges) Then
            Return "GROUPES"
        Else
            Return ""
        End If
    End Function

    Private Sub ListBoxAssignedRights_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxAssignedRights.DoubleClick, ListBoxAvailableRights.DoubleClick
        If ListBoxAssignedRights.Equals(sender) Then
            ButtonAddRemoveRight_Click(ButtonRemoveRight, Nothing)
        Else
            ButtonAddRemoveRight_Click(ButtonAddRight, Nothing)
        End If
    End Sub

    Private Sub Main_Form_Close(ByVal sender As Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not IsNothing(AssignedGroups) Then
            If Not IsNothing(AssignedGroups.GetChanges) Then
                If MsgBox("Désirez-vous sauvegarder vos changements?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
                    ButtonApply_Click(Nothing, Nothing)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'Seulement si on clic sur une checkbox
        If Me.DataGridView1.SelectedCells(0).ColumnIndex = 1 Then
            Me.CardsDTHasChanged = False
            Me.DataGridView1.EndEdit()
            For Each row As DataSet1.CardsRow In Me.DataSet1.Cards.Rows
                For Each rowInit As DataSet1.CardsRow In Me.CardsDTInit.Rows
                    'We compare the values changed for a same card
                    If rowInit.MiFareCardNumber = row.MiFareCardNumber Then
                        'The card was active and not unactive
                        If rowInit.Disabled <> row.Disabled Then
                            Me.CardsDTHasChanged = True
                        End If
                    End If
                Next
            Next

            Me.FixButton()
        End If
    End Sub

    Private Sub InitComboBoxRecType(ByVal currentRecType As String)
        If currentRecType = "0" Then
            Me.ComboBoxRecType.SelectedIndex = 0
            Me.RecTypeInit = 0
        ElseIf currentRecType = "1" Then
            Me.ComboBoxRecType.SelectedIndex = 1
            Me.RecTypeInit = 1
        Else
            Me.ComboBoxRecType.SelectedIndex = 2
            Me.RecTypeInit = 2
        End If

    End Sub

    Private Sub ComboBoxRecType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxRecType.SelectedIndexChanged
        FixButton()
    End Sub

    'Display if The individual is on site or off site
    Private Sub DisplayOnSite(ByVal IndivId As String)
        Me.LabelOnSite.Visible = False
        Me.LabelOffSite.Visible = False
        Me.PictureBoxOnSite.Visible = False

        If MyDB.IsIndividualOnSite(IndivId) Then
            Me.PictureBoxOnSite.Visible = True
            Me.PictureBoxOnSite.Image = My.Resources._in
            Me.LabelOnSite.Visible = True
        Else
            Me.PictureBoxOnSite.Visible = True
            Me.PictureBoxOnSite.Image = My.Resources.out
            Me.LabelOffSite.Visible = True
        End If
    End Sub

End Class
