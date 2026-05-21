Imports System.Data.SqlClient
Imports System.Web.Http

Namespace Controllers
    Public Class cMedicoWeb
        Public Property MedicoCodigo As Integer
        Public Property MedicoNome As String
        Public Property MedicoEspecialidade As String
    End Class

    Public Class MedicoWebController
        Inherits ApiController

        ' GET: api/MedicoWeb
        Public Function GetValues(Nasc As Date, Conv As Int32) As List(Of cMedicoWeb)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cMedicoWeb)

            Dim i As Integer = DateDiff(DateInterval.Year, Nasc, Now)

            strSQL = $"Select codigo as MedicoCodigo, NomeAgendaWeb as MedicoNome, isnull(EspecialidadeAgendaWeb, '') as MedicoEspecialidade
                from medico m where Tipo='M' 
                    and (isnull((SELECT sum(QUANTIDADE) From CONSULTAS_CONVENIO 
                        Where MEDICO=m.codigo and TURNO=1 and CONVENIO={Conv}),99)
                    + isnull((SELECT sum(QUANTIDADE) From CONSULTAS_CONVENIO 
                        Where MEDICO=m.codigo and TURNO=2 and CONVENIO={Conv}),99))>0
                and {i}>=IdadeMinimaPaciente and Ativo=-1 and AgendaWeb = -1 order by nome"

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