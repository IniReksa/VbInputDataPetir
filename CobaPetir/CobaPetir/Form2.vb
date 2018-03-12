Imports MySql.Data.MySqlClient

Public Class Form2
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Dim dbDataset As New DataTable

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call tampilgrafik()
    End Sub

    Sub tampilgrafik()
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=localhost;userid=root;database=db_bmkg"
        Dim READER As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim sqlgrafik As String
            sqlgrafik = "SELECT bln, SUM(nilai) as total from tbl_petir GROUP BY bln"
            COMMAND = New MySqlCommand(sqlgrafik, MysqlConn)
            READER = COMMAND.ExecuteReader
            While READER.Read
                Chart1.Series("Data Petir").Points.AddXY(READER("bln"), READER("total"))
            End While
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        tampilgrafik()
    End Sub
End Class