Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class cPacientePainelChamada
        Public Property Paciente As String
        Public Property Sala As String
    End Class

    Public Class PacienteChamadaController
        Inherits ApiController

        ' GET: api/PacienteChamada
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/PacienteChamada/5
        Public Function GetValue(ByVal id As Integer) As List(Of cPacientePainelChamada)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim sqlPar As New SqlParameter, colPar As New Collection
            Dim r As New List(Of cPacientePainelChamada)()

            'If Now.Minute Mod 2 = 1 Then
            '    r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala X",
            '    .Paciente = "Paciente X"
            '})
            'End If
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 2",
            '    .Paciente = "Paciente 01"
            '})
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 2",
            '    .Paciente = "Paciente 02"
            '})
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 1",
            '    .Paciente = "Paciente 03"
            '})
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 3",
            '    .Paciente = "Paciente 04"
            '})
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 2",
            '    .Paciente = "Paciente 05"
            '})
            'r.Add(New cPacientePainelChamada() With {
            '    .Sala = "Sala 1",
            '    .Paciente = "Paciente 06"
            '})
            sqlPar.DbType = DbType.Int32
            sqlPar.Value = id
            sqlPar.ParameterName = "@re"
            colPar.Add(sqlPar)

            strSQL = "select top 5 cp.Medico, m.NOME as MedicoNome, cp.Data_Consulta, cp.Hora, cp.Periodo, cp.Paciente
            , case when isnull(cp.PACIENTE, 0) > 0 then case when p.NomeSocial > '' then p.NomeSocial else p.NOME end else cp.NomePaci end as PacienteNome
            , cp.DataChamada, sa.nome as SalaAtendimento
            from ChamarPaciente cp
            inner join medico m on m.codigo=cp.MEDICO
            left outer join paciente p on p.codigo = cp.PACIENTE
            inner join SalaAtendimento sa on sa.codigo=m.SalaAtendimento
            where cast(cp.DataChamada as date) = cast(getdate() as date) and m.SalaProcedimento=@re
            order by DataChamada desc"

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            While sqlReader.Read
                r.Add(New cPacientePainelChamada() With {
                    .Sala = sqlReader("SalaAtendimento").ToString(),
                    .Paciente = sqlReader("PacienteNome").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function

        ' POST: api/PacienteChamada
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/PacienteChamada/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/PacienteChamada/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace