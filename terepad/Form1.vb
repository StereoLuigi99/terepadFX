Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Public Class Form1
    Dim selectedFilePath As String
    Dim suanpath As String = ""
    Private Sub IhaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles dosyasekme.Click

    End Sub

    Dim zatenverdik As Boolean = False

    Private Sub kaydetDosya_Click(sender As Object, e As EventArgs) Handles kaydetDosya.Click
        If otokayit = False Then

            If zatenverdik = False Then



                Dim saveFileDialog1 As New SaveFileDialog()

                ' Filtreyi ayarla (Sadece .txt dosyalarını kaydetmek için)
                saveFileDialog1.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*"
                saveFileDialog1.Title = "Metni Kaydet"

                ' Kullanıcı 'Kaydet' butonuna tıklarsa
                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    ' Dosya yolunu al
                    Dim filePath As String = saveFileDialog1.FileName

                    ' Dosyaya metni yaz
                    Using writer As New StreamWriter(filePath)
                        writer.Write(metinbox.Text)
                    End Using

                    zatenverdik = True
                    suanpath = filePath


                    Dim fileName As String = Path.GetFileName(filePath)

                    ' Kaydedildiği mesajını göster
                    Me.Text = $"terepadFX ({fileName})"
                    durumL.Text = "Kaydedildi."
                End If

                If zatenverdik = True Then
                    Using writer As New StreamWriter(suanpath)
                        writer.Write(metinbox.Text)
                    End Using
                End If

            End If

            If otokayit = True Then
                MessageBox.Show("Otokayıt açıkken bunu yapamazsınız.", "terepadFX", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        metinbox.AutoSize = True
    End Sub

    Private Sub YeniToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YeniToolStripMenuItem.Click
        If otokayit = False Then
            metinbox.Clear()
            Me.Text = "terepadFX - Yeni Yazı Dosyası"
            durumL.Text = "Yeni dosya"
            zatenverdik = False
            suanpath = ""
        End If

        If otokayit = True Then
            MessageBox.Show("Otokayıt açıkken bunu yapamazsınız.", "terepadFX", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    Private Sub YazıTipiVeBoyotunuDeğiştirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YazıTipiVeBoyotunuDeğiştirToolStripMenuItem.Click
        ' FontDialog oluştur
        Dim fontDialog As New FontDialog()

        ' FontDialog'u göster ve kullanıcı bir font seçtiyse devam et
        If fontDialog.ShowDialog() = DialogResult.OK Then
            ' TextBox'un fontunu değiştir
            metinbox.Font = fontDialog.Font
            durumL.Text = "Font değiştirildi."
        End If
    End Sub

    Private Sub TümünüPanoyaKopyalaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TümünüPanoyaKopyalaToolStripMenuItem.Click
        Dim metinkomple As String = metinbox.Text()
        Clipboard.SetText(metinkomple)
        durumL.Text = "Bütün metin panoya kopyalandı."
    End Sub

    Private Sub KaydetToolStripMenuItem_Click(sender As Object, e As EventArgs)
        kaydetcik()
    End Sub

    Private Sub TerepadBetaV03ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerepadBetaV03ToolStripMenuItem.Click

    End Sub


    'Başlık çubuğunu karanlık yapan eleman

    Private Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
    End Function

    ' Bu fonksiyon sadece pencere çubuğunu değil her yeri karanlık yapmayla görevli.
    Private Sub EnableDarkMode(hwnd As IntPtr, enable As Boolean)
        Dim useDarkMode As Integer = If(enable, 1, 0)
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, useDarkMode, Marshal.SizeOf(useDarkMode))

        Me.BackColor = Color.FromArgb(40, 36, 36)

        Label1.ForeColor = Color.White
        Label1.BackColor = Color.FromArgb(40, 36, 36)

        durumL.ForeColor = Color.White
        durumL.BackColor = Color.FromArgb(40, 36, 36)

        metinbox.BackColor = Color.FromArgb(40, 36, 36)
        metinbox.ForeColor = Color.White

        ' MenuStrip'lerin arka plan ve yazı rengini değiştir
        MenuStrip1.BackColor = Color.FromArgb(40, 36, 36)
        MenuStrip1.ForeColor = Color.White

        ' ToolStripMenuItem arka plan ve yazı rengini ayarlamak
        For Each item As ToolStripMenuItem In MenuStrip1.Items
            item.BackColor = Color.FromArgb(40, 36, 36)
            item.ForeColor = Color.White
            ChangeSubMenuColors(item)
        Next
    End Sub

    ' Aha bu da açık mod!
    Private Sub EnableLightMode(hwnd As IntPtr, enable As Boolean)
        Dim useDarkMode As Integer = If(enable, 0, 0)
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, useDarkMode, Marshal.SizeOf(useDarkMode))

        Me.BackColor = Color.FromArgb(245, 245, 245)

        Label1.ForeColor = Color.Black
        Label1.BackColor = Color.FromArgb(245, 245, 245)

        durumL.ForeColor = Color.Black
        durumL.BackColor = Color.FromArgb(245, 245, 245)

        metinbox.BackColor = Color.FromArgb(245, 245, 245)
        metinbox.ForeColor = Color.Black

        ' MenuStrip'lerin arka plan ve yazı rengini değiştir
        MenuStrip1.BackColor = Color.FromArgb(245, 245, 245)
        MenuStrip1.ForeColor = Color.Black

        ' ToolStripMenuItem arka plan ve yazı rengini ayarlamak
        For Each item As ToolStripMenuItem In MenuStrip1.Items
            item.BackColor = Color.FromArgb(245, 245, 245)
            item.ForeColor = Color.Black
            ChangeSubMenuColors(item)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnableDarkMode(Me.Handle, True)
        ctrlTimer.Interval = 1000

        durumL.Text = ""

        Me.Text = "terepadFX 1.21"

        ' Komut satırı argümanlarını al
        Dim args() As String = Environment.GetCommandLineArgs()

        ' Eğer bir dosya argümanı varsa, bu dosyayı aç
        If args.Length > 1 Then
            Dim filePath As String = args(1)

            ' Dosya mevcut mu kontrol et
            If File.Exists(filePath) Then
                Dim fileContent As String = File.ReadAllText(filePath)
                metinbox.Text = fileContent
                Dim fileName As String = Path.GetFileName(filePath)
                Me.Text = $"terepadFX ({fileName})"
                zatenverdik = True
                suanpath = filePath

            End If
        End If
    End Sub

    ' Alt menülerin de rengini değiştirmek için rekürsif fonksiyon
    Private Sub ChangeSubMenuColors(menuItem As ToolStripMenuItem)
        For Each subItem As ToolStripItem In menuItem.DropDownItems
            ' Sadece ToolStripMenuItem öğelerini işle
            If TypeOf subItem Is ToolStripMenuItem Then
                If durumL.ForeColor = Color.Black Then
                    subItem.BackColor = Color.FromArgb(245, 245, 245)
                    subItem.ForeColor = Color.Black
                End If
                If durumL.ForeColor = Color.White Then
                    subItem.BackColor = Color.FromArgb(40, 36, 36)
                    subItem.ForeColor = Color.White
                End If
                ' Eğer daha fazla alt menü varsa, onları da değiştir
                ChangeSubMenuColors(DirectCast(subItem, ToolStripMenuItem))
            End If
        Next
    End Sub

    Private Sub kaydetcik()
        If otokayit = False Then
            If zatenverdik = False Then


                Dim saveFileDialog1 As New SaveFileDialog()

                ' Filtreyi ayarla (Sadece .txt dosyalarını kaydetmek için)
                saveFileDialog1.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*"
                saveFileDialog1.Title = "Metni Kaydet"

                ' Kullanıcı 'Kaydet' butonuna tıklarsa
                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    ' Dosya yolunu al
                    Dim filePath As String = saveFileDialog1.FileName

                    ' Dosyaya metni yaz
                    Using writer As New StreamWriter(filePath)
                        writer.Write(metinbox.Text)
                    End Using
                    zatenverdik = True
                    suanpath = filePath
                    Dim fileName As String = Path.GetFileName(filePath)

                    ' Kaydedildiği mesajını göster
                    durumL.Text = "Kaydedildi!"
                End If
            End If

            If zatenverdik = True Then
                Using writer As New StreamWriter(suanpath)
                    writer.Write(metinbox.Text)
                End Using
            End If
        End If

        If otokayit = True Then
            MessageBox.Show("Otokayıt açıkken bunu yapamazsınız.", "terepadFX", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub


    Private alreadyPrompted As Boolean = False ' Durum değişkeni

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Eğer kullanıcı daha önce bir uyarı göstermişsek çıkış yapmasına izin verelim
        If alreadyPrompted Then
            Return ' Hiçbir şey yapma, doğrudan çık
        End If

        ' Formun kapanmasını engelle
        e.Cancel = True

        ' Kullanıcıdan onay iste
        Dim result As DialogResult
        Label1.Visible = False
        durumL.Text = "terepadFX kapatılmak üzere. Dosyayı kaydetmediyseniz değişiklikler kaybolacak."
        result = MessageBox.Show("Çıkış yapıyorsunuz. Eğer dosyayı kaydetmediyseniz değişiklikler kaybolacaktır.", "terepadFX - Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question)




        ' Kullanıcının hangi butona tıkladığını kontrol edelim
        Select Case result
            Case DialogResult.Yes
                alreadyPrompted = True ' Uyarıyı gösterdiğimizi belirt
                Me.Text = "Kapatılıyor!"
                durumL.Text = "terepadFX kapatıldı."
                Me.Close() ' Formu kapat


            Case DialogResult.No
                ' Hiçbir şey yapma, form kapanmayı iptal eder (e.Cancel = True)
                Label1.Visible = True
                durumL.Text = ""

        End Select
    End Sub

    Private Sub metinbox_TextChanged(sender As Object, e As EventArgs) Handles metinbox.TextChanged
        karakterbelirt()

        If metinbox.TextLength() > 32000 Then
            durumL.Text = "Metindeki karakterler 32000 değerinden büyük. terepadFX yavaşlayabilir."
        End If

        If durumL.Text = "Metindeki karakterler 32000 değerinden büyük. terepadFX yavaşlayabilir." Then
            If metinbox.TextLength() < 32000 Then
                durumL.Text = "Destan mı yazmıştınız?"
            End If
        End If


        If otokayit = True Then
            Using writer As New StreamWriter(filePath)
                writer.Write(metinbox.Text)
            End Using

        End If
    End Sub

    Private Sub TerepadHakkındaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerepadHakkındaToolStripMenuItem.Click
        about.Show()
    End Sub

    Private Sub AnlıkDolarTLHesaplamasınıMetneGeçirToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        calcu.Show()
        calcu.Text = "Şu anda hesap makinesi yapmaya üşendiğimden boş bir forma bakıyorsun."
    End Sub


    Dim filePath As String = ""
    Dim otokayit As Boolean = False
    Dim fileName As String = ""

    Private Sub AçToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AçToolStripMenuItem.Click
        ' OpenFileDialog nesnesini oluştur
        ' OpenFileDialog oluştur
        Dim ofd As New OpenFileDialog()

        ' .txt dosyalarını filtrele
        ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        ofd.Title = "Otokayıt yapılacak dosyayı seçin."

        ' OpenFileDialog'u göster ve dosya seçimi yapıldıysa devam et
        If ofd.ShowDialog() = DialogResult.OK Then
            ' Dosya yolunu bir değişkene ata
            filePath = ofd.FileName
            otokayit = True
            ' Dosya içeriğini bir değişkene ata
            metinbox.Text = File.ReadAllText(filePath)
            Dim fileName As String = Path.GetFileName(filePath)
            Me.Text = $"terepadFX ({fileName})"


            durumL.Text = "Otokayıt açık."
            zatenverdik = False
            AçToolStripMenuItem.Enabled = False
            KapatToolStripMenuItem.Enabled = True

        End If
    End Sub

    Private Sub karakterbelirt()
        Dim harfSayisi As Integer

        ' TextBox1 içindeki harflerin sayısını değişkene atayın
        harfSayisi = metinbox.Text.Length
        Label1.Text = $"Bu dosyada {harfSayisi} karakter."
    End Sub

    Private Sub KapatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KapatToolStripMenuItem.Click
        otokayit = False
        AçToolStripMenuItem.Enabled = True
        KapatToolStripMenuItem.Enabled = False

        Me.Text = $"terepadFX"
        durumL.Text = "Otokayıt kapalı."
        zatenverdik = False
    End Sub

    Private Sub ConsolasYazıTipineGeçToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsolasYazıTipineGeçToolStripMenuItem.Click
        ' varsayılan font
        metinbox.Font = New Font("Consolas", 12, FontStyle.Regular)
        durumL.Text = "Consolas 12pt fontuna geçildi."
    End Sub

    Private Sub durumL_Click(sender As Object, e As EventArgs)
        durumL.Text = ""
    End Sub


    Private Sub ÇıkışToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ÇıkışToolStripMenuItem.Click
        alreadyPrompted = True
        Application.Exit()
    End Sub



    Private Sub AçToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AçToolStripMenuItem1.Click
        If otokayit = False Then
            ' OpenFileDialog nesnesini oluştur
            ' OpenFileDialog oluştur
            Dim ofd As New OpenFileDialog()

            ' .txt dosyalarını filtrele
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            ofd.Title = "Bir .txt dosyası seçin"

            ' OpenFileDialog'u göster ve dosya seçimi yapıldıysa devam et
            If ofd.ShowDialog() = DialogResult.OK Then
                ' Dosya yolunu bir değişkene ata
                Dim filePath As String = ofd.FileName

                ' Dosya içeriğini bir değişkene ata
                metinbox.Text = File.ReadAllText(filePath)
                suanpath = filePath
                zatenverdik = True
                Dim fileName As String = Path.GetFileName(filePath)
                Me.Text = $"terepadFX ({fileName})"
                durumL.Text = "Dosya açıldı."
            End If
        End If

        If otokayit = True Then
            MessageBox.Show("Otokayıt açıkken bunu yapamazsınız.", "terepadFX", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Dim escbasili As Boolean = False
    Private ctrlPressed As Boolean = False

    Private WithEvents ctrlTimer As New Timer()

    Public altbarCount = -1
    Public lightDarkCount = -1

    ' Klavyeden bir tuşa basıldığında çalışacak olay
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' F8 tuşuna basıldıysa yapılacak işlem

        If e.KeyCode = Keys.Escape Then
            Me.Text = "terepadFX kapatılıyor!"
            durumL.Text = "Esc tuşuna basıldı. O tuştan elinizi çekerseniz terepadFX zorla kapatılır."
            metinbox.Text = "Üzgünüm ama terepadFX kapatılıyor!"
        End If

        If e.KeyCode = Keys.ControlKey And Not ctrlPressed Then
            ctrlPressed = True
            eventCounter = 0 ' Olay sayacını sıfırla
            durumL.Text = "Kısayollar hakkında bilgi almak için basılı tutmaya devam edin!"
            ctrlTimer.Start() ' Timer'ı başlatır
        End If

        ' Eğer Ctrl basılıysa ve S tuşuna basıldıysa
        If ctrlPressed AndAlso e.KeyCode = Keys.S Then
            kaydetcik()
        End If


        ' Eğer Ctrl basılıysa ve O tuşuna basıldıysa
        If ctrlPressed AndAlso e.KeyCode = Keys.O Then
            dosyaac()
        End If

    End Sub


    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        durumL.Text = ""

        If e.KeyCode = Keys.F2 Then
            altbarCount = altbarCount + 1
            If altbarCount Mod 2 = 0 Then
                MenuStrip1.Visible = False
                durumL.Text = "AltBar kapatıldı."
            End If
            If altbarCount Mod 2 = 1 Then
                MenuStrip1.Visible = True
                durumL.Text = "AltBar açıldı."
            End If
        End If

        If e.KeyCode = Keys.F3 Then
            lightDarkCount = lightDarkCount + 1
            If lightDarkCount Mod 2 = 0 Then
                EnableLightMode(Me.Handle, True)
            End If
            If lightDarkCount Mod 2 = 1 Then
                EnableDarkMode(Me.Handle, True)
            End If
        End If

        If e.KeyCode = Keys.Escape Then
            alreadyPrompted = True
            durumL.Text = "Olamaz. terepadFX kapatıldı. Vay esek kodlara bakıyor :D"
            Application.Exit()
        End If


        If e.KeyCode = Keys.ControlKey Then
            ctrlPressed = False
            ctrlTimer.Stop() ' Timer'ı durdurur
            durumL.Text = ""
        End If
    End Sub

    Private eventCounter As Integer = 0 ' Olay sayacını tutar

    ' Timer her intervalde (aralık) bu olayı tetikler
    Private Sub ctrlTimer_Tick(sender As Object, e As EventArgs) Handles ctrlTimer.Tick
        If ctrlPressed Then
            eventCounter += 1

            ' İlk olay (1. tetikleme)
            If eventCounter = 1 Or eventCounter = 5 Or eventCounter = 9 Or eventCounter = 13 Or eventCounter = 17 Or eventCounter = 21 Then
                durumL.Text = "Bir dosya açmak için Ctrl+O tuşuna basın."
            ElseIf eventCounter = 2 Or eventCounter = 6 Or eventCounter = 10 Or eventCounter = 14 Or eventCounter = 18 Or eventCounter = 22 Then
                durumL.Text = "Dosyayı kaydetmek için Ctrl+S tuşuna basın."
            ElseIf eventCounter = 3 Or eventCounter = 7 Or eventCounter = 11 Or eventCounter = 15 Or eventCounter = 19 Or eventCounter = 23 Then
                durumL.Text = "AltBar'ı kapatıp açmak için F2 tuşuna basın."
            ElseIf eventCounter = 4 Or eventCounter = 8 Or eventCounter = 12 Or eventCounter = 16 Or eventCounter = 20 Or eventCounter = 24 Then
                durumL.Text = "Açık/karanlık mod arası geçiş için F3 tuşuna basın."
            ElseIf eventCounter > 24 Then
                durumL.Text = "E abartma yeter artık! :d"
            End If
        End If
    End Sub

    Private Sub dosyaac()
        If otokayit = False Then
            ' OpenFileDialog nesnesini oluştur
            ' OpenFileDialog oluştur
            Dim ofd As New OpenFileDialog()

            ' .txt dosyalarını filtrele
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            ofd.Title = "Bir .txt dosyası seçin"

            ' OpenFileDialog'u göster ve dosya seçimi yapıldıysa devam et
            If ofd.ShowDialog() = DialogResult.OK Then
                ' Dosya yolunu bir değişkene ata
                Dim filePath As String = ofd.FileName

                ' Dosya içeriğini bir değişkene ata
                metinbox.Text = File.ReadAllText(filePath)
                suanpath = filePath
                zatenverdik = True
                Dim fileName As String = Path.GetFileName(filePath)
                Me.Text = $"terepadFX ({fileName})"
                durumL.Text = "Dosya açıldı."
                ctrlTimer.Stop()
            End If
        End If

        If otokayit = True Then
            MessageBox.Show("Otokayıt açıkken bunu yapamazsınız.", "terepadFX", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub yapiskannotlar()
        Me.TopMost = True
        Me.Text = "Yapışkan Notum (terepadFX)"
        Me.Width = 350  ' Genişlik
        Me.Height = 350 ' Yükseklik
        MenuStrip1.Visible = False
        Label1.Visible = False
        metinbox.Height = 400
        durumL.Visible = False
    End Sub

    Private Sub YapışkanNotlarModuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YapışkanNotlarModuToolStripMenuItem.Click
        yapiskannotlar()

    End Sub
End Class