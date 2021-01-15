Imports MySql.Data.MySqlClient
Public Class penjualan
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

    Private Sub penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call bersih()
        Call isiCombo()
        txthari.Text = Format(Now, "dd MMM yyy")
    End Sub

    Sub isiCombo()
        Call koneksi()
        CD = New MySqlCommand("SELECT kodepelanggan from tabel_pelanggan", CONN)
        DR = CD.ExecuteReader
        ComboBox1.Items.Clear()
        Do While DR.Read
            ComboBox1.Items.Add(DR.Item(0))
        Loop
        CD.Dispose()
        DR.Close()
        CONN.Close()

    End Sub

    Sub bersih()
        txtnofak.Text = ""
        txthari.Text = ""
        ComboBox1.Text = ""
        txnamapel.Text = ""
        txttotal.Text = ""
        txtbayar.Text = ""
        DataGridView1.Rows.Clear()
    End Sub


    Sub ambilNama()
        Call koneksi()
        CD = New MySqlCommand("SELECT namapelanggan from tabel_pelanggan where kodepelanggan = '" & ComboBox1.Text & "'", CONN)
        DR = CD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            txnamapel.Text = DR.Item(0)
        End If
    End Sub


    Sub hitungItem()
        Dim cari As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            cari = cari + DataGridView1.Rows(i).Cells(3).Value
            txttotal.Text = cari

        Next

    End Sub

    Sub hitungTotal()
        Dim cari As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            cari = cari + DataGridView1.Rows(i).Cells(6).Value
            txtbayar.Text = cari
        Next

    End Sub
   

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If e.ColumnIndex = 0 Then
            DataGridView1.Rows(e.RowIndex).Cells(0).Value = UCase(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
            Call koneksi()
            CD = New MySqlCommand("SELECT * FROM tabel_barang WHERE kodebarang = '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'", CONN)
            DR = CD.ExecuteReader
            If DR.Read Then
                DataGridView1.Rows(e.RowIndex).Cells(1).Value = DR.Item("namabarang")
                DataGridView1.Rows(e.RowIndex).Cells(2).Value = DR.Item("harga")
                DataGridView1.Rows(e.RowIndex).Cells(3).Value = 0
                DataGridView1.Rows(e.RowIndex).Cells(4).Value = DR.Item("stock")
                DataGridView1.Rows(e.RowIndex).Cells(5).Value = 0
                DataGridView1.Rows(e.RowIndex).Cells(6).Value = 0
            Else
                MsgBox("Maaf, Data Obat Tidak Ditemukan", MsgBoxStyle.Exclamation, "Peringatan")
                DataGridView1.Focus()
            End If
        End If
        If e.ColumnIndex = 3 Then
            DataGridView1.Rows(e.RowIndex).Cells(5).Value =
            DataGridView1.Rows(e.RowIndex).Cells(4).Value -
            DataGridView1.Rows(e.RowIndex).Cells(3).Value
            DataGridView1.Rows(e.RowIndex).Cells(6).Value =
            DataGridView1.Rows(e.RowIndex).Cells(2).Value *
            DataGridView1.Rows(e.RowIndex).Cells(3).Value
        End If

        Call hitungTotal()
        Call hitungItem()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call bersih()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call ambilNama()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtnofak.Text = "" Then
            MsgBox("NOMOR FAKTUR BELUM DI ISI !", MsgBoxStyle.Exclamation, "Peringatan")
        Else
            Dim simpan, simpan1, ubah As String
            Call koneksi()
            simpan = "INSERT INTO tabel_jual (nofakjual,tanggal,kodepelanggan,totalitem,totalbayar) VALUE (@P1,@P2,@P3,@P4,@P5)"
            simpan1 = "INSERT INTO tabel_detailjual (nofakjual,kodebarang,jumlah) VALUE (@P6,@P7,@P8)"
            ubah = "UPDATE tabel_barang SET stock = stock-@p9 WHERE kodebarang = @p10"
            CD = CONN.CreateCommand
            With CD
                .CommandText = simpan
                .Connection = CONN
                .Parameters.Add("p1", MySqlDbType.String, 5).Value = txtnofak.Text
                .Parameters.Add("P2", MySqlDbType.DateTime).Value = Format(Now, "dd/MM/yyyy hh:mm:ss")
                .Parameters.Add("p3", MySqlDbType.String, 6).Value = ComboBox1.Text
                .Parameters.Add("p4", MySqlDbType.Int32, 11).Value = txttotal.Text
                .Parameters.Add("p5", MySqlDbType.Int32, 11).Value = txtbayar.Text
                .ExecuteNonQuery()
            End With
            For i As Integer = 0 To DataGridView1.Rows.Count - 2
                CD = CONN.CreateCommand
                With CD
                    .CommandText = simpan1
                    .Connection = CONN
                    .Parameters.Add("p6", MySqlDbType.String, 5).Value = txtnofak.Text
                    .Parameters.Add("P7", MySqlDbType.String, 10).Value = DataGridView1.Rows(i).Cells(0).Value
                    .Parameters.Add("P8", MySqlDbType.Int32).Value = DataGridView1.Rows(i).Cells(3).Value
                    .ExecuteNonQuery()
                End With
                CD = CONN.CreateCommand
                With CD
                    .CommandText = ubah
                    .Connection = CONN
                    .Parameters.Add("p9", MySqlDbType.Int32).Value = DataGridView1.Rows(i).Cells(3).Value
                    .Parameters.Add("P10", MySqlDbType.String).Value = DataGridView1.Rows(i).Cells(0).Value
                    .ExecuteNonQuery()
                End With
            Next
            CONN.Close()
            CD.Dispose()
            bersih()
        End If
       
    End Sub
End Class