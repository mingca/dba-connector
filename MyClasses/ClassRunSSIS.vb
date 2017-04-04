'Imports SSIS = Microsoft.SqlServer.Dts.Runtime
'Imports System.IO

Public Class ClassRunSSIS 'LaunchSSISPackageService

    ' LaunchPackage Method Parameters:
    ' 1. sourceType: file, sql, dts
    ' 2. sourceLocation: file system folder, (none), logical folder
    ' 3. packageName: for file system, ".dtsx" extension is appended

    'Public Function LaunchPackage( _
    '  ByVal sourceType As String, _
    '      ByVal sourceLocation As String, _
    '      ByVal packageName As String) As Integer 'DTSExecResult

    '    Dim packagePath As String
    '    Dim myPackage As SSIS.Package
    '    Dim integrationServices As New SSIS.Application

    '    ' Combine path and file name.
    '    packagePath = Path.Combine(sourceLocation, packageName)

    '    Select Case sourceType
    '        Case "file"
    '            ' Package is stored as a file.
    '            ' Add extension if not present.
    '            If String.IsNullOrEmpty(Path.GetExtension(packagePath)) Then
    '                packagePath = String.Concat(packagePath, ".dtsx")
    '            End If
    '            If File.Exists(packagePath) Then
    '                myPackage = integrationServices.LoadPackage(packagePath, Nothing)
    '            Else
    '                Throw New ApplicationException( _
    '                  "Invalid file location: " & packagePath)
    '            End If
    '        Case "sql"
    '            ' Package is stored in MSDB.
    '            ' Combine logical path and package name.
    '            If integrationServices.ExistsOnSqlServer(packagePath, ".", String.Empty, String.Empty) Then
    '                myPackage = integrationServices.LoadFromSqlServer( _
    '                  packageName, "(local)", String.Empty, String.Empty, Nothing)
    '            Else
    '                Throw New ApplicationException( _
    '                  "Invalid package name or location: " & packagePath)
    '            End If
    '        Case "dts"
    '            ' Package is managed by SSIS Package Store.
    '            ' Default logical paths are File System and MSDB.
    '            If integrationServices.ExistsOnDtsServer(packagePath, ".") Then
    '                myPackage = integrationServices.LoadFromDtsServer(packagePath, "localhost", Nothing)
    '            Else
    '                Throw New ApplicationException( _
    '                  "Invalid package name or location: " & packagePath)
    '            End If
    '        Case Else
    '            Throw New ApplicationException( _
    '              "Invalid sourceType argument: valid values are 'file', 'sql', and 'dts'.")
    '    End Select

    '    Return myPackage.Execute()

    'End Function

End Class
