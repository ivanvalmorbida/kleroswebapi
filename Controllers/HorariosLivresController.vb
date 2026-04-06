Imports System.Data.SqlClient
Imports System.Web.Http
Imports System.Web.Mvc

Namespace Controllers
    Public Class cHorariosLivres
        Public Property ID As Integer
        Public Property AgendaDataConsulta As String
        Public Property AgendaDiaSemana As String
        Public Property AgendaPeriodo As String
        Public Property AgendaHora As String
        Public Property AgendaObservacao As String

    End Class
    Public Class HorariosLivresController
        Inherits ApiController
        ' GET: api/ConvenioWeb
        Public Function GetValues(ByVal id As Integer) As List(Of cHorariosLivres)
            Dim sqlReader As SqlDataReader, strSQL As String, cn As New Conexao
            Dim r As New List(Of cHorariosLivres)
            Dim sqlPar As New SqlParameter, colPar As New Collection

            sqlPar.DbType = DbType.Int32
            sqlPar.Value = id
            sqlPar.ParameterName = "@med"
            colPar.Add(sqlPar)

            strSQL = "select id, convert(char, DATA_CONSULTA, 103) as AgendaDataConsulta
             , case when DATEpart(WEEKDAY, DATA_CONSULTA) = 2 then 'Segunda' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 3 then 'Terça'  else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 4 then 'Quarta' 
              else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 5 then 'Quinta' else case when DATEpart(WEEKDAY, DATA_CONSULTA) = 6 then 'Sexta' end end end end end as AgendaDiaSemana
             , Período as AgendaPeriodo, Hora as AgendaHora, case when STATUS = '$$$' then 'Horário para Convênio Particular' else '' end as AgendaObservacao
             --, STATUS, m.NOME as AgendaMedicoNome
             from AGENDA_CLINICA
             inner join medico m on m.CODIGO=MEDICO
             left outer join PARAMETROS_GERAIS pg on pg.COD_PARTICULAR > 0
             where medico = @med and DATA_CONSULTA >= getdate()
              and DATA_CONSULTA <= getdate() + 8
             and isnull(NOMEPACI, '') = ''
             order by DATA_CONSULTA, HORA"

            sqlReader = cn.OpenReaderWithParam(strSQL, colPar)
            While sqlReader.Read
                r.Add(New cHorariosLivres() With {
                    .ID = sqlReader("id").ToString(),
                    .AgendaDataConsulta = sqlReader("AgendaDataConsulta").ToString(),
                    .AgendaDiaSemana = sqlReader("AgendaDiaSemana").ToString(),
                    .AgendaHora = sqlReader("AgendaHora").ToString(),
                    .AgendaObservacao = sqlReader("AgendaObservacao").ToString(),
                    .AgendaPeriodo = sqlReader("AgendaPeriodo").ToString()
                })
            End While
            sqlReader.Close()
            cn.CloseConection()
            Return r
        End Function
    End Class
End Namespace