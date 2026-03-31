Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class cMedicoWeb
        Public Property MedicoCodigo As Integer
        Public Property MedicoNome As String
        Public Property MedicoEspecialidade As String
    End Class

    Public Class MedicoWebController
        Inherits ApiController

        ' GET: api/ConvenioWeb
        Public Function GetValues() As List(Of cMedicoWeb)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cMedicoWeb)

            strSQL = "Select codigo as MedicoCodigo, nome as MedicoNome, isnull(EspecialidadeAgendaWeb, '') as MedicoEspecialidade
                from medico m where Tipo = 'M' and Ativo=-1 and AgendaWeb = -1 order by nome"

            sqlReader = cn.OpenReader(strSQL)
            While sqlReader.Read
                r.Add(New cMedicoWeb() With {
                    .MedicoCodigo = sqlReader("MedicoCodigo").ToString(),
                    .MedicoEspecialidade = sqlReader("MedicoEspecialidade").ToString(),
                    .MedicoNome = sqlReader("MedicoNome").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace