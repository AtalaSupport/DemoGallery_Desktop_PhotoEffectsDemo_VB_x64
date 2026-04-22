Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO

Imports Atalasoft.Imaging
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging.ImageProcessing.Effects
Imports WinDemoHelperMethods.WinDemoHelperMethods

Namespace PhotoEffectsDemo
    Public Class Form1
        Inherits System.Windows.Forms.Form
        Private _fileName As String = Nothing
        Private _validLicense As Boolean

        Private mainMenu1 As System.Windows.Forms.MainMenu
        Private menuFile As System.Windows.Forms.MenuItem
        Private WithEvents menuFileOpen As System.Windows.Forms.MenuItem
        Private WithEvents menuFileSaveAs As System.Windows.Forms.MenuItem
        Private menuItem4 As System.Windows.Forms.MenuItem
        Private WithEvents menuExit As System.Windows.Forms.MenuItem
        Private WithEvents viewerOriginal As Atalasoft.Imaging.WinControls.WorkspaceViewer
        Private WithEvents viewerModified As Atalasoft.Imaging.WinControls.WorkspaceViewer
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private statusPosition As System.Windows.Forms.StatusBarPanel
        Private statusColor As System.Windows.Forms.StatusBarPanel
        Private statusInfo As System.Windows.Forms.StatusBarPanel
        Private menuItem3 As System.Windows.Forms.MenuItem
        Private menuEffects As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectsLevels As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectsCooler As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectsWarmer As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectsMagic As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectCorrection As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectMultiply As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectPortrait As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectShadowBoost As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectsSkinTones As System.Windows.Forms.MenuItem
        Private WithEvents menuGradient As System.Windows.Forms.MenuItem
        Private menuItem1 As System.Windows.Forms.MenuItem
        Private menuAbout As System.Windows.Forms.MenuItem
        Private WithEvents menuAboutDialog As System.Windows.Forms.MenuItem
        Private menuItem2 As System.Windows.Forms.MenuItem
        Private menuItem5 As System.Windows.Forms.MenuItem
        Private WithEvents menuEffectAutoLevels As System.Windows.Forms.MenuItem
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

        Shared Sub New()
            HelperMethods.PopulateDecoders(RegisteredDecoders.Decoders)
        End Sub

        Public Sub New()
            CheckLicenseFile()

            If Me._validLicense Then
                '
                ' Required for Windows Form Designer support
                '
                InitializeComponent()

                Me.menuEffects.Enabled = False
                Me.menuFileSaveAs.Enabled = False

                ' This should point to the "DotImage 4.0\Images\PhotoEffects" folder.
                Me._fileName = System.IO.Path.GetFullPath("..\..\Images\PhotoEffects\ColorMagicSample2.jpg")
                If (Not System.IO.File.Exists(Me._fileName)) Then
                    Me._fileName = System.IO.Path.GetFullPath("..\..\..\..\..\Images\PhotoEffects\ColorMagicSample2.jpg")
                    If (Not System.IO.File.Exists(Me._fileName)) Then
                        Me._fileName = ""
                    End If
                End If
            End If
        End Sub

#Region "Check for license code"

        Private Sub CheckLicenseFile()
            ' Make sure a license for DotImage and Advanced DocClean exist.
            Try
                Dim img As AtalaImage = New AtalaImage
                img.Dispose()
            Catch e1 As Atalasoft.Imaging.AtalasoftLicenseException
                ShowLicenseMessage("DotImage")
                Return
            End Try

            If AtalaImage.Edition = LicenseEdition.Photo Then
                ShowLicenseMessage("DotImage Photo Pro or DotImage Document Imaging")
                Return
            End If

            Try
                Dim cmd As PhotoColorCoolerCommand = New PhotoColorCoolerCommand
                Me._validLicense = True
            Catch e1 As Atalasoft.Imaging.AtalasoftLicenseException
                ShowLicenseMessage("Advanced Photo Effects")
            End Try
        End Sub

        Private Sub ShowLicenseMessage(ByVal product As String)
            If MessageBox.Show("This demo requires a " & product & " license." & Constants.vbCrLf & "An evaluation license can be requested with our activation utility." & Constants.vbCrLf & Constants.vbCrLf & "Would you like to run this utility now?", product & " License Required", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Dim activationUtil As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) & "\Atalasoft\DotImage "

                ' Use reflection to find out which version of DotImage we are using.
                Dim assemblies As System.Reflection.AssemblyName() = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                For Each name As System.Reflection.AssemblyName In assemblies
                    If name.Name = "Atalasoft.dotImage" Then
                        activationUtil &= name.Version.ToString(2)
                        Exit For


                    End If
                Next name

                activationUtil &= "\AtalasoftToolkitActivation.exe"


                If System.IO.File.Exists(activationUtil) Then
                    System.Diagnostics.Process.Start(activationUtil)
                Else
                    MessageBox.Show("We were unable to location the activation utility on this system." & Constants.vbCrLf & "Please run it from the start menu.", "AtalasoftToolkitActivation.exe Not Found")
                End If
            End If
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            If (Not Me._validLicense) Then
                Application.Exit()
            End If
        End Sub

