Imports System.Runtime.InteropServices
Imports System.Security.Policy

Public Class about

    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.BackColor = Color.FromArgb(40, 36, 36)
        EnableDarkMode(Me.Handle, True)
    End Sub

    Public lightDarkCount = -1
    Private Sub about_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F3 Then
            lightDarkCount = lightDarkCount + 1
            If lightDarkCount Mod 2 = 0 Then
                EnableLightMode(Me.Handle, True)
            End If
            If lightDarkCount Mod 2 = 1 Then
                EnableDarkMode(Me.Handle, True)
            End If
        End If
    End Sub


    'Başlık çubuğunu karanlık yapan eleman

    Private Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
    End Function
    Private Sub EnableDarkMode(hwnd As IntPtr, enable As Boolean)
        Dim useDarkMode As Integer = If(enable, 1, 0)
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, useDarkMode, Marshal.SizeOf(useDarkMode))

        Me.BackColor = Color.FromArgb(40, 36, 36)

        Label1.ForeColor = Color.White
        Label1.BackColor = Color.FromArgb(40, 36, 36)

        Label2.ForeColor = Color.White
        Label2.BackColor = Color.FromArgb(40, 36, 36)

        Label3.ForeColor = Color.White
        Label3.BackColor = Color.FromArgb(40, 36, 36)

        Label4.ForeColor = Color.White
        Label4.BackColor = Color.FromArgb(40, 36, 36)

        Label5.ForeColor = Color.White
        Label5.BackColor = Color.FromArgb(40, 36, 36)

        Label6.ForeColor = Color.White
        Label6.BackColor = Color.FromArgb(40, 36, 36)

        Label7.ForeColor = Color.White
        Label7.BackColor = Color.FromArgb(40, 36, 36)

        PictureBox1.BackColor = Color.FromArgb(40, 36, 36)
    End Sub

    Private Sub EnableLightMode(hwnd As IntPtr, enable As Boolean)
        Dim useDarkMode As Integer = If(enable, 0, 0)
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, useDarkMode, Marshal.SizeOf(useDarkMode))

        Me.BackColor = Color.FromArgb(245, 245, 245)

        Label1.ForeColor = Color.Black
        Label1.BackColor = Color.FromArgb(245, 245, 245)

        Label2.ForeColor = Color.Black
        Label2.BackColor = Color.FromArgb(245, 245, 245)

        Label3.ForeColor = Color.Black
        Label3.BackColor = Color.FromArgb(245, 245, 245)

        Label4.ForeColor = Color.Black
        Label4.BackColor = Color.FromArgb(245, 245, 245)

        Label5.ForeColor = Color.Black
        Label5.BackColor = Color.FromArgb(245, 245, 245)

        Label6.ForeColor = Color.Black
        Label6.BackColor = Color.FromArgb(245, 245, 245)

        Label7.ForeColor = Color.Black
        Label7.BackColor = Color.FromArgb(245, 245, 245)

        PictureBox1.BackColor = Color.FromArgb(245, 245, 245)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Process.Start("https://github.com/tereyagburak")
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Process.Start("https://github.com/StereoLuigi99")
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("https://github.com/StereoLuigi99/terepadFX")
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Process.Start("https://github.com/StereoLuigi99/terepadFX")
    End Sub
End Class