Imports MySql.Data.MySqlClient
Public Class pelanggan

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
            DA = New MySqlDataAdapter("select * from tabel_pelanggan", CONN)
            DS = New DataSet
            DA.Fill(DS, "pelanggan")
            DataGridView1.DataSource = (DS.Tables("pelanggan"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub aturdgvpelanggan()
        Try
            DataGridView1.Columns(0).HeaderText = "KODE PELANGGAN"
            DataGridView1.Columns(0).Width = 100

            DataGridView1.Columns(1).HeaderText = "NAMA PELANGGAN"
            DataGridView1.Columns(1).Width = 300

            DataGridView1.Columns(2).HeaderText = "ALAMAT"
            DataGridView1.Columns(2).Width = 100

            DataGridView1.Columns(3).HeaderText = "TELP"
            DataGridView1.Columns(3).Width = 300
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub bersihtextbox()
        Txtkodeplg.Text = ""
        txtplg.Text = ""
        txtalamat.Text = ""
        txttelp.Text = ""
    End Sub

    Sub insertpelanggan()
        Try
            If Txtkodeplg.Text = "" Or txtplg.Text = "" Or txtalamat.Text = "" Or txttelp.Text = "" Then
                MsgBox("Data Belum Lengkap", MsgBoxStyle.Exclamation)
            Else
                Dim query As String = "Insert into tabel_pelanggan values ('" & Txtkodeplg.Text & "','" & txtplg.Text & "','" & txtalamat.Text & "','" & txttelp.Text & "');"
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

    Sub deleteDatapelanggan()
        Dim id As String
        id = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Dim Query As String = "DELETE from tabel_pelanggan where kodepelanggan ='" & id & "'"
        CD = New MySqlCommand(Query, CONN)
        CD.ExecuteNonQuery()
        MsgBox("Data berhasil dihapus")
        Call tampil()
    End Sub

    Sub muatpelanggan()
        With DataGridView1
            Txtkodeplg.Text = .Item(0, .CurrentRow.Index).Value
            txtplg.Text = .Item(1, .CurrentRow.Index).Value
            txtalamat.Text = .Item(2, .CurrentRow.Index).Value
            txttelp.Text = .Item(3, .CurrentRow.Index).Value

        End With
    End Sub

    Sub cari()
        Using CD = New MySqlCommand("SELECT * from tabel_pelanggan where namapelanggan LIKE '%" & Textcari.Text & "%'", CONN)
            Using DR As MySqlDataReader = CD.ExecuteReader
                Using Tabel As New DataTable
                    Tabel.Load(DR)
                    If Tabel.Rows.Count = 0 Then
                        DataGridView1.DataSource = Nothing
                    Else
                        DataGridView1.DataSource = Tabel
                        Call aturdgvpelanggan()
                    End If
                End Using


            End Using


        End Using

    End Sub

    Sub updatepelanggan()
        Try
            If Txtkodeplg.Text = "" Or txtplg.Text = "" Or txtalamat.Text = "" Or txttelp.Text = "" Then
                MsgBox("Data belum lengkap!", MsgBoxStyle.Exclamation)
            Else
                Dim Query As String = "UPDATE tabel_pelanggan SET namapelanggan = '" & txtplg.Text & "', alamat = '" & txtalamat.Text & "', telp = '" & txttelp.Text & "' WHERE kodepelanggan = '" & Txtkodeplg.Text & "'"
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
    Private Sub pegawai_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call aturdgvpelanggan()
        Call tampil()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call insertpelanggan()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call muatpelanggan()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim result As DialogResult = MessageBox.Show("Apakah anda yakin ?", "Informasi", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Call deleteDatapelanggan()
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Call bersihtextbox()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Call updatepelanggan()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class