#End Region

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
            Me.mainMenu1 = New System.Windows.Forms.MainMenu()
            Me.menuFile = New System.Windows.Forms.MenuItem()
            Me.menuFileOpen = New System.Windows.Forms.MenuItem()
            Me.menuFileSaveAs = New System.Windows.Forms.MenuItem()
            Me.menuItem4 = New System.Windows.Forms.MenuItem()
            Me.menuExit = New System.Windows.Forms.MenuItem()
            Me.menuEffects = New System.Windows.Forms.MenuItem()
            Me.menuEffectAutoLevels = New System.Windows.Forms.MenuItem()
            Me.menuEffectsLevels = New System.Windows.Forms.MenuItem()
            Me.menuItem3 = New System.Windows.Forms.MenuItem()
            Me.menuEffectsCooler = New System.Windows.Forms.MenuItem()
            Me.menuEffectsWarmer = New System.Windows.Forms.MenuItem()
            Me.menuItem1 = New System.Windows.Forms.MenuItem()
            Me.menuEffectsMagic = New System.Windows.Forms.MenuItem()
            Me.menuEffectCorrection = New System.Windows.Forms.MenuItem()
            Me.menuEffectMultiply = New System.Windows.Forms.MenuItem()
            Me.menuItem2 = New System.Windows.Forms.MenuItem()
            Me.menuEffectPortrait = New System.Windows.Forms.MenuItem()
            Me.menuEffectShadowBoost = New System.Windows.Forms.MenuItem()
            Me.menuEffectsSkinTones = New System.Windows.Forms.MenuItem()
            Me.menuItem5 = New System.Windows.Forms.MenuItem()
            Me.menuGradient = New System.Windows.Forms.MenuItem()
            Me.menuAbout = New System.Windows.Forms.MenuItem()
            Me.menuAboutDialog = New System.Windows.Forms.MenuItem()
            Me.viewerOriginal = New Atalasoft.Imaging.WinControls.WorkspaceViewer()
            Me.viewerModified = New Atalasoft.Imaging.WinControls.WorkspaceViewer()
            Me.statusBar1 = New System.Windows.Forms.StatusBar()
            Me.statusPosition = New System.Windows.Forms.StatusBarPanel()
            Me.statusColor = New System.Windows.Forms.StatusBarPanel()
            Me.statusInfo = New System.Windows.Forms.StatusBarPanel()
            CType(Me.statusPosition, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusColor, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusInfo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' mainMenu1
            ' 
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuEffects, Me.menuAbout})
            ' 
            ' menuFile
            ' 
            Me.menuFile.Index = 0
            Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFileOpen, Me.menuFileSaveAs, Me.menuItem4, Me.menuExit})
            Me.menuFile.Text = "&File"
            ' 
            ' menuFileOpen
            ' 
            Me.menuFileOpen.Index = 0
            Me.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
            Me.menuFileOpen.Text = "&Open"
            '			Me.menuFileOpen.Click += New System.EventHandler(Me.menuFileOpen_Click);
            ' 
            ' menuFileSaveAs
            ' 
            Me.menuFileSaveAs.Index = 1
            Me.menuFileSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlS
            Me.menuFileSaveAs.Text = "&Save As..."
            '			Me.menuFileSaveAs.Click += New System.EventHandler(Me.menuFileSaveAs_Click);
            ' 
            ' menuItem4
            ' 
            Me.menuItem4.Index = 2
            Me.menuItem4.Text = "-"
            ' 
            ' menuExit
            ' 
            Me.menuExit.Index = 3
            Me.menuExit.Text = "E&xit"
            '			Me.menuExit.Click += New System.EventHandler(Me.menuExit_Click);
            ' 
            ' menuEffects
            ' 
            Me.menuEffects.Index = 1
            Me.menuEffects.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuEffectAutoLevels, Me.menuEffectsLevels, Me.menuItem3, Me.menuEffectsCooler, Me.menuEffectsWarmer, Me.menuItem1, Me.menuEffectsMagic, Me.menuEffectCorrection, Me.menuEffectMultiply, Me.menuItem2, Me.menuEffectPortrait, Me.menuEffectShadowBoost, Me.menuEffectsSkinTones, Me.menuItem5, Me.menuGradient})
            Me.menuEffects.Text = "&Effects"
            ' 
            ' menuEffectAutoLevels
            ' 
            Me.menuEffectAutoLevels.Index = 0
            Me.menuEffectAutoLevels.Text = "Auto Levels"
            '			Me.menuEffectAutoLevels.Click += New System.EventHandler(Me.menuEffectAutoLevels_Click);
            ' 
            ' menuEffectsLevels
            ' 
            Me.menuEffectsLevels.Index = 1
            Me.menuEffectsLevels.Text = "&Levels"
            '			Me.menuEffectsLevels.Click += New System.EventHandler(Me.menuEffectsLevels_Click);
            ' 
            ' menuItem3
            ' 
            Me.menuItem3.Index = 2
            Me.menuItem3.Text = "-"
            ' 
            ' menuEffectsCooler
            ' 
            Me.menuEffectsCooler.Index = 3
            Me.menuEffectsCooler.Text = "&Cooler"
            '			Me.menuEffectsCooler.Click += New System.EventHandler(Me.menuEffectsCooler_Click);
            ' 
            ' menuEffectsWarmer
            ' 
            Me.menuEffectsWarmer.Index = 4
            Me.menuEffectsWarmer.Text = "&Warmer"
            '			Me.menuEffectsWarmer.Click += New System.EventHandler(Me.menuEffectsWarmer_Click);
            ' 
            ' menuItem1
            ' 
            Me.menuItem1.Index = 5
            Me.menuItem1.Text = "-"
            ' 
            ' menuEffectsMagic
            ' 
            Me.menuEffectsMagic.Index = 6
            Me.menuEffectsMagic.Text = "Color Magic"
            '			Me.menuEffectsMagic.Click += New System.EventHandler(Me.menuEffectsMagic_Click);
            ' 
            ' menuEffectCorrection
            ' 
            Me.menuEffectCorrection.Index = 7
            Me.menuEffectCorrection.Text = "Correction"
            '			Me.menuEffectCorrection.Click += New System.EventHandler(Me.menuEffectCorrection_Click);
            ' 
            ' menuEffectMultiply
            ' 
            Me.menuEffectMultiply.Index = 8
            Me.menuEffectMultiply.Text = "Multiply"
            '			Me.menuEffectMultiply.Click += New System.EventHandler(Me.menuEffectMultiply_Click);
            ' 
            ' menuItem2
            ' 
            Me.menuItem2.Index = 9
            Me.menuItem2.Text = "-"
            ' 
            ' menuEffectPortrait
            ' 
            Me.menuEffectPortrait.Index = 10
            Me.menuEffectPortrait.Text = "Portrait"
            '			Me.menuEffectPortrait.Click += New System.EventHandler(Me.menuEffectPortrait_Click);
            ' 
            ' menuEffectShadowBoost
            ' 
            Me.menuEffectShadowBoost.Index = 11
            Me.menuEffectShadowBoost.Text = "Shadow Boost"
            '			Me.menuEffectShadowBoost.Click += New System.EventHandler(Me.menuEffectShadowBoost_Click);
            ' 
            ' menuEffectsSkinTones
            ' 
            Me.menuEffectsSkinTones.Index = 12
            Me.menuEffectsSkinTones.Text = "Skin Tones"
            '			Me.menuEffectsSkinTones.Click += New System.EventHandler(Me.menuEffectsSkinTones_Click);
            ' 
            ' menuItem5
            ' 
            Me.menuItem5.Index = 13
            Me.menuItem5.Text = "-"
            ' 
            ' menuGradient
            ' 
            Me.menuGradient.Index = 14
            Me.menuGradient.Text = "Gradient"
            '			Me.menuGradient.Click += New System.EventHandler(Me.menuGradient_Click);
            ' 
            ' menuAbout
            ' 
            Me.menuAbout.Index = 2
            Me.menuAbout.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAboutDialog})
            Me.menuAbout.Text = "&About"
            ' 
            ' menuAboutDialog
            ' 
            Me.menuAboutDialog.Index = 0
            Me.menuAboutDialog.Text = "Advanced Photo Effects Demo..."
            '			Me.menuAboutDialog.Click += New System.EventHandler(Me.menuAboutDialog_Click);
            ' 
            ' viewerOriginal
            ' 
            Me.viewerOriginal.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFitShrinkOnly
            Me.viewerOriginal.BackColor = System.Drawing.Color.White
            Me.viewerOriginal.Centered = True
            Me.viewerOriginal.DisplayProfile = Nothing
            Me.viewerOriginal.Location = New System.Drawing.Point(8, 8)
            Me.viewerOriginal.Magnifier.BackColor = System.Drawing.Color.White
            Me.viewerOriginal.Magnifier.BorderColor = System.Drawing.Color.Black
            Me.viewerOriginal.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me.viewerOriginal.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Magnifier
            Me.viewerOriginal.Name = "viewerOriginal"
            Me.viewerOriginal.OutputProfile = Nothing
            Me.viewerOriginal.ScrollPosition = New System.Drawing.Point(144, 212)
            Me.viewerOriginal.Selection = Nothing
            Me.viewerOriginal.Size = New System.Drawing.Size(288, 424)
            Me.viewerOriginal.TabIndex = 0
            Me.viewerOriginal.Text = "workspaceViewer1"
            '			Me.viewerOriginal.MouseMovePixel += New System.Windows.Forms.MouseEventHandler(Me.viewerOriginal_MouseMovePixel);
            '			Me.viewerOriginal.MouseLeave += New System.EventHandler(Me.viewer_MouseLeave);
            ' 
            ' viewerModified
            ' 
            Me.viewerModified.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFitShrinkOnly
            Me.viewerModified.BackColor = System.Drawing.Color.White
            Me.viewerModified.Centered = True
            Me.viewerModified.DisplayProfile = Nothing
            Me.viewerModified.Location = New System.Drawing.Point(312, 8)
            Me.viewerModified.Magnifier.BackColor = System.Drawing.Color.White
            Me.viewerModified.Magnifier.BorderColor = System.Drawing.Color.Black
            Me.viewerModified.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me.viewerModified.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Magnifier
            Me.viewerModified.Name = "viewerModified"
            Me.viewerModified.OutputProfile = Nothing
            Me.viewerModified.ScrollPosition = New System.Drawing.Point(144, 212)
            Me.viewerModified.Selection = Nothing
            Me.viewerModified.Size = New System.Drawing.Size(288, 424)
            Me.viewerModified.TabIndex = 1
            Me.viewerModified.Text = "workspaceViewer2"
            '			Me.viewerModified.MouseMovePixel += New System.Windows.Forms.MouseEventHandler(Me.viewerModified_MouseMovePixel);
            '			Me.viewerModified.MouseLeave += New System.EventHandler(Me.viewer_MouseLeave);
            ' 
            ' statusBar1
            ' 
            Me.statusBar1.Location = New System.Drawing.Point(0, 435)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusPosition, Me.statusColor, Me.statusInfo})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(608, 22)
            Me.statusBar1.TabIndex = 2
            ' 
            ' statusPosition
            ' 
            Me.statusPosition.Alignment = System.Windows.Forms.HorizontalAlignment.Center
            Me.statusPosition.Text = "-"
            ' 
            ' statusColor
            ' 
            Me.statusColor.Alignment = System.Windows.Forms.HorizontalAlignment.Center
            Me.statusColor.Text = "-"
            Me.statusColor.Width = 200
            ' 
            ' statusInfo
            ' 
            Me.statusInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusInfo.Width = 292
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(608, 457)
            Me.Controls.Add(Me.viewerModified)
            Me.Controls.Add(Me.viewerOriginal)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
            Me.Menu = Me.mainMenu1
            Me.MinimumSize = New System.Drawing.Size(464, 376)
            Me.Name = "Form1"
            Me.Text = "Advanced Photo Effects Demo"
            '			Me.Layout += New System.Windows.Forms.LayoutEventHandler(Me.Form1_Layout);
            CType(Me.statusPosition, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusColor, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusInfo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.Run(New Form1())
        End Sub

#Region "File Menu"

        Private Sub menuFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFileOpen.Click
            Dim dlg As OpenFileDialog = New OpenFileDialog()
            dlg.Filter = HelperMethods.CreateDialogFilter(True)
            dlg.FileName = Me._fileName

            ' try to locate images folder
            Dim imagesFolder As String = Application.ExecutablePath
            ' we assume we are running under the DotImage install folder
            Dim pos As Integer = imagesFolder.IndexOf("DotImage ")
            If pos <> -1 Then
                imagesFolder = imagesFolder.Substring(0, imagesFolder.IndexOf("\", pos)) & "\Images\PhotoEffects"
            End If

            'use this folder as starting point			
            dlg.InitialDirectory = imagesFolder

            Try
                If dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    Try
                        Me._fileName = dlg.FileName
                        Me.viewerModified.Images.Clear()
                        Me.viewerOriginal.Images.Clear()
                        Me.statusColor.Text = "-"
                        Me.statusInfo.Text = ""
                        Me.statusPosition.Text = "-"

                        If System.IO.File.Exists(Me._fileName) Then
                            Me.viewerOriginal.Open(Me._fileName, 0)
                        End If

                        If Not Me.viewerOriginal.Image Is Nothing Then
                            Me.menuEffects.Enabled = True
                        End If
                    Catch ex As System.Exception
                        MessageBox.Show(Me, ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Finally
                dlg.Dispose()
            End Try
        End Sub

        Private Sub menuFileSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFileSaveAs.Click
            Dim dlg As SaveFileDialog = New SaveFileDialog()
            dlg.Filter = "Joint Photographic Experts Group (*.jpg)|*.jpg|Tagged Image File (*.tif)|*.tif|Portable Network Graphic (*.png)|*.png|Adobe (tm) Photoshop format (*.psd)|*.psd"

            Try
                If dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    Dim encoder As ImageEncoder = Nothing

                    Select Case dlg.FilterIndex
                        Case 1
                            encoder = New JpegEncoder(85, 0, False)
                        Case 2
                            encoder = New TiffEncoder(TiffCompression.Default, False)
                        Case 3
                            encoder = New PngEncoder()
                        Case 4
                            encoder = New PsdEncoder()
                    End Select

                    Try
                        Me.viewerModified.Save(dlg.FileName, encoder, 0)
                    Catch ex As System.Exception
                        MessageBox.Show(Me, ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Finally
                dlg.Dispose()
            End Try
        End Sub

        Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
            Me.Close()
        End Sub

#End Region

#Region "Events"

        Private Sub Form1_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles MyBase.Layout
            ' calculate the viewer size.
            Dim width As Integer = (Me.ClientSize.Width - 24) / 2
            Dim height As Integer = Me.ClientSize.Height - 16 - Me.statusBar1.Height

            Me.viewerOriginal.Size = New Size(width, height)
            Me.viewerModified.Size = New Size(width, height)
            Me.viewerModified.Location = New Point(width + 16, 8)
        End Sub

        Private Sub viewerOriginal_MouseMovePixel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles viewerOriginal.MouseMovePixel
            SetStatus(e, Me.viewerOriginal.Image)
        End Sub

        Private Sub viewerModified_MouseMovePixel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles viewerModified.MouseMovePixel
            SetStatus(e, Me.viewerModified.Image)
        End Sub

        Private Sub SetStatus(ByVal e As System.Windows.Forms.MouseEventArgs, ByVal image As AtalaImage)
            Me.statusPosition.Text = "X: " & e.X.ToString() & ", Y: " & e.Y.ToString()

            If image.PixelFormat = PixelFormat.Pixel32bppCmyk Then
                Dim cmy As CmykColor = image.GetPixelCmykColor(e.X, e.Y)
                Me.statusColor.Text = "C: " & cmy.C.ToString() & ", M: " & cmy.M.ToString() & ", Y: " & cmy.Y.ToString() & ", K: " & cmy.K.ToString()
            Else
                Dim clr As Color = image.GetPixelColor(e.X, e.Y)
                Me.statusColor.Text = "A: " & clr.A.ToString() & ",R: " & clr.R.ToString() & ", G: " & clr.G.ToString() & ", B: " & clr.B.ToString()
            End If
        End Sub

        Private Sub viewer_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewerOriginal.MouseLeave, viewerModified.MouseLeave
            Me.statusColor.Text = "-"
            Me.statusPosition.Text = "-"
        End Sub

#End Region

#Region "Effects Menu"

        Private Sub menuEffectAutoLevels_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectAutoLevels.Click
            ApplyCommandDialog(New AutoLevelsCommand())
        End Sub

        Private Sub menuEffectsLevels_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectsLevels.Click
            ApplyCommandDialog(New AdvancedLevelsCommand())
        End Sub

        Private Sub menuEffectsCooler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectsCooler.Click
            ApplyCommandDialog(New PhotoColorCoolerCommand())
        End Sub

        Private Sub menuEffectsWarmer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectsWarmer.Click
            ApplyCommandDialog(New PhotoColorWarmerCommand())
        End Sub

        Private Sub menuEffectsMagic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectsMagic.Click
            ApplyCommandDialog(New PhotoColorMagicCommand())
        End Sub

        Private Sub menuEffectCorrection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectCorrection.Click
            ApplyCommandDialog(New PhotoColorCorrectCommand())
        End Sub

        Private Sub menuEffectMultiply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectMultiply.Click
            ApplyCommandDialog(New PhotoColorMultiplyCommand())
        End Sub

        Private Sub menuEffectPortrait_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectPortrait.Click
            ' This command does not have extra properties, so just apply it.
            ApplyCommand(New PhotoPortraitCommand())
        End Sub

        Private Sub menuEffectShadowBoost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectShadowBoost.Click
            ' This command does not have extra properties, so just apply it.
            ApplyCommand(New PhotoShadowBoostCommand())
        End Sub

        Private Sub menuEffectsSkinTones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuEffectsSkinTones.Click
            ' This command does not have extra properties, so just apply it.
            ApplyCommand(New PhotoSkinTonesCommand())
        End Sub

        Private Sub menuGradient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuGradient.Click
            ApplyCommandDialog(New NDGradFilterCommand())
        End Sub

        Private Sub ApplyCommandDialog(ByVal command As Atalasoft.Imaging.ImageProcessing.ImageCommand)
            Dim dlg As PropertiesDialog = New PropertiesDialog(command)
            If dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                ApplyCommand(command)
            End If
            dlg.Dispose()
        End Sub

        Private Sub ApplyCommand(ByVal command As Atalasoft.Imaging.ImageProcessing.ImageCommand)
            Me.Cursor = Cursors.WaitCursor
            Me.viewerModified.Images.Clear()
            Me.viewerModified.Images.Add(CType(Me.viewerOriginal.Image.Clone(), AtalaImage))
            Me.viewerModified.ApplyCommand(command)
            Me.Cursor = Cursors.Default

            Me.menuFileSaveAs.Enabled = (Not Me.viewerModified.Image Is Nothing)
        End Sub

#End Region

        Private Sub menuAboutDialog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAboutDialog.Click
            Dim frm As AtalaDemos.AboutBox.About = New AtalaDemos.AboutBox.About("About...", "Advanced Photo Effects Demo")
            frm.Description = "This demo uses Atalasoft DotImage and the Advanced Photo Effects add-on to apply color enhancement and correction effects on color images."
            frm.ShowDialog(Me)
            frm.Dispose()
        End Sub

    End Class
End Namespace
