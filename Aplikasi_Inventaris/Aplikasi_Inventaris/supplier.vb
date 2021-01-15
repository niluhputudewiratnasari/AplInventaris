Imports MySql.Data.MySqlClient
Public Class supplier
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
            DA = New MySqlDataAdapter("select * from tabel_suplier", CONN)
            DS = New DataSet
            DA.Fill(DS, "supplier")
            DataGridView1.DataSource = (DS.Tables("supplier"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub aturdgvsuplier()
        Try
            DataGridView1.Columns(0).HeaderText = "KODE SUPLIER"
            DataGridView1.Columns(0).Width = 100

            DataGridView1.Columns(1).HeaderText = "NAMA SUPLIER"
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
        Txtkodesp.Text = ""
        txtsuplier.Text = ""
        txtalamat.Text = ""
        txttelp.Text = ""
    End Sub

    Sub insertsuplier()
        Try
            If Txtkodesp.Text = "" Or txtsuplier.Text = "" Or txtalamat.Text = "" Or txttelp.Text = "" Then
                MsgBox("Data Belum Lengkap", MsgBoxStyle.Exclamation)
            Else
                Dim query As String = "Insert into tabel_suplier values ('" & Txtkodesp.Text & "','" & txtsuplier.Text & "','" & txtalamat.Text & "','" & txttelp.Text & "');"
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

    Sub deleteDatasuplier()
        Dim id As String
        id = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Dim Query As String = "DELETE from tabel_suplier where kodesuplier ='" & id & "'"
        CD = New MySqlCommand(Query, CONN)
        CD.ExecuteNonQuery()
        MsgBox("Data berhasil dihapus")
        Call tampil()
    End Sub

    Sub muatsuplier()
        With DataGridView1
            Txtkodesp.Text = .Item(0, .CurrentRow.Index).Value
            txtsuplier.Text = .Item(1, .CurrentRow.Index).Value
            txtalamat.Text = .Item(2, .CurrentRow.Index).Value
            txttelp.Text = .Item(3, .CurrentRow.Index).Value

        End With
    End Sub

    Sub cari()
        Using CD = New MySqlCommand("SELECT * from tabel_suplier where namasuplier LIKE '%" & Textcari.Text & "%'", CONN)
            Using DR As MySqlDataReader = CD.ExecuteReader
                Using Tabel As New DataTable
                    Tabel.Load(DR)
                    If Tabel.Rows.Count = 0 Then
                        DataGridView1.DataSource = Nothing
                    Else
                        DataGridView1.DataSource = Tabel
                        Call aturdgvsuplier()
                    End If
                End Using


            End Using


        End Using

    End Sub

    Sub updatesuplier()
        Try
            If Txtkodesp.Text = "" Or txtsuplier.Text = "" Or txtalamat.Text = "" Or txttelp.Text = "" Then
                MsgBox("Data belum lengkap!", MsgBoxStyle.Exclamation)
            Else
                Dim Query As String = "UPDATE tabel_suplier SET namasuplier = '" & txtsuplier.Text & "', alamat = '" & txtalamat.Text & "', telp = '" & txttelp.Text & "' WHERE kodesuplier = '" & Txtkodesp.Text & "'"
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

    Private Sub supplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call aturdgvsuplier()
        Call tampil()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call insertsuplier()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Call muatsuplier()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
         Dim result As DialogResult = MessageBox.Show("Apakah anda yakin ?", "Informasi", MessageBoxButtons.YesNo)
        If (result = DialogResult.Yes) Then
            Call deleteDatasuplier()
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Call bersihtextbox()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Call updatesuplier()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class