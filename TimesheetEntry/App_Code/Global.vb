Imports Microsoft.VisualBasic

<Serializable()> Public Structure EmployeeInfoStruct
    Dim EmployeeLogin As String
    Dim EmployeeName As String
    Dim Company As String ' should not be required
    Dim EmployeeNumber As String ' should not be required
    Dim EmployeeID As Integer
    Dim DisciplineCode As String
    Dim DisciplineDescription As String
    Dim StandardWorkingHours As Integer
    Dim DemobilisationDate As DateTime
End Structure
Public Class Utils
    Public Sub New()
        ' do nothing
    End Sub
    Public Shared Function GetEmployeeInfo(ByVal LoginName As String, ByVal ConnectionString As String) As EmployeeInfoStruct
        ' Function to retrieve the employee information from PAF_DATA employee record
        ' if the current login is not found, the function return Nothing
        Dim sqlCmd As Data.SqlClient.SqlCommand
        Dim SQLText As String = "SELECT EmployeeId, CompanyID, EmployeeNumber, RTRIM(LastName) + ', '+ RTRIM(Firstname) As Name, DisciplineCode, DisciplineDescription, StandardWorkingHours, DemobilisationDate FROM employeeInfo WHERE EmployeeLogin = @LoginName"
        Dim Reader As Data.SqlClient.SqlDataReader
        Dim SQLConnection As Data.SqlClient.SqlConnection

        SQLConnection = New Data.SqlClient.SqlConnection(ConnectionString)
        SQLConnection.Open()

        sqlCmd = New Data.SqlClient.SqlCommand(SQLText, SQLConnection)

        ' test if calling parameter is decent
        If LoginName.Length = 0 Then
            Return Nothing
        End If

        Try
            sqlCmd.Parameters.Add(New Data.SqlClient.SqlParameter("@LoginName", LoginName))
            Reader = sqlCmd.ExecuteReader
            If Not Reader.HasRows Then ' i.e. login name not defined
                ' throw an exception to close the reader
                Throw New Exception("No record found in PAF")
            End If
            Reader.Read()
            GetEmployeeInfo = New EmployeeInfoStruct
            GetEmployeeInfo.EmployeeLogin = LoginName

            If Reader.IsDBNull(0) Then ' EmployeeID
                ' throw an exception to close the reader
                Throw New Exception("No employeeID defined in PAF")
            End If
            GetEmployeeInfo.EmployeeID = Reader.GetInt32(0)

            If Reader.IsDBNull(1) Then ' CompanyID
                ' throw an exception to close the reader
                Throw New Exception("No CompanyID defined in PAF")
            End If
            GetEmployeeInfo.Company = Reader.GetString(1)

            If Reader.IsDBNull(2) Then ' EmployeeNumber
                ' throw an exception to close the reader
                Throw New Exception("No EmployeeNumber defined in PAF")
            End If
            GetEmployeeInfo.EmployeeNumber = Reader.GetString(2).Trim
            If Reader.IsDBNull(3) Then ' EmployeeName
                GetEmployeeInfo.EmployeeName = "Name"
            Else
                GetEmployeeInfo.EmployeeName = Reader.GetString(3)
            End If

            If Reader.IsDBNull(4) Then ' DisciplineCode
                GetEmployeeInfo.DisciplineCode = "N/A"
            Else
                GetEmployeeInfo.DisciplineCode = Reader.GetString(4)
            End If
            If Reader.IsDBNull(5) Then ' DisciplineDescription
                GetEmployeeInfo.DisciplineDescription = "N/A"
            Else
                GetEmployeeInfo.DisciplineDescription = Reader.GetString(5)
            End If

            If Reader.IsDBNull(6) Then ' StandardWorkingHours
                GetEmployeeInfo.StandardWorkingHours = 45
            Else
                GetEmployeeInfo.StandardWorkingHours = Reader.GetInt32(6)
            End If

            If Reader.IsDBNull(7) Then ' DemobilisationDate
                GetEmployeeInfo.DemobilisationDate = Now()
            Else
                GetEmployeeInfo.DemobilisationDate = Reader.GetDateTime(7)
            End If


            Reader.Close()
            Return GetEmployeeInfo
        Catch ex As Exception
            If Not IsNothing(Reader) Then
                Reader.Close()
            End If
            Throw (ex) ' to be managed by the caller
        End Try
        Return Nothing
    End Function
End Class