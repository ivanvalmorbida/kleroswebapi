Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class cConfirmaEnvioMsg
        Public Property id As Integer
        Public Property sended As Boolean
        Public Property erro As String
        Public Property type As String
    End Class

    Public Class ConfirmaEnvioMsgController
        Inherits ApiController

        ' GET: api/ConfirmaEnvioMsg
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/ConfirmaEnvioMsg/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function

        ' POST: api/ConfirmaEnvioMsg
        Public Sub PostValue(<FromBody()> ByVal obj As cConfirmaEnvioMsg)
            Dim strSQL As String, cn As New Conexao

            '-- Na confirmação do envio da MSG para 48 horas
            'update AGENDA_CLINICA set STATUS = 'WS1' where id = id

            'insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
            'values(MEDICO, Data, PERIODO, HORA, 4, getdate(), 999, 'Anterior -> ' & StatusAnterior & 'Novo -> WhatsAPP msg 48h', 1)

        End Sub

        ' PUT: api/ConfirmaEnvioMsg/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/ConfirmaEnvioMsg/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace