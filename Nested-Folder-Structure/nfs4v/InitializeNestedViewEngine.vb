Imports System.IO
Imports System.Web.Mvc
Imports System.Web

Public NotInheritable Class NestedViewEngine

    Public Sub New()
        ViewEngines.Engines.Clear()
    End Sub

    Public Shared Function Engine() As RazorViewEngine

        Dim vList As New List(Of String)
        Dim pList As New List(Of String)
        Dim mList As New List(Of String)

        Dim dir As New DirectoryInfo(HttpContext.Current.Server.MapPath("~/Views/"))

        ViewEngines.Engines.Clear()             'Geçmiş siliniyor. -Bu satır olmadan zaten standart yapı dahil olur. Aşağıdaki 3 satıra ihtiyaç olmaz.
        vList.Add("~/Views/{1}/{0}.vbhtml")     'Standart yapı tanımlanıyor. "~/Views/HomeController/Index.vbhtml"      
        pList.Add("~/Views/Shared/{0}.vbhtml")  'Standart yapı tanımlanıyor. "~/Views/Shared/_Layout.vbhtml"            
        mList.Add("~/Views/Shared/{0}.vbhtml")  'Standart yapı tanımlanıyor. "~/Views/Shared/_pLogOnPartial.vbhtml"     

        For Each item As DirectoryInfo In dir.GetDirectories
            vList.Add("~/Views/" + item.Name + "/{1}/{0}.vbhtml")
            pList.Add("~/Views/" + item.Name + "/_Shared/Partial/{0}.vbhtml")
            mList.Add("~/Views/" + item.Name + "/_Shared/Layout/{0}.vbhtml")
        Next

        Dim customEngine As New RazorViewEngine
        customEngine.ViewLocationFormats = vList.ToArray
        customEngine.PartialViewLocationFormats = pList.ToArray
        customEngine.MasterLocationFormats = mList.ToArray
        Return customEngine
    End Function

End Class