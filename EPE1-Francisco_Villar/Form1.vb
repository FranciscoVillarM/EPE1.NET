Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form1

    ' Ruta del archivo Excel
    Private Const RutaArchivo As String = "C:\Users\franc\Desktop\Visual Studio 2022\EPE1-Francisco_Villar\EPE1-Francisco_Villar\Resources\Lista.xls"

    ' Declarar variables a nivel de clase para almacenar los datos
    Private productos As New List(Of Producto)

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        ' Cargar los datos del Excel al ComboBox
        CargarDatosExcel()
        ' Llenar el ComboBox con los ID de los productos
        cmbProductos.DataSource = productos
        cmbProductos.DisplayMember = "Id"
        cmbProductos.ValueMember = "Id"
        btnCargar.Enabled = False
    End Sub

    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged

        ' Obtener el producto seleccionado
        Dim productoSeleccionado As Producto = cmbProductos.SelectedItem
        ' Mostrar los detalles del producto
        txtDescripcion.Text = productoSeleccionado.Nombre
        txtPrecio.Text = productoSeleccionado.Precio
        txtStock.Text = productoSeleccionado.Stock

    End Sub

    Private Sub CargarDatosExcel()
        ' Crear objetos Excel
        Dim excelApp As New Excel.Application
        Dim excelWorkbook As Excel.Workbook = excelApp.Workbooks.Open(RutaArchivo)
        Dim excelSheet As Excel.Worksheet = CType(excelWorkbook.Sheets(1), Excel.Worksheet)

        Dim row As Integer = 2 ' Empezar desde la segunda fila ya que la primera es encabezado
        While Not excelSheet.Cells(row, 1).Value Is Nothing
            Dim producto As New Producto
            producto.Id = CInt(excelSheet.Cells(row, 1).Value) ' Columna A: Id
            producto.Nombre = excelSheet.Cells(row, 2).Value.ToString() ' Columna B: Nombre
            producto.Precio = CInt(excelSheet.Cells(row, 6).Value) ' Columna F: Precio
            producto.Stock = CInt(excelSheet.Cells(row, 7).Value) ' Columna G: Stock
            productos.Add(producto)
            row += 1
        End While

        ' Cerrar y liberar objetos Excel
        excelWorkbook.Close(False)
        excelApp.Quit()

        ReleaseObject(excelSheet)
        ReleaseObject(excelWorkbook)
        ReleaseObject(excelApp)
    End Sub

    ' Clase para representar un producto
    Private Class Producto
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Precio As Double
        Public Property Stock As Integer
    End Class

    ' Método para liberar objetos COM
    Private Sub ReleaseObject(ByVal obj As Object)

    End Sub
End Class