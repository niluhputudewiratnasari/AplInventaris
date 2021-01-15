Imports MySql.Data.MySqlClient
Public Class barang
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
    Sub tampil()
        Try
            DA = New MySqlDataAdapter("select * from tabel_barang", CONN)
            DS = New DataSet
            DA.Fill(DS, "barang")
            DataGridView1.DataSource = (DS.Tables("barang"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub aturdgbbarang()
        Try
            DataGridView1.Columns(0).HeaderText = "KODE BARANG"
            DataGridView1.Columns(0).Width = 100

            DataGridView1.Columns(1).HeaderText = "NAMA BARANG"
            DataGridView1.Columns(1).Width = 300

            DataGridView1.Columns(2).HeaderText = "HARGA"
            DataGridView1.Columns(2).Width = 100

            DataGridView1.Columns(3).HeaderText = "STOK"
            DataGridView1.Columns(3).Width = 300
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub bersihtextbox()
        Txtkode.Text = ""
        txtbarang.Text = ""
        txtharga.Text = ""
        txtstok.Text = ""
    End Sub

    Sub insertbarang()
        Try
            If Txtkode.Text = "" Or txtbarang.Text = "" Or txtharga.Text = "" Or txtstok.Text = "" Then
                MsgBox("Data Belum Lengkap", MsgBoxStyle.Exclamation)
            Else
                Dim query As String = "Insert into tabel_barang values ('" & Txtkode.Text & "','" & txtbarang.Text & "','" & txtharga.Text & "','" & txtstok.Text & "');"
                CD = New MySqlCommand(query, CONN)
                CD.ExecuteNonQuery()
                Call tampil()
                MsgBox("Berhasil Disimpan")
                Call bersihtextbox()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub deleteDataBarang()
        Dim id As String
        id = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Dim Query As String = "DELETE from tabel_barang where kodebarang ='" & id & "'"
        CD = New MySqlCommand(Query, CONN)
        CD.ExecuteNonQuery()
        MsgBox("Data berhasil dihapus")
        Call tampil()
    End Sub

    Sub muatBarang()
        With DataGridView1
            Txtkode.Text = .Item(0, .CurrentRow.Index).Value
            txtbarang.Text = .Item(1, .CurrentRow.Index).Value
            txtharga.Text = .Item(2, .CurrentRow.Index).Value
            txtstok.Text = .Item(3, .CurrentRow.Index).Value

        End With
    End Sub

    Sub cari()
        Using CD = New MySqlCommand("SELECT * from tabel_barang where namabarang LIKE '%" & Textcari.Text & "%'", CONN)
            Using DR As MySqlDataReader = CD.ExecuteReader
                Using Tabel As New DataTable
                    Tabel.Load(DR)
                    If Tabel.Rows.Count = 0 Then
                        DataGridView1.DataSource = Nothing
                    Else
                        DataGridView1.DataSource = Tabel
                        Call aturdgbbarang()
                    End If
                End Using


            End Using


        End Using

    End Sub

    Sub updateBarang()
        Try
            If Txtkode.Text = "" Or txtbarang.Text = "" Or txtharga.Text = "" Or txtstok.Text = "" Then
                MsgBox("Data belum lengkap!", MsgBoxStyle.Exclamation)
            Else
                Dim Query As String = "UPDATE tabel_barang SET namabarang = '" & txtbarang.Text & "', harga = '" & txtharga.Text & "', stock = '" & txtstok.Text & "' WHERE kodebarang = '" & Txtkode.Text & "'"
                CD = New MySqlCommand(Query, CONN)
                CD.ExecuteNonQuery()
                Call tampil()
                Call bersihtextbox()
                MsgBox("Data berhasil di update")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub barang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call aturdgbbarang()
        Call tampil()

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call insertbarang()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim result As DialogResult = MessageBox.Show("Apakah anda yakin ?", "Informasi",
        '                                             MessageBoxButtons.YesNo)
        'If (result = DialogResult.Yes) Then
        'Call deleteDataBarang()
        'End If
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call muatBarang()
    End Sub

    Private Sub Textcari_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Textcari.KeyUp
        Call cari()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Call updateBarang()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Call bersihtextbox()
    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah anda yakin ?", "Informasi", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Call deleteDataBarang()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class