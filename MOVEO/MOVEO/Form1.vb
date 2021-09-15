Imports System.IO.Ports
Public Class Form1
    Public ejex, ejey, ejez, ejee, ejeb, ejes As Long
    Dim copiado As String
    Dim indata As String
    Dim seleccionado As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ports As String() = SerialPort.GetPortNames()
        Dim port As String
        ejex = 0
        ejey = 0
        ejez = 0
        ejee = 0
        ejes = 40
        ejeb = 0

        For Each port In ports
            cmb_ports.Items.Add(port)
        Next port
    End Sub
    Private Sub bloqueoinicial()
        WriteGCode("T0")
        Espera(100)
        WriteGCode("G1 X" & 1 & " Y" & 1 & " Z" & 1 & " E" & 1)
        Espera(200)
        WriteGCode("G1 X" & 0 & " Y" & 0 & " Z" & 0 & " E" & 0)
        Espera(200)
        'WriteGCode("T1")
        'WriteGCode("G1 E" & ejee)
        'WriteGCode("M280 P1 S" & ejes)
        'WriteGCode("T0")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ejex = ejex + NumericUpDown1.Value
        WriteGCode("G1 X" & ejex)
        actualizaposiciones()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ejey = ejey + NumericUpDown1.Value
        WriteGCode("G1 Y" & ejey)
        actualizaposiciones()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ejey = ejey - NumericUpDown1.Value
        WriteGCode("G1 Y" & ejey)
        actualizaposiciones()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ejez = ejez + NumericUpDown1.Value
        WriteGCode("G1 Z" & ejez)
        actualizaposiciones()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ejez = ejez - NumericUpDown1.Value
        WriteGCode("G1 Z" & ejez)
        actualizaposiciones()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ejes = ejes + NumericUpDown1.Value
        If ejes > 180 Then ejes = 180
        WriteGCode("M280 P1 S" & ejes)
        actualizaposiciones()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ejee = ejee + NumericUpDown1.Value
        WriteGCode("T0")
        System.Threading.Thread.Sleep(100)
        WriteGCode("G1 E" & ejee)
        actualizaposiciones()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ejee = ejee - NumericUpDown1.Value
        WriteGCode("T0")
        System.Threading.Thread.Sleep(100)
        WriteGCode("G1 E" & ejee)
        actualizaposiciones()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ejeb = ejeb + NumericUpDown1.Value
        WriteGCode("T1")
        System.Threading.Thread.Sleep(100)
        WriteGCode("G1 E" & ejeb)
        actualizaposiciones()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ejeb = ejeb - NumericUpDown1.Value
        WriteGCode("T1")
        System.Threading.Thread.Sleep(100)
        WriteGCode("G1 E" & ejeb)
        actualizaposiciones()
    End Sub


    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        ejes = ejes - NumericUpDown1.Value
        If ejes < 40 Then ejes = 40
        If ejes > 180 Then ejes = 180
        WriteGCode("M280 P1 S" & ejes)
        actualizaposiciones()
    End Sub


    Private Sub WriteGCode(ByVal gcode As String)
        'SerialPort1.Open()
        If Not SerialPort1.IsOpen Then
            Try
                SerialPort1.Open()
                'FlushPort()
            Catch ex As Exception
                MessageBox.Show("Puerto serial no encontrado!")
                Exit Sub
            End Try
        End If

        'FlushPort()
        SerialPort1.WriteLine(gcode)
        'ReadPortOK() ' 
    End Sub

    Private Sub writeline(ByVal linea As String)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ListBox1.Items.Add("G1 " & "X" & ejex & " Y" & ejey & " Z" & ejez & " E" & ejee & " B" & ejeb & " S" & ejes)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If ListBox1.SelectedIndex > -1 Then ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If MsgBox("DELETE ALL?", 1, "CONFIRMATION") = 1 Then
            ListBox1.Items.Clear()
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ListBox1.Items.AddRange(IO.File.ReadAllLines(OpenFileDialog1.FileName))
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sb As New System.Text.StringBuilder()

            For Each o As Object In ListBox1.Items
                sb.AppendLine(o)
            Next

            System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If ListBox1.SelectedIndex > -1 Then copiado = ListBox1.Items(ListBox1.SelectedIndex)
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If copiado <> "" Then ListBox1.Items.Add(copiado)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If ListBox1.SelectedIndex > -1 Then ListBox1.Items(ListBox1.SelectedIndex) = "G1 " & "X" & ejex & " Y" & ejey & " Z" & ejez & " E" & ejee & " B" & ejeb & " S" & ejes
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_conectar.Click

        If Not SerialPort1.IsOpen Then
            SerialPort1.PortName = cmb_ports.SelectedItem
            SerialPort1.BaudRate = cmb_baud.SelectedItem
            txt_comunicacion.Text = SerialPort1.PortName
            txt_comunicacion.Text = txt_comunicacion.Text & SerialPort1.BaudRate
            Try
                SerialPort1.Open()

                btn_conectar.Text = "Desconectar"
                bloqueoinicial()


            Catch ex As Exception
                MessageBox.Show("Puerto serial no encontrado!")
                btn_conectar.Text = "Conectar"
                Exit Sub
            End Try
        Else
            Try
                SerialPort1.Close()
                btn_conectar.Text = "Conectar"

            Catch ex As Exception
                MessageBox.Show("Imposible cerrar la conexión!")
                btn_conectar.Text = "Desconectar"
                Exit Sub
            End Try

        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Dim instruccion As String
        instruccion = ListBox1.SelectedItem
        moveracoordenada(instruccion)

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        executeall()

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If MsgBox("¿You want reset axis values?", vbYesNo, "Confirm") = vbYes Then
            ejex = 1
            ejey = 1
            ejez = 1
            ejee = 1
            ejes = 1
            ejeb = 1
            bloqueoinicial()
            actualizaposiciones()
            ejex = 0
            ejey = 0
            ejez = 0
            ejee = 0
            ejes = 0
            ejeb = 0
            actualizaposiciones()
            bloqueoinicial()

        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        WriteGCode(txt_comand.Text)
        txt_comunicacion.Text = txt_comunicacion.Text & vbCrLf & "MI PC - " & txt_comand.Text
        actualizaposiciones()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ListBox1.SelectedIndex = seleccionado
        moveracoordenada(ListBox1.SelectedItem)
        If seleccionado < ListBox1.Items.Count Then seleccionado = seleccionado + 1
        Timer1.Enabled = False
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        indata = SerialPort1.ReadExisting()
        txt_out(indata)
    End Sub
    Private Sub txt_out(ByVal s As String)

        'txt_comunicacion.Text = s

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ejex = ejex - NumericUpDown1.Value
        WriteGCode("G1 X" & ejex)
        actualizaposiciones()
    End Sub
    Public Sub Espera(Segundos As Double)
        Dim tfinal As Date
        Dim tcomienzo As Date

        tfinal = Date.Now.AddMilliseconds(Segundos)

        Do While tfinal > tcomienzo
            Application.DoEvents()
            tcomienzo = Date.Now
        Loop
    End Sub

    Private Sub executeall()
        ListBox1.Enabled = False
        Button1.Enabled = False
        ListBox1.SelectedItem = 0
        Dim movanterior As String = ""
        Dim pasosmax, maxx, maxy, maxz, maxe, esperamov As Integer
        For i = 0 To ListBox1.Items.Count - 1
            ListBox1.SelectedIndex = i
            Dim coord As String = ListBox1.SelectedItem
            If i > 0 Then
                movanterior = ListBox1.Items(ListBox1.SelectedIndex - 1)
            Else
                movanterior = ListBox1.SelectedItem
            End If
            Dim ejescoord_a() As String = Split(coord)
            Dim prox_a, proy_a, proz_a, proe_a, prob_a, pros_a As String
            Dim ejex_a, ejey_a, ejez_a, ejee_a, ejeb_a, ejes_a As Integer
            prox_a = ejescoord_a(1)
            proy_a = ejescoord_a(2)
            proz_a = ejescoord_a(3)
            proe_a = ejescoord_a(4)
            prob_a = ejescoord_a(5)
            pros_a = ejescoord_a(6)
            ejex_a = Int(prox_a.Substring(1, prox_a.Length - 1))
            ejey_a = Int(proy_a.Substring(1, proy_a.Length - 1))
            ejez_a = Int(proz_a.Substring(1, proz_a.Length - 1))
            ejee_a = Int(proe_a.Substring(1, proe_a.Length - 1))
            ejeb_a = Int(prob_a.Substring(1, prob_a.Length - 1))
            ejes_a = Int(pros_a.Substring(1, pros_a.Length - 1))

            If ejex > ejex_a Then maxx = Math.Abs(ejex - ejex_a) Else If ejex_a > ejex Then maxx = Math.Abs(ejex_a - ejex) Else If ejex_a = ejex Then maxx = 0
            If ejey > ejey_a Then maxy = Math.Abs(ejey - ejey_a) Else If ejey_a > ejey Then maxy = Math.Abs(ejey_a - ejey) Else If ejey_a = ejey Then maxy = 0
            If ejez > ejez_a Then maxz = Math.Abs(ejez - ejez_a) Else If ejez_a > ejez Then maxz = Math.Abs(ejez_a - ejex) Else If ejez_a = ejez Then maxz = 0
            If ejee > ejee_a Then maxe = Math.Abs(ejee - ejee_a) Else If ejee_a > ejee Then maxe = Math.Abs(ejee_a - ejee) Else If ejee_a = ejee Then maxe = 0
            pasosmax = Math.Max(maxx, maxy)
            pasosmax = Math.Max(pasosmax, maxz)
            pasosmax = Math.Max(pasosmax, maxe)
            esperamov = pasosmax * 50

            Dim ejescoord() As String = Split(coord)
            Dim prox, proy, proz, proe, prob, pros As String
            prox = ejescoord(1)
            proy = ejescoord(2)
            proz = ejescoord(3)
            proe = ejescoord(4)
            prob = ejescoord(5)
            pros = ejescoord(6)
            ejex = Int(prox.Substring(1, prox.Length - 1))
            ejey = Int(proy.Substring(1, proy.Length - 1))
            ejez = Int(proz.Substring(1, proz.Length - 1))
            ejee = Int(proe.Substring(1, proe.Length - 1))
            ejeb = Int(prob.Substring(1, prob.Length - 1))
            ejes = Int(pros.Substring(1, pros.Length - 1))
            actualizaposiciones()

            WriteGCode("T0")
            'System.Threading.Thread.Sleep(100)
            Espera(10)
            WriteGCode("G1 X" & ejex & " Y" & ejey & " Z" & ejez & " E" & ejee)
            'WriteGCode("G1 X" & ejex & " Y" & ejey & " Z" & ejez)
            'System.Threading.Thread.Sleep(100)
            'WriteGCode("G1 E" & ejee)
            'System.Threading.Thread.Sleep(50)
            'WriteGCode("T1")
            'System.Threading.Thread.Sleep(100)
            'WriteGCode("G1 E" & ejeb)
            'System.Threading.Thread.Sleep(9000)
            'txt_comunicacion.Text = txt_comunicacion.Text & Str(esperamov)
            If movanterior <> coord Then Espera(esperamov + 1500)
            WriteGCode("M280 P1 S" & ejes)
            'System.Threading.Thread.Sleep(50)
            Espera(300)
            'WriteGCode("T0")
        Next
        ListBox1.Enabled = True
        Button1.Enabled = True
    End Sub

    Private Sub moveracoordenada(coord As String)
        Dim ejescoord() As String = Split(coord)
        Dim prox, proy, proz, proe, prob, pros As String
        prox = ejescoord(1)
        proy = ejescoord(2)
        proz = ejescoord(3)
        proe = ejescoord(4)
        prob = ejescoord(5)
        pros = ejescoord(6)
        ejex = Int(prox.Substring(1, prox.Length - 1))
        ejey = Int(proy.Substring(1, proy.Length - 1))
        ejez = Int(proz.Substring(1, proz.Length - 1))
        ejee = Int(proe.Substring(1, proe.Length - 1))
        ejeb = Int(prob.Substring(1, prob.Length - 1))
        ejes = Int(pros.Substring(1, pros.Length - 1))
        actualizaposiciones()
        WriteGCode("T0")
        'System.Threading.Thread.Sleep(100)
        Espera(100)
        WriteGCode("G1 X" & ejex & " Y" & ejey & " Z" & ejez & " E" & ejee)
        'WriteGCode("G1 X" & ejex & " Y" & ejey & " Z" & ejez)
        'System.Threading.Thread.Sleep(100)
        'WriteGCode("G1 E" & ejee)
        'System.Threading.Thread.Sleep(50)
        'WriteGCode("T1")
        'System.Threading.Thread.Sleep(100)
        'WriteGCode("G1 E" & ejeb)
        'System.Threading.Thread.Sleep(9000)
        Espera(4000)
        WriteGCode("M280 P1 S" & ejes)
        'System.Threading.Thread.Sleep(50)
        Espera(50)
        WriteGCode("T0")
    End Sub

    Sub actualizaposiciones()
        lbl_posx.Text = ejex
        lbl_posy.Text = ejey
        lbl_posz.Text = ejez
        lbl_posa.Text = ejee
        lbl_posb.Text = ejeb
        lbl_poss.Text = ejes

    End Sub
End Class
