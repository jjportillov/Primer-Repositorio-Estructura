/*El codigo se escribio en excelo vba"
ESTABLECIENDO VARIABLES GLOBALES (INTRO)

'Macro que lo controla todo (pensar en ella como si fuera el Main())

'ESTABLECIENDO DIRECTORIOS (INICIO)

'PROCESANDO LA INFORMACIÓN (NUDO)

'GUARDADNO LOS RESULTADOS (DESENLACE)
ESTABLECIENDO VARIABLES GLOBALES (INTRO)
Dim FolderSeleccionado As String

'Macro que lo controla todo (pensar en ella como si fuera el Main())
Sub Principal()
    Call EscogerFolder
End Sub

'ESTABLECIENDO DIRECTORIOS (INICIO)
'Pedirle al usuario que indique dónde se encuentra la carpeta con los dos archivos Jsonl base
Sub EscogerFolder()
    With Application.FileDialog(msoFileDialogFolderPicker)
        .Title = "Seleccionar carpeta"
        .ButtonName = "Seleccionar"
        If .Show = -1 Then
            FolderSeleccionado = .SelectedItems(1)
            MsgBox (FolderSeleccionado)
        End If
    End With
End Sub

'PROCESANDO LA INFORMACIÓN (NUDO)

'GUARDADNO LOS RESULTADOS (DESENLACE)
Segundo avance:
'ESTABLECIENDO VARIABLES GLOBALES (INTRO)
Dim FolderSeleccionado As String
Dim NombreDelArchivo As Variant
Dim Archivo As Object

'Macro que lo controla todo (pensar en ella como si fuera el Main())
Sub Principal()
    Call EscogerFolder
    Call EliminarArchivosDeTexto
End Sub

'ESTABLECIENDO DIRECTORIOS (INICIO)
'Pedirle al usuario que indique dónde se encuentra la carpeta con los dos archivos Jsonl base
Sub EscogerFolder()
    With Application.FileDialog(msoFileDialogFolderPicker)
        .Title = "Seleccionar carpeta"
        .ButtonName = "Seleccionar"
        If .Show = -1 Then
            FolderSeleccionado = .SelectedItems(1)
            'MsgBox (FolderSeleccionado)
        End If
    End With
End Sub
Sub EliminarArchivosDeTexto()
    NombreDelArchivo = Dir(FolderSeleccionado & "\*.txt")
    While NombreDelArchivo <> ""
        'MsgBox (NombreDelArchivo)
        Kill (FolderSeleccionado & "\" & NombreDelArchivo)
        NombreDelArchivo = Dir
    Wend
End Sub
Sub Duplicar_Y_RenombrarArchivos()
    
End Sub

'PROCESANDO LA INFORMACIÓN (NUDO)

'GUARDADNO LOS RESULTADOS (DESENLACE)*/
Console.WriteLine("hola");