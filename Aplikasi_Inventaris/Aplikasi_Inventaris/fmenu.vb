Imports MySql.Data.MySqlClient

Public Class fMenu
    Dim CONN As MySqlConnection
    Dim STR As String
    Dim CD As MySqlCommand
    Dim DR As MySqlDataReader
    Dim DA As MySqlDataAdapter
    Dim DS As DataSet

    Sub koneksi()
        STR = "Server=localhost; user id=root; password=; database=dbinventaris"
        CONN = New MySqlConnection(STR)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If

    End Sub

    Private Sub BarangToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BarangToolStripMenuItem.Click
        barang.Show()
    End Sub

    Private Sub fMenu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SupplierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierToolStripMenuItem.Click
        supplier.Show()
    End Sub

    Private Sub PegawaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PegawaiToolStripMenuItem.Click
        supplier.Show()
    End Sub

    Private Sub PembelianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PembelianToolStripMenuItem.Click
        pembelian.Show()
    End Sub

    Private Sub PenjualanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenjualanToolStripMenuItem.Click
        penjualan.Show()
    End Sub
End Class