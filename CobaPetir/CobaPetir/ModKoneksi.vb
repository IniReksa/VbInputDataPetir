Imports System.Windows.Forms.DataVisualization.Charting
Imports MySql.Data.MySqlClient

Module ModKoneksi
    Public Cmd As MySqlCommand
    Public Rd As MySqlDataReader
    Public Conn As MySqlConnection
    Public dt As DataTable
    Public ds As DataSet
    Public adapter As MySqlDataAdapter
    Public koneksigrafik As String

    Public Sub Koneksi()
        Conn = New MySqlConnection("server=localhost;user=root;database=db_bmkg")
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
                'MsgBox("Terkoneksi", MsgBoxStyle.Information, "konfirmasi")
            End If
        Catch Pesan As MySql.Data.MySqlClient.MySqlException
            MsgBox("Mencoba menghubungi server... " + vbCrLf + _
            "Pastikan Database Server Telah Aktif", MsgBoxStyle.Exclamation, "Perhatian")
        End Try
    End Sub

    Public Sub grafik_petir(ByVal chart1 As Chart)
        Dim sqlgrafik As String

        Try
            sqlgrafik = "SELECT * FROM tbl_petir"
            adapter = New MySqlDataAdapter(sqlgrafik, Conn)
            ds = New DataSet
            adapter.Fill(ds, "tbl_petir")
            chart1.Series("Ket").XValueMember = "Nama"
            chart1.Series("Ket").YValueMembers = "JUMLAH"
            chart1.DataSource = ds.Tables("tbl_petir")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module
