
Imports System.Windows.Forms.DataVisualization.Charting
Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call tampildatapivot()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim vTgl As String
        Dim vBln As String
        Dim vdatepick As String

        vdatepick = DateTimePicker1.Value.ToString("dd/MM/yy")
        vTgl = DateTimePicker1.Value.Day
        vBln = DateTimePicker1.Value.ToString("MM")

        Cmd = New MySqlCommand("SELECT * FROM tbl_petir WHERE tgl='" & vTgl & "' AND bln='" & vBln & "'", Conn)
        Rd = Cmd.ExecuteReader()
        If Rd.Read() = True Then
            MsgBox("Data Sudah Ada")
        Else
            Try
                Rd.Close()
                Cmd = New MySqlCommand("insert into tbl_petir(id,bln,nilai,tgl)values " & _
                          "('','" & vBln & "','" & TextBox1.Text & "','" & vTgl & "')", Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Tanggal " & vdatepick & " Berhasil Disimpan", MsgBoxStyle.Information, "Konfirmasi")
                Call tampildatapivot()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Terjadi Kesalahan...")
            End Try
        End If
        TextBox1.Text = ""
        Rd.Close()
    End Sub

    Sub tampildatapivot()
        Try
            Dim sqlpivot As String

            sqlpivot = "SELECT IFNULL(tgl, 'TOTAL') AS Tanggal, SUM( IF(bln = 1, nilai,0) ) AS Januari, SUM( IF( bln = 2, nilai, 0) ) AS Februari, SUM( IF( bln = 3, nilai, 0) ) AS Maret FROM tbl_petir GROUP BY tgl ASC WITH ROLLUP"
            adapter = New MySqlDataAdapter(sqlpivot, Conn)
            ds = New DataSet
            adapter.Fill(ds, "tbl_petir")
            tabelMhs.DataSource = ds.Tables("tbl_petir")
            tabelMhs.ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Terjadi Kesalahan...")
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim grafik As New Form2
        grafik.Show()
    End Sub
End Class
