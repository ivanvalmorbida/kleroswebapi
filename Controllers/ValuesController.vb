Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Http
Imports KlerosWebApi.Controllers

Public Class ValuesController
    Inherits ApiController

    ' GET api/values
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function

    ' GET api/values/5
    Public Function GetValue(ByVal id As Integer) As String
        Dim arr2(), strKey As String, coll As NameValueCollection

        coll = HttpContext.Current.Request.Headers
        arr2 = coll.GetValues("keyXXX")
        If Not IsNothing(arr2) Then strKey = arr2(0) Else strKey = ""

        If strKey = "Ivanluis###" Then
            Return "ValueX"
        Else
            Throw New HttpResponseException(Request.CreateResponse(
            HttpStatusCode.Unauthorized,
            "Usuário não autorizado"))
        End If
    End Function

    ' POST api/values
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/values/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/values/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class
