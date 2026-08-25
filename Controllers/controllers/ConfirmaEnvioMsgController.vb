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

        ' POST: api/ConfirmaEnvioMsg
        Public Sub PostValue(<FromBody()> ByVal obj As cConfirmaEnvioMsg)
            Dim cn As New Conexao
            If obj.sended Then
                'Na confirmação do envio da MSG para 48 horas
                cn.Execute("update AGENDA_CLINICA set STATUS = 'WS1' Where id=" & obj.id)

                cn.Execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                select MEDICO, DATA_CONSULTA, PERIODO, HORA, 4 evento, getdate() alterado, 
                0 funcionario, 'Novo -> WhatsAPP msg 48h' historico, 1 tipo from AGENDA_CLINICA
                where id=" & obj.id)
            Else
                'Caso de erro no envio
                cn.Execute("update AGENDA_CLINICA set STATUS = 'WX1' Where id=" & obj.id)

                cn.Execute("insert into TRILHA_AGENDA (MEDICO, Data, PERIODO, HORA, 
                EVENTO, DATA_ALTERACAO, FUNCIONARIO, HISTORICO, TipoAgenda)
                select MEDICO, DATA_CONSULTA, PERIODO, HORA, 4 evento, getdate() alterado, 
                0 funcionario, 'Novo -> WhatsAPP erro 48h Msg: " & obj.erro & "' historico, 1 tipo 
                from AGENDA_CLINICA where id=" & obj.id)
            End If
        End Sub
    End Class
End Namespace
