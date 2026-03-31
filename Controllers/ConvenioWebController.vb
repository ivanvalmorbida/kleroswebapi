Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class cConvenioWeb
        Public Property ConvenioCodigo As Integer
        Public Property ConvenioNome As String
    End Class

    Public Class ConvenioWebController
        Inherits ApiController

        ' GET: api/ConvenioWeb
        Public Function GetValues() As List(Of cConvenioWeb)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cConvenioWeb)

            strSQL = "select codigo as ConvenioCodigo, nome as ConvenioNome 
                from convenio where ATIVO = 1 and AgendaWeb = 1 order by nome"

            sqlReader = cn.OpenReader(strSQL)
            While sqlReader.Read
                r.Add(New cConvenioWeb() With {
                    .ConvenioCodigo = sqlReader("ConvenioCodigo").ToString(),
                    .ConvenioNome = sqlReader("ConvenioNome").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace