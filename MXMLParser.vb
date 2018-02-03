Imports System.Xml
Public Class MXMLParser
    Dim xmlNodes As List(Of xmlnode)
    Public xmlString As String
    Class attribute
        Dim name As String
        Dim value As String
        Public Function getName() As String
            getName = name
        End Function
        Public Function getValue() As String
            getValue = value
        End Function
        Public Sub setName(nm As String)
            name = nm
        End Sub
        Public Sub setValue(v As String)
            value = v
        End Sub
    End Class

    Class xmlnode
        Dim elementName As String = ""
        Dim elementText As String = ""
        Public attributtes As New ArrayList

        Public Function getName() As String
            getName = elementName
        End Function
        Public Function getText() As String
            getText = elementText
        End Function
        Public Function getAttribute(nam As String) As attribute
            For Each s As attribute In attributtes
                If (s.getName = nam) Then
                    getAttribute = s
                    Exit For
                End If
            Next
            getAttribute = Nothing
        End Function
        Public Sub setName(nam As String)
            elementName = nam
        End Sub
        Public Sub setText(nam As String)
            elementText = nam
        End Sub
        Public Sub setAttributes(nam As ArrayList)
            attributtes = nam
        End Sub
    End Class
    Public Function read(url As String) As ArrayList
        Dim nodes As New ArrayList
        Dim reader As XmlTextReader = New XmlTextReader(url)
        Dim xm As xmlnode = New xmlnode
        xmlString = ""
        Do While (reader.Read())
            Select Case reader.NodeType
                Case XmlNodeType.Element 'Display beginning of element.
                    xm.setName(reader.Name)
                    xmlString += "<" + reader.Name
                    Dim attrs As New ArrayList
                    If reader.HasAttributes Then 'If attributes exist
                        While reader.MoveToNextAttribute()
                            Dim att As New attribute
                            att.setName(reader.Name)
                            xmlString += " " + reader.Name + "='" + reader.Value + "'"
                            att.setValue(reader.Value)
                            attrs.Add(att)
                        End While
                    End If
                    xmlString += ">"
                    xm.setAttributes(attrs)
                Case XmlNodeType.Text
                    xmlString += reader.Value
                    xm.setText(reader.Value)
                Case XmlNodeType.CDATA
                    xmlString += "<![CDATA[" + reader.Value + "]]>"
                    xm.setText(reader.Value)
                Case XmlNodeType.EndElement
                    xmlString += "</" + reader.Name + ">"
                    nodes.Add(xm)
                    xm = New xmlnode
            End Select
        Loop
        read = nodes
    End Function
End Class
