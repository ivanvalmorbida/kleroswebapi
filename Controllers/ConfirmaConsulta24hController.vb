Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class ConfirmaConsulta24hController
        Inherits ApiController

        ' GET: api/ConfirmaConsulta48h
        Public Function GetValues() As List(Of cConfirmaConsulta)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cConfirmaConsulta)()

            strSQL = strSQLConfirmaConsulta.Replace("{d}", "1")

            sqlReader = cn.OpenReader(strSQL)
            While sqlReader.Read
                r.Add(New cConfirmaConsulta() With {
                    .CELULAR = sqlReader("CELULAR").ToString(),
                    .CLINICA = sqlReader("CLINICA").ToString(),
                    .DATA_CONSULTA = sqlReader("DATA_CONSULTA").ToString(),
                    .ENDERECO = sqlReader("ENDERECO").ToString(),
                    .HORA = sqlReader("HORA").ToString(),
                    .ID = sqlReader("ID").ToString(),
                    .MEDICONOME = sqlReader("MEDICONOME").ToString(),
                    .NOMEPACI = sqlReader("NOMEPACI").ToString(),
                    .TIPOATENDIMENTO = sqlReader("TIPOATENDIMENTO").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace