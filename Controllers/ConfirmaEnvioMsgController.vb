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
            Dim cn As New Conexao

                        '-- Na confirmação do envio da MSG para 48 horas
                cn.execute("update AGENDA_CLINICA set STATUS = 'WS1' Where id=" & obj.id)

                cn.execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                'select MEDICO, Data, PERIODO, HORA, 999 evento, getdate() alterado, 
                0 funcionario, 'WhatsAPP Enviou Msg '"& obj.sended &" historico, 1 tipo from agenda
                where id=" & obj.id)
                
                'Caso de erro no envio
                cn.execute("update AGENDA_CLINICA set STATUS = 'WS1' Where id=" & obj.id)
                cn.execute("insert into TRILHA_AGENDA (MEDICO, DATA, PERIODO, HORA, EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                    values (MEDICO, DATA, PERIODO, HORA, 4,  getdate(),  0 , 'Anterior -> ' & StatusAnterior & 'Novo -> WhatsAPP erro 48h Msg: ' & MsgErroRetorno, 1)"

        End Sub

        ' PUT: api/ConfirmaEnvioMsg/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/ConfirmaEnvioMsg/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace
