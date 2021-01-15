Imports MySql.Data.MySqlClient
Public Class fLogin
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

    Private Sub bMasuk_Click(sender As System.Object, e As System.EventArgs) Handles bMasuk.Click
        CD = New MySqlCommand("select * from tabel_login where username='" & txtusername.Text & "' and password='" & txtpassword.Text & "'", CONN)
        DR = CD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            fMenu.Show()
            Me.Hide()
        Else
            DR.Close()
            MsgBox("Username/Password Salah!", MsgBoxStyle.Critical)
            txtusername.Text = ""
            txtpassword.Text = ""

        End If
        CONN.Close()




    End Sub

    Private Sub fLogin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
