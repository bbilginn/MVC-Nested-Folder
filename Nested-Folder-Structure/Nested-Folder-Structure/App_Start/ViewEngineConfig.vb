Imports System.IO
Imports System.Web.Mvc
Imports System.Web
Public NotInheritable Class NestedView

    Public Shared Function Engine() As RazorViewEngine

        Dim vList As New List(Of String)
        Dim pList As New List(Of String)
        Dim mList As New List(Of String)

        Dim dir As New DirectoryInfo(HttpContext.Current.Server.MapPath("~/Views/"))

        ViewEngines.Engines.Clear()             'Geçmiş siliniyor. -Bu satır olmadan zaten standart yapı dahil olur. Aşağıdaki 3 satıra ihtiyaç olmaz.
        vList.Add("~/Views/{1}/{0}.vbhtml")     'Standart yapı ekleniyor. -> "~/Views/Home/Index.vbhtml"      
        pList.Add("~/Views/Shared/{0}.vbhtml")  'Standart yapı ekleniyor. -> "~/Views/Shared/_Layout.vbhtml"            
        mList.Add("~/Views/Shared/{0}.vbhtml")  'Standart yapı ekleniyor. -> "~/Views/Shared/_pLogOnPartial.vbhtml"     

        For Each item As DirectoryInfo In dir.GetDirectories ' "~/Views/" klasörü taranıyor.
            vList.Add("~/Views/" + item.Name + "/{1}/{0}.vbhtml")              'Views altı klasör desenine uygun view yapısı ekleniyor. ->      "~/Views/TESTS/First/Index.vbhtml"
            pList.Add("~/Views/" + item.Name + "/_Shared/Partial/{0}.vbhtml")  'Views altı klasör desenine uygun partial yapısı ekleniyor. ->   "~/Views/TESTS/_Shared/Partial/_pLogOnPartial.vbhtml"
            mList.Add("~/Views/" + item.Name + "/_Shared/Layout/{0}.vbhtml")   'Views altı klasör desenine uygun layout yapısı ekleniyor. ->    "~/Views/TESTS/_Shared/Layout/Master1.vbhtml"
        Next

        Dim customEngine As New RazorViewEngine
        customEngine.ViewLocationFormats = vList.ToArray          'Location-Format Set ediliyor.
        customEngine.PartialViewLocationFormats = pList.ToArray   'Location-Format Set ediliyor.
        customEngine.MasterLocationFormats = mList.ToArray        'Location-Format Set ediliyor.
        Return customEngine
    End Function

End Class